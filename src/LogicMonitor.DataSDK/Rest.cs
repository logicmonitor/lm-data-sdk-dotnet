using System;
using RestSharp;
using System.Collections.Generic;

using System.Net.Http;

namespace LogicMonitor.DataSDK
{
    public class Rest
    {
        SocketsHttpHandler poolManager;
        public Rest(Configuration configuration, int maxSize, int poolSize = 4)
        {
            poolManager = new SocketsHttpHandler();
            if (maxSize == 0)
            {
                if (configuration.ConnectionPoolMaxsize != 0)
                    maxSize = configuration.ConnectionPoolMaxsize;
                else
                    maxSize = 4;
            }

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
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.GET;
            RestResponse response = (RestResponse)client.Execute(request);
            return response;
        }

        public RestResponse Head(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.HEAD;
            RestResponse response = (RestResponse)client.Execute(request);
            return response;
        }

        public RestResponse Options(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.OPTIONS;
            RestResponse response = (RestResponse)client.Execute(request);
            return response;
        }

        public RestResponse Delete(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.DELETE;
            RestResponse response = (RestResponse)client.Execute(request);
            return response;
        }

        public RestResponse Post(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, TimeSpan requestTimeout, Dictionary<string, string> postParams = default)
        {
            var client = new RestClient(url);
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
            var client = new RestClient(url);
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

        public RestResponse Patch(string method, string url, string body, Dictionary<string, string> headers, Dictionary<string, string> queryParams, Dictionary<string, string> postParams, TimeSpan requestTimeout)
        {
            var client = new RestClient(url);
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
