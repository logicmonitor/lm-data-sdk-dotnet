/*
 * Copyright, 2021, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Internal;
using LogicMonitor.DataSDK.Model;

namespace LogicMonitor.DataSDK.Api
{
    public class Logs : BatchingCache
    {
        public Logs() : base()
        {

        }

        public Logs(bool batchs = default, int intervals = default, IResponseInterface responseCallbacks = default,
            ApiClients apiClients = default) : base(apiClient: apiClients, interval: intervals, batch: batchs, responseCallback: responseCallbacks)
        {

        }
        /// <summary>
        /// </summary>
        /// <param name="message">Log Message.</param>
        /// <param name="resource">Resource object.</param>
        public void SendLogs(string message, Resource resource,Dictionary<string,string> metadata = default)
        {
            LogsV1 logs = new LogsV1(message: message, resourceIds: resource.Ids, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),metaData:metadata);


            if (Batch == true)
            {
                AddRequest(logs.ToString(),"/log/ingest");
                
            }
            else
                SingleRequest(logs);
        }

        public void SingleRequest(LogsV1 logs)
        {
            MakeRequest(path: "/log/ingest", method: "POST",body:logs.ToString(), asyncRequest: false);
        }
    }
}
