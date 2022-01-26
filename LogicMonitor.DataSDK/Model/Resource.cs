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
    /// This Model is used to identify a resource.
    /// </summary>
    [DataContract]
    public class Resource
    {
        private readonly ObjectNameValidator objectNameValidator = new ObjectNameValidator();
        public Resource()
        {

        }
        public Resource(Dictionary<string,string> ids = default(Dictionary<string,string>), string name = default(string), string description = null, bool create = false)
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
            sb.Append("  Ids{\n");
            foreach (var item in Ids)
            {
                sb.Append("   "+item.Key).Append(":").Append(item.Value).Append("\n");
            }
            sb.Append("  }\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Properties: ").Append(Properties).Append("\n");
            sb.Append("  Create: ").Append(Create).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        public string ValidField()
        {
            string errorMsg = "";
            if(Name!=null)
            errorMsg += objectNameValidator.CheckResourceNameValidation(Create,Name);
            if (Description != null)
                errorMsg += objectNameValidator.CheckResourceNamDescriptionValidation(Description);
            errorMsg += objectNameValidator.CheckResourceIdsValidation(Ids);
            if(Properties != null)
                errorMsg += objectNameValidator.CheckResourcePropertiesValidation(Properties);
            return errorMsg;
        }


    }
}
