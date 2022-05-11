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
                return SingleRequest(logs);
            }
        }

        public RestResponse SingleRequest(LogsV1 logsV1)
        {
            Dictionary<string, string> Body = new Dictionary<string, string>();
            Body.Add("message", logsV1.Message);
            Body.Add("_lm.resourceId", JsonConvert.SerializeObject(logsV1.ResourceId));
            Body.Add("timestamp", logsV1.Timestamp);
            Body.Add("metadata", JsonConvert.SerializeObject(logsV1.MetaData));

            var bodyString = JsonConvert.SerializeObject(Body);


            List<string> logsV1s = new List<string>();
            logsV1s.Add(bodyString);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(logsV1s);
            body = body.Replace(@"\", "");
            body = body.Replace("\"{", "{");
            body = body.Replace("}\"", "}");
            BatchingCache b = new Logs();

            return b.MakeRequest(path: "/log/ingest", method: "POST", body:body, asyncRequest: false);
        }

        public override void _doRequest()
        {
            var list = CreateLogBody();
            var body1 = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            body1 = body1.Replace(@"\", "");
            body1 = body1.Replace("\"{", "{");
            body1 = body1.Replace("}\"", "}");
            BatchingCache b = new Logs();
            RestResponse response= b.MakeRequest(path: "/log/ingest", method: "POST", body: body1);
        }

        public override void _mergeRequest()
        {
            List<LogsV1> v1s = new List<LogsV1>();

            logPayloadCache.Add((LogsV1)rawRequest.Dequeue());
        }

        private List<string> CreateLogBody()
        {
            string bodyString =null;

            List<string> logsV1s = new List<string>();
            foreach (var item in logPayloadCache)
            {
                Dictionary<string, string> Body = new Dictionary<string, string>();
                Body.Add("message", item.Message);
                Body.Add("_lm.resourceId", JsonConvert.SerializeObject(item.ResourceId));
                Body.Add("timestamp", item.Timestamp);
                Body.Add("metadata", JsonConvert.SerializeObject(item.MetaData));

                bodyString = JsonConvert.SerializeObject(Body);

                logsV1s.Add(bodyString);
            }
            logPayloadCache.Clear();
            return logsV1s;
        }

    }
}