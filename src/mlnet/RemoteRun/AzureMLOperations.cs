// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.MachineLearning.Services.Workspaces;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Rest;

namespace Microsoft.ML.CLI.CodeGenerator
{
    public static class AzureMLOperations
    {
        //public static void ListWorkspaces(ServiceClientCredentials credentials, Guid subscriptionId)
        //{
        //    var amlClient = new WorkspaceClient(credentials);

        //    var allWorkspaces = amlClient.Workspaces.List(subscriptionId);
        //    foreach (var w in allWorkspaces)
        //    {
        //        Console.WriteLine("{0} {1} {2}", w.Type, w.ResourceGroupName, w.Name);
        //    }
        //}

        public static  IEnumerable<ArmData> ListWorkspaces(ServiceClientCredentials credentials, Guid subscriptionId)
        {
            var amlClient = new WorkspaceClient(credentials);

            var workspaceFetcher = amlClient.Workspaces.List(subscriptionId);

            return workspaceFetcher;
        }

        //public static async Task<ArmData> ListWorkspacesAsync(ServiceClientCredentials credentials, Guid subscriptionId)
        //{
        //    var amlClient = new WorkspaceClient(credentials);

        //    var workspaceFetcher = amlClient.Workspaces.GetPagedList(subscriptionId);
        //    do
        //    {
        //        var nxtWorkspaces = await workspaceFetcher.FetchNextPageAsync().ConfigureAwait(false);

        //        foreach (var w in nxtWorkspaces)
        //        {
        //            Console.WriteLine("{0} {1} {2}", w.Type, w.ResourceGroupName, w.Name);
        //        }
        //    }
        //    while (!workspaceFetcher.OnLastPage);
        //}

        public static async Task<Workspace> GetSpecificWorkspaceAsync(ServiceClientCredentials credentials, Guid subscriptionId, string resourceGroupName, string workspaceName)
        {
            var amlClient = new WorkspaceClient(credentials);

            return await amlClient.Workspaces.GetAsync(subscriptionId, resourceGroupName, workspaceName).ConfigureAwait(false);
        }

        //public static async Task<Workspace> CreateWorkspaceAsync(ServiceClientCredentials credentials, Guid subscriptionId, string resourceGroupName, string workspaceName)
        //{
        //    var amlClient = new WorkspaceClient(credentials);
        //    return await amlClient.Workspaces.CreateAsync(Region.USWest2, workspaceName, subscriptionId, resourceGroupName, workspaceName, createResourceGroup: false); //, workspaceName + "storage", workspaceName + "kv", workspaceName + "appinsights", workspaceName + "containers");
        //}

        public static async Task CreateExperimentIfNotExist(Workspace ws, string expName)
        {
            var exp = await ws.Experiments.CreateIfNotExistAsync(expName).ConfigureAwait(false);

            Console.WriteLine("Experiment: {0}", exp.Name);
        }
    }
}
