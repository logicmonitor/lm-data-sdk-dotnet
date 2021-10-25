/*
 * Copyright, 2021, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LogicMonitor.DataSDK.Model;
using LogicMonitor.DataSDK.Api;
using LogicMonitor.DataSDK;
using RestSharp;
using System.IO;
using Serilog;

namespace StockMarketData
{
    class Program
    {
        public static Resource resource;
        public static ApiClients apiClients;
        public static MyResponse responseInterface;

        public Program()
        {

            Dictionary<string, string> resourceIds = new Dictionary<string, string>();
            resourceIds.Add("system.displayname", "MarketData");
            resource = new Resource(name: "MarketData", ids: resourceIds, create: true);

            responseInterface = new MyResponse();

            //Enter Account details using `export id=  api_key="" type=LMv1 Lm_company=`
            Authenticate authenticate = new Authenticate();
            authenticate.Id = Environment.GetEnvironmentVariable("id");
            authenticate.Key = Environment.GetEnvironmentVariable("api_key");
            authenticate.Type = Environment.GetEnvironmentVariable("type");
            Configuration configuration = new Configuration(company: "lmaakashkhopade", authentication: authenticate);
            apiClients = new ApiClients(configuration); 
        }
        public  void ReadLog()
        {
            Logs logs = new Logs(batchs:false,intervals:0,responseCallbacks:responseInterface,apiClients:apiClients);

            var reseteevnt = new AutoResetEvent(false);
            var watcher = new FileSystemWatcher(".");
            watcher.Filter = "/Users/aakashkhopade/Desktop/logfile.txt";
            watcher.EnableRaisingEvents = true;
            watcher.Changed += (s, e) => reseteevnt.Set();
            int count = 0;
            var fs = new FileStream("/Users/aakashkhopade/Desktop/logfile.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var sr = new StreamReader(fs))
            {
                Thread.Sleep(1000 * 60);
                var s = "";
                while (true)
                {
                    //Console.WriteLine("Searching");
                    s = sr.ReadLine();
                    if (s != null)
                    {
                        //Console.WriteLine(s);
                        logs.SendLogs(message: s, resource: resource);

                    }
                    else
                    {
                        reseteevnt.WaitOne(1000*60);
                    }
                }
            }
        }

        public static void WriteLog(string message)
        {

            Log.Information(message);
        }
        public static void Main(string[] args)
        {

            Program program = new Program();
            Thread thread = new Thread(program.ReadLog);
            thread.IsBackground = true;
            thread.Start();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("/Users/aakashkhopade/Desktop/logfile.txt")
            .CreateLogger();

            var Symbols = new List<string>();
            Symbols.Add("MSFT");
            Symbols.Add("AAPL");
            string ApiKey = "";//twelvedata.com api key.
            string Format   = "json";
            string Interval = "1min";
            int OutputSize  = 30;
            string symbol   = "AaPL";


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
            Console.WriteLine(data.ToString());

            string resourceName = meta.SelectToken("exchange").ToString();
            string dataSourceName = meta.SelectToken("symbol").ToString();
            string dataSourceGroupName = meta.SelectToken("type").ToString();
            string dataSouceInstanceName = meta.SelectToken("currency").ToString();

            //Create Data Source and Data Source Instance
            DataSource dataSource = new DataSource(Name: dataSourceName, Group: dataSourceGroupName);
            DataSourceInstance dataSourceInstance = new DataSourceInstance(name: dataSouceInstanceName);

            //Creating Data Points open, hign, low and close
            DataPoint open = new DataPoint(name: "Open");
            DataPoint high = new DataPoint(name: "High");
            DataPoint low = new DataPoint(name: "Low");
            DataPoint close = new DataPoint(name: "close");

           
            //Creating Data Point values
            Dictionary<string, string> highValue = new Dictionary<string, string>();
            Dictionary<string, string> lowValue = new Dictionary<string, string>();
            Dictionary<string, string> closeValue = new Dictionary<string, string>();
            Dictionary<string, string> openValue = new Dictionary<string, string>();


            double previous;
            double current = 0;

            Metrics metrics1 = new Metrics(batchs: false, intervals: 0, responseInterface, apiClients);
            var count = 0;
            foreach (var item in values)
            {

                //Storing Data value and its epoch time 
                highValue.Add(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), item.SelectToken("high").ToString());
                lowValue.Add(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), item.SelectToken("low").ToString());
                openValue.Add(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), item.SelectToken("open").ToString());
                closeValue.Add(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), item.SelectToken("close").ToString());

                //Generating Logs
                previous = Convert.ToDouble(item.SelectToken("close"));
                if (current == 0)
                    WriteLog(string.Format(" Monitoring Stock: {0} ", dataSourceName));
                else if (previous - current > 0)
                    WriteLog(string.Format(" The {0} is Bearish in {1} inteval.",dataSourceName,Interval));
                else if (previous - current < 0)
                    WriteLog(string.Format(" The {0} is Bullish in {1} inteval.", dataSourceName, Interval));
                else
                    WriteLog(string.Format(" The {0} is Consoladating in {1} inteval.", dataSourceName, Interval));

                current = previous;
                count++;
                // Send Metrics every 3 minutes
                if (count == 3)
                {
                    // Call LM Server using SDK.
                    metrics1.SendMetrics(resource: resource, dataSource: dataSource, dataSourceInstance: dataSourceInstances, dataPoint: open, values: openValue);
                    metrics1.SendMetrics(resource: resource, dataSource: dataSource, dataSourceInstance: dataSourceInstances, dataPoint: high, values: highValue);
                    metrics1.SendMetrics(resource: resource, dataSource: dataSource, dataSourceInstance: dataSourceInstances, dataPoint: low, values: lowValue);
                    metrics1.SendMetrics(resource: resource, dataSource: dataSource, dataSourceInstance: dataSourceInstances, dataPoint: close, values: closeValue);
                    count = 0;

                    highValue.Clear();
                    lowValue.Clear();
                    closeValue.Clear();
                    openValue.Clear();

                }
                    Thread.Sleep(1000 * 60);


            }

        }
    }

    class MyResponse : IResponseInterface
    {
        public void ErrorCallback(RestResponse response)
        {
            Console.WriteLine("ErrorCallback");
        }

        public void SuccessCallback(RestResponse response)
        {
            Console.WriteLine("SuccessCallback");

        }
    }
}
