/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using System.Diagnostics;
using System;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using LogicMonitor.DataSDK.Api;
using LogicMonitor.DataSDK;
using RestSharp;
using System.Net;
using System.Threading;

namespace Example
{
    class Program
    {
       
        static void Main(string[] args)
        {

            var resourceName = Dns.GetHostName();
            var CProcess= Process.GetCurrentProcess();
            
            Dictionary<string, string> resourceIds = new Dictionary<string, string>();
            resourceIds.Add("system.displayname", resourceName.ToString());

            var InstanceName = "Instance";
            string dataSourceGroup = "Data Source Group";
            var dataSourceName= "dotnet";
            var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var endTime = DateTime.UtcNow;
            var startTime = DateTime.UtcNow;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;
            string CpuUsage = "cpuUsage";
           

            MyResponse responseInterface = new MyResponse();
        
            string yourCompany = "YourCompanyName";
            //For LMv1 authentication use Following variables.
            string yourAccessID = "YourAccessID";
            string yourAccessKey= "YourAccessKey";
            Configuration configuration = new configuration(yourCompany, yourAccessID, yourAccessKey);

            //For Bearer authentication use Following.
            // string myBearerToken = "YourBearerToken";
            // Configuration configuration = new configuration(yourCompany, yourBearerToken);

            ApiClient apiClient = new ApiClient(configuration);

            Metrics metrics = new Metrics(batch: false, interval: 0, responseInterface, apiClient);

            Resource resource = new Resource(name: resourceName, ids: resourceIds, create: true);
            DataSource dataSource = new DataSource(Name: dataSourceName, Group: dataSourceGroup);
            DataSourceInstance dataSourceInstance = new DataSourceInstance(name: InstanceName);

            DataPoint dataPoint = new DataPoint(name: CpuUsage);
            Dictionary<string, string> CpuUsageValue = new Dictionary<string, string>();

            while (true)
            {
                string epochTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                string cpuUsageMetric = (cpuUsedMs / (Environment.ProcessorCount * totalMsPassed)).ToString();
                
                CpuUsageValue.Add(epochTime, cpuUsageMetric);
                metrics.SendMetrics(resource: resource, dataSource: dataSource, dataSourceInstance: dataSourceInstance, dataPoint: dataPoint, values: CpuUsageValue);
                Thread.Sleep(60 * 1000);
            }

        }
        
        class MyResponse : IResponseInterface
        {
            public void ErrorCallback(RestResponse response)
            {
                Console.WriteLine("Custom message for ErrorCallback");
            }

            public void SuccessCallback(RestResponse response)
            {
                Console.WriteLine("Custom message for SuccessCallback");
            }
        }
    }
}
