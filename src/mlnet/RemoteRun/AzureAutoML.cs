// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using Azure.MachineLearning.Services.Workspaces;
using AzureML;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.ML.AutoML;
using Microsoft.ML.CLI.Data;
using Microsoft.ML.CLI.Utilities;
using Microsoft.Rest;
using NLog;

namespace Microsoft.ML.CLI.CodeGenerator
{
    internal partial class AzureAutoML
    {
        private AzureAutoTrainCommandSettings _settings;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private TaskKind _taskKind;

        public AzureAutoML(AzureAutoTrainCommandSettings settings)
        {
            _settings = settings;
            _taskKind = Utils.GetTaskKind(settings.MlTask);
        }

        public int RunAutoML()
        {
            try
            {
				AutoMLRunner.RunAutoML(
					TimeSpan.FromSeconds(_settings.MaxExplorationTime),
                    _settings.MlTask,
                    _settings.SubscriptionId,
                    _settings.ResourceGroup,
                    _settings.Experiment,
                    _settings.Workspace,
                    _settings.TrainFile,
                    _settings.LabelColumnName,
                    computeTarget: _settings.ComputeTarget);
			}
            finally
            {
                Console.ResetColor();
            }

            return 0;
        }
    }
}
