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
    /// This Model is used to identify a resource.
    /// </summary>
    [DataContract]
    public class Resource
    {
        ObjectNameValidator objectNameValidator = new ObjectNameValidator();
        public Resource(Dictionary<string,string> ids = default(Dictionary<string,string>), string name = default(string), string description = default(string), bool create = default(bool))
        {
            this.Ids = ids;
            this.Name = name;
            this.Description = description;
            this.Create = create;
            string errorMsg = ValidField();
            if (errorMsg != null && errorMsg.Length > 0)
                throw new ArgumentException(errorMsg);
        }
        /// <summary>
        /// An array of existing resource properties that will be used to identify the resource. See Managing Resources that Ingest Push Metrics for information on
        /// the types of properties that can be used. If no resource is matched and the create parameter is set to TRUE, a new resource is created with these
        /// specified resource IDs set on it. If the system.displayname and/or system.hostname property is included as resource IDs, they will be used as host
        /// name and display name respectively in the resulting resource.
        /// </summary>
        [DataMember(Name = "Ids", EmitDefaultValue = false)]
        public Dictionary<string, string> Ids { get; set; }

        /// <summary>
        /// Resource unique name. Only considered when creating a new resource.
        /// </summary>
        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///  Resource description. Only considered when creating a new resource.
        /// </summary>
        [DataMember(Name = "Description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// New properties for resource. Updates to existing resource properties are not considered. Depending on the property name, we will convert these properties
        /// into system, auto, or custom properties.
        /// </summary>
        [DataMember(Name = "Properties", EmitDefaultValue = false)]
        public Dictionary<string, string> Properties { get; set; }

        /// <summary>
        /// Do you want to create the resource. 
        /// </summary>
        [DataMember(Name = "Create", EmitDefaultValue = false)]
        public bool Create { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataPoint {\n");
            sb.Append("  Ids: ").Append(Ids).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Properties: ").Append(Properties).Append("\n");
            sb.Append("  Create: ").Append(Create).Append("\n");
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
            return this.Equals(input as Resource);
        }

        public bool Equals(Resource input)
        {
            if (input == null)
                return false;

            return
                (
                    this.Ids == input.Ids ||
                    (this.Ids != null &&
                    this.Ids.Equals(input.Ids)
                    )
                ) &&
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name)
                    )
                 ) &&
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description)
                    )
                ) &&
                (
                    this.Properties == input.Properties ||
                    (this.Properties != null &&
                    this.Properties.Equals(input.Properties))
                ) &&
                (
                    this.Create == input.Create ||
                    this.Create.Equals(input.Create)
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
                if (this.Ids != null)
                    hashCode = hashCode * 59 + this.Ids.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                if (this.Properties != null)
                    hashCode = hashCode * 59 + this.Properties.GetHashCode();
                return hashCode;
            }
        }

        public string ValidField()
        {
            string errorMsg = "";
            errorMsg += objectNameValidator.CheckResourceNameValidation(Create,Name);
            errorMsg += objectNameValidator.CheckResourceNamDescriptionValidation(Description);
            errorMsg += objectNameValidator.CheckResourceIdsValidation(Ids);
            errorMsg += objectNameValidator.CheckResourcePropertiesValidation(Properties);
            return errorMsg;
        }


    }
}
