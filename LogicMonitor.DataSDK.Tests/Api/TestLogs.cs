using System;
using System.Collections.Generic;
using System.Net;
using LogicMonitor.DataSDK.Api;
using LogicMonitor.DataSDK.Internal;
using LogicMonitor.DataSDK.Model;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace LogicMonitor.DataSDK.Tests.Api
{
   [TestFixture]
   public class TestLogs
   {
       public ApiClient apiClient;
       public Resource resource;
       [SetUp]
       public void Setup()
        {
            string AccessID = "Shkjafhdfhjshs";
            string AccessKey = "sdkHfi924urlasfd";
            Configuration config = new Configuration(company: "lmabcd", accessID: AccessID, accessKey: AccessKey);
            apiClient = new ApiClient(config);

           string resourceName = "abcd";
           Dictionary<string, string> resourceIds = new Dictionary<string, string>();
           resourceIds.Add("system.displayname", "abcdtest");
           Dictionary<string, string> value = new Dictionary<string, string>();
           value.Add(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), string.Format("12" + 10));
          resource = new Resource(name: resourceName, ids: resourceIds, description: "desc" , create: false);
       }
       [Test]
       public void TestBatchSendLogs()
       {
           MyResponse responseInterface = new MyResponse();
           Logs logs = new Logs(batch: true, interval: 0, responseCallback: responseInterface, apiClient: apiClient);
           Dictionary<string, string> res = new Dictionary<string, string>();
           res.Add("test", "test");
           string m = "Sample msg for testing";
           Assert.AreEqual(null,logs.SendLogs(m, resource, null));
       }

   }
   class MyResponse : IResponseInterface
   {
       public void ErrorCallback(RestResponse response)
       {
           Console.WriteLine("ErrorCallback from My Response");
       }

       public void SuccessCallback(RestResponse response)
       {
           Console.WriteLine("SuccessCallback from My Response");
       }
   }
}

