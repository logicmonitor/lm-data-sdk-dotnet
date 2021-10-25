/*
 * Copyright, 2021, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LogicMonitor.DataSDK.Model
{
    /// <summary>
    /// RestDataPointV1
    /// </summary>
    [DataContract(Name = "RestDataPointV1")]
    public partial class RestDataPointV1 : IEquatable<RestDataPointV1>, IValidatableObject
    {
        public RestDataPointV1()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RestDataPointV1" /> class.
        /// </summary>
        /// <param name="dataPointAggregationType">dataPointAggregationType.</param>
        /// <param name="dataPointDescription">dataPointDescription.</param>
        /// <param name="dataPointName">dataPointName.</param>
        /// <param name="dataPointType">dataPointType.</param>
        /// <param name="values">values.</param>
        public RestDataPointV1(string dataPointAggregationType = default(string), string dataPointDescription = default(string), string dataPointName = default(string), string dataPointType = default(string), Dictionary<string, string> values = default(Dictionary<string, string>))
        {
            this.DataPointAggregationType = dataPointAggregationType;
            this.DataPointDescription = dataPointDescription;
            this.DataPointName = dataPointName;
            this.DataPointType = dataPointType;
            this.Values = values;
        }

        /// <summary>
        /// Gets or Sets DataPointAggregationType
        /// </summary>
        [DataMember(Name = "dataPointAggregationType", EmitDefaultValue = false)]
        public string DataPointAggregationType { get; set; }

        /// <summary>
        /// Gets or Sets DataPointDescription
        /// </summary>
        [DataMember(Name = "dataPointDescription", EmitDefaultValue = false)]
        public string DataPointDescription { get; set; }

        /// <summary>
        /// Gets or Sets DataPointName
        /// </summary>
        [DataMember(Name = "dataPointName", EmitDefaultValue = false)]
        public string DataPointName { get; set; }

        /// <summary>
        /// Gets or Sets DataPointType
        /// </summary>
        [DataMember(Name = "dataPointType", EmitDefaultValue = false)]
        public string DataPointType { get; set; }

        /// <summary>
        /// Gets or Sets Values
        /// </summary>
        [DataMember(Name = "values", EmitDefaultValue = false)]
        public Dictionary<string, string> Values { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RestDataPointV1 {\n");
            sb.Append("  DataPointAggregationType: ").Append(DataPointAggregationType).Append("\n");
            sb.Append("  DataPointDescription: ").Append(DataPointDescription).Append("\n");
            sb.Append("  DataPointName: ").Append(DataPointName).Append("\n");
            sb.Append("  DataPointType: ").Append(DataPointType).Append("\n");
            sb.Append("  Values: ").Append(Values).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as RestDataPointV1);
        }

        /// <summary>
        /// Returns true if RestDataPointV1 instances are equal
        /// </summary>
        /// <param name="input">Instance of RestDataPointV1 to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RestDataPointV1 input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.DataPointAggregationType == input.DataPointAggregationType ||
                    (this.DataPointAggregationType != null &&
                    this.DataPointAggregationType.Equals(input.DataPointAggregationType))
                ) && 
                (
                    this.DataPointDescription == input.DataPointDescription ||
                    (this.DataPointDescription != null &&
                    this.DataPointDescription.Equals(input.DataPointDescription))
                ) && 
                (
                    this.DataPointName == input.DataPointName ||
                    (this.DataPointName != null &&
                    this.DataPointName.Equals(input.DataPointName))
                ) && 
                (
                    this.DataPointType == input.DataPointType ||
                    (this.DataPointType != null &&
                    this.DataPointType.Equals(input.DataPointType))
                ) && 
                (
                    this.Values == input.Values ||
                    this.Values != null &&
                    input.Values != null &&
                    this.Values.SequenceEqual(input.Values)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.DataPointAggregationType != null)
                    hashCode = hashCode * 59 + this.DataPointAggregationType.GetHashCode();
                if (this.DataPointDescription != null)
                    hashCode = hashCode * 59 + this.DataPointDescription.GetHashCode();
                if (this.DataPointName != null)
                    hashCode = hashCode * 59 + this.DataPointName.GetHashCode();
                if (this.DataPointType != null)
                    hashCode = hashCode * 59 + this.DataPointType.GetHashCode();
                if (this.Values != null)
                    hashCode = hashCode * 59 + this.Values.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
