/*
 * Copyright, 2021, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using System;
using LogicMonitor.DataSDK.Model;
using LogicMonitor.DataSDK.Api;
using LogicMonitor.DataSDK;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using RestSharp;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)

        {
            var resourceName = Dns.GetHostName();
            var temp = Process.GetCurrentProcess();
            var startTime = DateTime.UtcNow;
            var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var endTime = DateTime.UtcNow;
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;

            Authenticate authenticate = new Authenticate();
            authenticate.Id = Environment.GetEnvironmentVariable("id");
            authenticate.Key = Environment.GetEnvironmentVariable("api_key");
            authenticate.Type = Environment.GetEnvironmentVariable("LMv1");
            Configuration configuration = new Configuration(company: Environment.GetEnvironmentVariable("company"), authentication: authenticate);

            ApiClients apiClients = new ApiClients(configuration);
            
            Dictionary<string, string> resourceIds = new Dictionary<string, string>();
            resourceIds.Add("system.displayname", resourceName.ToString());
            MyResponse responseInterface = new MyResponse();
            Resource resource = new Resource(name: resourceName.ToString(), ids: resourceIds, create: true);

            Logs logs = new Logs(batchs: false, intervals: 0, responseCallbacks: responseInterface, apiClients: apiClients);
            string msg =  "Program function  has CPU Usage " + (cpuUsedMs / (Environment.ProcessorCount * totalMsPassed)).ToString()+" Milliseconds";
            logs.SendLogs(message: msg, resource: resource);

        }
    }
    class MyResponse : IResponseInterface
    {
        public void ErrorCallback(RestResponse response)
        {
            Console.WriteLine("Custom message for ErrorCallback");
        }

        public void SuccessCallback( RestResponse response)
        {
            Console.WriteLine("Custom message for SuccessCallback");
        }
    }
}