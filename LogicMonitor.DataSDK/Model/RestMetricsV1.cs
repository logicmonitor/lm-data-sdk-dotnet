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
    /// RestMetricsV1
    /// </summary>
    [DataContract(Name = "RestMetricsV1")]
    public partial class RestMetricsV1 : IEquatable<RestMetricsV1>, IValidatableObject
    {

        public RestMetricsV1()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RestMetricsV1" /> class.
        /// </summary>
        /// <param name="dataSource">dataSource.</param>
        /// <param name="dataSourceDisplayName">dataSourceDisplayName.</param>
        /// <param name="dataSourceGroup">dataSourceGroup.</param>
        /// <param name="dataSourceId">dataSourceId.</param>
        /// <param name="instances">instances.</param>
        /// <param name="resourceDescription">resourceDescription.</param>
        /// <param name="resourceIds">resourceIds.</param>
        /// <param name="resourceName">resourceName.</param>
        /// <param name="resourceProperties">resourceProperties.</param>
        public RestMetricsV1(string dataSource = default(string), string dataSourceDisplayName = default(string), string dataSourceGroup = default(string), int dataSourceId = default(int), List<RestDataSourceInstanceV1> instances = default(List<RestDataSourceInstanceV1>), string resourceDescription = default(string), Dictionary<string, string> resourceIds = default(Dictionary<string, string>), string resourceName = default(string), Dictionary<string, string> resourceProperties = default(Dictionary<string, string>))
        {
            this.DataSource = dataSource;
            this.DataSourceDisplayName = dataSourceDisplayName;
            this.DataSourceGroup = dataSourceGroup;
            this.DataSourceId = dataSourceId;
            this.Instances = instances;
            this.ResourceDescription = resourceDescription;
            this.ResourceIds = resourceIds;
            this.ResourceName = resourceName;
            this.ResourceProperties = resourceProperties;
        }

        /// <summary>
        /// Gets or Sets DataSource
        /// </summary>
        [DataMember(Name = "dataSource", EmitDefaultValue = false)]
        public string DataSource { get; set; }

        /// <summary>
        /// Gets or Sets DataSourceDisplayName
        /// </summary>
        [DataMember(Name = "dataSourceDisplayName", EmitDefaultValue = false)]
        public string DataSourceDisplayName { get; set; }

        /// <summary>
        /// Gets or Sets DataSourceGroup
        /// </summary>
        [DataMember(Name = "dataSourceGroup", EmitDefaultValue = false)]
        public string DataSourceGroup { get; set; }

        /// <summary>
        /// Gets or Sets DataSourceId
        /// </summary>
        [DataMember(Name = "dataSourceId", EmitDefaultValue = false)]
        public int DataSourceId { get; set; }

        /// <summary>
        /// Gets or Sets Instances
        /// </summary>
        [DataMember(Name = "instances", EmitDefaultValue = false)]
        public List<RestDataSourceInstanceV1> Instances { get; set; }

        /// <summary>
        /// Gets or Sets ResourceDescription
        /// </summary>
        [DataMember(Name = "resourceDescription", EmitDefaultValue = false)]
        public string ResourceDescription { get; set; }

        /// <summary>
        /// Gets or Sets ResourceIds
        /// </summary>
        [DataMember(Name = "resourceIds", EmitDefaultValue = false)]
        public Dictionary<string, string> ResourceIds { get; set; }

        /// <summary>
        /// Gets or Sets ResourceName
        /// </summary>
        [DataMember(Name = "resourceName", EmitDefaultValue = false)]
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or Sets ResourceProperties
        /// </summary>
        [DataMember(Name = "resourceProperties", EmitDefaultValue = false)]
        public Dictionary<string, string> ResourceProperties { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RestMetricsV1 {\n");
            sb.Append("  DataSource: ").Append(DataSource).Append("\n");
            sb.Append("  DataSourceDisplayName: ").Append(DataSourceDisplayName).Append("\n");
            sb.Append("  DataSourceGroup: ").Append(DataSourceGroup).Append("\n");
            sb.Append("  DataSourceId: ").Append(DataSourceId).Append("\n");
            sb.Append("  Instances: ").Append(Instances).Append("\n");
            sb.Append("  ResourceDescription: ").Append(ResourceDescription).Append("\n");
            sb.Append("  ResourceIds: ").Append(ResourceIds).Append("\n");
            sb.Append("  ResourceName: ").Append(ResourceName).Append("\n");
            sb.Append("  ResourceProperties: ").Append(ResourceProperties).Append("\n");
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
            return this.Equals(input as RestMetricsV1);
        }

        /// <summary>
        /// Returns true if RestMetricsV1 instances are equal
        /// </summary>
        /// <param name="input">Instance of RestMetricsV1 to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RestMetricsV1 input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.DataSource == input.DataSource ||
                    (this.DataSource != null &&
                    this.DataSource.Equals(input.DataSource))
                ) && 
                (
                    this.DataSourceDisplayName == input.DataSourceDisplayName ||
                    (this.DataSourceDisplayName != null &&
                    this.DataSourceDisplayName.Equals(input.DataSourceDisplayName))
                ) && 
                (
                    this.DataSourceGroup == input.DataSourceGroup ||
                    (this.DataSourceGroup != null &&
                    this.DataSourceGroup.Equals(input.DataSourceGroup))
                ) && 
                (
                    this.DataSourceId == input.DataSourceId ||
                    this.DataSourceId.Equals(input.DataSourceId)
                ) && 
                (
                    this.Instances == input.Instances ||
                    this.Instances != null &&
                    input.Instances != null &&
                    this.Instances.SequenceEqual(input.Instances)
                ) && 
                (
                    this.ResourceDescription == input.ResourceDescription ||
                    (this.ResourceDescription != null &&
                    this.ResourceDescription.Equals(input.ResourceDescription))
                ) && 
                (
                    this.ResourceIds == input.ResourceIds ||
                    this.ResourceIds != null &&
                    input.ResourceIds != null &&
                    this.ResourceIds.SequenceEqual(input.ResourceIds)
                ) && 
                (
                    this.ResourceName == input.ResourceName ||
                    (this.ResourceName != null &&
                    this.ResourceName.Equals(input.ResourceName))
                ) && 
                (
                    this.ResourceProperties == input.ResourceProperties ||
                    this.ResourceProperties != null &&
                    input.ResourceProperties != null &&
                    this.ResourceProperties.SequenceEqual(input.ResourceProperties)
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
                if (this.DataSource != null)
                    hashCode = hashCode * 59 + this.DataSource.GetHashCode();
                if (this.DataSourceDisplayName != null)
                    hashCode = hashCode * 59 + this.DataSourceDisplayName.GetHashCode();
                if (this.DataSourceGroup != null)
                    hashCode = hashCode * 59 + this.DataSourceGroup.GetHashCode();
                hashCode = hashCode * 59 + this.DataSourceId.GetHashCode();
                if (this.Instances != null)
                    hashCode = hashCode * 59 + this.Instances.GetHashCode();
                if (this.ResourceDescription != null)
                    hashCode = hashCode * 59 + this.ResourceDescription.GetHashCode();
                if (this.ResourceIds != null)
                    hashCode = hashCode * 59 + this.ResourceIds.GetHashCode();
                if (this.ResourceName != null)
                    hashCode = hashCode * 59 + this.ResourceName.GetHashCode();
                if (this.ResourceProperties != null)
                    hashCode = hashCode * 59 + this.ResourceProperties.GetHashCode();
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
