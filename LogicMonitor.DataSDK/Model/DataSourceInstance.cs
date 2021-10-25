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
            return this.Equals(input as DataSourceInstance);
        }

        public bool Equals(DataSourceInstance input)
        {
            if (input == null)
                return false;

            return
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description)
                    )
                ) &&
                (
                    this.DisplayName == input.DisplayName ||
                    (this.DisplayName != null &&
                    this.DisplayName.Equals(input.DisplayName)
                    )
                ) &&
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) &&
                (
                    this.Properties == input.Properties ||
                    (this.Properties != null &&
                    this.Properties.Equals(input.Properties)
                    )
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
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                if (this.DisplayName != null)
                    hashCode = hashCode * 59 + this.DisplayName.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.Properties != null)
                    hashCode = hashCode * 59 + this.Properties.GetHashCode();
                return hashCode;
            }
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
