﻿/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LogicMonitor.DataSDK.Utils
{

    public class ObjectNameValidator
    {/// <summary>
     /// </summary>
        public ObjectNameValidator()
        {
        }

        public const string RegexResourceName = "^[a-z:A-Z0-9\\._\\-]+$";
        public const string RegexInvalidResourceName = "[*<?,;`\\\\]";
        public const string RegexInvalidDataSourceName = "[^a-zA-Z #@_0-9:&\\.\\+]";
        public const string RegexInstanceName = "^[a-z:A-Z0-9\\._\\-\\t;]+$";
        public const string RegexInvalidDeviceDisplayName = "[*<?,;`\\n]";
        public const string RegexDataPoint = "^[a-zA-Z_0-9]+$";
        public const string RegexInvalidDataSourceDisplayName = "[^a-zA-Z:/ _0-9\\(\\)\\.#\\+@<>]";
        public const string RegexDataSourceGroupName = "[a-zA-Z0-9_\\- ]+$";
        public const string RegexCompanyName = "^[a-zA-Z0-9_.\\-]+$";
        public const string RegexAuthId = "^[a-zA-Z0-9]+$";
        public const string RegexAuthKey = "\\s";
        public const string RegexDataSourceId9Digit = "^[0-9]{0,9}$";
        public const string RegexDataSourceIdExpo = @"^e\^\-?\d*?$";

        private readonly Regex PatternResourceName = new Regex(RegexResourceName);
        private readonly Regex PatternInvalidResourceName = new Regex(RegexInvalidResourceName);
        private readonly Regex PatternInvalidDataSourceName = new Regex(RegexInvalidDataSourceName);
        private readonly Regex PatternInstanceName = new Regex(RegexInstanceName);
        private readonly Regex PatternInvalidDeviceDisplayName = new Regex(RegexInvalidDeviceDisplayName);
        private readonly Regex PatternDataPoint = new Regex(RegexDataPoint);
        private readonly Regex PatternDataSourceGroupName = new Regex(RegexDataSourceGroupName);
        private readonly Regex PatternCompanyName = new Regex(RegexCompanyName);
        private readonly Regex PatternAuthId = new Regex(RegexAuthId);
        private readonly Regex PatternAuthKey = new Regex(RegexAuthKey);
        private readonly Regex PatternDataSourceId9Digit = new Regex(RegexDataSourceId9Digit);
        private readonly Regex PatternDataSourceIdExpo = new Regex(RegexDataSourceIdExpo);

        private readonly List<string> InvalidDataPointNameSet = new List<string>
        {
            "SIN","COS","LOG","EXP","FLOOR","CEIL","ROUND","POW","ABS","SQRT","RANDOM",
            "LT","LE","GT","GE","EQ","NE","IF","MIN","MAX","LIMIT","DUP","EXC","POP","UN","UNKN","NOW","TIME","PI","E",
            "AND","OR","XOR",
            "INF","NEGINF","STEP","YEAR","MONTH","DATE","HOUR","MINUTE","SECOND","WEEK",
            "SIGN","RND","SUM2","AVG2","PERCENT","RAWPERCENTILE","IN","NANTOZERO","MIN2","MAX2"
        };

        public bool PassEmptyAndSpellCheck(string name)
        {
            if (name.Length == 0 || name.StartsWith(" ") || name.EndsWith(" "))
                return false;
            else
                return true;
        }

        public bool IsNameLengthValid(string name)
        {
            if (name.Length <= 255)
                return true;
            return false;
        }

        public bool IsValidResourceName(string resourceName)
        {
            return PatternResourceName.IsMatch(resourceName);
        }

        public bool IsInValidResourceId(string resourceId)
        {
            return PatternInvalidResourceName.IsMatch(resourceId);
        }

        public bool IsValidDataSourceName(string name)
        {
            return (name.Length == 0);
        }

        public bool IsValidDataSourceId9Digit(int dataSourceID)
        {
            return PatternDataSourceId9Digit.IsMatch(dataSourceID.ToString());
        }
        public bool IsValidDataSourceIdExpo(int dataSourceID)
        {
            return PatternDataSourceIdExpo.IsMatch(dataSourceID.ToString());
        }

        public string ValidateDataSourceName(string name)
        {
            if (name.Length == 0)
                return "Datasource name cannot be empty";
            if (name.Contains("-"))
            {
                if (name.IndexOf("-") == (name.Length - 1))
                {
                    if (name.Length == 1)
                        return "Datasource name cannot be single \"-\"";
                    name = name.Replace(" -", "");
                    if (name.Length == 0)
                        return "Space is not allowed in start and end";

                }
                else
                    return "Support \"-\" for datasource name when its the last char";
            }
            return _Validate(name, "Datasource name", PatternInvalidDataSourceName);
        }

        public string _Validate(string name, string fieldName, Regex patternInvalidDataSourceName)
        {
            if (name == null)
                return string.Format("{0} can't be empty", fieldName);
            if (name.StartsWith(" ") || name.EndsWith(" "))
                return String.Format("Space is not allowed at start and end in {0}", fieldName);
            var matcher = patternInvalidDataSourceName.IsMatch(name);
            if (matcher)
                return string.Format("{0} is not allowed in {1}", name, fieldName);
            return "";
        }

        public bool IsValidInstanceName(string instanceName)
        {
            return PatternInstanceName.IsMatch(instanceName);
        }

        public bool IsValidCompanyName(string companyName)
        {
            return PatternCompanyName.IsMatch(companyName);
        }

        public bool IsValidAuthId(string authId)
        {
            return PatternAuthId.IsMatch(authId);
        }

        public bool IsValidAuthKey(string authKey)
        {
            return !PatternAuthKey.IsMatch(authKey);
        }

        public string ValidateDeviceDisplayName(string name)
        {
            return _Validate(name, "instance display name", PatternInvalidDeviceDisplayName);
        }


        public string ValidDataPointName(string name)
        {
            if (name == null)
                return ObjectNameConstant.DataPoint.DatapointNameNull;
            if (!PatternDataPoint.IsMatch(name))
                return string.Format("Invalid Data point {0}", name);
            if (InvalidDataPointNameSet.Contains(name.ToUpper()))
                return string.Format("{0} is a keyword and cannot be use as datapoint name.", name);
            return "";
        }
        public string CheckDataPointNameValidation(string dataPointName)
        {
            StringBuilder errorMsg = new StringBuilder();
            if (dataPointName == null)
                errorMsg.Append(ObjectNameConstant.DataPoint.DatapointNameMandatory);
            else
            {
                string str = ValidDataPointName(dataPointName);
                if (!PassEmptyAndSpellCheck(dataPointName))
                    errorMsg.Append(ObjectNameConstant.DataPoint.DatapointNameTrailing);
                else if (dataPointName.Length > 128)
                    errorMsg.Append(ObjectNameConstant.DataPoint.DatapointNameSize);
                else if (str != null)
                    errorMsg.Append(str);
            }
            return errorMsg.ToString();
        }
        public string CheckDataPointAggerationTypeValidation(string dataPointAggerationType, int percentileValue = default)
        {
            StringBuilder errorMsg = new StringBuilder();

            if (dataPointAggerationType != null)
            {
                var dataPointAggerationTypes = new List<string> { "none", "avg", "sum", "percentile" };
                dataPointAggerationType = dataPointAggerationType.ToLower();
                if ( !dataPointAggerationTypes.Contains(dataPointAggerationType) )
                    errorMsg.Append(string.Format("The datapoint aggeration type is having invalid datapoint aggreation Type {0}.", dataPointAggerationType));
                if (dataPointAggerationType == "percentile")
                    CheckPercentileValue(percentileValue);
            }
            return errorMsg.ToString();

        }
        //check message from here
        public string CheckDataPointDescriptionValidation(string dataPointDescription)
        {
            StringBuilder errorMsg = new StringBuilder();
            if (dataPointDescription != null && (dataPointDescription.Length > 1024))
                errorMsg.Append(ObjectNameConstant.DataPoint.DatapointDescriptionSize);
            return errorMsg.ToString();
        }
        public string CheckDataPointTypeValidation(string dataPointType)
        {
            StringBuilder errorMsg = new StringBuilder();

            if (dataPointType != null)
            {
                var dataPointTypes = new List<string> { "counter", "gauge", "derive" };
                dataPointType = dataPointType.ToLower();
                if(!dataPointTypes.Contains(dataPointType))
                    errorMsg.Append(string.Format("The datapoint type is having invalid dataPointType {0}.", dataPointType));
            }
            return errorMsg.ToString();

        }
        public string CheckPercentileValue(int percentileValue)
        {
            StringBuilder errorMsg = new StringBuilder();
            if (percentileValue < 0 || percentileValue > 100)
                errorMsg.Append(ObjectNameConstant.DataPoint.DatapointPercentile);
            return errorMsg.ToString();
        }


        public string CheckDataSourceNameValidation(string dataSource)
        {
            StringBuilder errorMsg = new StringBuilder();
            if (dataSource == null)
                errorMsg.Append(ObjectNameConstant.DataSource.DatasourceMandatory);
            else
            {
                if (!PassEmptyAndSpellCheck(dataSource))
                    errorMsg.Append(ObjectNameConstant.DataSource.DatasourceNameTrailing);
                else if (dataSource.Length > 64)
                    errorMsg.Append(ObjectNameConstant.DataSource.DatasourceDisplayNameSize);
                else if (IsValidDataSourceName(dataSource))
                    errorMsg.Append(string.Format("Invalid datasource name : {0}", dataSource));
            }
            return errorMsg.ToString();
        }
        public string CheckDataSourceDisplayNameValidation(string dataSourceDisplayName)
        {
            StringBuilder errorMsg = new StringBuilder();
            if (dataSourceDisplayName != null)
            {
                if (!PassEmptyAndSpellCheck(dataSourceDisplayName))
                    errorMsg.Append(ObjectNameConstant.DataSource.DatasourceDisplayNameTrailing);
                else if (dataSourceDisplayName.Length > 64)
                    errorMsg.Append(ObjectNameConstant.DataSource.DatasourceDisplayNameSize) ;
                else if (!IsValidDataSourceDisplayName(dataSourceDisplayName))
                    errorMsg.Append(string.Format("Invalid datasource display name : {0}", dataSourceDisplayName));
            }
            return errorMsg.ToString();
        }
        public string CheckDataSourceGroupNameValidation(string dataSourceGroupName)
        {
            StringBuilder errorMsg = new StringBuilder();
            if (dataSourceGroupName != null)
            {
                if (!PassEmptyAndSpellCheck(dataSourceGroupName))
                    errorMsg.Append(ObjectNameConstant.DataSource.DatasourceGroupNameTrailing);
                else if (dataSourceGroupName.Length < 2)
                    errorMsg.Append(ObjectNameConstant.DataSource.DatasourceGroupNameMinSize);
                else if (dataSourceGroupName.Length > 128)
                    errorMsg.Append(ObjectNameConstant.DataSource.DatasourceGroupNameSize);
                else if (!IsValidDataSourceGroupName(dataSourceGroupName))
                    errorMsg.Append(string.Format("Invalid datasource group name : {0}", dataSourceGroupName));
            }
            return errorMsg.ToString();
        }
        public bool IsValidDataSourceDisplayName(string name)
        {
            var errormsg = ValidateDataSourceDisplayName(name); //Need to check

            if (errormsg.Length == 0)
                return true;
            else
                return false;
        }
        public string ValidateDataSourceDisplayName(string name)
        {
            if (name.Length == 0)
                return ObjectNameConstant.DataSource.DataSourceDisplayNameMandatory;
            if (name.Contains("-") && (name.IndexOf("-") == (name.Length - 1)))
            {
                if (name.Length == 1)
                    return "Datasource display name cannot be single \"-\"";
                name = name.Replace(" -", "");
                if (name.Length == 0)
                    return ObjectNameConstant.DataSource.DataSourceDisplayNameSpace ;
                else
                    return "Support \"-\" for datasource display name when its the last char";

            }
            return _Validate(name, "Datasource display name", PatternInvalidDataSourceName);
        }
        public bool IsValidDataSourceGroupName(string name)
        {
            return PatternDataSourceGroupName.IsMatch(name);
        }
        public string CheckDataSourceId(int dataSourceId)
        {
            StringBuilder errorMsg = new StringBuilder();

            if (IsValidDataSourceId9Digit(dataSourceId) == false)
                errorMsg.Append(ObjectNameConstant.DataSource.DataSourceId);
            return errorMsg.ToString();
        }


        public string CheckResourceNameValidation(bool createFlag, string resourceName)
        {
            StringBuilder errorMsg = new StringBuilder();
            if (createFlag || resourceName == null)
            {
                if (resourceName == null)
                    errorMsg.Append(ObjectNameConstant.Resource.ResourceNameMandatory);
                else if (!PassEmptyAndSpellCheck(resourceName))
                    errorMsg.Append(ObjectNameConstant.Resource.ResourceNameTrailing);
                else if (!IsNameLengthValid(resourceName))
                    errorMsg.Append(ObjectNameConstant.Resource.ResourceNameSize);
                else if (!IsValidResourceName(resourceName))
                    errorMsg.Append(string.Format("Invalid resource name : {0}", resourceName));
            }
            return errorMsg.ToString();

        }
        public string CheckResourceNamDescriptionValidation(string description)
        {
            StringBuilder errorMsg = new StringBuilder();
            if (description != null && (description.Length > 65535))
                errorMsg.Append(ObjectNameConstant.Resource.ResourceDescriptionSize);
            return errorMsg.ToString();

        }
        public string CheckResourceIdsValidation(Dictionary<string, string> resourceIds)
        {
            StringBuilder errorMsg = new StringBuilder();
            if (resourceIds.Count == 0)
            {
                errorMsg.Append((ObjectNameConstant.Resource.ResourceIdEmpty));
            }
            else
            {
                foreach (var item in resourceIds)
                {
                    if (!PassEmptyAndSpellCheck(item.Key))
                        errorMsg.Append(ObjectNameConstant.Resource.ResourceIdKeyTrailing);
                    else if (!IsNameLengthValid(item.Key))
                        errorMsg.Append(ObjectNameConstant.Resource.ResourceIdKeySize);
                    else if (IsInValidResourceId(item.Key))
                        errorMsg.Append(String.Format("Invalid resource Id key {0}", item.Key));
                    else if (!PassEmptyAndSpellCheck(item.Value))
                        errorMsg.Append(ObjectNameConstant.Resource.ResourceIdValueTrailing);
                    else if (item.Value.Length > 24000)
                        errorMsg.Append(ObjectNameConstant.Resource.ResourceIdValueSize);
                    else if (IsInValidResourceId(item.Value))
                        errorMsg.Append(string.Format("Invalid resource Id Value : {0} for Key : {1}.", item.Value, item.Key));
                }
            }
            return errorMsg.ToString(); ;
        }
        public string CheckResourcePropertiesValidation(Dictionary<string, string> resourceProperties)
        {
            StringBuilder errorMsg = new StringBuilder();
            if (resourceProperties.Count == 0)
                errorMsg.Append(ObjectNameConstant.Resource.ResourcePropertiesEmpty);
            else
            {
                foreach (var item in resourceProperties)
                {
                    if (!PassEmptyAndSpellCheck(item.Key))
                        errorMsg.Append(ObjectNameConstant.Resource.ResourcePropertiesKeyTrailing) ;
                    else if (!IsNameLengthValid(item.Key))
                        errorMsg.Append(ObjectNameConstant.Resource.ResourcePropertiesKeySize);
                    else if (item.Key.Contains("##"))
                        errorMsg.Append(ObjectNameConstant.Resource.ResourcePropertiesCannotContain);
                    else if (item.Key.ToLower().StartsWith("system.") || item.Key.ToLower().StartsWith("auto."))
                        errorMsg.Append(string.Format("Resource Properties Should not contain System or auto properties :: {0}.", item.Key));
                    else if (IsInValidResourceId(item.Key))
                        errorMsg.Append(String.Format("Invalid resource Properties key {0}", item.Key));
                    else if (!PassEmptyAndSpellCheck(item.Value))
                        errorMsg.Append(ObjectNameConstant.Resource.ResourcePropertiesValueTrailing);
                    else if (item.Value.Length > 24000)
                        errorMsg.Append(ObjectNameConstant.Resource.ResourcePropertiesValueSize);
                    else if (IsInValidResourceId(item.Value))
                        errorMsg.Append(string.Format("Invalid resource Properties Value : {0} for Key : {1}.", item.Value, item.Key));
                }
            }

            return errorMsg.ToString(); ;
        }


        public string CheckInstanceNameValidation(string instanceName)
        {
            StringBuilder errorMsg = new StringBuilder();
            if (instanceName == null)
                errorMsg.Append(ObjectNameConstant.DataSourceInstance.InstanceNameMandatory);
            else
            {
                if (!PassEmptyAndSpellCheck(instanceName))
                    errorMsg.Append(ObjectNameConstant.DataSourceInstance.InstanceNameTrailing);
                else if (!IsNameLengthValid(instanceName))
                    errorMsg.Append(ObjectNameConstant.DataSourceInstance.InstanceNameSize);
                else if (!IsValidInstanceName(instanceName))
                    errorMsg.Append(string.Format("Invalid instance name : {0}", instanceName));
            }
            return errorMsg.ToString();
        }
        public string CheckInstanceDisplayNameValidation(string instanceDisplayName)
        {
            StringBuilder errorMsg = new StringBuilder();
            if (instanceDisplayName != null)
            {
                if (!PassEmptyAndSpellCheck(instanceDisplayName))
                    errorMsg.Append(ObjectNameConstant.DataSourceInstance.InstanceDisplayNameTrailing);
                else if (!IsNameLengthValid(instanceDisplayName))
                    errorMsg.Append(ObjectNameConstant.DataSourceInstance.InstanceDisplayNameSize);
                else if (Convert.ToBoolean(ValidateDeviceDisplayName(instanceDisplayName)))
                    errorMsg.Append(string.Format("Invalid instance display name : {0}", instanceDisplayName));
            }
            return errorMsg.ToString();
        }
        public string CheckInstancePropertiesValidation(Dictionary<string, string> instanceProperties)
        {
            StringBuilder errorMsg = new StringBuilder();

            if (instanceProperties.Count > 0)
            {
                foreach (var item in instanceProperties)
                {
                    if (!PassEmptyAndSpellCheck(item.Key))
                        errorMsg.Append(ObjectNameConstant.DataSourceInstance.InstancePropertiesKeyTrailing);
                    else if (!IsNameLengthValid(item.Key))
                        errorMsg.Append(ObjectNameConstant.DataSourceInstance.InstancePropertiesKeySize);
                    else if (item.Key.ToLower().StartsWith("system.") || item.Key.ToLower().StartsWith("auto."))
                        errorMsg.Append(string.Format("Instance Properties Should not contain System or auto properties :: {0}.", item.Key));
                    else if (IsInValidResourceId(item.Key))
                        errorMsg.Append(String.Format("Invalid instance Properties key {0}", item.Key));
                    else if (!PassEmptyAndSpellCheck(item.Value))
                        errorMsg.Append(ObjectNameConstant.DataSourceInstance.InstancePropertiesValueTrailing);
                    else if (item.Value.Length > 24000)
                        errorMsg.Append(ObjectNameConstant.DataSourceInstance.InstancePropertiesValueSize);
                    else if (IsInValidResourceId(item.Value))
                        errorMsg.Append(string.Format("Invalid instance properties value : {0} for Key : {1}.", item.Value, item.Key));
                }
            }
            return errorMsg.ToString();
        }
        public string CheckDataSourceInstanceId(int instanceID)
        {
            StringBuilder errorMsg = new StringBuilder();

            if (IsValidDataSourceId9Digit(instanceID) == false)
                errorMsg.Append(ObjectNameConstant.DataSourceInstance.InstanceId);
            return errorMsg.ToString();
        }
    }
}
