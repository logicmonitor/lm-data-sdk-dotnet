/*
 * Copyright, 2021, LogicMonitor, Inc.
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
    /// This model is used to defining the datasource object.
    /// </summary>
    [DataContract]
    public class DataSource
    {
        ObjectNameValidator objectNameValidator = new ObjectNameValidator();
        public DataSource(string Name = default(string), string DisplayName = default(string), string Group = default(string), int Id = default(int) )
        {
            this.Name = Name;
            this.DisplayName = DisplayName;
            this.Group = Group;
            this.Id = Id;
            string errorMsg = ValidField();
            if (errorMsg != null && errorMsg.Length > 0)
                throw new ArgumentException(errorMsg);
        }
        /// <summary>
        /// DataSource unique name. Used to match an existing DataSource. If no existing DataSource matches the name provided here,
        /// a new DataSource is created with this name.        /// </summary>
        [DataMember(Name ="Name",EmitDefaultValue =false)]
        public string Name { get; set; }

        /// <summary>
        /// DataSource display name. Only considered when creating a new DataSource.
        /// </summary>
        [DataMember(Name = "DisplayName", EmitDefaultValue = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// DataSource group name. Only considered when DataSource does not already belong to a group. Used to organize the DataSource within
        /// a DataSource group. If no existing DataSource group matches, a new group is created with this name and the DataSource is organized under the new group.
        /// </summary>
        [DataMember(Name = "Group", EmitDefaultValue = false)]
        public string Group { get; set; }

        /// <summary>
        /// DataSource unique ID. Used only to match an existing DataSource. If no existing DataSource matches the provided ID, an error results.
        /// </summary>
        [DataMember(Name = "Id", EmitDefaultValue = false)]
        public int Id { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataPoint {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
            sb.Append("  Group: ").Append(Group).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
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

        public bool Equals(DataSource input)
        {
            if (input == null)
                return false;

            return
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name)
                    )
                ) &&
                (
                    this.DisplayName == input.DisplayName ||
                    (this.DisplayName != null &&
                    this.DisplayName.Equals(input.DisplayName)
                    )
                ) &&
                (
                    this.Group == input.Group ||
                    (this.Group != null &&
                    this.Group.Equals(input.Group))
                ) &&
                (
                    this.Id == input.Id ||
                    (
                    this.Id.Equals(input.Id)
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
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.DisplayName != null)
                    hashCode = hashCode * 59 + this.DisplayName.GetHashCode();
                if (this.Group != null)
                    hashCode = hashCode * 59 + this.Group.GetHashCode();
                if (this.Id != 0)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                return hashCode;
            }
        }

        public string ValidField()
        {
            int dataSourceId = Id;
            string errorMsg = "";

            if (dataSourceId == 0)
            {
                errorMsg += objectNameValidator.CheckDataSourceNameValidation(Name);
                errorMsg += objectNameValidator.CheckDataSourceDisplayNameValidation(DisplayName);
                errorMsg += objectNameValidator.CheckDataSourceGroupNameValidation(Group);
            }
            else if (dataSourceId < 0)
            {
                errorMsg += string.Format("DataSource Id {0} should not be negative.", dataSourceId);
                errorMsg += objectNameValidator.CheckDataSourceNameValidation(Name);
                errorMsg += objectNameValidator.CheckDataSourceDisplayNameValidation(DisplayName);
                errorMsg += objectNameValidator.CheckDataSourceGroupNameValidation(Group);
            }
            
            return errorMsg;
        }
    }
}
