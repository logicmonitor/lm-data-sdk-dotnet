using System.Collections.Generic;
using LogicMonitor.DataSDK.Internal;
using LogicMonitor.DataSDK.Model;
using RestSharp;

namespace LogicMonitor.DataSDK.Api
{
    /// <summary>
    /// This Class is used to Send Metrics.This class is used by user to interact with LogicMonitor. 
    /// </summary>
    public class Metrics : BatchingCache
    {

        public static readonly object _lock;
        public Metrics() : base()
        {

        }
        public Metrics(bool batchs = default, int intervals = default, IResponseInterface responseCallbacks = default, ApiClients apiClients = default) : base(apiClient: apiClients, interval: intervals, batch: batchs, responseCallback: responseCallbacks)
        {


        }
        /// <summary>
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataSourceInstance"></param>
        /// <param name="dataPoint"></param>
        /// <param name="values"></param>
        public RestResponse SendMetrics(Resource resource, DataSource dataSource, DataSourceInstance dataSourceInstance, DataPoint dataPoint, Dictionary<string, string> values)
        {
            if (Batch == true)
            {
                var dataPoints = new List<RestDataPointV1>();
                
                    var restDataPoint = new RestDataPointV1(
                    dataPointAggregationType: dataPoint.AggregationType,
                    dataPointDescription: dataPoint.Description,
                    dataPointName: dataPoint.Name,
                    dataPointType: dataPoint.Type,
                    values: values
                    );
                    dataPoints.Add(restDataPoint);
                


                var instances = new List<RestDataSourceInstanceV1>();
                
                    var restInstance = new RestDataSourceInstanceV1(
                    dataPoints: dataPoints,
                    instanceDescription: dataSourceInstance.Description,
                    instanceDisplayName: dataSourceInstance.DisplayName,
                    instanceName: dataSourceInstance.Name,
                    instanceProperties: dataSourceInstance.Properties
                    );
                    instances.Add(restInstance);
                
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
                AddRequest(body: body, path: "/metric/ingest");
                return null;
            }
            else
                return SingleRequest(resource, dataSource, dataSourceInstance, dataPoint, values);
        }

        public static RestResponse SingleRequest(Resource resource, DataSource dataSource, DataSourceInstance dataSourceInstance, DataPoint dataPoint, Dictionary<string, string> values)
        {

            var dataPoints = new List<RestDataPointV1>();
            
                var restDataPoint = new RestDataPointV1(
                dataPointAggregationType: dataPoint.AggregationType,
                dataPointDescription: dataPoint.Description,
                dataPointName: dataPoint.Name,
                dataPointType: dataPoint.Type,
                values: values
                );
                dataPoints.Add(restDataPoint);
            


            var instances = new List<RestDataSourceInstanceV1>();
            
                var restInstance = new RestDataSourceInstanceV1(
                dataPoints: dataPoints,
                instanceDescription: dataSourceInstance.Description,
                instanceDisplayName: dataSourceInstance.DisplayName,
                instanceName: dataSourceInstance.Name,
                instanceProperties: dataSourceInstance.Properties
                );
                instances.Add(restInstance);
            
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

            return MakeRequest(path: "/metric/ingest", method: "POST", body: body, create: resource.Create, asyncRequest: false);

        }

    }
}
