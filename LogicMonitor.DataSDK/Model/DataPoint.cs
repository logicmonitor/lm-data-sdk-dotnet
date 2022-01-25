/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Runtime.Serialization;
using System.Text;
using LogicMonitor.DataSDK.Utils;

namespace LogicMonitor.DataSDK.Model
{

    /// <summary>
    /// This model is used to defining the Datapoint object.
    /// </summary>
    [DataContract(Name ="DataPoint")]
    public partial class DataPoint
    {
        private readonly ObjectNameValidator objectNameValidator = new ObjectNameValidator();

        public DataPoint(string aggregation = default(string), string description = default(string), string name = default(string), string type = default(string) )
        {
            this.AggregationType = aggregation;
            this.Description = description;
            this.Name = name;
            this.Type = type;
            string errorMsg = ValidField();
            if (errorMsg != null && errorMsg.Length > 0)
                throw new ArgumentException(errorMsg);
        }
        /// <summary>
        /// The aggregation method, if any, that should be used if data is pushed in sub-minute intervals. Allowed options are “sum”,
        /// “average” and “none”(default) where “none” would take last value for that minute. Only considered when creating a new
        /// datapoint. See the About the Push Metrics REST API section of this guide for more information on datapoint value aggregation intervals.
        /// </summary>
        [DataMember(Name = "Aggreaation", EmitDefaultValue = false)]
        public string AggregationType { get; set; }

        /// <summary>
        /// Datapoint description. Only considered when creating a new datapoint.
        /// </summary>
        [DataMember(Name = "Description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Datapoint name. If no existing datapoint matches for specified DataSource, a new datapoint is created with this name.
        /// </summary>
        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///  Metric type as a number in string format. Allowed options are “guage” (default) and “counter”. Only considered when creating a new datapoint.
        /// </summary>
        [DataMember(Name = "Type", EmitDefaultValue = false)]
        public string Type { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataPoint {\n");
            sb.Append("  Aggregation: ").Append(AggregationType).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        public string ValidField()
        {
            string errorMsg = "";
            errorMsg += objectNameValidator.CheckDataPointNameValidation(Name);
            errorMsg += objectNameValidator.CheckDataPointAggerationTypeValidation(AggregationType);
            errorMsg += objectNameValidator.CheckDataPointDescriptionValidation(Description);
            errorMsg += objectNameValidator.CheckDataPointTypeValidation(Type);
            return errorMsg;
        }
    }
}
