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
    /// A paginated list of objects.
    /// </summary>
    public partial class PaginatedServiceList
    {
        /// <summary>
        /// Initializes a new instance of the PaginatedServiceList class.
        /// </summary>
        public PaginatedServiceList()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the PaginatedServiceList class.
        /// </summary>
        /// <param name="nextLink">A continuation link (absolute URI) to the
        /// next page of results in the list.</param>
        /// <param name="value">An array of objects of type T.</param>
        public PaginatedServiceList(string nextLink = default(string), IList<ServiceResponseBase> value = default(IList<ServiceResponseBase>))
        {
            NextLink = nextLink;
            Value = value;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets a continuation link (absolute URI) to the next page of
        /// results in the list.
        /// </summary>
        [JsonProperty(PropertyName = "nextLink")]
        public string NextLink { get; set; }

        /// <summary>
        /// Gets or sets an array of objects of type T.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public IList<ServiceResponseBase> Value { get; set; }

    }
}