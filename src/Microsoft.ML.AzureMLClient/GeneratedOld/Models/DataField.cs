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

    public partial class DataField
    {
        /// <summary>
        /// Initializes a new instance of the DataField class.
        /// </summary>
        public DataField()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the DataField class.
        /// </summary>
        /// <param name="type">Possible values include: 'String', 'Boolean',
        /// 'Integer', 'Decimal', 'Date', 'Unknown', 'Error', 'Null',
        /// 'DataRow', 'List', 'Stream'</param>
        public DataField(string type = default(string), object value = default(object), string warningCode = default(string))
        {
            Type = type;
            Value = value;
            WarningCode = warningCode;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets possible values include: 'String', 'Boolean', 'Integer',
        /// 'Decimal', 'Date', 'Unknown', 'Error', 'Null', 'DataRow', 'List',
        /// 'Stream'
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public object Value { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "warningCode")]
        public string WarningCode { get; private set; }

    }
}