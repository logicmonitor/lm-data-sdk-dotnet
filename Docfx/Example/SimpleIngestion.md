# LogicMonitor.DataSDK - the C# library for the LogicMonitor API-Ingest Rest API
LogicMonitor is a SaaS-based performance monitoring platform that provides full visibility into complex, hybrid 
infrastructures, offering granular performance monitoring and actionable data and insights. API-Ingest provides the 
entry point in the form of public rest APIs for ingesting metrics into LogicMonitor. For using this application users 
have to create LMAuth token using access id and key from santaba.

- SDK version: 0.0.7-alpha

<a name="frameworks-supported"></a>
## Frameworks supported
- .NET Core >= 3.1

<a name="dependencies"></a>
## Dependencies

- [RestSharp](https://www.nuget.org/packages/RestSharp) - 106.13.0 or later
- [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json/) - 12.0.3 or later
- [Microsoft.Extenstion.Logging](https://www.nuget.org/packages/Newtonsoft.Json/) - 12.0.3 or later
- [Microsoft.Extenstion.Hosting](https://www.nuget.org/packages/Newtonsoft.Json/) - 12.0.3 or later

NOTE: RestSharp versions greater than 105.1.0 have a bug which causes file uploads to fail. See [RestSharp#742](https://github.com/restsharp/RestSharp/issues/742).
NOTE: RestSharp for .Net Core creates a new socket for each api call, which can lead to a socket exhaustion problem. See [RestSharp#1406](https://github.com/restsharp/RestSharp/issues/1406).

<a name="Configration"></a>
## Configration

SDK must be configured with LogicMonitor.DataSDK Configuration class. 
While using LMv1 authentication set AccessID and AccessKey properties, In Case of BearerToken Authentication set Bearer Token property.Company's name or Account name <b> must </b> be passed to Company property.

<a name="Model"></a>
## Model
- Resource

```csharp
Resource resource = new Resource(Ids,Name,Description,Properties,Create);
```
Ids(Dictonary<string,string>): An Dictionary of existing resource properties that will be used to identify the resource. See Managing Resources 
that Ingest Push Metrics for information on the types of properties that can be used. If no resource is matched and the 
create parameter is set to TRUE, a new resource is created with these specified resource IDs set on it. If the 
system.displayname and/or system.hostname property is included as resource IDs, they will be used as host name and 
display name respectively in the resulting resource.

Name(string): Resource unique name. Only considered when creating a new resource.

Properties(Dictonary<string,string>) : New properties for resource. Updates to existing resource properties are not considered. Depending on the property name,
we will convert these properties into system, auto, or custom properties.

Description(string):  Resource description. Only considered when creating a new resource.

Create(bool): Do you want to create the resource.

- DataSource
```csharp
DataSource dataSource = new DataSource(DataSourceName,DataSourceGroup,DisplayName,Id );
```
Name(string):  DataSource unique name. Used to match an existing DataSource. If no existing DataSource matches the name provided
here, a new DataSource is created with this name.

DisplayName(string): DataSource display name. Only considered when creating a new DataSource.

Group(string): DataSource group name. Only considered when DataSource does not already belong to a group. Used to organize the
DataSource within a DataSource group. If no existing DataSource group matches, a new group is created with this name 
and the DataSource is organized under the new group.

Id(int): DataSource unique ID. Used only to match an existing DataSource. If no existing DataSource matches the provided ID, 
an error results.


- DatasourceInstance
```csharp
DataSourceInstance dataSourceInstance = new DataSourceInstance(Name,DisplayName,Description,Properties);
```
Name(string): Instance name. If no existing instance matches, a new instance is created with this name.

DisplayName(string): Instance display name. Only considered when creating a new instance.

Properties(Dictionary<string,string>): New properties for instance. Updates to existing instance properties are not considered. Depending on the 
property name, we will convert these properties into system, auto, or custom properties.

Description(string):  Resource description. Only considered when creating a new resource.

- DataPoint
```csharp
DataPoint dataPoint = new DataPoint(Name,Description,AggregationType,Description);
```
Name(string): Datapoint name. If no existing datapoint matches for specified DataSource, a new datapoint is created with this 
name.

AggreationType(string):The aggregation method, if any, that should be used if data is pushed in sub-minute intervals. Allowed options are 
“sum”, “average” and “none”(default) where “none” would take last value for that minute. 
Only considered when creating a new datapoint. See the About the Push Metrics REST API section of this guide for more 
information on datapoint value aggregation intervals.

Description(string): Datapoint description. Only considered when creating a new datapoint.

Type(string): Metric type as a number in string format. Allowed options are “guage” (default) and “counter”. Only considered 
when creating a new datapoint.

- Value
```csharp
Dictionary<string,string> value = new Dictionary<string,string>();
```
Value is a dictionary which stores the time of data emittion(in epoch) as Key of Dictionary and Metrics data as Value of Dictionary.

<a name="getting-started"></a>
## Getting Started

```csharp
using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using LogicMonitor.DataSDK.Api;
using LogicMonitor.DataSDK;
using RestSharp;
namespace IncludeDll
{
    class Program 
    {
        static void Main(string[] args)
        {
            
            string resourceName = "SampleDevice.net";
            Dictionary<string, string> resourceIds = new Dictionary<string, string>();
            resourceIds.Add("system.displayname","SampleDevice1");
            resourceIds.Add("some.custom.property", "value");
            string dataSourceName = "MBM";
            string dataSourceGroup = "123";
            string saname = "Instance1";
            string datapointname = "datapoint1";

            MyResponse responseInterface = new MyResponse();

            ApiClient apiClient = new ApiClient(configuration);

            Metrics metrics = new Metrics(batch:false,interval:0,responseInterface, apiClient);

            Resource resource = new Resource(name: resourceName,ids:resourceIds);
            DataSource dataSource = new DataSource( Name:dataSourceName, Group: dataSourceGroup);
            DataSourceInstance dataSourceInstance = new DataSourceInstance(name: saname);
            DataPoint dataPoint = new DataPoint(name:datapointname);
            Dictionary<string, string> value = new Dictionary<string, string>();
            
            string epochTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            string metricData = "4212";
            value.Add(epochTime, metricData);
            metrics.SendMetrics(resource: resource, dataSource: dataSource, dataSourceInstance: dataSourceInstance, dataPoint: dataPoint, values: value);

            
        }

    }
    
}
```

<a name="documentation-for-api-endpoints"></a>
## Documentation for API Endpoints

All URIs are relative to *https://AccountName.logicmonitor.com/rest*


<a name="documentation-for-authorization"></a>
## Documentation for Authorization

<a name="LMv1"></a>
### LMv1

- **Type**: API key
- **API key parameter name**: Authorization
- **Location**: HTTP header

