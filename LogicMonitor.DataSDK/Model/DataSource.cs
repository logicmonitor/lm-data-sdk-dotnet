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
    /// This model is used to defining the datasource object.
    /// </summary>
    [DataContract]
    public class DataSource
    {
        private readonly ObjectNameValidator objectNameValidator = new ObjectNameValidator();
        public DataSource()
        {
            
        }
        public DataSource(string name = default(string), string displayName = default(string), string group = default(string), int id = default(int), bool singleInstanceDS = false )
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.Group = group;
            this.Id = id;
            SingleInstanceDS = singleInstanceDS;
            string errorMsg = ValidField();
            if (errorMsg != null && errorMsg.Length > 0)
                throw new ArgumentException(errorMsg);
        }
        /// <summary>
        /// DataSource unique name. Used to match an existing DataSource. If no existing DataSource matches the name provided here,
        /// a new DataSource is created with this name.
        /// </summary>
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

        /// <summary>
        /// When set to "true", DataSource Instance are not to be set, when "true".
        /// </summary>
        [DataMember(Name = "singleInstanceDS", EmitDefaultValue = true)]
        public bool SingleInstanceDS { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataSource {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
            sb.Append("  Group: ").Append(Group).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  SingleInstanceDS: ").Append(SingleInstanceDS).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        public string ValidField()
        {
            int _dataSourceId = Id;
            string errorMsg = "";
            if (_dataSourceId >= 0)
            {
                errorMsg += objectNameValidator.CheckDataSourceId(_dataSourceId);
                errorMsg += objectNameValidator.CheckDataSourceNameValidation(Name);
                errorMsg += objectNameValidator.CheckDataSourceDisplayNameValidation(DisplayName);
                errorMsg += objectNameValidator.CheckDataSourceGroupNameValidation(Group);
            }
            else if (_dataSourceId < 0)
            {
                errorMsg += string.Format("DataSource Id {0} should not be negative.", _dataSourceId);
                errorMsg += objectNameValidator.CheckDataSourceNameValidation(Name);
                errorMsg += objectNameValidator.CheckDataSourceDisplayNameValidation(DisplayName);
                errorMsg += objectNameValidator.CheckDataSourceGroupNameValidation(Group);
            }
            
            return errorMsg;
        }
    }
}
