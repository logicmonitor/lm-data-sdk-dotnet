/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */
using System;
using System.Collections.Generic;
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

        public Logs(bool batch, int interval = default, IResponseInterface responseCallback = default,
            ApiClient apiClient = default) : base(apiClient: apiClient, interval: interval, batch: batch, responseCallback: responseCallback)
        {

        }
        /// <summary>
        /// </summary>
        /// <param name="message">Log Message.</param>
        /// <param name="resource">Resource object.</param>
        public RestResponse SendLogs(string message, Resource resource, Dictionary<string, string> metadata = default,string timestamp=default)
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
                return Send(body);
            }
        }

        public string SingleRequest(LogsV1 logsV1)
        {
            var bodyString = CreateLogBody(logsV1);
            List<string> logsV1s = new List<string>();
            logsV1s.Add(bodyString);
            var body = SerializeList(logsV1s);
            return body;
        }

        public override void _doRequest()
        {
            List<string> logsV1s = new List<string>();
            foreach (var item in logPayloadCache)
            {
              var bodystring = CreateLogBody(item);
              logsV1s.Add(bodystring);
            }
            logPayloadCache.Clear();
            var body = SerializeList(logsV1s);
            Send(body);

        }
        public string SerializeList(List<string> list)
        {
          var body = Newtonsoft.Json.JsonConvert.SerializeObject(list);
          body = body.Replace(@"\", "");
          body = body.Replace("\"{", "{");
          body = body.Replace("}\"", "}");
          return body;

        }
        public RestResponse Send(string body)
        {
          BatchingCache b = new Logs();
          return b.MakeRequest(path: Constants.Path.LogIngestPath, method: "POST", body: body);
        }
        public override void _mergeRequest()
        {
            List<LogsV1> v1s = new List<LogsV1>();

            logPayloadCache.Add((LogsV1)rawRequest.Dequeue());
        }

        public string CreateLogBody(LogsV1 item)
        {
            Dictionary<string, string> Body = new Dictionary<string, string>();
            Body.Add("message", item.Message);
            Body.Add("_lm.resourceId", Newtonsoft.Json.JsonConvert.SerializeObject(item.ResourceId));
            Body.Add("timestamp", item.Timestamp);
            Body.Add("metadata", Newtonsoft.Json.JsonConvert.SerializeObject(item.MetaData));
            var bodyString = Newtonsoft.Json.JsonConvert.SerializeObject(Body);
            return bodyString;
        }

    }
}