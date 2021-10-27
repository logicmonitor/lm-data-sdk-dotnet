/*
 * Copyright, 2021, LogicMonitor, Inc.
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
        ObjectNameValidator objectNameValidator = new ObjectNameValidator();
        public DataSourceInstance(string description = default(string), string displayName = default(string), string name = default(string), Dictionary<string,string> properties = default(Dictionary<string, string>))
        {
            this.Description = description;
            this.DisplayName = displayName;
            this.Name = name;
            this.Properties = properties;
            string errorMsg = ValidField();
            if (errorMsg != null && errorMsg.Length > 0)
                throw new ArgumentException(errorMsg);
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataPoint {\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Properties: ").Append(Properties).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        public string ValidField()
        {
            string errorMsg = "";
            errorMsg += objectNameValidator.CheckInstanceNameValidation(Name);
            errorMsg += objectNameValidator.CheckInstanceDisplayNameValidation(DisplayName);
            errorMsg += objectNameValidator.CheckInstancePropertiesValidation(Properties);
            return errorMsg;
        }

    }
}
