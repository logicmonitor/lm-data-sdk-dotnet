using System;
namespace LogicMonitor.DataSDK.Utils
{
    public class ObjectNameConstant
    {
        public struct DataPoint
        {
            public const string DatapointNameNull = "Data point name cannot be null";
            public const string DatapointNameMandatory = "Datapoint name is mandatory.";
            public const string DatapointNameTrailing = "DataPoint Name Should not be empty or have tailing spaces.";
            public const string DatapointNameSize = "Datapoint Name size should not be greater than 128 characters.";

            public const string DatapointDescriptionSize = "Datapoint description should not be greater than 1024 characters.";

            public const string DatapointPercentile = "Percentile value must be in Range of 0-100";

        };

        public struct DataSource
        {
            public const string DatasourceMandatory = "Datasource is mandatory.";
            public const string DatasourceNameTrailing = "Datasource Name Should not be empty or have tailing spaces.";
            public const string DatasourceNameSize = "Datasource Name size should not be greater than 64 characters.";

            public const string DatasourceDisplayNameTrailing = "Datasource display name Should not be empty or have tailing spaces.";
            public const string DatasourceDisplayNameSize = "Datasource display name size should not be greater than 64 characters.";
            public const string DataSourceDisplayNameMandatory = "Datasource display name cannot be empty";
            public const string DataSourceDisplayNameSpace = "Space is not allowed in start and end";
            public const string DataSourceId = "DataSource Id cannot be more than 9 digit";

            public const string DatasourceGroupNameTrailing = "Datasource group name Should not be empty or have tailing spaces.";
            public const string DatasourceGroupNameSize = "Datasource group name size should not be greater than 128 characters.";
            public const string DatasourceGroupNameMinSize = "Datasource group name size should not be less than 2 characters.";
        };

        public struct DataSourceInstance
        {

            public const string InstanceNameMandatory = "Instance name is mandatory.";
            public const string InstanceNameTrailing = "Instance Name Should not be empty or have tailing spaces.";
            public const string InstanceNameSize = "Instance Name size should not be greater than 255 characters.";

            public const string InstanceDisplayNameTailing = "Instance display name Should not be empty or have tailing spaces.";
            public const string InstanceDisplayNameSize = "Instance display name size should not be greater than 255 characters.";

            public const string InstancePropertiesKeyTailing = "Instance Properties Key should not be null, empty or have tailing spaces.";
            public const string InstancePropertiesKeySize = "Instance Properties Key should not be greater than 255 characters.";
            public const string InstancePropertiesValueTailing = "Instance Properties Value should not be null, empty or have tailing spaces.";
            public const string InstancePropertiesValueSize = "Instance Properties Value should not be greater than 24000 characters.";

            public const string InstanceId = "DataSource Instance Id cannot be more than 9 digit";
        };

        public struct Resource
        {
            public const string ResourceNameMandatory = "Resource name is mandatory.";
            public const string ResourceNameTrailing = "Resource Name Should not be empty or have tailing spaces.";
            public const string ResourceNameSize = "Resource Name size should not be greater than 255 characters.";

            public const string ResourceDescriptionSize = "Resource Description Size should not be greater than 65535 characters.";

            public const string ResourceIdEmpty = "No Element in Resource Id.";
            public const string ResourceIdKeyTrailing = "Resource Id Key should not be null, empty or have trailing spaces.";
            public const string ResourceIdKeySize = "Resource Id Key should not be greater than 255 characters."; 
            public const string ResourceIdValueTrailing = "Resource Id Value should not be null, empty or have trailing spaces.";
            public const string ResourceIdValueSize = "Resource Id Value should not be greater than 24000 characters.";

            public const string ResourcePropertiesEmpty = "No Element in Resource Properties.";
            public const string ResourcePropertiesKeyTrailing = "Resource Properties Key should not be null, empty or have trailing spaces."; 
            public const string ResourcePropertiesKeySize = "Resource Properties Key should not be greater than 255 characters.";
            public const string ResourcePropertiesCannotContain = "Cannot use '##' in property name.";
            public const string ResourcePropertiesValueTrailing = "Resource Properties Value should not be null, empty or have trailing spaces.";
            public const string ResourcePropertiesValueSize = "Resource Properties Value should not be greater than 24000 characters.";


        };
    }
}
