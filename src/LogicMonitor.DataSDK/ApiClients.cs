using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using RestSharp;

namespace LogicMonitor.DataSDK
{

    /// <summary>
    /// This class is Controller.
    /// </summary>
    public class ApiClients
    {
        public Configuration configuration;
        public string pool;
        private Rest rest_client;
        public Dictionary<string, string> default_headers = new Dictionary<string, string>();
        private int maxSize;
        public PlatformID Platform { get; }
        public ApiClients() { }
        public ApiClients(Configuration configuration)
        {

            if (configuration == null)
                configuration = new Configuration();

            this.configuration = configuration;
            this.rest_client = new Rest(configuration, maxSize);
            // Set default API version
            default_headers.Add("X-version", "1");
        }



        public object user_agent
        {
            get
            {
                return this.default_headers["User-Agent"];
            }
            set
            {
                this.default_headers["User-Agent"] = (string)value;
            }
        }

        public RestResponse Callapi(
              string path,
              string method,
              TimeSpan _request_timeout,
              Dictionary<string, string> pathParams = default,
              Dictionary<string, string> queryParams = default,
              Dictionary<string, string> headerParams = default,
              string body = null,
              Dictionary<string, string> post_params = default,
              string files = default,
              string responseType = default,
              string authSetting = default,
              bool _return_http_data_only = default,
              Dictionary<string, string> collectionFormats = default,
              Dictionary<string, string> _preload_content = default

              )
        {

            var url = configuration.host + path;
            Update_params_for_auth(headerParams, queryParams, authSetting, path, method, body, files);
            var response_data = Request(method: method, url: url, queryParams: queryParams, _request_timeout: _request_timeout, headers: headerParams, post_params: post_params, body: body);


            Console.WriteLine("Response: {0}", response_data.Content.ToString());
            return response_data;
        }





        public RestResponse CallApi(
            string path,
            string method,
            TimeSpan _request_timeout,
            Dictionary<string, string> pathParams = default,
            Dictionary<string, string> queryParams = default,
            Dictionary<string, string> headerParams = default,
            string body = default,
            Dictionary<string, string> post_params = default,
            string files = null,
            string responseType = null,
            string authSetting = null,
            bool asyncRequest = true,
            bool _return_http_data_only = true,
            Dictionary<string, string> collectionFormats = default,
            Dictionary<string, string> _preload_content = default
            )
        {
            if (asyncRequest == false)
            {
                return Callapi(path: path, method: method, _request_timeout: _request_timeout, headerParams: headerParams, pathParams: pathParams, queryParams: queryParams, body: body, post_params: post_params, files: files, responseType: responseType, authSetting: authSetting, _return_http_data_only: _return_http_data_only, collectionFormats: collectionFormats, _preload_content: _preload_content);
            }
            else
            {
                return CallAsync(path, method, _request_timeout, pathParams, queryParams, headerParams, body, post_params, files, responseType, authSetting, _return_http_data_only, Convert.ToBoolean(collectionFormats), _preload_content)
                    .Result;
            }
        }

        public async Task<RestResponse> CallAsync(
            string path,
            string method,
            TimeSpan _request_timeout,
            Dictionary<string, string> pathParams = null,
            Dictionary<string, string> queryParams = null,
            object headerParams = null,
            string body = null,
            Dictionary<string, string> post_params = null,
            string files = null,
            string responseType = null,
            string authSetting = null,
            bool asyncRequest = true,
            bool _return_http_data_only = true,
            Dictionary<string, string> collectionFormats = null,
            Dictionary<string, string> _preload_content = null
            )
        {
            var thread = await Task.FromResult(this.Callapi(path: path, method: method, _request_timeout: _request_timeout, pathParams: pathParams, queryParams: queryParams, body: body, post_params: post_params, files: files, responseType: responseType, authSetting: authSetting, _return_http_data_only: _return_http_data_only, collectionFormats: collectionFormats, _preload_content: _preload_content));
            return thread;
        }

        public RestResponse Request(
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
                rest_client = new Rest(configuration, 4);

            if (method == "GET")
            {
                return rest_client.Get("GET", url, queryParams: queryParams, requestTimeout: _request_timeout, headers: headers, body: body);
            }
            else if (method == "HEAD")
            {
                return rest_client.Head("HEAD", url, queryParams: queryParams, requestTimeout: _request_timeout, headers: headers, body: body);
            }
            else if (method == "OPTIONS")
            {
                return this.rest_client.Options("OPTIONS", url, queryParams: queryParams, headers: headers, requestTimeout: _request_timeout, body: body);
            }
            else if (method == "POST")
            {

                return rest_client.Post("POST", url, queryParams: queryParams, headers: headers, postParams: post_params, requestTimeout: _request_timeout, body: body);

            }
            else if (method == "PUT")
            {
                return this.rest_client.Put("PUT", url, queryParams: queryParams, headers: headers, postParams: post_params, requestTimeout: _request_timeout, body: body);
            }
            else if (method == "PATCH")
            {
                return this.rest_client.Patch("PATCH", url, queryParams: queryParams, headers: headers, postParams: post_params, requestTimeout: _request_timeout, body: body);
            }
            else if (method == "DELETE")
            {
                return this.rest_client.Delete("DELETE", url, headers: headers, requestTimeout: _request_timeout, body: body, queryParams: queryParams);
            }
            else
            {
                throw new ArgumentException("http method must be `GET`, `HEAD`, `OPTIONS`, `POST`, `PATCH`, `PUT` or `DELETE`.");
            }
        }

        public string SelectHeaderAccept(string accepts)
        {
            if (accepts == "")
            {
                return null;
            }

            if (accepts.Contains("application/json"))
            {
                return "application/json";
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
                return "application/json";
            }

            if (content_types.Contains("application/json") || content_types.Contains("*/*"))
            {
                return "application/json";
            }
            else
            {
                return content_types;
            }
        }

        public void Update_params_for_auth(
                Dictionary<string, string> headers,
                Dictionary<string, string> querys,
                string auth_settings,
                string resource_path,
                string method,
                string body = null,
                string files = null)
        {
            string msg;
            if (auth_settings == null)
            {
                return;
            }

            var auth_setting = this.configuration.authentication;
            if (auth_setting != null)
            {
                if (auth_setting.Key == null)
                {
                    return;
                }
                else if (auth_setting.Type == "Bearer" && auth_setting.Key != null)
                {
                    headers.Add("Authorization", string.Format("Bearer={0}", auth_setting.Key));

                }
                else
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
                    string hmac = HmacSHA256(auth_setting.Key, msg);
                    var a = System.Text.Encoding.UTF8.GetBytes(hmac);
                    string signature = Convert.ToBase64String(a);
                    var auth_hash = "LMv1 " + auth_setting.Id + ":" + signature + ":" + epoch;

                    headers.Add("Authorization", auth_hash);

                }
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
    }
}







