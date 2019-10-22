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

    /// <summary>
    /// Secrets related to a Machine Learning compute based on Databricks.
    /// </summary>
    [Newtonsoft.Json.JsonObject("Databricks")]
    public partial class DatabricksComputeSecrets : ComputeSecrets
    {
        /// <summary>
        /// Initializes a new instance of the DatabricksComputeSecrets class.
        /// </summary>
        public DatabricksComputeSecrets()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the DatabricksComputeSecrets class.
        /// </summary>
        /// <param name="databricksAccessToken">access token for databricks
        /// account.</param>
        public DatabricksComputeSecrets(string databricksAccessToken = default(string))
        {
            DatabricksAccessToken = databricksAccessToken;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets access token for databricks account.
        /// </summary>
        [JsonProperty(PropertyName = "databricksAccessToken")]
        public string DatabricksAccessToken { get; set; }

    }
}
