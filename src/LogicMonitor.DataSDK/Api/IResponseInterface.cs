using System;
using RestSharp;
namespace LogicMonitor.DataSDK.Api
{
    /// <summary>
    /// This interface is used to define the custom message for Successful and Failed request.
    /// </summary>
    public interface IResponseInterface
    {
        public void SuccessCallback(RestResponse response);
        public void ErrorCallback(RestResponse response);
    }
}

