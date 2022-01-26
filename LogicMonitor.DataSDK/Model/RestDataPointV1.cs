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
    /// RestDataPointV1
    /// </summary>
    [DataContract(Name = "RestDataPointV1")]
    public partial class RestDataPointV1
    {
        public RestDataPointV1()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RestDataPointV1" /> class.
        /// </summary>
        /// <param name="dataPointAggregationType">dataPointAggregationType.</param>
        /// <param name="dataPointDescription">dataPointDescription.</param>
        /// <param name="dataPointName">dataPointName.</param>
        /// <param name="dataPointType">dataPointType.</param>
        /// <param name="values">values.</param>
        public RestDataPointV1(string dataPointName,string dataPointAggregationType = default(string), string dataPointDescription = default(string),  string dataPointType = default(string), Dictionary<string, string> values = default(Dictionary<string, string>))
        {
            this.DataPointAggregationType = dataPointAggregationType;
            this.DataPointDescription = dataPointDescription;
            this.DataPointName = dataPointName;
            this.DataPointType = dataPointType;
            this.Values = values;
        }

        /// <summary>
        /// Gets or Sets DataPointAggregationType
        /// </summary>
        [DataMember(Name = "dataPointAggregationType", EmitDefaultValue = false)]
        public string DataPointAggregationType { get; set; }

        /// <summary>
        /// Gets or Sets DataPointDescription
        /// </summary>
        [DataMember(Name = "dataPointDescription", EmitDefaultValue = false)]
        public string DataPointDescription { get; set; }

        /// <summary>
        /// Gets or Sets DataPointName
        /// </summary>
        [DataMember(Name = "dataPointName", EmitDefaultValue = false)]
        public string DataPointName { get; set; }

        /// <summary>
        /// Gets or Sets DataPointType
        /// </summary>
        [DataMember(Name = "dataPointType", EmitDefaultValue = false)]
        public string DataPointType { get; set; }

        /// <summary>
        /// Gets or Sets Values
        /// </summary>
        [DataMember(Name = "values", EmitDefaultValue = false)]
        public Dictionary<string, string> Values { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RestDataPointV1 {\n");
            sb.Append("  DataPointAggregationType: ").Append(DataPointAggregationType).Append("\n");
            sb.Append("  DataPointDescription: ").Append(DataPointDescription).Append("\n");
            sb.Append("  DataPointName: ").Append(DataPointName).Append("\n");
            sb.Append("  DataPointType: ").Append(DataPointType).Append("\n");
            sb.Append("  Values{\n ");
            foreach (var item in Values) { 
            sb.Append("   "+item.Key).Append(":").Append(item.Value).Append("\n");
            }
            sb.Append("  }\n");
            sb.Append("}\n");
            return sb.ToString();
        }

    }

}
