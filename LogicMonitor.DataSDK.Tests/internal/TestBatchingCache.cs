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
            b = new BatchingCache();
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
            apiClient = new ApiClient(configuration);
            Metrics metrics1 = new Metrics(batch: false, interval: 5000, responseInterface, apiClient);

        }

        [TestCase("body", "/metric/ingest")]
        [TestCase("body", "/log/ingest")]
        public void TestAddRequest(string body, string path)
        {
            b.AddRequest(body, path);
            int count = b.rawRequest.Count;
            Assert.AreEqual(1,count);
            b.rawRequest.Clear();
        }

        [Test]
        public void TestGetPayload()
        {
            List<string> testlist = new List<string>();
            testlist.Add("A");
            testlist.Add("B");
            b.PayloadCache = testlist;
            List<string> testpayload = b.GetPayload();
            Assert.AreEqual(testpayload, testlist);
        }

        [Test]
        public void TestDoRequest()
        {
            MyResponse responseInterface = new MyResponse();
            BatchingCache b = new BatchingCache(apiClient, 0, true, responseInterface);
            b.rawRequest.Clear();
            b.AddRequest("body", "metric/ingest");
            b.AddRequest("body1", "metric/ingest");
            b.AddRequest("body2", "metric/ingest");
            t = Task.Run(() => b.DoRequest());
            Thread.Sleep(2000);
            Assert.AreEqual(0, b.PayloadCache.Count);
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

