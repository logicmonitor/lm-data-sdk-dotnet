using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LogicMonitor.DataSDK.Api;
using LogicMonitor.DataSDK.Internal;
using LogicMonitor.DataSDK.Model;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace LogicMonitor.DataSDK.Tests.@internal
{
   [TestFixture]
   public class TestBatchingCache
   {

       public ApiClient apiClient;
       public Task t;
       public BatchingCache b;

    [SetUp]
       public void Setup()
       {


           string resourceName = "abcd";
           Dictionary<string, string> resourceIds = new Dictionary<string, string>();
           resourceIds.Add("system.displayname", "abcdtest");
           b = new Metrics();
           string dataSourceGroup = "dotnetSDK";
           string saname = "Instance1";
           string AggType = "None";
           string datapointname = "datapoint1";
           Dictionary<string, string> value = new Dictionary<string, string>();
           value.Add(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), string.Format("12" + 10));

           MyResponse responseInterface = new MyResponse();

           Resource resource = new Resource(name: resourceName, ids: resourceIds);
           string dataSourceName = "Mac2";
           DataSource dataSource = new DataSource(name: dataSourceName, group: dataSourceGroup);
           DataSourceInstance dataSourceInstance = new DataSourceInstance(name: saname);
           DataPoint dataPoint = new DataPoint(name: datapointname, aggregation: AggType);
           
           string AccessID = "Shkjafhdfhjshs";
           string AccessKey = "sdkHfi924urlasfd";
           Configuration config = new Configuration(company: "lmabcd", accessID: AccessID, accessKey: AccessKey);
           apiClient = new ApiClient(config);
           Metrics metrics1 = new Metrics(batch: false, interval: 5000, responseInterface, apiClient);

       }
       [Test]
       public void TestAddRequest()
       {
          Dictionary<string, string> resourceIds = new Dictionary<string, string>();
          resourceIds.Add("system.displayname", "abcdtest");
          MyResponse responseInterface = new MyResponse();

          LogsV1 logsv1 = new LogsV1(message: "Logs", resourceIds);

          Logs logs = new Logs(batch: false, interval: 0, responseCallback: responseInterface, apiClient: apiClient);

          logs.AddRequest(logsv1);
          int count = logs.GetRequest().Count;
          Assert.AreEqual(1,count);
       }
   }



   class MyResponse : IResponseInterface
   {
       public void ErrorCallback( RestResponse response)
       {
           Console.WriteLine("ErrorCallback from My Response");
       }

       public void SuccessCallback( RestResponse response)
       {
           Console.WriteLine("SuccessCallback from My Response");
       }
   }
}

