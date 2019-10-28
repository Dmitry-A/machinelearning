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
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for Jasmine.
    /// </summary>
    public static partial class JasmineExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentRunId'>
            /// </param>
            /// <param name='jsonDefinition'>
            /// </param>
            /// <param name='file'>
            /// </param>
            public static RunStatus PostRemoteRun(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentRunId, string jsonDefinition = default(string), IList<object> file = default(IList<object>))
            {
                return operations.PostRemoteRunAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentRunId, jsonDefinition, file).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentRunId'>
            /// </param>
            /// <param name='jsonDefinition'>
            /// </param>
            /// <param name='file'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<RunStatus> PostRemoteRunAsync(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentRunId, string jsonDefinition = default(string), IList<object> file = default(IList<object>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PostRemoteRunWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentRunId, jsonDefinition, file, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentRunId'>
            /// </param>
            /// <param name='jsonDefinition'>
            /// </param>
            /// <param name='snapshotId'>
            /// </param>
            public static RunStatus PostRemoteSnapshotRun(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentRunId, string jsonDefinition = default(string), System.Guid? snapshotId = default(System.Guid?))
            {
                return operations.PostRemoteSnapshotRunAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentRunId, jsonDefinition, snapshotId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentRunId'>
            /// </param>
            /// <param name='jsonDefinition'>
            /// </param>
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<RunStatus> PostRemoteSnapshotRunAsync(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentRunId, string jsonDefinition = default(string), System.Guid? snapshotId = default(System.Guid?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PostRemoteSnapshotRunWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentRunId, jsonDefinition, snapshotId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentrunId'>
            /// </param>
            /// <param name='updatedIterations'>
            /// </param>
            /// <param name='updatedTime'>
            /// </param>
            /// <param name='updatedExitScore'>
            /// </param>
            public static void ContinueRun(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentrunId, int? updatedIterations = default(int?), int? updatedTime = default(int?), double? updatedExitScore = default(double?))
            {
                operations.ContinueRunAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentrunId, updatedIterations, updatedTime, updatedExitScore).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentrunId'>
            /// </param>
            /// <param name='updatedIterations'>
            /// </param>
            /// <param name='updatedTime'>
            /// </param>
            /// <param name='updatedExitScore'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ContinueRunAsync(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentrunId, int? updatedIterations = default(int?), int? updatedTime = default(int?), double? updatedExitScore = default(double?), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.ContinueRunWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentrunId, updatedIterations, updatedTime, updatedExitScore, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentrunId'>
            /// </param>
            /// <param name='updatedIterations'>
            /// </param>
            public static void ContinueRunLegacy(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentrunId, int updatedIterations)
            {
                operations.ContinueRunLegacyAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentrunId, updatedIterations).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentrunId'>
            /// </param>
            /// <param name='updatedIterations'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ContinueRunLegacyAsync(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentrunId, int updatedIterations, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.ContinueRunLegacyWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentrunId, updatedIterations, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='createParentRunDto'>
            /// </param>
            public static string CreateParentRun(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, CreateParentRunDto createParentRunDto = default(CreateParentRunDto))
            {
                return operations.CreateParentRunAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, createParentRunDto).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='createParentRunDto'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<string> CreateParentRunAsync(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, CreateParentRunDto createParentRunDto = default(CreateParentRunDto), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CreateParentRunWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, createParentRunDto, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentRunId'>
            /// </param>
            public static IterationTaskDto LocalRunGetNextTask(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentRunId)
            {
                return operations.LocalRunGetNextTaskAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentRunId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentRunId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IterationTaskDto> LocalRunGetNextTaskAsync(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentRunId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.LocalRunGetNextTaskWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentRunId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentRunId'>
            /// </param>
            /// <param name='workerId'>
            /// </param>
            public static PipelineDto GetPipeline(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentRunId, string workerId)
            {
                return operations.GetPipelineAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentRunId, workerId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentRunId'>
            /// </param>
            /// <param name='workerId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<PipelineDto> GetPipelineAsync(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentRunId, string workerId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetPipelineWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentRunId, workerId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentRunId'>
            /// </param>
            /// <param name='targetStatus'>
            /// </param>
            public static void ParentRunStatus(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentRunId, string targetStatus)
            {
                operations.ParentRunStatusAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentRunId, targetStatus).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='parentRunId'>
            /// </param>
            /// <param name='targetStatus'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ParentRunStatusAsync(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string parentRunId, string targetStatus, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.ParentRunStatusWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, parentRunId, targetStatus, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='runId'>
            /// </param>
            public static void CancelChildRun(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string runId)
            {
                operations.CancelChildRunAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, runId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// </param>
            /// <param name='resourceGroupName'>
            /// </param>
            /// <param name='workspaceName'>
            /// </param>
            /// <param name='experimentName'>
            /// </param>
            /// <param name='runId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task CancelChildRunAsync(this IJasmine operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string experimentName, string runId, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.CancelChildRunWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, experimentName, runId, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

    }
}