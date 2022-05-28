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
using System;
using Microsoft.Extensions.Logging;
using LogicMonitor.DataSDK.Utils;

namespace LogicMonitor.DataSDK.Api
{
    /// <summary>
    /// This Class is used to Send Metrics.This class is used by user to interact with LogicMonitor. 
    /// </summary>
    public class Metrics : BatchingCache
    {
        public static readonly object _lock;


        private ObjectNameValidator objectNameValidator = new ObjectNameValidator();
        //Input input;
        //Input input = new Input();
        Dictionary<string, string> classDictionary = new Dictionary<string, string>();
        public Metrics():base()
        {

        }
        public Metrics(bool batch, int interval= default, IResponseInterface responseCallback=default,ApiClient apiClient=default):base(apiClient:apiClient, interval:interval, batch:batch, responseCallback:responseCallback)
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
            
            string errorMsg = ValidField(dataSource:dataSource, dataSourceInstance:dataSourceInstance);
            if (errorMsg != null && errorMsg.Length > 0)
                throw new ArgumentException(errorMsg);


            MetricsV1 input = new MetricsV1(resource,dataSource,dataSourceInstance,dataPoint,values);
            
            if (Batch)
            {
                AddRequest(input);
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
      
        public override void _doRequest()
        {
            var responseList = new List<RestResponse>();
            List<RestMetricsV1> listOfRestMetricsV1True = new List<RestMetricsV1>();
            List<RestMetricsV1> listOfRestMetricsV1False = new List<RestMetricsV1>();

            Resource _removeElement = null;
            RestMetricsV1 restMetrics;
            RestResponse response;
            var payload = GetMetricsPayload();
            foreach (var res in payload)
            {
                Resource _resource = res.Key;
                DataSource _dataSource = new DataSource();
                var instances = new List<RestDataSourceInstanceV1>();
                foreach (var ds in res.Value)
                {
                    _dataSource = ds.Key;
                    if (ds.Value.Count <= 100)
                    {
                        foreach (var ins in ds.Value)
                        {
                            DataSourceInstance _dataSourceInstance = ins.Key;
                            var listDataPoints = new List<RestDataPointV1>();
                            foreach (var dp in ins.Value)
                            {
                                DataPoint _dataPoint = dp.Key;
                                Dictionary<string, string> valuePairs = new Dictionary<string, string>();
                                foreach (var value in dp.Value)
                                {
                                    valuePairs.Add(value.Key, value.Value);
                                }
                                var restDataPoint = new RestDataPointV1(
                                dataPointAggregationType: _dataPoint.AggregationType,
                                dataPointDescription: _dataPoint.Description,
                                dataPointName: _dataPoint.Name,
                                dataPointType: _dataPoint.Type,
                                percentileValue: _dataPoint.PercentileValue,
                                values: valuePairs
                                );
                                listDataPoints.Add(restDataPoint);
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
            dataPointAggregationType: input.dataPoint.AggregationType,
            dataPointDescription: input.dataPoint.Description,
            dataPointName: input.dataPoint.Name,
            dataPointType: input.dataPoint.Type,
            percentileValue: input.dataPoint.PercentileValue,
            values: input.values
            ) ;
            dataPoints.Add(restDataPoint);

            var instances = new List<RestDataSourceInstanceV1>();

            var restInstance = new RestDataSourceInstanceV1(
            dataPoints: dataPoints,
            instanceDescription: input.dataSourceInstance.Description,
            instanceDisplayName: input.dataSourceInstance.DisplayName,
            instanceName: input.dataSourceInstance.Name,
            instanceProperties: input.dataSourceInstance.Properties,
            instanceId:input.dataSourceInstance.InstanceId
            );
            instances.Add(restInstance);

            var restMetrics = new RestMetricsV1(
                resourceIds: input.resource.Ids,
                resourceName: input.resource.Name,
                resourceProperties: input.resource.Properties,
                resourceDescription: input.resource.Description,
                dataSource: input.dataSource.Name,
                dataSourceDisplayName: input.dataSource.DisplayName,
                dataSourceGroup: input.dataSource.Group,
                dataSourceId: input.dataSource.Id,
                singleInstanceDS:input.dataSource.SingleInstanceDS,
                instances: instances
                );
            
            List<RestMetricsV1> listOfRestMetricsV1 = new List<RestMetricsV1>();
            listOfRestMetricsV1.Add(restMetrics);

            var body = Newtonsoft.Json.JsonConvert.SerializeObject(listOfRestMetricsV1);
            body = body.Replace(@"\", "");
            body = body.Replace("\"{", "{");
            body = body.Replace("}\"", "}");
            return body;
            
        }

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
