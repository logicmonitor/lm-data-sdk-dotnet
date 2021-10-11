using System;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Internal;
using LogicMonitor.DataSDK.Model;
using RestSharp;

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
        public RestResponse SendLogs(string message, Resource resource, Dictionary<string, string> metadata = default)
        {
            LogsV1 logs = new LogsV1(message: message, resourceIds: resource.Ids, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), metaData: metadata);


            if (Batch == true)
            {
                AddRequest(logs.ToString(), "/log/ingest");
                return null;
            }
            else
                return SingleRequest(logs);
        }

        public RestResponse SingleRequest(LogsV1 logs)
        {
           return MakeRequest(path: "/log/ingest", method: "POST", body: logs.ToString(), asyncRequest: false);
        }
    }
}