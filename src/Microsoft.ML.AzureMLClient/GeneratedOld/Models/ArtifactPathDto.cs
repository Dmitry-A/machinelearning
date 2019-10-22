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

    public partial class ArtifactPathDto
    {
        /// <summary>
        /// Initializes a new instance of the ArtifactPathDto class.
        /// </summary>
        public ArtifactPathDto()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ArtifactPathDto class.
        /// </summary>
        public ArtifactPathDto(string path = default(string))
        {
            Path = path;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

    }
}
