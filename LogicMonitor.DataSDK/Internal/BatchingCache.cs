/*
 * Copyright, 2021, LogicMonitor, Inc.
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
using LogicMonitor.DataSDK.Client;

namespace LogicMonitor.DataSDK.Internal
{

    public class BatchingCache
    {
        public static ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LogicMonitor.DataSDK.Internal.BatchingCache", LogLevel.Information)
                .AddConsole();
        });

        public static ILogger _logger = loggerFactory.CreateLogger<BatchingCache>();
        public const int _DefaultQueue = 100;
        public const char _AddRequest = 'A';
        public const char _MergeRequest = 'M';
        public const char _PayloadSend = 'S';
        public const char _PayloadBuild = 'B';
        public const char _PayloadTotal = 'T';
        public const char _PayloadException = 'E';


        public static ApiClients ApiClient { get; set; }
        public int Interval { get; set; }
        public static bool Batch { get; set; }
        public static IResponseInterface ResponseCallback { get; set; }

        private static readonly Object _Lock = new Object();
        public static Queue<string> rawRequest = new Queue<string>();
        public static List<string> PayloadCache = new List<string>();
        public static List<string> Payload = new List<string>();
        public long _lastTimeSend;
        public long _lastTimeStat;
        public Dictionary<char, int> _Counter;
        public Dictionary<char, int> _RequestCounter;
        public Thread mergeThread;
        public Thread requestThread;
        public static Semaphore _hasRequest;
        private static string Path { get; set; }

        public BatchingCache()
        {
        }
        public BatchingCache(ApiClients apiClient = default, int interval = default, bool batch = default, IResponseInterface responseCallback = default)
        {
            if (apiClient == null)
                apiClient = new ApiClients();

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
            if (Batch == true)
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
        public static Queue<string> GetRequest()
        {
            return rawRequest;
        }

        public static List<string> GetPayload()
        {
            return PayloadCache;
        }

        private static void MergeRequest()
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
                            //_logger.LogInformation(Payload.Count + " " + PayloadCache.Count);
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
            List<string> restRequest = new List<string>();

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
                        catch (ApiException ex)
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

        public static void AddRequest(string body, string path)
        {
            try
            {

                if (path.Contains("metric"))
                {
                    rawRequest.Enqueue("M" + body);

                }

                if (path.Contains("log"))
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
        public RestResponse MakeRequest(string body, string path = default, string method = default, bool create = true, bool asyncRequest = false)
        {
            TimeSpan _request_timeout = TimeSpan.FromMinutes(2);
            var collectionFormats = new Dictionary<string, string>();
            Dictionary<string, string> pathParams = new Dictionary<string, string>();
            var queryParams = new Dictionary<string, string>();

            if (create == true && path == "/metric/ingest")
            {
                queryParams.Add("create", "true");
            }

            var headersParams = new Dictionary<String, string>();
            string authSetting = "LMv1";

            var formParams = new Dictionary<string, string>();
            string localVarFiles = "";

            if (ApiClient == null)
                ApiClient = new ApiClients();

            headersParams.Add("Accept", ApiClient.SelectHeaderAccept("application/json"));
            headersParams.Add("Content-Type", ApiClient.SelectHeaderContentType("application/json"));

            var _responseType = "PushMetricAPIResponse";
            return ApiClient.CallApi(
                path,
                method,
                _request_timeout,
                pathParams: pathParams,
                queryParams,
                headersParams,
                body: body,
                post_params: formParams,
                files: localVarFiles,
                responseType: _responseType,
                authSetting: authSetting,
                asyncRequest: asyncRequest,
                collectionFormats: collectionFormats
                );
        }
    }
}