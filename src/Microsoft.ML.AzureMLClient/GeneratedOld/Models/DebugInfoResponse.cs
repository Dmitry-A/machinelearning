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
    /// Internal debugging information not intended for external clients.
    /// </summary>
    public partial class DebugInfoResponse
    {
        /// <summary>
        /// Initializes a new instance of the DebugInfoResponse class.
        /// </summary>
        public DebugInfoResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the DebugInfoResponse class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="data">The data associated with the error.</param>
        /// <param name="errorResponse">The error response.</param>
        public DebugInfoResponse(string type = default(string), string message = default(string), string stackTrace = default(string), DebugInfoResponse innerException = default(DebugInfoResponse), IDictionary<string, object> data = default(IDictionary<string, object>), ErrorResponse errorResponse = default(ErrorResponse))
        {
            Type = type;
            Message = message;
            StackTrace = stackTrace;
            InnerException = innerException;
            Data = data;
            ErrorResponse = errorResponse;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        [JsonProperty(PropertyName = "stackTrace")]
        public string StackTrace { get; set; }

        /// <summary>
        /// Gets or sets the inner exception.
        /// </summary>
        [JsonProperty(PropertyName = "innerException")]
        public DebugInfoResponse InnerException { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the error.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public IDictionary<string, object> Data { get; set; }

        /// <summary>
        /// Gets or sets the error response.
        /// </summary>
        [JsonProperty(PropertyName = "errorResponse")]
        public ErrorResponse ErrorResponse { get; set; }

    }
}