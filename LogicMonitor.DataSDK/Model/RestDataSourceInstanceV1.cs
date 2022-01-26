/*
 * Copyright, 2022, LogicMonitor, Inc.
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
    /// RestDataSourceInstanceV1
    /// </summary>
    [DataContract(Name = "RestDataSourceInstanceV1")]
    public partial class RestDataSourceInstanceV1
    {
        public RestDataSourceInstanceV1()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RestDataSourceInstanceV1" /> class.
        /// </summary>
        /// <param name="dataPoints">dataPoints.</param>
        /// <param name="instanceDescription">instanceDescription.</param>
        /// <param name="instanceDisplayName">instanceDisplayName.</param>
        /// <param name="instanceGroup">instanceGroup.</param>
        /// <param name="instanceId">instanceId.</param>
        /// <param name="instanceName">instanceName.</param>
        /// <param name="instanceProperties">instanceProperties.</param>
        /// <param name="instanceWildValue">instanceWildValue.</param>
        public RestDataSourceInstanceV1(string instanceDisplayName ,List<RestDataPointV1> dataPoints = default(List<RestDataPointV1>), string instanceDescription = default(string),  string instanceGroup = default(string), int instanceId = default(int), string instanceName = default(string), Dictionary<string, string> instanceProperties = default(Dictionary<string, string>), string instanceWildValue = default(string))
        {
            this.DataPoints = dataPoints;
            this.InstanceDescription = instanceDescription;
            this.InstanceDisplayName = instanceDisplayName;
            this.InstanceGroup = instanceGroup;
            this.InstanceId = instanceId;
            this.InstanceName = instanceName;
            this.InstanceProperties = instanceProperties;
            this.InstanceWildValue = instanceWildValue;
        }

        /// <summary>
        /// Gets or Sets DataPoints
        /// </summary>
        [DataMember(Name = "dataPoints", EmitDefaultValue = false)]
        public List<RestDataPointV1> DataPoints { get; set; }

        /// <summary>
        /// Gets or Sets InstanceDescription
        /// </summary>
        [DataMember(Name = "instanceDescription", EmitDefaultValue = false)]
        public string InstanceDescription { get; set; }

        /// <summary>
        /// Gets or Sets InstanceDisplayName
        /// </summary>
        [DataMember(Name = "instanceDisplayName", EmitDefaultValue = false)]
        public string InstanceDisplayName { get; set; }

        /// <summary>
        /// Gets or Sets InstanceGroup
        /// </summary>
        [DataMember(Name = "instanceGroup", EmitDefaultValue = false)]
        public string InstanceGroup { get; set; }

        /// <summary>
        /// Gets or Sets InstanceId
        /// </summary>
        [DataMember(Name = "instanceId", EmitDefaultValue = false)]
        public int InstanceId { get; set; }

        /// <summary>
        /// Gets or Sets InstanceName
        /// </summary>
        [DataMember(Name = "instanceName", EmitDefaultValue = false)]
        public string InstanceName { get; set; }

        /// <summary>
        /// Gets or Sets InstanceProperties
        /// </summary>
        [DataMember(Name = "instanceProperties", EmitDefaultValue = false)]
        public Dictionary<string, string> InstanceProperties { get; set; }

        /// <summary>
        /// Gets or Sets InstanceWildValue
        /// </summary>
        [DataMember(Name = "instanceWildValue", EmitDefaultValue = false)]
        public string InstanceWildValue { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RestDataSourceInstanceV1 {\n");
            sb.Append("  DataPoints{\n ");
            foreach (var item in DataPoints)
            {
                if(item != null)
                sb.Append(item.ToString()).Append("\n");
            }
            sb.Append("  }\n");
            sb.Append("  InstanceDescription: ").Append(InstanceDescription).Append("\n");
            sb.Append("  InstanceDisplayName: ").Append(InstanceDisplayName).Append("\n");
            sb.Append("  InstanceGroup: ").Append(InstanceGroup).Append("\n");
            sb.Append("  InstanceId: ").Append(InstanceId).Append("\n");
            sb.Append("  InstanceName: ").Append(InstanceName).Append("\n");
            sb.Append("  InstanceProperties{\n ");
            foreach (var item in InstanceProperties)
            {
                sb.Append("   "+item.Key).Append(":").Append(item.Value).Append("\n");
            }
            sb.Append("  }\n");
            sb.Append("  InstanceWildValue: ").Append(InstanceWildValue).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

    }

}
