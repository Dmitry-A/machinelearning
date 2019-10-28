using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Azure.MachineLearning.Services;
using Azure.MachineLearning.Services.AutoML;
using Azure.MachineLearning.Services.Compute;
using Azure.MachineLearning.Services.Datastores;
using Azure.MachineLearning.Services.RunArtifacts;
using Azure.MachineLearning.Services.Runs;
using Azure.MachineLearning.Services.Workspaces;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.ML.CLI.Utilities;
using Microsoft.Rest;

namespace AzureML
{
    internal static class AutoMLRunner
    {
        private const string _dataRefName = "AUTOML1";

        public static void RunAutoML(
            TimeSpan maxExplorationTime,
            string taskType,
            string subscriptionId,
            string resourceGroup,
            string experimentName,
            string workspaceName,
            string trainingFileName,
            string labelColumnName,
            string computeTarget = null)
        {
            var serviceClientCredentials = GetAzureCredentialViaCli();

            try
            {
                Console.Write("Verifying parameters: workspace.. ");

                var workspace = AmlUtils.CallAMLAndHandleExceptions(
                    () =>
                    {
                        var wsClient = new WorkspaceClient(serviceClientCredentials);
                        return wsClient.Workspaces.GetAsync(new Guid(subscriptionId), resourceGroup, workspaceName).Result;
                    }, "workspace", workspaceName);

                //A.ListRuns(workspace);
                //return;

                Console.Write("Ok, experiment.. ");

                var experiment = AmlUtils.CallAMLAndHandleExceptions(
                    () => { return workspace.Experiments.GetAsync(experimentName).Result; }, "experiment", experimentName);

                Console.Write("Ok, compute target.. ");

                var computeTargets = AmlUtils.CallAMLAndHandleExceptions(
                    () => { return workspace.ComputeTargets.List(); }, "computeTargets", experimentName);

                ComputeTarget aMLComputeTarget = null;
                if (computeTarget != null)
                {
                    aMLComputeTarget = computeTargets.Where(ct => ct.Name.Contains(computeTarget)).FirstOrDefault();
                }
                else
                {
                    if(computeTargets.Count() == 1)
                    {
                        aMLComputeTarget = computeTargets.First();
                    }
                }
                // AttachAsync((AMLComputeTarget)workspace.ComputeTargets.ProvisionAsync(computeTargetName, acConfig).Result;

                if (aMLComputeTarget == null) { throw new Exception($"No compute targets found in workspace {workspace.Name}"); }

                Console.WriteLine("Ok.");

                var autoMLConfig = GetTrainingRunConfig(workspace, aMLComputeTarget, maxExplorationTime, taskType, trainingFileName, labelColumnName);

                // Training
                Console.WriteLine($"Starting AutoML run in workspace {workspace.Name}, experiment {experiment.Name} using compute target {aMLComputeTarget.Name}.");

                //var parentRun = (AutoMLRun)experiment.Runs.List().Where(r => r.Id == "AutoML_e4fc1f36-5943-40b2-9263-e225d360be9d").FirstOrDefault();

                ////var model = run.RegisterModelAsync("", modelPath).Result;
                //var bestModelPath1 = new FileInfo(@"models\bestModel.onnx");

                //Console.WriteLine(Directory.GetCurrentDirectory());
                //run.DownloadBestModelAsync(downloadFilePath: bestModelPath1).Wait();

                //var rc = run.ListChildren();
                //Console.Write(rc.Count());
                //var v1 = rc.Where(r1 => r1.Properties["bla"] == null);

                AutoMLRun parentRun = experiment.Runs.CreateAsync(autoMLConfig).Result as AutoMLRun;

                Console.WriteLine($"Created AutoML run {parentRun.Name}.");
                Console.WriteLine($"You can also monitor this run using URL: {AutoMLRunMonitoring.GetRunUrl(parentRun, workspace.SubscriptionId.ToString(), workspace.ResourceGroupName, workspace.Name)}.");
                Console.WriteLine();

                var bestRun = AutoMLRunMonitoring.ReportStatus(parentRun, workspace, experiment);

                Console.WriteLine($"Completed a training run");

                //Console.WriteLine("Print Run Details: ");
                //var details = parentRun.GetRunDetailsAsync().Result;
                //Console.WriteLine("Status from RunDetails: {0}", details.Status);

                /*
                Console.WriteLine("Error Code: {0}", details.Error.Error.Code);
                Console.WriteLine("Error Message: {0}", details.Error.Error.Message);
                Console.WriteLine("Debug Info: {0}", details.Error.Error.DebugInfo.Message);
                Console.WriteLine("Debug Info Type: {0}", details.Error.Error.DebugInfo.Type);
                */

                //Console.WriteLine("Starting retrieveing and registering the model");
                //List<RunArtifact> runLogs = parentRun.RunArtifact.ListRunOutputs().ToList();

                Directory.CreateDirectory("models");

                var bestModelPath = new FileInfo(@"models\bestModel.onnx");
                var bestModelMapPath = new FileInfo(@"models\labels.json");
                Console.Write($"Downloading best model to {bestModelPath}..");

                //var bestRun = parentRun.GetBestRunAsync().Result;

                var modelPath = "train_artifacts/model.onnx";
                var modelMapPath = "train_artifacts/labels.json";

                parentRun.DownloadRunArtifactAsync(bestRun.bestRun, modelPath, bestModelPath).Wait();
                parentRun.DownloadRunArtifactAsync(bestRun.bestRun, modelMapPath, bestModelMapPath).Wait();

                var projGen = new InferenceCsProjectBuilder();

                projGen.BuildInferenceCsproj(bestModelPath.FullName, bestModelMapPath.FullName, Utils.GetTaskKind(taskType));

                Console.WriteLine($"Done.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }

        }

        private static RunConfigurationBase GetTrainingRunConfig(Workspace workspace, ComputeTarget ct, TimeSpan maxExplorationTime, string taskType, string trainingFileName, string labelColumnName)
        {
            var datastores = workspace.Datastores.List().Where(ds => ds.DatastoreName.ToLowerInvariant() == "workspaceblobstore"); // *

            if (datastores.Count() == 0)
            {
                throw new ArgumentException($"Didn't find a default file datastore in workspace {workspace.Name}.");
            }

            var datastore = datastores.First();

            // get default workspace storage container name
            var defaultStorageContainer = workspace.StorageAccountArmId.Split('/').LastOrDefault();

            if (string.IsNullOrWhiteSpace(defaultStorageContainer))
            {
                throw new ArgumentException($"Default storage container is not defined for workspace {workspace.Name}");
            }

            //var ds = workspace.Datastores.RegisterAzureBlobStorageDatastoreAsync(
            //    "AUTOML1",
            //    "traindata",
            //    defaultStorageContainer, accountKey: "").Result;

            //string projectFolder = Path.Combine(Directory.GetCurrentDirectory(), "project");
            //string getDataPath = GetGetDataPath(projectFolder);
            //GenerateGetData.GenerateGetDataPy(_dataRefName, trainingFileName, labelColumnName, getDataPath);

            var autoMLSettings = new AutoMLSettings(
               iterationTimeoutInMin: (int)maxExplorationTime.TotalMinutes,
               iterations: 5,
               primaryMetric: "accuracy",
               preprocess: true,
               nCrossValidations: 5);

            // * \/
            autoMLSettings.TaskType = "image-classification";
            autoMLSettings.EnableOnnxCompatibleModels = false;
            autoMLSettings.EnableTensorFlow = true;
            autoMLSettings.EnableDnn = true;
            autoMLSettings.ImagesFolder = "images";
            autoMLSettings.LabelsFile = "images/WeatherData/weather.tsv";
            //autoMLSettings.ImagesFolder = "images";
            //autoMLSettings.LabelsFile = "images/crack/labels.csv";
            autoMLSettings.Epochs = 10;
            autoMLSettings.ComputeTarget = ct.Name;

            var autoMLConfig = new AutoMLConfiguration(
                autoMLSettings: autoMLSettings,
                projectFolder: null, //new DirectoryInfo(projectFolder), // *
                task: taskType);

            //autoMLConfig.DockerConfiguration.Enabled = true;
            autoMLConfig.PipPackages.Add("azureml-sdk[automl]");
            autoMLConfig.ComputeTarget = ct;

            // Construct a data reference
            var drc = new DataReferenceConfiguration();
            drc.DataStoreName = datastore.DatastoreName;
            drc.Mode = DataStoreMode.Mount;
            //drc.PathOnDataStore = string.Empty;

            autoMLConfig.PythonVersion = new Version(3, 6, 5);
            autoMLConfig.DockerConfiguration.BaseImage = "mcr.microsoft.com/azureml/base-gpu:intelmpi2018.3-cuda10.0-cudnn7-ubuntu16.04";

            autoMLConfig.ScriptFile = new FileInfo("train.py");

            autoMLConfig.DataReferences = new Dictionary<string, DataReferenceConfiguration>()
            {
                { "default", new DataReferenceConfiguration() { DataStoreName = datastore.DatastoreName, Mode = DataStoreMode.Mount } },
                { "labels_file_root", new DataReferenceConfiguration() { DataStoreName = datastore.DatastoreName, Mode = DataStoreMode.Mount } }
            };

            return autoMLConfig;
        }

        private static ServiceClientCredentials GetAzureCredentialViaCli()
        {

            var token = AzAuth.GetAccessToken().Result;

            var tokenCredentials = new TokenCredentials(token);
            return new AzureCredentials(
                tokenCredentials,
                tokenCredentials,
                null,  // TODO: provide a way to specify TenantId?
                AzureEnvironment.AzureGlobalCloud);
        }

        private static string GetGetDataPath(string projectFolder)
        {
            if (!Directory.Exists(projectFolder))
            {
                Directory.CreateDirectory(projectFolder);
            }

            return Path.Combine(projectFolder, "get_data.py");
        }
    }
}
