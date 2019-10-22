// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.ML.CLI.CodeGenerator;
using Microsoft.ML.CLI.Data;

namespace Microsoft.ML.CLI.Commands.New
{
    internal class AzureAutoTrainCommand : ICommand
    {
        private readonly AzureAutoTrainCommandSettings _settings;

        internal AzureAutoTrainCommand(AzureAutoTrainCommandSettings settings)
        {
            _settings = settings;
        }

        public void Execute()
        {
            var remoteRun = new AzureAutoML(_settings);
            remoteRun.RunAutoML();
        }
    }
}
