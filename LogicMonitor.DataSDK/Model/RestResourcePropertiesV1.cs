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
    /// RestResourcePropertiesV1
    /// </summary>
    [DataContract(Name = "RestResourcePropertiesV1")]
    public partial class RestResourcePropertiesV1
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestResourcePropertiesV1" /> class.
        /// </summary>
        /// <param name="resourceIds">resourceIds.</param>
        /// <param name="resourceName">resourceName.</param>
        /// <param name="resourceProperties">resourceProperties.</param>
        public RestResourcePropertiesV1(Dictionary<string, string> resourceIds = default(Dictionary<string, string>), string resourceName = default(string), Dictionary<string, string> resourceProperties = default(Dictionary<string, string>))
        {
            this.ResourceIds = resourceIds;
            this.ResourceName = resourceName;
            this.ResourceProperties = resourceProperties;
        }

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
            sb.Append("class RestResourcePropertiesV1 {\n");

            sb.Append("  RestResourcePropertiesV1{\n");
            foreach (var item in ResourceProperties)
            {
                sb.Append("   "+item.Key).Append(": ").Append(item.Value).Append("\n");
            }
            sb.Append("  }\n");
            sb.Append("  ResourceIds{\n");
            foreach (var item in ResourceIds)
            {
                sb.Append("   "+item.Key).Append(": ").Append(item.Value).Append("\n");
            }
            sb.Append("  }\n");
            sb.Append("  ResourceName: ").Append(ResourceName).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

    }

}
