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
    /// Wrapper for error response to follow ARM guidelines.
    /// </summary>
    public partial class MachineLearningServiceError
    {
        /// <summary>
        /// Initializes a new instance of the MachineLearningServiceError
        /// class.
        /// </summary>
        public MachineLearningServiceError()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MachineLearningServiceError
        /// class.
        /// </summary>
        /// <param name="error">The error response.</param>
        public MachineLearningServiceError(MLCErrorResponse error = default(MLCErrorResponse))
        {
            Error = error;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets the error response.
        /// </summary>
        [JsonProperty(PropertyName = "error")]
        public MLCErrorResponse Error { get; private set; }

    }
}