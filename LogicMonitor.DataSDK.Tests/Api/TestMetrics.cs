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
   public class TestMetrics
   {
       public ApiClient apiClient;
       public Resource resource;
       public Metrics metrics;
       public DataSource dataSource;
       public DataSourceInstance dataSourceInstance;
       public DataPoint dataPoint;
      [SetUp]
       public void Setup()
        {
            string AccessID = "Shkjafhdfhjshs";
            string AccessKey = "sdkHfi924urlasfd";
            Configuration config = new Configuration(company: "lmabcd", accessID: AccessID, accessKey: AccessKey);
            string dataSourceGroup = "dotnetSDK";
           string saname = "Instance1";
           string AggType = "None";
           string datapointname = "datapoint1";
           apiClient = new ApiClient(config);

           string resourceName = "abcd";
           Dictionary<string, string> resourceIds = new Dictionary<string, string>();
           resourceIds.Add("system.displayname", "abcdtest");
           Dictionary<string, string> value = new Dictionary<string, string>();
           value.Add(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), string.Format("12" + 10));
           MyResponse responseInterface = new MyResponse();
           metrics = new Metrics(batch: true, interval: 0, responseInterface, apiClient);


           string dataSourceName = "Mac2";
           dataSource = new DataSource(name: dataSourceName, group: dataSourceGroup);
           dataSourceInstance = new DataSourceInstance(name: saname);
           dataPoint = new DataPoint(name: datapointname, aggregation: AggType);

           resource = new Resource(name: resourceName, ids: resourceIds);
       }
       [Test]
       public void TestBatchSendMetrics()
       {
           Dictionary<string, string> values = new Dictionary<string, string>();
           values.Add("123234252", "100");
           Assert.AreEqual(null, metrics.SendMetrics(resource,dataSource,dataSourceInstance,dataPoint,values));
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
}

