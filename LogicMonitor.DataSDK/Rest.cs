/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If  copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Xml;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace LogicMonitor.DataSDK
{
    public class Rest
    {

        public RestClient client;
        
        public Rest(RestClient restClient= default)
        {
            if(restClient == default)
            {
                client = new RestClient();
            }
            else
            {
                client = restClient;
            }
            client.UserAgent = string.Format("{0}/{1} (.NET version:{2}; os:{3}, arch:'NA')",Constants.PackageID,Constants.PackageVersion, Environment.Version.ToString(), Environment.OSVersion);
        }

        public void Request(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout, Dictionary<string, string> postParams)
        {
            method = method.ToUpper();
            if (!Enum.IsDefined(typeof(Method), method))
            {
                throw new ArgumentException(string.Format("Invalid method {0}", method));
            }
            if (!headers.ContainsKey(Constants.HeaderKey.ContentType))
                headers[Constants.HeaderKey.ContentType] = Constants.HeaderKey.ApplicationJson;
        }

        public async Task<RestResponse> Get(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout)
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.GET;
            RestResponse response = (RestResponse) await client.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> Head(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout)
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.HEAD;
            RestResponse response = (RestResponse)await client.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> Options(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout)
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.OPTIONS;
            RestResponse response = (RestResponse)await client.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> Delete(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout)
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.DELETE;
            RestResponse response = (RestResponse)await client.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> Post(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout, Dictionary<string, string> postParams = default,bool gzip = true )
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.POST;
            headers.Add(Constants.HeaderKey.XVersion, "2");
            request.AddHeaders(headers);
            foreach (var item in queryParams)
            {
                request.AddQueryParameter(item.Key, item.Value);
            }
            if(gzip == true)
            {

                var compressedBytes = GZip(body);
                request.AddHeader(Constants.HeaderKey.ContentEncoding, Constants.HeaderKey.GZip);
                request.AddParameter(Constants.HeaderKey.ApplicationXGzip, compressedBytes, ParameterType.RequestBody);
            }
            else
            {
                request.AddJsonBody(body);
            }
            Request(method, url, body, headers, queryParams, requestTimeout, postParams);
            RestResponse response = (RestResponse)await client.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> Put(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout, Dictionary<string, string> postParams = default, bool gzip = true)
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.PUT;
            request.AddHeaders(headers);
            foreach (var item in queryParams)
            {
                request.AddQueryParameter(item.Key, item.Value);
            }
            if (gzip == true)
            {

                var compressedBytes = GZip(body);
                request.AddHeader(Constants.HeaderKey.ContentEncoding, Constants.HeaderKey.GZip);
                request.AddParameter(Constants.HeaderKey.ApplicationXGzip, compressedBytes, ParameterType.RequestBody);
            }
            else
            {
                request.AddJsonBody(body);
            }
            Request(method, url, body, headers, queryParams, requestTimeout, postParams);
            RestResponse response = (RestResponse) await client.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> Patch(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams,  TimeSpan requestTimeout, Dictionary<string, string> postParams = default, bool gzip = true)
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.PATCH;
            request.AddHeaders(headers);
            foreach (var item in queryParams)
            {
                request.AddQueryParameter(item.Key, item.Value);
            }
            if (gzip == true)
            {
                var compressedBytes = GZip(body);
                request.AddHeader(Constants.HeaderKey.ContentEncoding, Constants.HeaderKey.GZip);
                request.AddParameter(Constants.HeaderKey.ApplicationXGzip, compressedBytes, ParameterType.RequestBody);
            }
            else
            {
                request.AddJsonBody(body);
            }
            Request(method, url, body, headers, queryParams, requestTimeout, postParams);
            RestResponse response = (RestResponse)await client.ExecuteAsync(request);
            return response;
        }

        public static byte[] GZip(string body)
        {
            var dataStream = new MemoryStream();
            using (var zipStream = new GZipStream(dataStream, CompressionMode.Compress))
            using (var writer = new StreamWriter(zipStream))
                writer.Write(body);

            var compressedBytes = dataStream.ToArray();
            return compressedBytes;
        }
    }
}
