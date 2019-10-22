using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.MachineLearning.Services;
using Azure.MachineLearning.Services.Metrics;
using Azure.MachineLearning.Services.Runs;
using Newtonsoft.Json.Linq;

namespace AzureML
{
    internal class RunStats
    {
        public string GetPrimaryMetricFromProperties(Run run)
        {
            var parsedJson = JObject.Parse(run.Properties["AMLSettingsJsonString"]);
            return parsedJson["primary_metric"].ToString();
        }

        public async Task<(Run bestRun, double bestScore)> GetBestRunAsync(
            IPageFetcher<Run> childRunPageFetcher,
            string queryMetric,
            Dictionary<string, List<string>> customHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var bestScore = 0.0;
            Run bestRun = null;
            do
            {
                IEnumerable<Run> childRuns = await childRunPageFetcher.FetchNextPageAsync(
                    customHeaders,
                    cancellationToken).ConfigureAwait(false);

                foreach (var child in childRuns)
                {
                    IPageFetcher<RunMetric> childMetricPagedList = child.GetMetricsPagedList();

                    double bestMetricScore = await GetBestMetricAsync(
                        childMetricPagedList,
                        queryMetric,
                        customHeaders,
                        cancellationToken).ConfigureAwait(false);

                    if (bestMetricScore > bestScore)
                    {
                        bestScore = bestMetricScore;
                        bestRun = child;
                    }
                }
            }
            while (!childRunPageFetcher.OnLastPage);

            return (bestRun, bestScore);
        }

        public async Task<double> GetBestMetricAsync(
            IPageFetcher<RunMetric> childRunMetricPageFetcher,
            string queryMetric,
            Dictionary<string, List<string>> customHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var bestScore = 0.0;
            do
            {
                IEnumerable<RunMetric> metrics = await childRunMetricPageFetcher.FetchNextPageAsync(
                    customHeaders,
                    cancellationToken).ConfigureAwait(false);

                foreach (var metric in metrics)
                {
                    foreach (var cell in metric.Cells)
                    {
                        if (cell.TryGetValue(queryMetric, out var cellValue))
                        {
                            double metricValue = (double)cellValue;
                            bestScore = Math.Max(bestScore, metricValue);
                        }
                    }
                }
            }
            while (!childRunMetricPageFetcher.OnLastPage);

            return bestScore;
        }
    }
}
