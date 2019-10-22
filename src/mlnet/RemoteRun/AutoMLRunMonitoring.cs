using System;
using System.Linq;
using System.Threading;
using Azure.MachineLearning.Services.Experiments;
using Azure.MachineLearning.Services.Runs;
using Azure.MachineLearning.Services.Workspaces;

namespace AzureML
{
    internal static class AutoMLRunMonitoring
    {
        private static TimeSpan _refreshInterval = TimeSpan.FromSeconds(1);

        public static void ReportStatus(AutoMLRun autoMLRun, Workspace workspace, Experiment experiment)
        {
            var setupIterationStatus = MonitorSetupIteration(autoMLRun, experiment);

            if (setupIterationStatus != "Completed")
            {
                // TODO: not sure what to do here yet. Main run will fail and the current flow works Ok so might be nothing.
            }

            MonitorParentRun(autoMLRun);
        }

        public static string MonitorParentRun(AutoMLRun autoMLRun)
        {
            var fpm = new ConsoleFixedPositionMessage(3, enableSpinner: true);

            do
            {
                Thread.Sleep(_refreshInterval);

                autoMLRun.RefreshAsync().Wait();
                var childRuns = autoMLRun.ListChildren();

                var runsByStatus = childRuns.GroupBy(cr => cr.Status, cr => cr);

                var childRunStats = string.Join(",", runsByStatus.Select(r => $"{r.Key}: {r.Count()}"));

                var completedChildRuns = childRuns.Where(cr => cr.InTerminalState && cr.Status == "Completed");

                (Run bestRun, double bestScore) bestRun = (null, 0);
                string bestRunStats = "";

                if (completedChildRuns.Any())
                {
                    var runStats = new RunStats();
                    bestRun = runStats.GetBestRunAsync(autoMLRun.GetPagedListOfChildren(), runStats.GetPrimaryMetricFromProperties(autoMLRun)).Result;

                    var algo = bestRun.bestRun.Properties.ContainsKey("run_algorithm") ? bestRun.bestRun.Properties["run_algorithm"] : "unknown";
                    var preproc = bestRun.bestRun.Properties.ContainsKey("run_preprocessor") ? bestRun.bestRun.Properties["run_preprocessor"] : "unknown";
                    bestRunStats = $"Best {runStats.GetPrimaryMetricFromProperties(autoMLRun)} metric value is {bestRun.bestScore} using algorithm {algo} and preprocessor {preproc}";
                }

                if (autoMLRun.InTerminalState)
                {
                    var finalMsg = $"AutoML sweep final status is {autoMLRun.Status}. Run time is {(autoMLRun.EndTimeUtc - autoMLRun.CreatedUtc).Value.TotalSeconds} seconds.";

                    fpm.WriteContent(new[] { finalMsg, "Child run stats: " + childRunStats, bestRunStats }, finalMessage: true);

                    return autoMLRun.Status;
                }

                var statusContent = new[]
                {
                    "Running AutoML pipeline sweep..",
                    $"Current child run stats: {childRunStats}",
                    bestRunStats
                };

                fpm.WriteContent(statusContent);
            }
            while (true);
        }

        public static string MonitorSetupIteration(AutoMLRun autoMLRun, Experiment experiment)
        {
            Run setupIteration;

            var fpm = new ConsoleFixedPositionMessage(1, enableSpinner: true);

            do
            {
                Thread.Sleep(_refreshInterval);
                var runs = experiment.Runs;
                var runList = runs.List();

                var childRuns = autoMLRun.ListChildren();
                // TODO: figure out a better way to find setup iteration for this run
                setupIteration = runList.Where(cr => cr.Id.StartsWith(autoMLRun.Id) && cr.Properties.ContainsKey("iteration") && cr.Properties["iteration"] == "setup").FirstOrDefault();

                // TODO: check for overall experiment terminal status and handle that
                if (setupIteration != null)
                {
                    break;
                    //Console.WriteLine($"Setup iteration {setupIteration.Name} is {setupIteration.Status}");
                }

                fpm.WriteContent("Waiting for setup iteration to get created..");
            }
            while (true);

            //var fpm = new ConsoleFixedPositionMessage(1, enableSpinner: true);

            do
            {
                setupIteration.RefreshAsync().Wait();
                fpm.WriteContent($"Setup iteration in progress, status is {setupIteration.Status}..");

                if(setupIteration.InTerminalState)
                {
                    fpm.WriteContent($"Setup iteration final status is {setupIteration.Status}. Run time {(setupIteration.EndTimeUtc - setupIteration.CreatedUtc).Value.TotalSeconds} seconds.", true);
                    return setupIteration.Status;
                }

                Thread.Sleep(_refreshInterval);
            }
            while (true);
        }

        public static string GetRunUrl(AutoMLRun run, string subscriptionId, string resourceGroupName, string workspaceName)
        {
            return $"https://mlworkspacecanary.azure.ai/portal/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.MachineLearningServices/workspaces/{workspaceName}/experiments/{run.ExperimentName}/runs/{run.Id}";
        }
    }
}
