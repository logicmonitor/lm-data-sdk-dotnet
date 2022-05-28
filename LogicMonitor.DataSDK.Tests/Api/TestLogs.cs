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
       public Logs logs,batchlogs;
       public Dictionary<string, string> resourceIds;
       public Dictionary<string, string> value;
       string resourceName;
       public LogsV1 logsv1;
       string message;
       string timestamp;
       [SetUp]
       public void Setup()
        {
           MyResponse responseInterface = new MyResponse();
            string AccessID = "DummyLmAccessID";
            string AccessKey = "DummyAccessKey"; message = "Sample Log";
           timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
           Configuration config = new Configuration(company: "YourCompanyName", accessID: AccessID, accessKey: AccessKey);
           apiClient = new ApiClient(config);
           resourceName = "abcd";
           resourceIds = new Dictionary<string, string>();
           resourceIds.Add("system.displayname", "abcdtest");
           value = new Dictionary<string, string>();
           value.Add(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), string.Format("12" + 10));
           resource = new Resource(name: resourceName, ids: resourceIds, description: "desc" , create: false);
           logs = new Logs(batch: false, interval: 0, responseCallback: responseInterface, apiClient: apiClient);
           batchlogs = new Logs(batch: true, interval: 0, responseCallback: responseInterface, apiClient: apiClient);
           logsv1 = new LogsV1(message:message,resourceIds,timeStamp:timestamp);

    }
    [Test]
       public void TestBatchSendLogs()
       {
           Dictionary<string, string> res = new Dictionary<string, string>();
           res.Add("test", "test");
           Assert.AreEqual(null,batchlogs.SendLogs(message, resource, null));
       }

      [Test]
      public void TestSingleRequest()
      {
      Assert.AreEqual("[{\"message\":\"Sample Log\",\"_lm.resourceId\":{\"system.displayname\":\"abcdtest\"},\"timestamp\":\""+timestamp+"\",\"metadata\":\"null\"}]", logs.SingleRequest(logsv1));
      }

    [Test]
    public void TestserializeList()
    {
      List<String> list = new List<string>();
      list.Add("\"{TestLog}\"");
      string expectedbody = "[\"{TestLog}\"]";
      string actualbody = logs.SerializeList(list);
      Assert.AreEqual(expectedbody, actualbody);
    }

    [Test]
    public void TestCreateLogBody()
    {
      string expectedbody = "{\"message\":\"Sample Log\",\"_lm.resourceId\":\"{\\\"system.displayname\\\":\\\"abcdtest\\\"}\",\"timestamp\":\"" + timestamp + "\",\"metadata\":\"null\"}";
      string actualbody = logs.CreateLogBody(logsv1);
      Assert.AreEqual(expectedbody, actualbody);
    }

    [Test]
    public void TestMergeRequest()
    {
      logs.AddRequest(logsv1);
      Assert.AreEqual(1, logs.GetRequest().Count);
      logs._mergeRequest();
      Assert.AreEqual(0, logs.GetRequest().Count);

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

