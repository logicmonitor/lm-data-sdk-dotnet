/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using RestSharp;

namespace LogicMonitor.DataSDK
{

    /// <summary>
    /// This class Bind the SDK configration and Model used.Object of this class passed to Metrics/Logs object.
    /// This client handles the client-server communication, and is invariant across implementations.
    /// </summary>
    public class ApiClient
    {
        public Configuration configuration;
        private Rest rest_client;
        public long startTime;
        private int MetricCounter = 1;
        private int LogCounter = 1;
        private readonly Dictionary<string, string> default_headers = new Dictionary<string, string>();
        public ApiClient()
        {
            configuration = new Configuration();
            this.rest_client = new Rest();
            // Set default API version
            default_headers.Add(Constants.HeaderKey.XVersion, "1");
        }
        public ApiClient(Configuration configuration)
        {

            if (configuration == null)
                configuration = new Configuration();
            this.configuration = configuration;
            this.rest_client = new Rest();
            // Set default API version
            default_headers.Add(Constants.HeaderKey.XVersion, "1");
        }

        public async Task<RestResponse> Callapi(
              string path,
              string method,
              TimeSpan _request_timeout,
              Dictionary<string, string> queryParams = default,
              Dictionary<string, string> headerParams = default,
              string body = null,
              string authSetting = default

              )
        {

            var url = this.configuration.host + path;
            this.Update_params_for_auth(headerParams, queryParams, authSetting, path, method, body);
            var response_data = await this.Request(method: method, url: url, queryParams: queryParams, _request_timeout: _request_timeout, headers: headerParams, body: body);
            return response_data;
        }

        public async Task<RestResponse> CallApiAsync(
            string path, string method, TimeSpan _request_timeout, Dictionary<string, string> queryParams = default,
            Dictionary<string, string> headerParams = default, string body = default, string authSetting = null)
        {

                return await CallAsync(path, method, _request_timeout, queryParams, headerParams, body, authSetting);

        }

        public async Task<RestResponse> CallAsync(
            string path,
            string method,
            TimeSpan _request_timeout,
            Dictionary<string, string> queryParams = null,
            Dictionary<string, string> headerParams = null,
            string body = null,
            string authSetting = null

            )
        {
            var thread = await this.Callapi(path: path, method: method, _request_timeout: _request_timeout, headerParams: headerParams, queryParams: queryParams, body: body, authSetting: authSetting);
            return thread;
        }

        public async Task<RestResponse> Request(
            string method,
            string url,
            TimeSpan _request_timeout,
            string body,
            Dictionary<string, string> queryParams = default,
            Dictionary<string, string> headers = default,
            Dictionary<string, string> post_params = default
             )
        {
            if (rest_client == null)
                rest_client = new Rest();
            //Number of request
            var timeRateLimit =CheckNumberOfRequest(url);
            if(!timeRateLimit)
            {
                throw new ArgumentException("The number of requests exceeds the rate limit");
            }
            if (method == "GET")
            {
                return await rest_client.Get("GET", url, queryParams: queryParams, requestTimeout: _request_timeout, headers: headers, body: body);
            }
            else if (method == "POST")
            {
                return await rest_client.Post("POST", url, queryParams: queryParams, headers: headers, postParams: post_params, requestTimeout: _request_timeout, body: body, gzip: configuration.GZip);

            }
            else if (method == "DELETE")
            {
                return await this.rest_client.Delete("DELETE", url, headers: headers, requestTimeout: _request_timeout, body: body, queryParams: queryParams);
            }
            else if (method == "PUT")
            {
                return await rest_client.Put("PUT", url, queryParams: queryParams, headers: headers, postParams: post_params, requestTimeout: _request_timeout, body: body, gzip: configuration.GZip);
            }
            else if (method == "PATCH")
            {
                return await rest_client.Patch("PATCH", url, queryParams: queryParams, headers: headers, postParams: post_params, requestTimeout: _request_timeout, body: body, gzip: configuration.GZip);
            }
            else
            {
                throw new ArgumentException("http method must be `GET`, `POST`, `PATCH`, `PUT` or `DELETE`.");
            }
        }

        public string SelectHeaderAccept(string accepts)
        {
            if (accepts == "")
            {
                return null;
            }

            if (accepts.Contains(Constants.HeaderKey.ApplicationJson))
            {
                return Constants.HeaderKey.ApplicationJson;
            }
            else
            {
                return (accepts);
            }
        }
        public string SelectHeaderContentType(string content_types)
        {
            if (content_types == "")
            {
                return Constants.HeaderKey.ApplicationJson;
            }

            if (content_types.Contains(Constants.HeaderKey.ApplicationJson) || content_types.Contains("*/*"))
            {
                return Constants.HeaderKey.ApplicationJson;
            }
            else
            {
                return content_types;
            }
        }

        public bool Update_params_for_auth(
                Dictionary<string, string> headers,
                Dictionary<string, string> querys,
                string auth_settings,
                string resource_path,
                string method,
                string body = null)
        {
            string msg;
            if (auth_settings == null)
            {
                return false;
            }

            
            if (configuration.AccessKey != null && configuration.AccessID !=null)
            {
                DateTimeOffset n = DateTimeOffset.UtcNow;
                long epoch = n.ToUnixTimeMilliseconds();
                if (body != null)
                {
                    //body is serialized
                    msg = method + epoch + body + resource_path;
                }
                else
                {
                    msg = method + epoch + resource_path;
                }
                // Construct signature
                string hmac = HmacSHA256(configuration.AccessKey, msg);
                var a = System.Text.Encoding.UTF8.GetBytes(hmac);
                string signature = Convert.ToBase64String(a);
                var auth_hash = "LMv1 " + configuration.AccessID + ":" + signature + ":" + epoch;

                headers.Add("Authorization", auth_hash);
                return true;
            }
            else if (configuration.BearerToken != null)
            {
                headers.Add("Authorization", string.Format("Bearer={0}", configuration.BearerToken));
                return true;
            }
            else
            {
                throw new ArgumentException("Authentication token must be in `header`");
            }

        }

        public static string HmacSHA256(string key, string data)
        {
            string hash;
            ASCIIEncoding encoder = new ASCIIEncoding();
            var code = System.Text.Encoding.UTF8.GetBytes(key);
            using (HMACSHA256 hmac = new HMACSHA256(code))
            {
                Byte[] hmBytes = hmac.ComputeHash(encoder.GetBytes(data));
                hash = ToHexString(hmBytes);
            }
            return hash;
        }

        public static string ToHexString(byte[] array)
        {
            StringBuilder hex = new StringBuilder(array.Length * 2);
            foreach (byte b in array)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public bool CheckNumberOfRequest(string url)
        {
            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            int difference = (int)(currentTime - startTime);
            if (difference < 60 && url.Contains("metric/ingest") && MetricCounter < configuration.RequestPerMin)
            {
                MetricCounter++;
                return true;
            }
            if (difference < 60 && url.Contains("log/ingest") && LogCounter < configuration.RequestPerMin)
            {
                LogCounter++;
                return true;
            }
            if (difference >= 60)
            {
                startTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                MetricCounter = 1;
                LogCounter = 1;
                return true;
            }
            return false;
        }
    }
}







