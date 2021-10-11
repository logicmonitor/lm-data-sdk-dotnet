using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LogicMonitor.DataSDK.Utils
{
    
    public class ObjectNameValidator
    {/// <summary>
    /// </summary>
        public ObjectNameValidator()
        {
        }

        public const string RegexResourceName                 = "[a-z:A-Z0-9\\._\\-]+$";
        public const string RegexInvalidResourceName          = "[*<?,;`\\\\]";
        public const string RegexInvalidDataSourceName        = "[^a-zA-Z #@_0-9:&\\.\\+]";
        public const string RegexInstanceName                 = "[a-z:A-Z0-9\\._\\-\\t ]+$";
        public const string RegexInvalidDeviceDisplayName     = "[*<?,;`\\n]";
        public const string RegexDataPoint                    = "[a-zA-Z_0-9]+$";
        public const string RegexInvalidDataSourceDisplayName = "[^a-zA-Z:/ _0-9\\(\\)\\.#\\+@<>]";
        public const string RegexDataSourceGroupName          = "[a-zA-Z0-9_\\- ]+$";
        public const string RegexCompanyName                  = "^[a-zA-Z0-9_.\\-]+$";
        public const string RegexAuthId                       = "^[a-zA-Z0-9]+$";
        public const string RegexAuthKey                      = "\\s";

        Regex PatternResourceName = new Regex(RegexResourceName);
        Regex PatternInvalidResourceName = new Regex(RegexInvalidResourceName);
        Regex PatternInvalidDataSourceName= new Regex(RegexInvalidDataSourceName);
        Regex PatternInstanceName = new Regex(RegexInstanceName);
        Regex PatternInvalidDeviceDisplayName = new Regex(RegexInvalidDeviceDisplayName);
        Regex PatternDataPoint = new Regex(RegexDataPoint);
        Regex PatternInvalidDataSourceDisplayName = new Regex(RegexInvalidDataSourceDisplayName);
        Regex PatternDataSourceGroupName = new Regex(RegexDataSourceGroupName);
        Regex PatternCompanyName = new Regex(RegexCompanyName);
        Regex PatternAuthId = new Regex(RegexAuthId);
        Regex PatternAuthKey = new Regex(RegexAuthKey);

        List<string> InvalidDataPointNameSet = new List<string>
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

        public string ValidateDataSourceName(string name)
        {
            if (name.Length == 0)
                return "Datasource name cannot be empty";
            if (name.Contains("-"))
            {
                if(name.IndexOf("-") == (name.Length-1) )
                {
                    if (name.Length == 1)
                        return "Datasource name cannot be single \"-\"";
                    name = name.Replace(" -", "");
                    if (name.Length == 0)
                        return "Space is not allowed in start and end";
                    else
                        return "Support \"-\" for datasource name when its the last char";
                }
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
                return string.Format("{0} is not allowed in {1}",name, fieldName);
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
            return PatternAuthKey.IsMatch(authKey);
        }

        public string ValidateDeviceDisplayName(string name)
        {
            return _Validate(name, "instance display name", PatternInvalidDeviceDisplayName);
        }
         
        public string ValidDataPointName(string name)
        {
            if (name == null)
                return "Data point name cannot be null";
            if (!PatternDataPoint.IsMatch(name))
                return string.Format("Invalid Data point {0}",name);
            if (InvalidDataPointNameSet.Contains(name.ToUpper()))
                return string.Format("{0} is a keyword and cannot be use as datapoint name.");
            return "";
        }

        public bool IsValidDataSourceDisplayName(string name)
        {
            return Convert.ToBoolean(ValidateDataSourceDisplayName(name)); //Need to check
        }

        public string ValidateDataSourceDisplayName(string name)
        {
            if (name.Length == 0)
                return "Datasource display name cannot be empty";
            if (name.Contains("-"))
            {
                if (name.IndexOf("-") == (name.Length - 1))
                {
                    if (name.Length == 1)
                        return "Datasource display name cannot be single \"-\"";
                    name = name.Replace(" -", "");
                    if (name.Length == 0)
                        return "Space is not allowed in start and end";
                    else
                        return "Support \"-\" for datasource display name when its the last char";
                }
            }
            return _Validate(name, "Datasource display name", PatternInvalidDataSourceName);
        }

        public bool IsValidDataSourceGroupName(string name)
        {
            return PatternDataSourceGroupName.IsMatch(name);
        }

        
        public string CheckResourceNameValidation(bool createFlag,string resourceName)
        {
            string errorMsg = "";
            if (createFlag || resourceName == null)
            {
                if (resourceName == null)
                    errorMsg += "Resource name is mandatory.";
                else if (!PassEmptyAndSpellCheck(resourceName))
                    errorMsg += "Resource Name Should not be empty or have tailing spaces.";
                else if (!IsNameLengthValid(resourceName))
                    errorMsg += "Resource Name size should not be greater than 255 characters.";
                else if(!IsValidResourceName(resourceName))
                    errorMsg += string.Format("Invalid resource name : {0}", resourceName); 
            }
            return errorMsg;
                
        }

        public string CheckResourceNamDescriptionValidation(string description)
        {
            string errorMsg = "";
            if (description != null)
                if (description.Length > 65535)
                    errorMsg += "Resource Description Size should not be greater than 65535 characters.";


            return errorMsg;

        }
        public string CheckResourceIdsValidation(Dictionary<string, string> resourceIds)
        {
            string errorMsg = "";
            if (resourceIds.Equals(null))
                errorMsg += "Resource Ids is mandatory.";
            else
            {
                if (resourceIds.Count == 0)
                    errorMsg += "No Element in Resource Id.";
                else
                {
                    foreach (var item in resourceIds)
                    {
                        if (!PassEmptyAndSpellCheck(item.Key))
                            errorMsg += "Resource Id Key should not be null, empty or have trailing spaces.";
                        else if (!IsNameLengthValid(item.Key))
                            errorMsg += "Resource Id Key should not be greater than 255 characters.";
                        else if(IsInValidResourceId(item.Key))
                            errorMsg += String.Format("Invalid resource Id key {0}",item.Key);

                        if (!PassEmptyAndSpellCheck(item.Value))
                            errorMsg += "Resource Id Value should not be null, empty or have trailing spaces.";
                        else if (item.Value.Length > 24000)
                            errorMsg += "Resource Id Value should not be greater than 24000 characters.";
                        else if (IsInValidResourceId(item.Value))
                            errorMsg += string.Format("Invalid resource Id Value : {0} for Key : {1}.", item.Value, item.Key);
                    }
                }
            }
            return errorMsg;
        }

        public string CheckResourcePropertiesValidation(Dictionary<string, string> resourceProperties)
        {
            string errorMsg = "";
            if (resourceProperties != null)
            {
                if (resourceProperties.Count == 0)
                    errorMsg += "No Element in Resource Id.";
                else
                {
                    foreach (var item in resourceProperties)
                    {
                        if (!PassEmptyAndSpellCheck(item.Key))
                            errorMsg += "Resource Properties Key should not be null, empty or have trailing spaces.";
                        else if (!IsNameLengthValid(item.Key))
                            errorMsg += "Resource Properties Key should not be greater than 255 characters.";
                        else if (item.Key.Contains("##"))
                            errorMsg += "Cannot use '##' in property name.";
                        else if (item.Key.ToLower().StartsWith("system.") || item.Key.ToLower().StartsWith("auto."))
                            errorMsg += string.Format("Resource Properties Should not contain System or auto properties :: {0}.",item.Key);
                        else if (IsInValidResourceId(item.Key))
                            errorMsg += String.Format("Invalid resource Properties key {0}", item.Key);

                        if (!PassEmptyAndSpellCheck(item.Value))
                            errorMsg += "Resource Properties Value should not be null, empty or have trailing spaces.";
                        else if (item.Value.Length > 24000)
                            errorMsg += "Resource Properties Value should not be greater than 24000 characters.";
                        else if (IsInValidResourceId(item.Value))
                            errorMsg += string.Format("Invalid resource Properties Value : {0} for Key : {1}.", item.Value, item.Key);
                    }
                }
            }

            return errorMsg;
        }


        public string CheckDataSourceNameValidation( string dataSource)
        {
            string errorMsg = "";
            if ( dataSource != null)
            {
                if (dataSource == null)
                    errorMsg += "Datasource is mandatory.";
                else if (!PassEmptyAndSpellCheck(dataSource))
                    errorMsg += "Datasource Name Should not be empty or have tailing spaces.";
                else if (dataSource.Length > 64)
                    errorMsg += "Datasource Name size should not be greater than 64 characters.";
                else if (IsValidDataSourceName(dataSource))
                    errorMsg += string.Format("Invalid datasource name : {0}", dataSource);
            }
            return errorMsg;
        }

        public string CheckDataSourceDisplayNameValidation(string dataSourceDisplayName)
        {
            string errorMsg = "";
            if (dataSourceDisplayName != null)
            {
                if (!PassEmptyAndSpellCheck(dataSourceDisplayName))
                    errorMsg += "Datasource display name Should not be empty or have tailing spaces.";
                else if (dataSourceDisplayName.Length > 64)
                    errorMsg += "Datasource display name size should not be greater than 64 characters.";
                else if (!IsValidDataSourceDisplayName(dataSourceDisplayName))
                    errorMsg += string.Format("Invalid datasource display name : {0}", dataSourceDisplayName);
            }
            return errorMsg;
        }

        public string CheckDataSourceGroupNameValidation(string dataSourceGroupName)
        {
            string errorMsg = "";
            if (dataSourceGroupName != null)
            {
                if (!PassEmptyAndSpellCheck(dataSourceGroupName))
                    errorMsg += "Datasource group name Should not be empty or have tailing spaces.";
                else if (dataSourceGroupName.Length < 2)
                    errorMsg += "Datasource group name size should not be less than 2 characters.";
                else if (dataSourceGroupName.Length > 128)
                    errorMsg += "Datasource group name size should not be greater than 128 characters.";
                else if (!IsValidDataSourceGroupName(dataSourceGroupName))
                    errorMsg += string.Format("Invalid datasource group name : {0}", dataSourceGroupName);
            }
            return errorMsg;
        }

        public string CheckInstanceNameValidation( string instanceName)
        {
            string errorMsg = "";
            if (instanceName != null)
            {
                if (instanceName == null)
                    errorMsg += "Instance name is mandatory.";
                else if (!PassEmptyAndSpellCheck(instanceName))
                    errorMsg += "Instance Name Should not be empty or have tailing spaces.";
                else if (!IsNameLengthValid(instanceName))
                    errorMsg += "Resource Name size should not be greater than 255 characters.";
                else if (!IsValidInstanceName(instanceName))
                    errorMsg += string.Format("Invalid instance name : {0}", instanceName);
            }
            return errorMsg;
        }

        public string CheckInstanceDisplayNameValidation(string instanceDisplayName)
        {
            string errorMsg = "";
            if (instanceDisplayName != null)
            {
                if (!PassEmptyAndSpellCheck(instanceDisplayName))
                    errorMsg += "Instance display name Should not be empty or have tailing spaces.";
                else if (!IsNameLengthValid(instanceDisplayName))
                    errorMsg += "Instance display name size should not be greater than 255 characters.";
                else if (Convert.ToBoolean(ValidateDeviceDisplayName(instanceDisplayName)))
                    errorMsg += string.Format("Invalid instance display name : {0}", instanceDisplayName);
            }
            return errorMsg;
        }
        public string CheckInstancePropertiesValidation(Dictionary<string, string> instanceProperties)
        {
            string errorMsg = "";
            if (instanceProperties != null)
            {
                if (instanceProperties.Count > 0)
                { 
                    foreach (var item in instanceProperties)
                    {
                        if (!PassEmptyAndSpellCheck(item.Key))
                            errorMsg += "Instance Properties Key should not be null, empty or have trailing spaces.";
                        else if (!IsNameLengthValid(item.Key))
                            errorMsg += "Instance Properties Key should not be greater than 255 characters.";
                        else if (item.Key.ToLower().StartsWith("system.") || item.Key.ToLower().StartsWith("auto."))
                            errorMsg += string.Format("Instance Properties Should not contain System or auto properties :: {0}.", item.Key);
                        else if (!IsInValidResourceId(item.Key))
                            errorMsg += String.Format("Invalid instance Properties key {0}", item.Key);

                        if (!PassEmptyAndSpellCheck(item.Value))
                            errorMsg += "Instance Properties Value should not be null, empty or have trailing spaces.";
                        else if (item.Value.Length > 24000)
                            errorMsg += "Instance Properties Value should not be greater than 24000 characters.";
                        else if (!IsInValidResourceId(item.Value))
                            errorMsg += string.Format("Invalid instance properties value : {0} for Key : {1}.", item.Value, item.Key);
                    }
                }
            }
            return errorMsg;
        }
        public string CheckDataPointNameValidation(string dataPointName)
        {
            string errorMsg = "";
            if (dataPointName != null)
            {
                string str = ValidDataPointName(dataPointName);
                if (dataPointName == null)
                    errorMsg += "Datapoint name is mandatory.";
                else if (!PassEmptyAndSpellCheck(dataPointName))
                    errorMsg += "Instance Name Should not be empty or have tailing spaces.";
                else if (dataPointName.Length > 128)
                    errorMsg += "Resource Name size should not be greater than 128 characters.";
                else if (str != null)
                    errorMsg += str;
            }
            return errorMsg;
        }

        public string CheckDataPointDescriptionValidation(string dataPointDescription)
        {
            string errorMsg = "";
            if (dataPointDescription != null)
                if (dataPointDescription.Length > 1024)
                    errorMsg += "Datapoint description should not be greater than 1024 characters.";
            return errorMsg;
        }

        public string CheckDataPointTypeValidation(string dataPointType)
        {
            string errorMsg = "";
            
            if(dataPointType != null)
            {
                var dataPointTypes = new List<string> { "counter", "guage", "derive" };
                dataPointType = dataPointType.ToLower();
                var counter = 0;
                foreach(var type in dataPointTypes)
                {
                    if (dataPointType != type)
                        counter++;
                }

                if(counter == 3)
                    errorMsg += string.Format("The datapoint type is having invalid dataPointType {0}.", dataPointType); 
            }
            return errorMsg;

        }


        public string CheckDataPointAggerationTypeValidation(string dataPointAggerationType)
        {
            string errorMsg = "";

            if (dataPointAggerationType != null)
            {
                var dataPointAggerationTypes = new List<string> { "none", "avg", "sum"};
                dataPointAggerationType = dataPointAggerationType.ToLower();
                var counter = 0;
                foreach (var type in dataPointAggerationTypes)
                {
                    if (dataPointAggerationType != type)
                        counter++;
                }

                if (counter == 3)
                    errorMsg += string.Format("The datapoint aggeration type is having invalid datapoint aggreation Type {0}.", dataPointAggerationType); 
            }
            return errorMsg;

        }


    }
}
