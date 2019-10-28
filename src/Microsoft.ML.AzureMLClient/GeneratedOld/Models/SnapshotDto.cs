// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator 1.0.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Azure.MachineLearning.Services.GeneratedOld.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class SnapshotDto
    {
        /// <summary>
        /// Initializes a new instance of the SnapshotDto class.
        /// </summary>
        public SnapshotDto()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the SnapshotDto class.
        /// </summary>
        public SnapshotDto(System.Guid? id = default(System.Guid?), DirTreeNode root = default(DirTreeNode))
        {
            Id = id;
            Root = root;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public System.Guid? Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "root")]
        public DirTreeNode Root { get; set; }

    }
}