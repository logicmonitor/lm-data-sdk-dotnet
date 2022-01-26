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
using Microsoft.Extensions.Logging;

namespace LogicMonitor.DataSDK.Internal
{

    public class BatchingCache
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
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


        public static ApiClient ApiClient { get; set; }
        public int Interval { get; set; }
        public static bool Batch { get; set; }
        public static IResponseInterface ResponseCallback { get; set; }

        private  readonly Object _Lock = new Object();
        public Queue<string> rawRequest = new Queue<string>();
        public List<string> PayloadCache = new List<string>();
        private List<string> Payload = new List<string>();
        private long _lastTimeSend;
        private long _lastTimeStat;
        private Thread mergeThread;
        private Thread requestThread;
        private Semaphore _hasRequest;
        private  string Path { get; set; }

        public BatchingCache()
        {
        }
        public BatchingCache(ApiClient apiClient , int interval = 0, bool batch = false, IResponseInterface responseCallback = default)
        {
            if (apiClient == null)
                apiClient = new ApiClient();

            ApiClient = apiClient;
            this.Interval = interval;
            Batch = batch;

            if (responseCallback is IResponseInterface)
                ResponseCallback = responseCallback;
            else
                _logger.LogWarning("Resonse callback is not a defined or valid");

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
        public  Queue<string> GetRequest()
        {
            return rawRequest;
        }

        public  List<string> GetPayload()
        {
            return PayloadCache;
        }

        public void MergeRequest()
        {
            while (_hasRequest.WaitOne())
            {
                while (GetRequest().Count > 0)
                {
                    var singleRequest = GetRequest().Dequeue();
                    lock (_Lock)
                    {
                        PayloadCache.Add(singleRequest);
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
                        try
                        {
                            foreach (var item in PayloadCache)
                            {
                                Payload.Add(item);
                            }
                            PayloadCache.Clear();
                            DoRequest(Payload, path: Path);
                            Payload.Clear();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Got exception" + ex);

                        }
                        _lastTimeSend = currentTime;

                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }

                }
            }
        }
        private void DoRequest(List<string> body, string path)
        {
            var responseList = new List<RestResponse>();
            if (body.Count != 0)
            {
                lock (_Lock)
                {
                    foreach (var item in body)
                    {

                        var pathparam = item.Substring(0, 1);
                        if (pathparam.Equals("M"))
                        {
                            path = "/metric/ingest";
                        }
                        else
                        {
                            path = "/log/ingest";

                        }
                        string bodystring = item.Substring(1);
                        RestResponse response = new RestResponse();
                        try
                        {
                            response = MakeRequest(path: path, method: "POST", body: bodystring, create: true);
                            responseList.Add(response);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Got exception" + ex);
                            BatchingCache.ResponseHandler(response: response);
                        }
                    }
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


            var smsg = string.Format("{0} CurrentTime:{1} LastReqestTime:{2} SendMetricsCalls:{3}" +
                " MergedRequest:{4} BuildingRestPayload:{5} RestApiSend:{6} PossibleRestApiReqests:{7} RestException:{8}", GetType().Name, DateTime.Now.ToString("HH:mm:ss GMT")
                , lastRequestTime.ToString("HH:mm:ss"),
                addRequest, mergeRequest, payloadBuild, payloadSend, payloadTotal, payloadException);

            _logger.LogDebug(smsg);
        }

        public static void ResponseHandler(RestResponse response = default)
        {
            _logger.LogDebug("Response is {0}  {1}  \n{2}", response, response.StatusCode, response.Headers.ToString());

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
                _logger.LogError("Got Exception in response callback {0}", Convert.ToString(ex));
            }
        }

        public void AddRequest(string body, string path)
        {
            try
            {

                if (path.Contains("metric"))
                {
                    rawRequest.Enqueue("M" + body);

                }

                else if (path.Contains("log"))
                {
                    rawRequest.Enqueue("L" + body);

                }
                //Semaphore release
                _hasRequest.Release();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
        public RestResponse MakeRequest(string body, string path = default, string method = default, bool create = false, bool asyncRequest = false)
        {
            TimeSpan _request_timeout = TimeSpan.FromMinutes(2);
            var queryParams = new Dictionary<string, string>();

            if (create && path == "/metric/ingest")
            {
                queryParams.Add("create", "true");
            }
            var headersParams = new Dictionary<String, string>();
            string authSetting = "LMv1";
            if (ApiClient == null)
                ApiClient = new ApiClient();

            headersParams.Add("Accept", ApiClient.SelectHeaderAccept("application/json"));
            headersParams.Add("Content-Type", ApiClient.SelectHeaderContentType("application/json"));

            return ApiClient.CallApi(
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