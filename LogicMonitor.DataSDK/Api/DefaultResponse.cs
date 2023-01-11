using System;
using RestSharp;

namespace LogicMonitor.DataSDK.Api
{
    public class DefaultResponse : IResponseInterface
    {
        public DefaultResponse()
        {
        }

        public void ErrorCallback(RestResponse response)
        {
            Console.WriteLine("Response: {0}", response.Content.ToString()) ;
        }

        public void SuccessCallback(RestResponse response)
        {
            Console.WriteLine("Response: {0}", response.Content.ToString());
        }
    }
}

