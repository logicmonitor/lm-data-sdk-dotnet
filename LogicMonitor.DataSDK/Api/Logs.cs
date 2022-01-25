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

        public Logs(bool batch , int interval = default, IResponseInterface responseCallback = default,
            ApiClient apiClient = default) : base(apiClient: apiClient, interval: interval, batch: batch, responseCallback: responseCallback)
        {

        }
        /// <summary>
        /// </summary>
        /// <param name="message">Log Message.</param>
        /// <param name="resource">Resource object.</param>
        public RestResponse SendLogs(string message, Resource resource,Dictionary<string,string> metadata = default)
        {
            LogsV1 logs = new LogsV1(message: message, resourceIds: resource.Ids, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), metaData: metadata);


            if (Batch)
            {
                AddRequest(logs.ToString(), "/log/ingest");
                return null;
            }
            else
                return SingleRequest(logs);
        }

        public RestResponse SingleRequest(LogsV1 logs)
        {
            BatchingCache b = new BatchingCache();

            return b.MakeRequest(path: "/log/ingest", method: "POST", body: logs.ToString(), asyncRequest: false);
        }
    }
}