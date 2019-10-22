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

                // var parentRun = (AutoMLRun)experiment.Runs.List().Where(r => r.Id == "AutoML_9498b503-9e57-420a-9204-4f8375812e13").FirstOrDefault();

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

                AutoMLRunMonitoring.ReportStatus(parentRun, workspace, experiment);

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
                var bestModelMapPath = new FileInfo(@"models\bestModelMap.json");
                Console.Write($"Downloading best model to {bestModelPath}..");

                var bestRun = parentRun.GetBestRunAsync().Result;

                var modelPath = "outputs/model.onnx";
                var modelMapPath = "outputs/model_onnx.json";

                parentRun.DownloadRunArtifactAsync(bestRun, modelPath, bestModelPath).Wait();
                parentRun.DownloadRunArtifactAsync(bestRun, modelMapPath, bestModelMapPath).Wait();

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
            var datastores = workspace.Datastores.List().Where(ds => ds.DatastoreName.ToLowerInvariant() == "workspacefilestore");

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

            // Construct a data reference
            var drc = new DataReferenceConfiguration();
            drc.DataStoreName = datastore.DatastoreName;
            drc.Mode = DataStoreMode.Mount;
            drc.PathOnDataStore = string.Empty;

            string projectFolder = Path.Combine(Directory.GetCurrentDirectory(), "project");
            string getDataPath = GetGetDataPath(projectFolder);
            GenerateGetData.GenerateGetDataPy(_dataRefName, trainingFileName, labelColumnName, getDataPath);

            var autoMLSettings = new AutoMLSettings(
               iterationTimeoutInMin: (int)maxExplorationTime.TotalMinutes,
               iterations: 5,
               primaryMetric: "AUC_weighted",
               preprocess: true,
               nCrossValidations: 5);

            autoMLSettings.TaskType = taskType;
            autoMLSettings.EnableOnnxCompatibleModels = true;
            autoMLSettings.EnableTensorFlow = false;

            var autoMLConfig = new AutoMLConfiguration(
                autoMLSettings: autoMLSettings,
                projectFolder: new DirectoryInfo(projectFolder),
                task: taskType);

            //autoMLConfig.DockerConfiguration.Enabled = true;
            autoMLConfig.PipPackages.Add("azureml-sdk[automl]");
            autoMLConfig.ComputeTarget = ct;

            autoMLConfig.DataReferences = new Dictionary<string, DataReferenceConfiguration>();
            autoMLConfig.DataReferences.Add(_dataRefName, drc);

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
