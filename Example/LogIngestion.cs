/*
 * Copyright, 2022, LogicMonitor, Inc.
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

namespace Example
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

            Dictionary<string, string> resourceIds = new Dictionary<string, string>();
            resourceIds.Add("system.displayname", resourceName.ToString());
            MyResponse responseInterface = new MyResponse();

            //Pass the Authenticate Variables as Enviroment variable.
            ApiClient apiClient = new ApiClient();
            Logs logs = new Logs(batch: false, interval: 0, responseCallback: responseInterface, apiClient: apiClient);

            Resource resource = new Resource(name: resourceName.ToString(), ids: resourceIds, create: true);

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
