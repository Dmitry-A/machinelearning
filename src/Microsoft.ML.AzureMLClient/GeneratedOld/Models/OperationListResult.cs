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
    /// An array of operations supported by the resource provider.
    /// </summary>
    public partial class OperationListResult
    {
        /// <summary>
        /// Initializes a new instance of the OperationListResult class.
        /// </summary>
        public OperationListResult()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the OperationListResult class.
        /// </summary>
        /// <param name="value">List of AML workspace operations supported by
        /// the AML workspace resource provider.</param>
        public OperationListResult(IList<Operation> value = default(IList<Operation>))
        {
            Value = value;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets list of AML workspace operations supported by the AML
        /// workspace resource provider.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public IList<Operation> Value { get; set; }

    }
}
