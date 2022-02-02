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
- [Microsoft.Extenstion.Logging](https://www.nuget.org/packages/Microsoft.Extensions.Logging/) - 5.0.0 or later
- [Microsoft.Extenstion.Hosting](https://www.nuget.org/packages/Microsoft.Extensions.Hosting/) - 5.0.0 or later

<a name = "Single Log Ingestion"></a>
## Single Log Ingestion.

SDK must be configured with LogicMonitor.DataSDK Configuration class. 
While using LMv1 authentication set AccessID and AccessKey properties, In Case of BearerToken Authentication set Bearer Token property.Company's name or Account name <b>must</b> be passed to Company property.All properties can be set using environment variable.

For Log ingestion, log message has to be passed along the resource object to idetify the resource.Create a resource object using LogicMonitor.DataSDK.Models namespace.

```csharp
ApiClient apiClient = new ApiClient();

Logs logs = new Logs(batch: false, interval: 0, responseCallback: responseInterface, apiClient: apiClient);

Resource resource = new Resource(name: resourceName.ToString(), ids: resourceIds, create: true);
string msg =  "Program function  has CPU Usage " + (cpuUsedMs / (Environment.ProcessorCount * totalMsPassed)).ToString()+" Milliseconds";

logs.SendLogs(message: msg, resource: resource);
```

<a name="Model"></a>
## Model

- Resource

```csharp
Resource resource = new Resource(Ids,Name,Description,Properties,Create);
```

<b>Ids(Dictonary<string,string>):</b> An Dictionary of existing resource properties that will be used to identify the resource. See Managing Resources 
that Ingest Push Metrics for information on the types of properties that can be used. If no resource is matched and the 
create parameter is set to TRUE, a new resource is created with these specified resource IDs set on it. If the 
system.displayname and/or system.hostname property is included as resource IDs, they will be used as host name and 
display name respectively in the resulting resource.

<b>Name(string):</b> Resource unique name. Only considered when creating a new resource.

<b>Properties(Dictonary<string,string>):</b> New properties for resource. Updates to existing resource properties are not considered. Depending on the property name,
we will convert these properties into system, auto, or custom properties.

<b>Description(string):</b>  Resource description. Only considered when creating a new resource.

- Message
<br>
Message is string which contains the message that is used to be logged and send to  LM site.

- Metadata

Metadata is dictionary which can used to ingest metadata. It can be viewd on LM site along with the logs and can we logged message.

- Timestamp
Log generated time in Date Time format. 

<a name="documentation-for-api-endpoints"></a>
## Documentation for API Endpoints

All URIs are relative to *https://AccountName.logicmonitor.com/rest*
