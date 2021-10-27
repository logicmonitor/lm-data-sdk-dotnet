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

        public ApiClients apiClients;
        public Task t;
  
        [SetUp]
        public void Setup()
        {


            string resourceName = "abcd";
            Dictionary<string, string> resourceIds = new Dictionary<string, string>();
            resourceIds.Add("system.displayname", "abcdtest");

            string dataSourceGroup = "dotnetSDK";
            string saname = "Instance1";
            string AggType = "None";
            string datapointname = "datapoint1";
            Dictionary<string, string> value = new Dictionary<string, string>();
            value.Add(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), string.Format("12" + 10));


            MyResponse responseInterface = new MyResponse();


            Resource resource = new Resource(name: resourceName, ids: resourceIds);
            string dataSourceName = "Mac2";
            DataSource dataSource = new DataSource(Name: dataSourceName, Group: dataSourceGroup);
            DataSourceInstance dataSourceInstance = new DataSourceInstance(name: saname);
            DataPoint dataPoint = new DataPoint(name: datapointname, aggregation: AggType);

            List<DataSourceInstance> listDataSourceInstance = new List<DataSourceInstance>();
            listDataSourceInstance.Add(dataSourceInstance);
            List<DataPoint> listDataPoint = new List<DataPoint>();
            listDataPoint.Add(dataPoint);
            Authenticate authenticate = new Authenticate();
            authenticate.Id = "jVR895k2F2ZgYr8t7tbV";
            authenticate.Key = "Lh=r=U]8[__-M39}5]{49E%F)bi(mVDRUa83_YKE";
            authenticate.Type = "LMv1";

            Configuration configuration = new Configuration(company: "lmabcd", authentication: authenticate);
            apiClients = new ApiClients(configuration);
            Metrics metrics1 = new Metrics(batchs: false, intervals: 5000, responseInterface, apiClients);

        }

        [TestCase("body", "/metric/ingest")]
        [TestCase("body", "/log/ingest")]
        public void TestAddRequest(string body, string path)
        {
            BatchingCache.AddRequest(body, path);
            int count = BatchingCache.rawRequest.Count;
            Assert.AreEqual(count, 1);
            BatchingCache.rawRequest.Clear();
        }

        [Test]
        public void TestGetPayload()
        {
            List<string> testlist = new List<string>();
            testlist.Add("A");
            testlist.Add("B");
            BatchingCache.PayloadCache = testlist;
            List<string> testpayload = BatchingCache.GetPayload();
            Assert.AreEqual(testpayload, testlist);
        }

        [Test]
        public void TestDoRequest()
        {
            MyResponse responseInterface = new MyResponse();
            BatchingCache b = new BatchingCache(apiClients, 0, true, responseInterface);
            BatchingCache.rawRequest.Clear();
            BatchingCache.AddRequest("body", "path");
            BatchingCache.AddRequest("body1", "path");
            BatchingCache.AddRequest("body2", "path");
            t = Task.Run(() => b.DoRequest());
            Thread.Sleep(2000);
            Assert.AreEqual(0, BatchingCache.PayloadCache.Count);
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

