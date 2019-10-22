// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.ML.AutoML;
using Microsoft.ML.CLI.Templates.Console;
using Microsoft.ML.CLI.Utilities;
using Microsoft.ML.Data;

namespace Microsoft.ML.CLI.CodeGenerator.CSharp
{
    internal class OnnxCodeGenerator : IProjectGenerator
    {
        //private readonly Pipeline _pipeline;
        private readonly CodeGeneratorSettings _settings;
        private readonly ColumnInferenceResults _columnInferenceResult;
        private readonly List<(string Name, VectorDataViewType DvType)> _inputSchema;
        private readonly List<(string Name, VectorDataViewType DvType)> _outputSchema;

        internal OnnxCodeGenerator(
            List<(string Name, VectorDataViewType DvType)> inputSchema,
            List<(string Name, VectorDataViewType DvType)> outputSchema,
            ColumnInferenceResults columnInferenceResult,
            CodeGeneratorSettings settings)
        {
            //_pipeline = pipeline;
            _inputSchema = inputSchema;
            _outputSchema = outputSchema;
            _columnInferenceResult = columnInferenceResult;
            _settings = settings;
        }

        public void GenerateOutput()
        {
            // Get Namespace
            var namespaceValue = Utils.Normalize(_settings.OutputName);
            // TODO: test if this code works with various model outputs possible
            var labelColumn = _outputSchema.Where(c => c.Name == "label");
            if(labelColumn.Count() == 0 )
            {
                labelColumn = _outputSchema.Where(c => c.Name == "label");
                if (labelColumn.Count() == 0)
                {
                    throw new ArgumentException("Output schema must contain a label column.");
                }
            }

            var labelType = labelColumn.First().DvType.ItemType.GetRawKind();

            Type labelTypeCsharp = Utils.GetCSharpType(labelType);

            // Generate Model Project
            var modelProjectContents = GenerateModelProjectContents(namespaceValue, labelTypeCsharp);

            // Write files to disk.
            var modelprojectDir = Path.Combine(_settings.OutputBaseDir, $"{_settings.OutputName}.Model");
            var dataModelsDir = Path.Combine(modelprojectDir, "DataModels");
            var modelProjectName = $"{_settings.OutputName}.Model.csproj";

            Utils.WriteOutputToFiles(modelProjectContents.ModelInputCSFileContent, "ModelInput.cs", dataModelsDir);
            Utils.WriteOutputToFiles(modelProjectContents.ModelOutputCSFileContent, "ModelOutput.cs", dataModelsDir);
            Utils.WriteOutputToFiles(modelProjectContents.ModelProjectFileContent, modelProjectName, modelprojectDir);

            // Generate ConsoleApp Project
            var consoleAppProjectContents = GenerateConsoleAppProjectContents(namespaceValue, labelTypeCsharp);

            // Write files to disk.
            var consoleAppProjectDir = Path.Combine(_settings.OutputBaseDir, $"{_settings.OutputName}.ConsoleApp");
            var consoleAppProjectName = $"{_settings.OutputName}.ConsoleApp.csproj";

            Utils.WriteOutputToFiles(consoleAppProjectContents.ConsoleAppProgramCSFileContent, "Program.cs", consoleAppProjectDir);
            //Utils.WriteOutputToFiles(consoleAppProjectContents.modelBuilderCSFileContent, "ModelBuilder.cs", consoleAppProjectDir);
            Utils.WriteOutputToFiles(consoleAppProjectContents.ConsoleAppProjectFileContent, consoleAppProjectName, consoleAppProjectDir);

            // New solution file.
            Utils.CreateSolutionFile(_settings.OutputName, _settings.OutputBaseDir);

            // Add projects to solution
            var solutionPath = Path.Combine(_settings.OutputBaseDir, $"{_settings.OutputName}.sln");
            Utils.AddProjectsToSolution(modelprojectDir, modelProjectName, consoleAppProjectDir, consoleAppProjectName, solutionPath);
        }

        internal (string ConsoleAppProgramCSFileContent, string ConsoleAppProjectFileContent) GenerateConsoleAppProjectContents(string namespaceValue, Type labelTypeCsharp)
        {
            var predictProgramCSFileContent = GeneratePredictProgramCSFileContent(namespaceValue);
            predictProgramCSFileContent = Utils.FormatCode(predictProgramCSFileContent);

            var predictProjectFileContent = GeneratPredictProjectFileContent(namespaceValue);

            //var transformsAndTrainers = GenerateTransformsAndTrainers();
            //var modelBuilderCSFileContent = GenerateModelBuilderCSFileContent(
            //    transformsAndTrainers.Usings,
            //    transformsAndTrainers.TrainerMethod,
            //    transformsAndTrainers.PreTrainerTransforms,
            //    transformsAndTrainers.PostTrainerTransforms,
            //    namespaceValue, false, labelTypeCsharp.Name);

            //modelBuilderCSFileContent = Utils.FormatCode(modelBuilderCSFileContent);

            return (predictProgramCSFileContent, predictProjectFileContent);
        }

        internal (string ModelInputCSFileContent, string ModelOutputCSFileContent, string ModelProjectFileContent) GenerateModelProjectContents(string namespaceValue, Type labelTypeCsharp)
        {
            var classLabels = GenerateClassLabels(_inputSchema);
            var modelInputCSFileContent = GenerateModelInputCSFileContent(namespaceValue, classLabels);
            modelInputCSFileContent = Utils.FormatCode(modelInputCSFileContent);
            var modelOutputCSFileContent = GenerateModelOutputCSFileContent(labelTypeCsharp.Name, namespaceValue);
            modelOutputCSFileContent = Utils.FormatCode(modelOutputCSFileContent);
            var modelProjectFileContent = GenerateModelProjectFileContent();
            return (modelInputCSFileContent, modelOutputCSFileContent, modelProjectFileContent);
        }

        internal static IList<string> GenerateClassLabels(List<(string Name, VectorDataViewType DvType)> columns)
        {
            IList<string> result = new List<string>();
            foreach (var column in columns)
            {
                StringBuilder sb = new StringBuilder();
                int range = column.DvType.GetVectorSize();
                bool isArray = range > 0;
                sb.Append(Symbols.PublicSymbol);
                sb.Append(Symbols.Space);

                switch (column.DvType.ItemType.GetItemType().GetRawKind())
                {
                    case Microsoft.ML.Data.DataKind.String:
                        sb.Append(Symbols.StringSymbol);
                        break;
                    case Microsoft.ML.Data.DataKind.Boolean:
                        sb.Append(Symbols.BoolSymbol);
                        break;
                    case Microsoft.ML.Data.DataKind.Single:
                        sb.Append(Symbols.FloatSymbol);
                        break;
                    case Microsoft.ML.Data.DataKind.Double:
                        sb.Append(Symbols.DoubleSymbol);
                        break;
                    case Microsoft.ML.Data.DataKind.Int32:
                        sb.Append(Symbols.IntSymbol);
                        break;
                    case Microsoft.ML.Data.DataKind.UInt32:
                        sb.Append(Symbols.UIntSymbol);
                        break;
                    case Microsoft.ML.Data.DataKind.Int64:
                        sb.Append(Symbols.LongSymbol);
                        break;
                    case Microsoft.ML.Data.DataKind.UInt64:
                        sb.Append(Symbols.UlongSymbol);
                        break;
                    default:
                        throw new ArgumentException($"The data type '{column.DvType.GetRawKind()}' is not handled currently.");
                }

                if (range != 1)
                {
                    throw new ArgumentException("ONNX code generation requires vector input of size 1.");
                }

                if (column.Name.StartsWith("input_"))
                {
                    int position = int.Parse(column.Name.Replace("input_", ""));
                    result.Add($"[ColumnName(\"{column.Name}\"), LoadColumn({position})]");
                }
                else
                {
                    result.Add($"[ColumnName(\"{column.Name}\")]");
                }

                sb.Append(" ");
                sb.Append(Utils.Normalize(column.Name));
                sb.Append("{get; set;}");
                result.Add(sb.ToString());
                result.Add("\r\n");
            }

            return result;
        }

        private static string GenerateModelProjectFileContent()
        {
            var modelProject = new OnnxModelProject();
            return modelProject.TransformText();
        }

        private string GenerateModelOutputCSFileContent(string predictionLabelType, string namespaceValue)
        {
            var modelOutputClass = new OnnxModelOutputClass() { TaskType = _settings.MlTask.ToString(), PredictionLabelType = predictionLabelType, Namespace = namespaceValue };
            return modelOutputClass.TransformText();
        }

        private string GenerateModelInputCSFileContent(string namespaceValue, IList<string> classLabels)
        {
            ModelInputClass modelInputClass = new ModelInputClass() { Namespace = namespaceValue, ClassLabels = classLabels };
            return modelInputClass.TransformText();
        }

        private static string GeneratPredictProjectFileContent(string namespaceValue)
        {
            var predictProjectFileContent = new OnnxPredictProject() { Namespace = namespaceValue };
            return predictProjectFileContent.TransformText();
        }

        private string GeneratePredictProgramCSFileContent(string namespaceValue)
        {
            var predictProgram = new OnnxPredictProgram()
            {
                TaskType = _settings.MlTask.ToString(),
                LabelName = _settings.LabelName,
                Namespace = namespaceValue,
                // TODO: for ONNX we only have a model so no info on how to read the file..
                TestDataPath = "",
                TrainDataPath ="",
                //HasHeader = _columnInferenceResult.TextLoaderOptions.HasHeader,
                Separator = ',',
                //AllowQuoting = _columnInferenceResult.TextLoaderOptions.AllowQuoting,
                //AllowSparse = _columnInferenceResult.TextLoaderOptions.AllowSparse,
            };
            return predictProgram.TransformText();
        }

        private string GenerateModelBuilderCSFileContent(string usings,
            string trainerMethod,
            List<string> preTrainerTransforms,
            List<string> postTrainerTransforms,
            string namespaceValue,
            bool cacheBeforeTrainer,
            string predictionLabelType)
        {
            var modelBuilder = new ModelBuilder()
            {
                PreTrainerTransforms = preTrainerTransforms,
                PostTrainerTransforms = postTrainerTransforms,
                HasHeader = _columnInferenceResult.TextLoaderOptions.HasHeader,
                Separator = _columnInferenceResult.TextLoaderOptions.Separators.FirstOrDefault(),
                AllowQuoting = _columnInferenceResult.TextLoaderOptions.AllowQuoting,
                AllowSparse = _columnInferenceResult.TextLoaderOptions.AllowSparse,
                Trainer = trainerMethod,
                GeneratedUsings = usings,
                Path = _settings.TrainDataset,
                TestPath = _settings.TestDataset,
                TaskType = _settings.MlTask.ToString(),
                Namespace = namespaceValue,
                LabelName = _settings.LabelName,
                CacheBeforeTrainer = cacheBeforeTrainer,
            };

            return modelBuilder.TransformText();
        }
    }
}
