/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Xml;
using RestSharp;
using System.Collections.Generic;
using System.IO;

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
            client.UserAgent = string.Format("{0}/{1}",Setup.PackageID,Setup.PackageVersion);
        }

        public void Request(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout, Dictionary<string, string> postParams)
        {
            method = method.ToUpper();
            if (!Enum.IsDefined(typeof(Method), method))
            {
                throw new ArgumentException(string.Format("Invalid method {0}", method));
            }
            if (!headers.ContainsKey("Content-Type"))
                headers["Content-Type"] = "application/json";
        }

        public RestResponse Get(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout)
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.GET;
            RestResponse response = (RestResponse)client.Execute(request);
            return response;
        }

        public RestResponse Head(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout)
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.HEAD;
            RestResponse response = (RestResponse)client.Execute(request);
            return response;
        }

        public RestResponse Options(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout)
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.OPTIONS;
            RestResponse response = (RestResponse)client.Execute(request);
            return response;
        }

        public RestResponse Delete(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout)
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.DELETE;
            RestResponse response = (RestResponse)client.Execute(request);
            return response;
        }

        public RestResponse Post(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout, Dictionary<string, string> postParams = default)
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.POST;
            headers.Add("X-Version", "2");
            request.AddHeaders(headers);
            foreach (var item in queryParams)
            {
                request.AddQueryParameter(item.Key, item.Value);
            }
            request.AddJsonBody(body);
            Request(method, url, body, headers, queryParams, requestTimeout, postParams);
            RestResponse response = (RestResponse)client.Execute(request);
            return response;
        }

        public RestResponse Put(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout, Dictionary<string, string> postParams = default)
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.PUT;
            request.AddHeaders(headers);
            foreach (var item in queryParams)
            {
                request.AddQueryParameter(item.Key, item.Value);
            }
            request.AddJsonBody(body);

            Request(method, url, body, headers, queryParams, requestTimeout, postParams);
            RestResponse response = (RestResponse)client.Execute(request);
            return response;
        }

        public RestResponse Patch(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams,  TimeSpan requestTimeout, Dictionary<string, string> postParams = default)
        {
            client.BaseUrl = new System.Uri(url);
            var request = new RestRequest();
            request.Method = Method.PATCH;
            request.AddHeaders(headers);
            foreach (var item in queryParams)
            {
                request.AddQueryParameter(item.Key, item.Value);
            }
            request.AddJsonBody(body);
            Request(method, url, body, headers, queryParams, requestTimeout, postParams);
            RestResponse response = (RestResponse)client.Execute(request);
            return response;
        }

       
    }
}
