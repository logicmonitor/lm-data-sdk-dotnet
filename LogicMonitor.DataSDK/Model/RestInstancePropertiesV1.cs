/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.s
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
    /// RestInstancePropertiesV1
    /// </summary>
    [DataContract(Name = "RestInstancePropertiesV1")]
    public partial class RestInstancePropertiesV1
    {
        public RestInstancePropertiesV1()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RestInstancePropertiesV1" /> class.
        /// </summary>
        /// <param name="dataSource">dataSource.</param>
        /// <param name="dataSourceDisplayName">dataSourceDisplayName.</param>
        /// <param name="instanceId">instanceId.</param>
        /// <param name="instanceName">instanceName.</param>
        /// <param name="instanceProperties">instanceProperties.</param>
        /// <param name="resourceIds">resourceIds.</param>
        public RestInstancePropertiesV1(string dataSource , string dataSourceDisplayName = default(string), int instanceId = default(int), string instanceName = default(string), Dictionary<string, string> instanceProperties = default(Dictionary<string, string>), Dictionary<string, string> resourceIds = default(Dictionary<string, string>))
        {
            this.DataSource = dataSource;
            this.DataSourceDisplayName = dataSourceDisplayName;
            this.InstanceId = instanceId;
            this.InstanceName = instanceName;
            this.InstanceProperties = instanceProperties;
            this.ResourceIds = resourceIds;
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
        /// Gets or Sets ResourceIds
        /// </summary>
        [DataMember(Name = "resourceIds", EmitDefaultValue = false)]
        public Dictionary<string, string> ResourceIds { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RestInstancePropertiesV1 {\n");
            sb.Append("  DataSource: ").Append(DataSource).Append("\n");
            sb.Append("  DataSourceDisplayName: ").Append(DataSourceDisplayName).Append("\n");
            sb.Append("  InstanceId: ").Append(InstanceId).Append("\n");
            sb.Append("  InstanceName: ").Append(InstanceName).Append("\n");
            sb.Append("  InstanceProperties{\n");
            foreach (var item in InstanceProperties)
            {
                sb.Append("   " + item.Key).Append(": ").Append(item.Value).Append("\n");
            }
            sb.Append("  }\n"); sb.Append("  ResourceIds{\n");
            foreach (var item in ResourceIds)
            {
                sb.Append("   " + item.Key).Append(": ").Append(item.Value).Append("\n");
            }
            sb.Append("  }\n");
            sb.Append("}\n");
            return sb.ToString();
        }

    }

}
