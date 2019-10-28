﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.MachineLearning.Services.Experiments;
using Azure.MachineLearning.Services.Runs;
using Azure.MachineLearning.Services.Workspaces;

namespace AzureML
{
    internal static class AutoMLRunMonitoring
    {
        private static TimeSpan _refreshInterval = TimeSpan.FromSeconds(1);

        public static (Run bestRun, double bestScore) ReportStatus(AutoMLRun autoMLRun, Workspace workspace, Experiment experiment)
        {
            var setupIterationStatus = MonitorSetupIteration(autoMLRun, experiment);

            //if (setupIterationStatus != "Completed")
            //{
            //    // TODO: not sure what to do here yet. Main run will fail and the current flow works Ok so might be nothing.
            //}

            return MonitorParentRun(autoMLRun).Result;
        }

        public static async Task<(Run bestRun, double bestScore)> MonitorParentRun(AutoMLRun autoMLRun)
        {
            var fpm = new ConsoleFixedPositionMessage(3, enableSpinner: true);

            var failures = new FailureCounter(30);
            var childRunRetrieval = new FailureCounter(300);

            do
            {
                try
                {
                    Thread.Sleep(_refreshInterval);

                    await autoMLRun.RefreshAsync();
                    var autoMlChildRuns = autoMLRun.ListChildren();

                    // TODO: this isn't gonna universally work, only with images as they are now

                    // images runs only have one child which is hyperdrive run
                    var hdRun = autoMlChildRuns.FirstOrDefault();

                    if (hdRun == null)
                    {
                        childRunRetrieval.RecordFailure("Didn't find a child run.");

                        continue;
                    }

                    var hdChildRuns = hdRun.ListChildren();

                    var runsByStatus = hdChildRuns.GroupBy(cr => cr.Status, cr => cr);

                    var childRunStats = string.Join(",", runsByStatus.Select(r => $"{r.Key}: {r.Count()}"));

                    var completedChildRuns = hdChildRuns.Where(cr => cr.InTerminalState && cr.Status == "Completed" && cr.Type != "preparation");

                    (Run bestRun, double bestScore) bestRun = (null, 0);
                    string bestRunStats = "";

                    if (completedChildRuns.Any())
                    {
                        var runStats = new RunStats();
                        bestRun = await runStats.GetBestRunAsync(completedChildRuns, runStats.GetPrimaryMetricFromProperties(autoMLRun));

                        if (bestRun.bestRun == null)
                        {
                            continue;
                        }

                        var algo = bestRun.bestRun.Properties.ContainsKey("run_algorithm") ? bestRun.bestRun.Properties["run_algorithm"] : "unknown";
                        var preproc = bestRun.bestRun.Properties.ContainsKey("run_preprocessor") ? bestRun.bestRun.Properties["run_preprocessor"] : "unknown";
                        bestRunStats = $"Best {runStats.GetPrimaryMetricFromProperties(autoMLRun)} metric value is {bestRun.bestScore} using algorithm {algo} and preprocessor {preproc}";
                    }

                    if (hdRun.InTerminalState)
                    {
                        var finalMsg = $"AutoML sweep final status is {hdRun.Status}. Run time is {(hdRun.EndTimeUtc - hdRun.CreatedUtc).Value.TotalSeconds} seconds.";

                        fpm.WriteContent(new[] { finalMsg, "Child run stats: " + childRunStats, bestRunStats }, finalMessage: true);

                        return bestRun;
                    }

                    var statusContent = new[]
                    {
                        "Running AutoML pipeline sweep..",
                        $"Current child run stats: {childRunStats}",
                        bestRunStats
                    };

                    fpm.WriteContent(statusContent);
                }
                catch(Exception ex)
                {
                    failures.RecordFailure(ex);
                }
            }
            while (true);
        }

        public static async Task<string> MonitorSetupIteration(AutoMLRun autoMLRun, Experiment experiment)
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
                await setupIteration.RefreshAsync();
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