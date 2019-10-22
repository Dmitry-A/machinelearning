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

    /// <summary>
    /// Error response information.
    /// </summary>
    public partial class MLCErrorResponse
    {
        /// <summary>
        /// Initializes a new instance of the MLCErrorResponse class.
        /// </summary>
        public MLCErrorResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MLCErrorResponse class.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <param name="message">Error message.</param>
        /// <param name="details">An array of error detail objects.</param>
        public MLCErrorResponse(string code = default(string), string message = default(string), IList<MLCErrorDetail> details = default(IList<MLCErrorDetail>))
        {
            Code = code;
            Message = message;
            Details = details;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets error code.
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; private set; }

        /// <summary>
        /// Gets error message.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; private set; }

        /// <summary>
        /// Gets an array of error detail objects.
        /// </summary>
        [JsonProperty(PropertyName = "details")]
        public IList<MLCErrorDetail> Details { get; private set; }

    }
}
