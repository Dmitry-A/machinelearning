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

    /// <summary>
    /// Defines values for AllocationState.
    /// </summary>
    /// <summary>
    /// Determine base value for a given allowed value if exists, else return
    /// the value itself
    /// </summary>
    [JsonConverter(typeof(AllocationStateConverter))]
    public struct AllocationState : System.IEquatable<AllocationState>
    {
        private AllocationState(string underlyingValue)
        {
            UnderlyingValue=underlyingValue;
        }

        public static readonly AllocationState Steady = "Steady";

        public static readonly AllocationState Resizing = "Resizing";


        /// <summary>
        /// Underlying value of enum AllocationState
        /// </summary>
        private readonly string UnderlyingValue;

        /// <summary>
        /// Returns string representation for AllocationState
        /// </summary>
        public override string ToString()
        {
            return UnderlyingValue == null ? null : UnderlyingValue.ToString();
        }

        /// <summary>
        /// Compares enums of type AllocationState
        /// </summary>
        public bool Equals(AllocationState e)
        {
            return UnderlyingValue.Equals(e.UnderlyingValue);
        }

        /// <summary>
        /// Implicit operator to convert string to AllocationState
        /// </summary>
        public static implicit operator AllocationState(string value)
        {
            return new AllocationState(value);
        }

        /// <summary>
        /// Implicit operator to convert AllocationState to string
        /// </summary>
        public static implicit operator string(AllocationState e)
        {
            return e.UnderlyingValue;
        }

        /// <summary>
        /// Overriding == operator for enum AllocationState
        /// </summary>
        public static bool operator == (AllocationState e1, AllocationState e2)
        {
            return e2.Equals(e1);
        }

        /// <summary>
        /// Overriding != operator for enum AllocationState
        /// </summary>
        public static bool operator != (AllocationState e1, AllocationState e2)
        {
            return !e2.Equals(e1);
        }

        /// <summary>
        /// Overrides Equals operator for AllocationState
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is AllocationState && Equals((AllocationState)obj);
        }

        /// <summary>
        /// Returns for hashCode AllocationState
        /// </summary>
        public override int GetHashCode()
        {
            return UnderlyingValue.GetHashCode();
        }

    }
}