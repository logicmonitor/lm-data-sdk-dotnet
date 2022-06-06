/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */
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
        public Metrics():base()
        {

        }
        public Metrics(bool batch =true, int interval= 10, IResponseInterface responseCallback=default,ApiClient apiClient=default):base(apiClient:apiClient, interval:interval, batch:batch, responseCallback:responseCallback)
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
            string body = CreateRestMetricsBody(resource, dataSource, dataSourceInstance, dataPoint, values);
            if (Batch)
            {
                AddRequest(body: body, path: "/metric/ingest");
                return null;
            }
            else
            {
                string body = SingleRequest(input);
                return Send(Constants .Path.MetricIngestPath,body,"POST",input.resource.Create);
            }
        }

        public override void _mergeRequest()
        {
            var singleRequest = (MetricsV1)GetRequest().Dequeue();
            if (!MetricsPayloadCache.ContainsKey(singleRequest.resource))
            {
                MetricsPayloadCache.Add(singleRequest.resource, new Dictionary<DataSource, Dictionary<DataSourceInstance, Dictionary<DataPoint, Dictionary<string, string>>>>());
            }
            var _dataS = MetricsPayloadCache[singleRequest.resource];
            if(!_dataS.ContainsKey(singleRequest.dataSource))
            {
                _dataS.Add(singleRequest.dataSource, new Dictionary<DataSourceInstance, Dictionary<DataPoint, Dictionary<string, string>>>());
            }
            var _instance = _dataS[singleRequest.dataSource];
            if(!_instance.ContainsKey(singleRequest.dataSourceInstance))
            {
                _instance.Add(singleRequest.dataSourceInstance, new Dictionary<DataPoint, Dictionary<string, string>>());
            }
            var _dataPoint = _instance[singleRequest.dataSourceInstance];
            if(!_dataPoint.ContainsKey(singleRequest.dataPoint))
            {
                _dataPoint.Add(singleRequest.dataPoint, new Dictionary<string, string>());
            }
            var _value = _dataPoint[singleRequest.dataPoint];

            foreach(var item in singleRequest.values)
            {
                _value.Add(item.Key, item.Value);
            }
        }

                            var restInstance = new RestDataSourceInstanceV1(
                            dataPoints: listDataPoints,
                            instanceDescription: _dataSourceInstance.Description,
                            instanceDisplayName: _dataSourceInstance.DisplayName,
                            instanceName: _dataSourceInstance.Name,
                            instanceProperties: _dataSourceInstance.Properties,
                            instanceId: _dataSourceInstance.InstanceId
                            );
                            instances.Add(restInstance);
                        }
                    }
                    restMetrics = new RestMetricsV1(
                    resourceIds: _resource.Ids,
                    resourceName: _resource.Name,
                    resourceProperties: _resource.Properties,
                    resourceDescription: _resource.Description,
                    dataSource: _dataSource.Name,
                    dataSourceDisplayName: _dataSource.DisplayName,
                    dataSourceGroup: _dataSource.Group,
                    dataSourceId: _dataSource.Id,
                    singleInstanceDS: _dataSource.SingleInstanceDS,
                    instances: instances
                    );
                    if (_resource.Create)
                    {
                        listOfRestMetricsV1True.Add(restMetrics);
                    }
                    else
                    {
                        listOfRestMetricsV1False.Add(restMetrics);
                    }
                }
                _removeElement = _resource;
            }
            GetMetricsPayload().Remove(_removeElement);

            try
            {
                if (listOfRestMetricsV1True.Count != 0)
                {
                    var bodyTrue = Newtonsoft.Json.JsonConvert.SerializeObject(listOfRestMetricsV1True);
                    response = Send(Constants.Path.MetricIngestPath,bodyTrue,"POST",true);
                   // MakeRequest(path: "/v2/metric/ingest", method: "POST", body: bodyTrue,create:true);
                    responseList.Add(response);
                }
                if (listOfRestMetricsV1False.Count != 0 )
                {
                    var bodyFalse = Newtonsoft.Json.JsonConvert.SerializeObject(listOfRestMetricsV1False);
                    response = Send(Constants.Path.MetricIngestPath,bodyFalse, "POST",false);
                    //response = MakeRequest(path: "/v2/metric/ingest", method: "POST", body: bodyFalse,create:false);
                    responseList.Add(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Got exception" + ex);
                //BatchingCache.ResponseHandler(response: response);
            }
        }
        public string SingleRequest(MetricsV1 input)
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
            return body;
        }
        public static RestResponse SingleRequest(string body , bool create)
        {

    public static RestResponse Send(string path,string body,string method ,bool create)
    {
      BatchingCache b = new Metrics();
      return b.MakeRequest(path: path, method: method, body: body, create: create, asyncRequest: false);

    }

    public RestResponse UpdateResourceProperties(Dictionary<string, string> resourceIds, Dictionary<string, string> resourceProperties, bool patch = true)
    {
      var method = patch ? "PATCH" : "PUT";
      string body = GetResourcePropertyBody(resourceIds,resourceProperties,patch);
      return Send(Constants.Path.UpdateResourcePropertyPath, body, method, false);

    }
    public string GetResourcePropertyBody(Dictionary<string, string> resourceIds, Dictionary<string, string> resourceProperties, bool patch = true)
    {

            string errorMsg = "";
            if (resourceIds != null)
                errorMsg += objectNameValidator.CheckResourceIdsValidation(resourceIds);
            if (resourceProperties != null)
                errorMsg += objectNameValidator.CheckResourcePropertiesValidation(resourceProperties);
            if (errorMsg != null && errorMsg.Length > 0)
                throw new ArgumentException(errorMsg);

            var restMetrics = new RestMetricsV1(
                resourceIds: resourceIds,
                resourceProperties: resourceProperties
                );
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(restMetrics);
            body = body.Replace(@"\", "");
            body = body.Replace("\"{", "{");
            body = body.Replace("}\"", "}");
            //return b.MakeRequest(path: "/resource_property/ingest", method: method, body: body, asyncRequest: false);
            return body;
    }

    public RestResponse UpdateInstanceProperties(Dictionary<string, string> resourceIds, string dataSourceName, string instanceName, Dictionary<string, string> instanceProperties, bool patch = true)
    {
      var method = patch ? "PATCH" : "PUT";
      string body = GetInstancePropertyBody(resourceIds, dataSourceName, instanceName, instanceProperties, patch);
      return Send(Constants.Path.UpdateInsatancePropertyPath, body, method, false);
    }
    public string GetInstancePropertyBody(Dictionary<string, string> resourceIds, string dataSourceName, string instanceName, Dictionary<string, string> instanceProperties, bool patch = true)
    {
            string errorMsg = "";
            if (resourceIds != null)
                errorMsg += objectNameValidator.CheckResourceIdsValidation(resourceIds);
            if (dataSourceName != null)
                errorMsg += objectNameValidator.CheckDataSourceNameValidation(dataSourceName);
            if (instanceName != null)
                errorMsg += objectNameValidator.CheckInstanceNameValidation(instanceName);
            if (instanceProperties != null)
                errorMsg += objectNameValidator.CheckInstancePropertiesValidation(instanceProperties);
            if (errorMsg != null && errorMsg.Length > 0)
                throw new ArgumentException(errorMsg);
            var instance = new RestDataSourceInstanceV1(
                instanceName: instanceName,
                instanceProperties: instanceProperties
                );
            List<RestDataSourceInstanceV1> instances = new List<RestDataSourceInstanceV1>();
            instances.Add(instance);
            var restMetrics = new RestMetricsV1(
                resourceIds: resourceIds,
                dataSource: dataSourceName,
                instances: instances
                );
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(restMetrics);
            body = body.Replace(@"\", "");
            body = body.Replace("\"{", "{");
            body = body.Replace("}\"", "}");
            body = body.Replace("\"instances\":[{", "");
            body = body.Replace("}]", "");
            return body;
            //return b.MakeRequest(path: "
            //", method: method, body: body, asyncRequest: false);
        }
        public string ValidField(DataSource dataSource, DataSourceInstance dataSourceInstance)
        {
            string errorMsg = "";
            if (dataSource.SingleInstanceDS == false)
            {
                if (dataSourceInstance.InstanceId >= 0)
                {
                    errorMsg += objectNameValidator.CheckInstanceNameValidation(dataSourceInstance.Name);
                    errorMsg += objectNameValidator.CheckDataSourceId(dataSourceInstance.InstanceId);
                    if (dataSourceInstance.DisplayName != null)
                        errorMsg += objectNameValidator.CheckInstanceDisplayNameValidation(dataSourceInstance.DisplayName);
                    if (dataSourceInstance.Properties != null)
                        errorMsg += objectNameValidator.CheckInstancePropertiesValidation(dataSourceInstance.Properties);
                }
                else
                {
                    errorMsg += objectNameValidator.CheckInstanceNameValidation(dataSourceInstance.Name);
                    errorMsg += string.Format("DataSourceInstance Id {0} should not be negative.", dataSourceInstance.InstanceId);
                    if (dataSourceInstance.DisplayName != null)
                        errorMsg += objectNameValidator.CheckInstanceDisplayNameValidation(dataSourceInstance.DisplayName);
                    if (dataSourceInstance.Properties != null)
                        errorMsg += objectNameValidator.CheckInstancePropertiesValidation(dataSourceInstance.Properties);
                }
                return errorMsg;
            }
            else
                return null;
            
        }

    }
}
