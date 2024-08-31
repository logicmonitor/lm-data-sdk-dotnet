/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using LogicMonitor.DataSDK.Internal;
using LogicMonitor.DataSDK.Model;
using Newtonsoft.Json;
using RestSharp;

namespace LogicMonitor.DataSDK.Api
{

    /// <summary>
    /// This Class is used to Send Logs.This class is used by user to interact with LogicMonitor.
    /// </summary>
    public class Logs : BatchingCache
    {
        public Logs() : base()
        {

        }

        public Logs(bool batch =true, int interval = 10, IResponseInterface responseCallback = default,
            ApiClient apiClient = default) : base(apiClient: apiClient, interval: interval, batch: batch, responseCallback: responseCallback)
        {

        }
        /// <summary>
        /// </summary>
        /// <param name="message">Log Message.</param>
        /// <param name="resource">Resource object.</param>
        public async Task<RestResponse> SendLogs(string message, Resource resource, Dictionary<string, string> metadata = default,string timestamp=default)
        {
            LogsV1 logs = new LogsV1(message: message, resourceIds: resource.Ids, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), metaData: metadata);
            
            if (Batch)
            {
                AddRequest(logs);
                return null;
            }
            else
            {
                var body= SingleRequest(logs);

                var response = await Send(body);
                base.ResponseHandler(response);
                return response;
            }
        }

        public string SingleRequest(LogsV1 logsV1)
        {
            var bodyString = JsonConvert.SerializeObject(logsV1);
            List<string> logsV1s = new List<string>();
            logsV1s.Add(bodyString);
            var body = SerializeList(logsV1s);
            return body;
        }

        public override async Task _doRequest()
        {
            List<string> logsV1s = new List<string>();
            foreach (var item in logPayloadCache)
            {
              var bodystring = JsonConvert.SerializeObject(item);
              logsV1s.Add(bodystring);
            }
            logPayloadCache.Clear();
            if (logsV1s.Count > 0)
            {
                var body = SerializeList(logsV1s);
                var response = await Send(body);
                base.ResponseHandler(response: response);
                //Console.WriteLine("Res[pnese");

            }

        }
        public string SerializeList(List<string> list)
        {

            string body = "[";
            foreach (var item in list)
            {
                body += item.ToString() + ",";
                
            }
            body = body.Substring(0, body.Length - 1);
            body += "]";
            return body;

        }
        public async Task<RestResponse> Send(string body)
        {
          return await base.MakeRequest(path: Constants.Path.LogIngestPath, method: "POST", body: body);
        }
        public override void _mergeRequest()
        {
            List<LogsV1> v1s = new List<LogsV1>();
            LogsV1 currentLog = (LogsV1)rawRequest.Dequeue();

            int singleRequestSize = currentLog.ToString().Length;
            int logPayloadCacheSize = logPayloadCache.ToString().Length;
            int currentSize = singleRequestSize + logPayloadCacheSize;
            int gzipSize = Rest.GZip(logPayloadCache.ToString() + currentLog.ToString()).Length;

            if( (currentSize > Constants.SizeLimitation.MaximumLogPayloadSize ) ||
                (gzipSize > Constants.SizeLimitation.MaximumMetricsPayloadSizeOnCompression && ApiClient.configuration.GZip) )
            {
                rawRequest.Enqueue(currentLog);
                DoRequest();
            }
            logPayloadCache.Add(currentLog);


        }

        
        
    }
}