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
        ObjectNameValidator objectNameValidator = new ObjectNameValidator();

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
            return this.Equals(input as DataPoint);
        }

        public bool Equals(DataPoint input)
        {
            if (input == null)
                return false;

            return
                (
                    this.AggregationType == input.AggregationType ||
                    (this.AggregationType != null &&
                    this.AggregationType.Equals(input.AggregationType)
                    )
                ) &&
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description)
                    )
                ) &&
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) &&
                (
                    this.Type == input.Name ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type)
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
                if (this.AggregationType != null)
                    hashCode = hashCode * 59 + this.AggregationType.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
                return hashCode;
            }
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
