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
    /// RestMetricsV1
    /// </summary>
    [DataContract(Name = "RestMetricsV1")]
    public partial class RestMetricsV1 
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
        public RestMetricsV1(string dataSource , string dataSourceDisplayName , string dataSourceGroup = default(string), int dataSourceId = default(int), List<RestDataSourceInstanceV1> instances = default(List<RestDataSourceInstanceV1>), string resourceDescription = default(string), Dictionary<string, string> resourceIds = default(Dictionary<string, string>), string resourceName = default(string), Dictionary<string, string> resourceProperties = default(Dictionary<string, string>))
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
            sb.Append("  Instances{ ");
            foreach (var item in Instances)
            {
                if (item != null)
                    sb.Append(item.ToString());
            }
            sb.Append("}\n");
            sb.Append("  ResourceDescription: ").Append(ResourceDescription).Append("\n");
            sb.Append("  ResourceIds{\n");
            foreach (var item in ResourceIds)
            {
                sb.Append("   "+item.Key).Append(": ").Append(item.Value).Append("\n");
            }
            sb.Append("  }\n");
            sb.Append("  ResourceName: ").Append(ResourceName).Append("\n");
            sb.Append("  ResourceProperties{\n");
            foreach (var item in ResourceProperties)
            {
                sb.Append("   "+item.Key).Append(": ").Append(item.Value).Append("\n");
            }
            sb.Append("  }\n"); sb.Append("}\n");
            return sb.ToString();
        }
    }

}
