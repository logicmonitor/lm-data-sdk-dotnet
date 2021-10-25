/*
 * Copyright, 2021, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using LogicMonitor.DataSDK;
using LogicMonitor.DataSDK.Api;
using RestSharp;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace DemoDataSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            string Format = "json";
            string Interval = "1min";
            int OutputSize = 15;
            string symbol = "zs";
            string ApiKey = "";//twelvedata.com api key.
            
            //Receiving data from twelvedata.com 
            //This will act as datasource which will be send data to server.
            string url = string.Format("https://api.twelvedata.com/time_series?symbol={0}&format={1}&interval={2}&outputsize={3}&apikey={4}",
                symbol
                , Format
                , Interval
                , OutputSize
                , ApiKey);
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            RestResponse response = (RestResponse)client.Execute(request);
            JObject data = JObject.Parse(response.Content);
            JArray values = (JArray)data["values"];
            var meta = data["meta"];

            //Enter Account details using `export id=  api_key="" type=LMv1 Lm_company=`
            Authenticate authenticate = new Authenticate();
            authenticate.Id = Environment.GetEnvironmentVariable("id");
            authenticate.Key = Environment.GetEnvironmentVariable("api_key");
            authenticate.Type = Environment.GetEnvironmentVariable("type");
            Configuration configuration = new Configuration(company: Environment.GetEnvironmentVariable("Lm_company"), authentication: authenticate);

            ApiClients apiClients = new ApiClients(configuration);


            string resourceName = meta.SelectToken("exchange").ToString();
            string dataSourceName = meta.SelectToken("symbol").ToString();
            string dataSourceGroupName = meta.SelectToken("type").ToString();
            string dataSouceInstanceName = meta.SelectToken("currency").ToString();
            Dictionary<string, string> resourceIds = new Dictionary<string, string>();
            resourceIds.Add("system.displayname", "MarketMetrics");
            
            MyResponse responseInterface = new MyResponse();
            Resource resource = new Resource(name: resourceName, ids: resourceIds, create: true);
            DataSource dataSource = new DataSource(Name: dataSourceName, Group: dataSourceGroupName);
            DataSourceInstance dataSourceInstance = new DataSourceInstance(name: dataSouceInstanceName);
            DataPoint open = new DataPoint(name: "Open");
            Dictionary<string, string> openValue = new Dictionary<string, string>();

            Metrics metrics = new Metrics(batchs: false, intervals: 0, apiClients: apiClients);

            foreach (var item in values)
            {

                openValue.Add(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), item.SelectToken("open").ToString());
                metrics.SendMetrics(resource: resource, dataSource: dataSource, dataSourceInstance: dataSourceInstance, dataPoint: open, values: openValue);
                Thread.Sleep(1000 * 60);
                openValue.Clear();
            }

            Console.ReadLine();
            
        }
    }

    
    class MyResponse : IResponseInterface
    {
       public void ErrorCallback(RestResponse response)
       {
           Console.WriteLine("Custom ErrorCallback");
       }

       public void SuccessCallback(RestResponse response)
       {
           Console.WriteLine("Custom SuccessCallback");
       }
    }
}
