using System;
using System.Linq;
using System.Threading;
using Azure.MachineLearning.Services.Experiments;
using Azure.MachineLearning.Services.Runs;
using Azure.MachineLearning.Services.Workspaces;

namespace AzureML
{
    internal class FailureCounter
    {
        private int _failureCount;
        private readonly int _maxFailures;

        public FailureCounter(int maxFailures)
        {
            _maxFailures = maxFailures;
        }

        public void RecordFailure(string description)
        {
            if(++_failureCount >= _maxFailures)
            {
                throw new Exception(description);
            }
        }

        public void RecordFailure(Exception ex)
        {
            if (++_failureCount >= _maxFailures)
            {
                throw ex;
            }
        }
    }
}
