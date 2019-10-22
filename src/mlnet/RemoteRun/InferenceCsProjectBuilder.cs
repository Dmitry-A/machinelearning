using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.CLI.CodeGenerator;
using Microsoft.ML.CLI.CodeGenerator.CSharp;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Onnx;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AzureML
{
    internal class InferenceCsProjectBuilder
    {
        public void BuildInferenceCsproj(string onnxModelFilePath, string onnxModelMapFilePath, TaskKind taskKind)
        {
            try
            {
                // TODO: this code is using private members via reflection because ML.NET is not offering any way to get an input schema
                // this should go away once this issue is resolved: https://github.com/dotnet/machinelearning/issues/4335

                var context = new MLContext();
                var estimator = context.Transforms.ApplyOnnxModel(onnxModelFilePath);

                var assembly = typeof(OnnxTransformer).Assembly;
                var onnxModelType = assembly.GetType("Microsoft.ML.Transforms.Onnx.OnnxModel");
                var onnxModelInfoType = assembly.GetType("Microsoft.ML.Transforms.Onnx.OnnxModel+OnnxModelInfo");
                var onnxVariableInfoType = assembly.GetType("Microsoft.ML.Transforms.Onnx.OnnxModel+OnnxVariableInfo");
                var onnxTransofrmerType = typeof(OnnxTransformer);

                var transformerPrivate = (OnnxTransformer)typeof(OnnxScoringEstimator).GetField("Transformer", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(estimator);
                var onnxModel = transformerPrivate.GetType().GetField("Model", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(transformerPrivate);

                var modelInfo = onnxModel.GetType().GetProperty("ModelInfo", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(onnxModel);

                var inputsInfoArray = (Array)modelInfo.GetType().GetProperty("InputsInfo", BindingFlags.Public | BindingFlags.Instance).GetValue(modelInfo);
                var outputsInfoArray = (Array)modelInfo.GetType().GetProperty("OutputsInfo", BindingFlags.Public | BindingFlags.Instance).GetValue(modelInfo);

                var inputTypes = GetSchema(inputsInfoArray);
                var outputTypes = GetSchema(outputsInfoArray);

                //var dvBuilder = new DataViewSchema.Builder();
                //foreach (var nextColumn in inputTypes)
                //{
                //    dvBuilder.AddColumn(nextColumn.Name, nextColumn.DvType);
                //}

                //var schema = dvBuilder.ToSchema();

                var mappedInputTypes = MapInputs(inputTypes, onnxModelMapFilePath);

                var codeGenerator = new OnnxCodeGenerator(
                    inputTypes,
                    outputTypes,
                    new ColumnInferenceResults()
                    {
                        TextLoaderOptions = new TextLoader.Options(),
                        ColumnInformation = new ColumnInformation() { LabelColumnName = "label" }
                    },
                    new CodeGeneratorSettings()
                    {
                        MlTask = taskKind,
                        OutputBaseDir = ".",
                        OutputName = "MyNamespace",
                        TrainDataset = "x:\\dummypath\\dummy_train.csv",
                        TestDataset = "x:\\dummypath\\dummy_test.csv",
                        LabelName = "Label",
                        ModelPath = "x:\\models\\model.zip"
                    });

                codeGenerator.GenerateOutput();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
        }

        private List<(string Name, VectorDataViewType DvType)> GetSchema(Array schema)
        {
            if (schema.Length < 1)
            {
                throw new ArgumentException("No input fields found in the model.");
            }

            var schemaType = schema.GetValue(0).GetType();

            var schemaTypes = new List<(string Name, VectorDataViewType DvType)>();

            // TODO: all input types shoudl be vectors, that's Ok, but make sure they're always 1x1 (I'm told it's the case but need more confirmation)
            foreach (var nextInput in schema)
            {
                var dvType = (DataViewType)schemaType.GetProperty("DataViewType", BindingFlags.Public | BindingFlags.Instance).GetValue(nextInput);
                var name = (string)schemaType.GetProperty("Name", BindingFlags.Public | BindingFlags.Instance).GetValue(nextInput);

                // TODO: sometimes output type would be OnnxSeqnenceType for probabilities, figure out what to generate for that
                var vType = dvType as VectorDataViewType;
                if (vType == null || !vType.IsKnownSize)
                {
                    continue;
                    // throw new ArgumentException("ONNX model inputs should be known sized vector types.");
                }

                schemaTypes.Add((name, vType));
            }

            return schemaTypes;
        }

        private List<(string Name, VectorDataViewType DvType)> MapInputs(List<(string Name, VectorDataViewType DvType)> rawInputs, string mapFile)
        {
            var jObj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(mapFile));
            var rawSchema = jObj["InputRawColumnSchema"];

            return rawInputs;
        }
    }
}