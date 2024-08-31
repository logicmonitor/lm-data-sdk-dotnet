/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */
using System;
using RestSharp;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Api;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LogicMonitor.DataSDK.Model;

namespace LogicMonitor.DataSDK.Internal
{
    public abstract class BatchingCache
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Information)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LogicMonitor.DataSDK.Internal.BatchingCache", LogLevel.Information)
                .AddConsole();
        });

        public static readonly ILogger _logger = loggerFactory.CreateLogger<BatchingCache>();
        public const int _DefaultQueue = 100;
        public const char _AddRequest = 'A';
        public const char _MergeRequest = 'M';
        public const char _PayloadSend = 'S';
        public const char _PayloadBuild = 'B';
        public const char _PayloadTotal = 'T';
        public const char _PayloadException = 'E';

        public ApiClient ApiClient { get; set; }
        public int Interval { get; set; }
        public bool Batch { get; set; }
        public IResponseInterface ResponseCallback { get; set; }

        private readonly Object _Lock = new Object();
        protected Queue<IInput> rawRequest = new Queue<IInput>(100);
        protected Dictionary<Resource, Dictionary<DataSource, Dictionary<DataSourceInstance, Dictionary<DataPoint, Dictionary<string, string>>>>> MetricsPayloadCache = new Dictionary<Resource, Dictionary<DataSource, Dictionary<DataSourceInstance, Dictionary<DataPoint, Dictionary<string, string>>>>>();
        protected List<LogsV1> logPayloadCache = new List<LogsV1>();
        private long _lastTimeSend;
        private long _lastTimeStat;
        private Thread mergeThread;
        private Thread requestThread;
        private Semaphore _hasRequest;

        public DefaultResponse defaultResponse = new DefaultResponse();
        
        public BatchingCache()
        {
        }

        public BatchingCache(ApiClient apiClient, int interval = 10, bool batch = true, IResponseInterface responseCallback =default)
        {
            if (apiClient == null)
                apiClient = new ApiClient();

            ApiClient = apiClient;
            Interval = interval;
            Batch = batch;
            if (responseCallback == default)
            {
                ResponseCallback = defaultResponse;
            }
            if (responseCallback is IResponseInterface)
            {
                ResponseCallback = responseCallback;
            }
            else
                _logger.LogWarning("Response callback is not defined or is invalid");

            _lastTimeSend = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeSeconds()); //seconds since epoch

            _hasRequest = new Semaphore(0, int.MaxValue);
            _lastTimeStat = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            if (Batch)
            {
                mergeThread = new Thread(MergeRequest);
                mergeThread.IsBackground = true;
                mergeThread.Start();

                requestThread = new Thread(DoRequest);
                requestThread.IsBackground = true;
                requestThread.Start();

                _logger.LogInformation("{0} api processor is initialized with interval={1}.", GetType().Name, interval);
            }
            else
                _logger.LogInformation("{0} initialized without Batching support.", GetType().Name);
        }

        public Semaphore HasRequest()
        {
            return _hasRequest;
        }
        public Queue<IInput> GetRequest()
        {
            return rawRequest;
        }

        public Dictionary<Resource, Dictionary<DataSource, Dictionary<DataSourceInstance, Dictionary<DataPoint, Dictionary<string, string>>>>> GetMetricsPayload()
        {
            return MetricsPayloadCache;
        }

        public abstract void _mergeRequest();
        public abstract Task _doRequest();

        public void MergeRequest()
        {
            while (_hasRequest.WaitOne())
            {
                lock (_Lock)
                {
                    while (GetRequest().Count > 0)
                    {
                        this._mergeRequest();
                    }
                }
            }
        }

        public void DoRequest()
        {
            while (true)
            {
                lock (_Lock)
                {
                    var currentTime = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    if (currentTime > (_lastTimeStat + 10))
                    {
                        PrintStat();
                        _lastTimeStat = currentTime;
                    }
                    if (currentTime > (_lastTimeSend + Interval))
                    {
                        RestResponse response = new RestResponse();
                        try
                        {
                            this._doRequest();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Got exception" + ex);
                        }
                        _lastTimeSend = currentTime;
                    }
                    else
                        Thread.Sleep(1000);
                }
            }
        }

        public void PrintStat()
        {

            var addRequest = (BatchingCache._AddRequest);
            var mergeRequest = (BatchingCache._MergeRequest);
            var payloadSend = (BatchingCache._PayloadSend);
            var payloadBuild = (BatchingCache._PayloadBuild);
            var payloadTotal = (BatchingCache._PayloadTotal);
            var payloadException = (BatchingCache._PayloadException);
            DateTime lastRequestTime = new DateTime(_lastTimeSend);


            var smsg = string.Format("Class Name :{0} CurrentTime:{1} LastReqestTime:{2} SendMetricsCalls:{3} MergedRequest:{4} BuildingRestPayload:{5} RestApiSend:{6} PossibleRestApiReqests:{7} RestException:{8}",
                GetType().Name, DateTime.Now.ToString("HH:mm:ss GMT")
                , lastRequestTime.ToString("HH:mm:ss"),
                addRequest, mergeRequest, payloadBuild, payloadSend, payloadTotal, payloadException);
            _logger.LogDebug(smsg);
        }

        public void ResponseHandler(RestResponse response = default)
        {
            _logger.LogDebug("Response is {0} : {1}  \n{2}", response.Content.ToString(), response.StatusCode.ToString(), response.Headers.ToString());
            try
            {
                if (ResponseCallback != null)
                {
                    if ((int)response.StatusCode == 200 || (int)response.StatusCode == 202)
                        ResponseCallback.SuccessCallback(response);
                    if ((int)response.StatusCode >= 300)
                        ResponseCallback.ErrorCallback(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Got Exception in response callback {0}",ex.Message.ToString());
            }
        }

        public void AddRequest(IInput body)
        {
            try
            {
                rawRequest.Enqueue(body);
                //Semaphore release
                _hasRequest.Release();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
        public async Task<RestResponse> MakeRequest(string body, string path = default, string method = default, bool create = false, bool asyncRequest = false)
        {

            TimeSpan _request_timeout = TimeSpan.FromMinutes(2);
            var queryParams = new Dictionary<string, string>();
            if (create && path == Constants.Path.MetricIngestPath)
            {
                queryParams.Add("create", "true");
            }
            var headersParams = new Dictionary<string, string>();
            string authSetting = "LMv1";
            if (ApiClient == null)
                ApiClient = new ApiClient();
            headersParams.Add(Constants.HeaderKey.Accept, ApiClient.SelectHeaderAccept("application/json"));
            headersParams.Add(Constants.HeaderKey.ContentType, ApiClient.SelectHeaderContentType("application/json"));

            return await ApiClient.CallApiAsync(
                path,
                method,
                _request_timeout,
                queryParams,
                headersParams,
                body: body,
                authSetting: authSetting
                );
        }

        public async Task<RestResponse> MakeRequestAsync(string body, string path = default, string method = default, bool create = false)
        {

            TimeSpan _request_timeout = TimeSpan.FromMinutes(2);
            var queryParams = new Dictionary<string, string>();
            if (create && path == Constants.Path.MetricIngestPath)
            {
                queryParams.Add("create", "true");
            }
            var headersParams = new Dictionary<string, string>();
            string authSetting = "LMv1";
            if (ApiClient == null)
                ApiClient = new ApiClient();
            headersParams.Add(Constants.HeaderKey.Accept, ApiClient.SelectHeaderAccept("application/json"));
            headersParams.Add(Constants.HeaderKey.ContentType, ApiClient.SelectHeaderContentType("application/json"));

            return await ApiClient.CallApiAsync(
                path,
                method,
                _request_timeout,
                queryParams,
                headersParams,
                body: body,
                authSetting: authSetting
            );
        }
    }
}