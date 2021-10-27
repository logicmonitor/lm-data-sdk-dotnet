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
        public ApiClients apiClients;
        public Resource resource;
        [SetUp]
        public void Setup()
        {
            Authenticate authenticate = new Authenticate();
            authenticate.Id = "jVR895k2F2ZgYr8t7tbV";
            authenticate.Key = "Lh=r=U]8[__-M39}5]{49E%F)bi(mVDRUa83_YKE";
            authenticate.Type = "LMv1";

            Configuration configuration = new Configuration(company: "lmabcd", authentication: authenticate);
            apiClients = new ApiClients(configuration);

            string resourceName = "abcd";
            Dictionary<string, string> resourceIds = new Dictionary<string, string>();
            resourceIds.Add("system.displayname", "abcdtest");
            Dictionary<string, string> value = new Dictionary<string, string>();
            value.Add(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), string.Format("12" + 10));




           resource = new Resource(name: resourceName, ids: resourceIds);
        }
        [Test]
        public void TestBatchSendLogs()
        {
            MyResponse responseInterface = new MyResponse();
            Logs logs = new Logs(batchs: true, intervals: 0, responseCallbacks: responseInterface, apiClients: apiClients);
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

