// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator 1.0.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Azure.MachineLearning.Services.GeneratedOld
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for Swagger.
    /// </summary>
    public static partial class SwaggerExtensions
    {
            /// <summary>
            /// Get swagger.json file
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// The Azure Subscription ID.
            /// </param>
            /// <param name='resourceGroupName'>
            /// Name of the resource group in which the workspace is located.
            /// </param>
            /// <param name='workspace'>
            /// The name of the workspace.
            /// </param>
            public static string GetModel(this ISwagger operations, System.Guid subscriptionId, string resourceGroupName, string workspace)
            {
                return operations.GetModelAsync(subscriptionId, resourceGroupName, workspace).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get swagger.json file
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// The Azure Subscription ID.
            /// </param>
            /// <param name='resourceGroupName'>
            /// Name of the resource group in which the workspace is located.
            /// </param>
            /// <param name='workspace'>
            /// The name of the workspace.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<string> GetModelAsync(this ISwagger operations, System.Guid subscriptionId, string resourceGroupName, string workspace, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetModelWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspace, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}