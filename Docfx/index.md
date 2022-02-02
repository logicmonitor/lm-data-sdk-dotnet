# .Net SDK for LogicMonitor 
This SDK is for ingestion of the metrics into LogicMonitor platform.
## PushMetrics - Metrics Ingestion
## Overview
LogicMonitor’s Push Metrics feature allows you to send metrics directly to the LogicMonitor platform via a dedicated API, removing the need to route the data through a LogicMonitor Collector. Once ingested, these metrics are presented alongside all other metrics gathered via LogicMonitor, providing a single pane of glass for metric monitoring and alerting.


## Quick Start Notes:

1. Install the LogicMonitor.DataSDK [NuGet package](https://www.nuget.org/packages/Logicmonitor.DataSDK/) into your project.
```csharp
Install-Package Logicmonitor.DataSDK -Version 0.0.7-alpha
```
OR
```csharp
dotnet add package Logicmonitor.DataSDK --version 0.0.7-alpha
```

2.Include all required namespace.

```csharp
using LogicMonitor.DataSDK;
using LogicMonitor.DataSDK.Api;
using LogicMonitor.DataSDK.Model;
```

3.Read the Examples and Api Documentation.

4.See the project repository at [GitHub](https://github.com/logicmonitor/lm-data-sdk-dotnet).