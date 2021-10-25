/*
 * Copyright, 2021, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using LogicMonitor.DataSDK.Internal;
using LogicMonitor.DataSDK.Model;

namespace LogicMonitor.DataSDK.Api
{
    /// <summary>
    /// This Class is used to Send Metrics.This class is used by user to interact with LogicMonitor. 
    /// </summary>
    public class Metrics : BatchingCache
    {

        public static readonly object _lock;
        public Metrics():base()
        {
            
        }
        public Metrics(bool batchs= default, int intervals= default, IResponseInterface responseCallbacks=default,ApiClients apiClients=default):base(apiClient:apiClients, interval:intervals, batch:batchs, responseCallback:responseCallbacks)
        {
            

        }
        /// <summary>
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataSourceInstance"></param>
        /// <param name="dataPoint"></param>
        /// <param name="values"></param>
        public void SendMetrics(Resource resource, DataSource dataSource, List<DataSourceInstance> dataSourceInstance, List<DataPoint> dataPoint, Dictionary<string,string> values)
        {
            if (Batch == true)
            {
                var dataPoints = new List<RestDataPointV1>();
                foreach (var item in dataPoint)
                {
                    var restDataPoint = new RestDataPointV1(
                    dataPointAggregationType: item.AggregationType,
                    dataPointDescription: item.Description,
                    dataPointName: item.Name,
                    dataPointType: item.Type,
                    values: values
                    );
                    dataPoints.Add(restDataPoint);
                }


                var instances = new List<RestDataSourceInstanceV1>();
                foreach (var item in dataSourceInstance)
                {
                    var restInstance = new RestDataSourceInstanceV1(
                    dataPoints: dataPoints,
                    instanceDescription: item.Description,
                    instanceDisplayName: item.DisplayName,
                    instanceName: item.Name,
                    instanceProperties: item.Properties
                    );
                    instances.Add(restInstance);
                }
                var restMetrics = new RestMetricsV1(
                    resourceIds: resource.Ids,
                    resourceName: resource.Name,
                    resourceProperties: resource.Properties,
                    resourceDescription: resource.Description,
                    dataSource: dataSource.Name,
                    dataSourceDisplayName: dataSource.DisplayName,
                    dataSourceGroup: dataSource.Group,
                    dataSourceId: dataSource.Id,
                    instances: instances
                    );

                string body = Newtonsoft.Json.JsonConvert.SerializeObject(restMetrics);
                AddRequest(body:body, path: "/metric/ingest");
            }
            else
                SingleRequest(resource, dataSource, dataSourceInstance, dataPoint, values);
        }

        public static void SingleRequest(Resource resource, DataSource dataSource, List<DataSourceInstance> dataSourceInstance, List<DataPoint> dataPoint, Dictionary<string, string> values)
        {
           
            var dataPoints = new List<RestDataPointV1>();
            foreach(var item in dataPoint)
            {
                var restDataPoint = new RestDataPointV1(
                dataPointAggregationType: item.AggregationType,
                dataPointDescription: item.Description,
                dataPointName: item.Name,
                dataPointType: item.Type,
                values: values
                );
                dataPoints.Add(restDataPoint);
            }
            

            var instances = new List<RestDataSourceInstanceV1>();
            foreach(var item in dataSourceInstance)
            {
                var restInstance = new RestDataSourceInstanceV1(
                dataPoints: dataPoints,
                instanceDescription: item.Description,
                instanceDisplayName: item.DisplayName,
                instanceName: item.Name,
                instanceProperties: item.Properties
                );
                instances.Add(restInstance);
            }
            var restMetrics = new RestMetricsV1(
                resourceIds: resource.Ids,
                resourceName: resource.Name,
                resourceProperties: resource.Properties,
                resourceDescription: resource.Description,
                dataSource: dataSource.Name,
                dataSourceDisplayName: dataSource.DisplayName,
                dataSourceGroup: dataSource.Group,
                dataSourceId: dataSource.Id,
                instances:instances
                );
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(restMetrics);

            MakeRequest(path: "/metric/ingest", method: "POST", body: body, create: resource.Create, asyncRequest: false);
            
        }

    }
}
