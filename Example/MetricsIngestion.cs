/*
 * Copyright, 2021, LogicMonitor, Inc.
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

namespace PerfMon
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
            string dataSourceGroup = "DSG";
            var dataSourceName= "dotnet";
            var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;
            string CpuUsage = "cpuUsage";
           

            MyResponse responseInterface = new MyResponse();

            Authenticate authenticate = new Authenticate();
            authenticate.Id = Environment.GetEnvironmentVariable("id");
            authenticate.Key = Environment.GetEnvironmentVariable("key");
            authenticate.Type = Environment.GetEnvironmentVariable("type");
            Configuration configuration = new Configuration(company: Environment.GetEnvironmentVariable("company"), authentication: authenticate);
            ApiClients apiClients = new ApiClients(configuration);

            Resource resource = new Resource(name: resourceName, ids: resourceIds, create: true);
            DataSource dataSource = new DataSource(Name: dataSourceName, Group: dataSourceGroup);
            DataSourceInstance dataSourceInstance = new DataSourceInstance(name: InstanceName);

            DataPoint dataPoint = new DataPoint(name: CpuUsage.ToString());
            Dictionary<string, string> CpuUsageValue = new Dictionary<string, string>();


            Metrics metrics1 = new Metrics(batchs: false, intervals: 0, responseInterface, apiClients);

            while (true)
            {
                CpuUsageValue.Add(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), (cpuUsedMs / (Environment.ProcessorCount * totalMsPassed)).ToString());
                metrics1.SendMetrics(resource: resource, dataSource: dataSource, dataSourceInstance: dataSourceInstance, dataPoint: dataPoint, values: CpuUsageValue);
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