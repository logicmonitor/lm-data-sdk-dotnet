/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using LogicMonitor.DataSDK.Utils;

namespace LogicMonitor.DataSDK.Model
{
    /// <summary>
    /// This model is used to defining the DatasourceInstance object.
    /// </summary>
    [DataContract]
    public class DataSourceInstance
    {
        private readonly ObjectNameValidator objectNameValidator = new ObjectNameValidator();
        public DataSourceInstance()
        {

        }
        public DataSourceInstance(string name = null,string description = null, string displayName = null, Dictionary<string,string> properties = default(Dictionary<string, string>),int instanceId= default)
        {
            this.Description = description;
            this.DisplayName = displayName;
            this.Name = name;
            this.Properties = properties;
            InstanceId = instanceId;
            
        }

        /// <summary>
        /// Instance Description.
        /// </summary>
        [DataMember(Name = "Description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Instance display name. Only considered when creating a new instance.
        /// </summary>
        [DataMember(Name = "DisplayName", EmitDefaultValue = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Instance name. If no existing instance matches, a new instance is created with this name.
        /// </summary>
        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// New properties for instance. Updates to existing instance properties are not considered.
        /// Depending on the property name, we will convert these properties into system, auto, or custom properties.
        /// </summary>
        [DataMember(Name = "Properties", EmitDefaultValue = false)]
        public Dictionary<string, string> Properties { get; set; }

        /// <summary>
        /// Gets or Sets InstanceId
        /// </summary>
        [DataMember(Name = "instanceId", EmitDefaultValue = false)]
        public int InstanceId { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataPoint {\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            if (Properties.Count != 0)
            {
                foreach (var item in Properties)
                {
                    sb.Append("  Properties: ").Append(item.Key).Append(":").Append(item.Value).Append("\n");
                }
            }
            sb.Append("}\n");
            return sb.ToString();
        }

        

    }
}
