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
            //var body = CreateRestMetricsBody(resource, dataSource, dataSourceInstance, dataPoint, values);
            string errorMsg = ValidField(dataSource:dataSource, dataSourceInstance:dataSourceInstance);
            if (errorMsg != null && errorMsg.Length > 0)
                throw new ArgumentException(errorMsg);


            MetricsV1 input = new MetricsV1();
            input.resource = resource;
            input.dataSource = dataSource;
            input.dataSourceInstance = dataSourceInstance;
            input.dataPoint = dataPoint;

            
            foreach (var item in values)
            {
                input.values.Add(item.Key, item.Value);
            }

            if (Batch)
            {
                AddRequest(input);
                return null;
            }
            else
            {
                return SingleRequest(input);
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
            List<RestMetricsV1> listOfRestMetricsV1 = new List<RestMetricsV1>();
            var body = GetMetricsPayload();
            listOfRestMetricsV1 = CreateRestMetricsBody(body);
            RestResponse response;
            try
            {
                var body1 = Newtonsoft.Json.JsonConvert.SerializeObject(listOfRestMetricsV1);
                response = MakeRequest(path: "/v2/metric/ingest", method: "POST", body: body1);
                responseList.Add(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Got exception" + ex);
                //BatchingCache.ResponseHandler(response: response);
            }
        }




        private List<RestMetricsV1> CreateRestMetricsBody(Dictionary<Resource, Dictionary<DataSource, Dictionary<DataSourceInstance, Dictionary<DataPoint, Dictionary<string, string>>>>> body)
        {

            List<RestMetricsV1> listOfRestMetricsV1 = new List<RestMetricsV1>();
            Resource _removeElement = null;
            RestMetricsV1 restMetrics;
            foreach (var item in body)
            {
                Resource _resource = item.Key;
                DataSource _dataSource = new DataSource();
                var instances = new List<RestDataSourceInstanceV1>();

                foreach (var ds in item.Value)
                {
                    _dataSource = ds.Key;
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
                            percentileValue: _dataPoint.percentileValue,
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
                        instanceId:_dataSourceInstance.InstanceId
                        );
                        instances.Add(restInstance);

                        //if (_dataSource.SingleInstanceDS == true)
                        //    break;

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
                        singleInstanceDS:_dataSource.SingleInstanceDS,
                        instances: instances
                        );
                    listOfRestMetricsV1.Add(restMetrics);
                }
                _removeElement = _resource;
            }
            _ = GetMetricsPayload().Remove(_removeElement);
            return listOfRestMetricsV1;
        }
        public static RestResponse SingleRequest(MetricsV1 input)
        {
            var dataPoints = new List<RestDataPointV1>();

            var restDataPoint = new RestDataPointV1(
            dataPointAggregationType: input.dataPoint.AggregationType,
            dataPointDescription: input.dataPoint.Description,
            dataPointName: input.dataPoint.Name,
            dataPointType: input.dataPoint.Type,
            values: input.values
            );
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
            BatchingCache b = new Metrics();
            return b.MakeRequest(path: "/v2/metric/ingest", method: "POST", body: body, create: input.resource.Create, asyncRequest: false);

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
