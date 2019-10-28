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
    /// Secrets related to a Machine Learning compute based on AKS.
    /// </summary>
    [Newtonsoft.Json.JsonObject("VirtualMachine")]
    public partial class VirtualMachineSecrets : ComputeSecrets
    {
        /// <summary>
        /// Initializes a new instance of the VirtualMachineSecrets class.
        /// </summary>
        public VirtualMachineSecrets()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the VirtualMachineSecrets class.
        /// </summary>
        /// <param name="administratorAccount">Admin credentials for virtual
        /// machine.</param>
        public VirtualMachineSecrets(VirtualMachineSshCredentials administratorAccount = default(VirtualMachineSshCredentials))
        {
            AdministratorAccount = administratorAccount;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets admin credentials for virtual machine.
        /// </summary>
        [JsonProperty(PropertyName = "administratorAccount")]
        public VirtualMachineSshCredentials AdministratorAccount { get; set; }

    }
}