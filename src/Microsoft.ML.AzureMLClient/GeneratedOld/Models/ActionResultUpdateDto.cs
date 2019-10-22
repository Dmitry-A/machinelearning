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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class ActionResultUpdateDto
    {
        /// <summary>
        /// Initializes a new instance of the ActionResultUpdateDto class.
        /// </summary>
        public ActionResultUpdateDto()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ActionResultUpdateDto class.
        /// </summary>
        public ActionResultUpdateDto(IList<string> resultArtifactIds = default(IList<string>), string targetDataHash = default(string))
        {
            ResultArtifactIds = resultArtifactIds;
            TargetDataHash = targetDataHash;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "resultArtifactIds")]
        public IList<string> ResultArtifactIds { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "targetDataHash")]
        public string TargetDataHash { get; set; }

    }
}
