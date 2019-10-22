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
    /// Extension methods for Snapshot.
    /// </summary>
    public static partial class SnapshotExtensions
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            public static string GetSnapshotFilesZipSas(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), string accountName = default(string))
            {
                return operations.GetSnapshotFilesZipSasAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, accountName).GetAwaiter().GetResult();
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<string> GetSnapshotFilesZipSasAsync(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), string accountName = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetSnapshotFilesZipSasWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, accountName, null, cancellationToken).ConfigureAwait(false))
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='parentSnapshotId'>
            /// </param>
            /// <param name='files'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            public static void CreateSnapshot(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), System.Guid? parentSnapshotId = default(System.Guid?), IList<object> files = default(IList<object>), string accountName = default(string))
            {
                operations.CreateSnapshotAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, parentSnapshotId, files, accountName).GetAwaiter().GetResult();
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='parentSnapshotId'>
            /// </param>
            /// <param name='files'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task CreateSnapshotAsync(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), System.Guid? parentSnapshotId = default(System.Guid?), IList<object> files = default(IList<object>), string accountName = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.CreateSnapshotWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, parentSnapshotId, files, accountName, null, cancellationToken).ConfigureAwait(false)).Dispose();
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='deleteLatestSnapshotPointer'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            public static void DeleteSnapshotAndLastestPointer(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), bool? deleteLatestSnapshotPointer = default(bool?), string accountName = default(string))
            {
                operations.DeleteSnapshotAndLastestPointerAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, deleteLatestSnapshotPointer, accountName).GetAwaiter().GetResult();
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='deleteLatestSnapshotPointer'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteSnapshotAndLastestPointerAsync(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), bool? deleteLatestSnapshotPointer = default(bool?), string accountName = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.DeleteSnapshotAndLastestPointerWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, deleteLatestSnapshotPointer, accountName, null, cancellationToken).ConfigureAwait(false)).Dispose();
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            public static string GetSnapshotFilesZipLocation(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), string accountName = default(string))
            {
                return operations.GetSnapshotFilesZipLocationAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, accountName).GetAwaiter().GetResult();
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<string> GetSnapshotFilesZipLocationAsync(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), string accountName = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetSnapshotFilesZipLocationWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, accountName, null, cancellationToken).ConfigureAwait(false))
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            public static DirTreeNode GetSasUrls(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), string accountName = default(string))
            {
                return operations.GetSasUrlsAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, accountName).GetAwaiter().GetResult();
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<DirTreeNode> GetSasUrlsAsync(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), string accountName = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetSasUrlsWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, accountName, null, cancellationToken).ConfigureAwait(false))
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            public static DirTreeNode GetStorageUrls(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), string accountName = default(string))
            {
                return operations.GetStorageUrlsAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, accountName).GetAwaiter().GetResult();
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<DirTreeNode> GetStorageUrlsAsync(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), string accountName = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetStorageUrlsWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, accountName, null, cancellationToken).ConfigureAwait(false))
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
            /// <param name='projectName'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            public static SnapshotDto GetLatestSnapshot(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string projectName = default(string), string accountName = default(string))
            {
                return operations.GetLatestSnapshotAsync(subscriptionId, resourceGroupName, workspaceName, projectName, accountName).GetAwaiter().GetResult();
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
            /// <param name='projectName'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<SnapshotDto> GetLatestSnapshotAsync(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string projectName = default(string), string accountName = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetLatestSnapshotWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, projectName, accountName, null, cancellationToken).ConfigureAwait(false))
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
            /// <param name='fileList'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='parentSnapshotId'>
            /// </param>
            public static IList<MerkleDiffEntry> SnapshotDiffConstruction(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, FlatDirTreeNodeListDto fileList, string projectName = default(string), System.Guid? parentSnapshotId = default(System.Guid?))
            {
                return operations.SnapshotDiffConstructionAsync(subscriptionId, resourceGroupName, workspaceName, fileList, projectName, parentSnapshotId).GetAwaiter().GetResult();
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
            /// <param name='fileList'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='parentSnapshotId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<MerkleDiffEntry>> SnapshotDiffConstructionAsync(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, FlatDirTreeNodeListDto fileList, string projectName = default(string), System.Guid? parentSnapshotId = default(System.Guid?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SnapshotDiffConstructionWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, fileList, projectName, parentSnapshotId, null, cancellationToken).ConfigureAwait(false))
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
            /// <param name='projectName'>
            /// </param>
            /// <param name='snapshotId1'>
            /// </param>
            /// <param name='snapshotId2'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            public static IList<MerkleDiffEntry> GetSnapshotDiff(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string projectName = default(string), System.Guid? snapshotId1 = default(System.Guid?), System.Guid? snapshotId2 = default(System.Guid?), string accountName = default(string))
            {
                return operations.GetSnapshotDiffAsync(subscriptionId, resourceGroupName, workspaceName, projectName, snapshotId1, snapshotId2, accountName).GetAwaiter().GetResult();
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
            /// <param name='projectName'>
            /// </param>
            /// <param name='snapshotId1'>
            /// </param>
            /// <param name='snapshotId2'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<MerkleDiffEntry>> GetSnapshotDiffAsync(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, string projectName = default(string), System.Guid? snapshotId1 = default(System.Guid?), System.Guid? snapshotId2 = default(System.Guid?), string accountName = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetSnapshotDiffWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, projectName, snapshotId1, snapshotId2, accountName, null, cancellationToken).ConfigureAwait(false))
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            public static SnapshotDto GetSnapshotMetadata(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), string accountName = default(string))
            {
                return operations.GetSnapshotMetadataAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, accountName).GetAwaiter().GetResult();
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
            /// <param name='snapshotId'>
            /// </param>
            /// <param name='projectName'>
            /// </param>
            /// <param name='accountName'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<SnapshotDto> GetSnapshotMetadataAsync(this ISnapshot operations, System.Guid subscriptionId, string resourceGroupName, string workspaceName, System.Guid snapshotId, string projectName = default(string), string accountName = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetSnapshotMetadataWithHttpMessagesAsync(subscriptionId, resourceGroupName, workspaceName, snapshotId, projectName, accountName, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// alive
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static void Ping(this ISnapshot operations)
            {
                operations.PingAsync().GetAwaiter().GetResult();
            }

            /// <summary>
            /// alive
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task PingAsync(this ISnapshot operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.PingWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// alive
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static void Alive(this ISnapshot operations)
            {
                operations.AliveAsync().GetAwaiter().GetResult();
            }

            /// <summary>
            /// alive
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task AliveAsync(this ISnapshot operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.AliveWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// version
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static MetaApiVersionResponse GetServiceVersionMetadata(this ISnapshot operations)
            {
                return operations.GetServiceVersionMetadataAsync().GetAwaiter().GetResult();
            }

            /// <summary>
            /// version
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<MetaApiVersionResponse> GetServiceVersionMetadataAsync(this ISnapshot operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetServiceVersionMetadataWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
