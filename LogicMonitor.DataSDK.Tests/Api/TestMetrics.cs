using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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
       public Dictionary<string, string> resourceIds;
       public Dictionary<string, string> value;
       public string time;
       public string resourceName;
       public string dataSourceName;
       public string dataSourceGroup;
       public string AggType;
       public string instanceName;
       public string datapointname;
       public Dictionary<string, string> instanceProperties;
       public Dictionary<string, string> resourceProperties;
       public string displayName;

      [SetUp]
       public void Setup()
       {
            string AccessID = "DummyLmAccessID";
            string AccessKey = "DummyAccessKey";
            Configuration config = new Configuration(company: "YourCompanyName", accessID: AccessID, accessKey: AccessKey);
           apiClient = new ApiClient(config);

           dataSourceGroup = "dotnetSDK";
           instanceName = "Instance1";
           AggType = "None";
           datapointname = "datapoint1";

           resourceName = "abcd";
           displayName = "abcdtest";
           resourceIds = new Dictionary<string, string>();
           resourceIds.Add("system.displayname", displayName);

           MyResponse responseInterface = new MyResponse();

          value = new Dictionary<string, string>();
          time = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          value.Add(time, string.Format("12" + 10));

          metrics = new Metrics(batch: false, interval: 0, responseInterface, apiClient);

           instanceProperties = new Dictionary<string, string>();
           resourceProperties = new Dictionary<string, string>();
          
           dataSourceName = "Mac2";
           dataSource = new DataSource(name: dataSourceName, group: dataSourceGroup);
           dataSourceInstance = new DataSourceInstance(name: instanceName);
           dataPoint = new DataPoint(name: datapointname, aggregation: AggType);
           resource = new Resource(name: resourceName, ids: resourceIds);
       }
       [Test]
       public async Task TestBatchSendMetrics()
       {
           MyResponse responseInterface = new MyResponse();
  
           metrics = new Metrics(batch: true, interval: 0, responseInterface, apiClient);
           Dictionary<string, string> values = new Dictionary<string, string>();
           values.Add("123234252", "100");
           Assert.AreEqual(null,await  metrics.SendMetricsAsync(resource,dataSource,dataSourceInstance,dataPoint,values));
       }

      [TestCase("Mac1-","DotnetSDK","Instance1")]
      [TestCase("Mac1-", "Dotnet SDK-_", "Instance1")]
      [TestCase("Mac1-", "DotnetSDK", "Inst;anc-e_1.")] 
      public void TestValidField(string dataSourceName, string dataSourceGroup, string instanceName)
      {
        dataSource = new DataSource(name: dataSourceName, group: dataSourceGroup);
        dataSourceInstance = new DataSourceInstance(name: instanceName);
        Assert.AreEqual("",metrics.ValidField(dataSource, dataSourceInstance));
      }

        [TestCase("Mac1-", "Dotnet SDK-_", "Instance1", -1)]
        [TestCase("Mac1-", "Dotnet SDK-_", "Instance1", -90)]
        public void TestInValidInstanceIdField(string dataSourceName, string dataSourceGroup, string instanceName, int id)
        {
            
            dataSource = new DataSource(name: dataSourceName, group: dataSourceGroup);
            dataSourceInstance = new DataSourceInstance(name: instanceName, instanceId: id);
            Assert.AreEqual("DataSourceInstance Id "+ id +" should not be negative.", metrics.ValidField(dataSource, dataSourceInstance));
        }

        [TestCase("Mac1-", "Dotnet SDK-_", "Instance1", -1)]
        [TestCase("Mac1-", "Dotnet SDK-_", "Instance1", -90)]
        public void TestInValidInstanceIDPropertyField(string dataSourceName, string dataSourceGroup, string instanceName, int id)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            properties.Add(" PropertyKey", " PropertyValue");
            dataSource = new DataSource(name: dataSourceName, group: dataSourceGroup);
            dataSourceInstance = new DataSourceInstance(name: instanceName, instanceId: id,properties:properties);
            Assert.AreEqual("DataSourceInstance Id " + id + " should not be negative.Instance Properties Key should not be null, empty or have tailing spaces.", metrics.ValidField(dataSource, dataSourceInstance));
        }
        [TestCase("Mac1-", "DotnetSDK", "Instance1")]
        public void TestInValidInstancePropertiesField(string dataSourceName, string dataSourceGroup, string instanceName)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            properties.Add(" PropertyKey"," PropertyValue");
            dataSource = new DataSource(name: dataSourceName, group: dataSourceGroup);
            dataSourceInstance = new DataSourceInstance(name: instanceName,properties:properties);
            Assert.AreEqual("Instance Properties Key should not be null, empty or have tailing spaces.", metrics.ValidField(dataSource, dataSourceInstance));
        }


        [TestCase("Mac1-", "Dotnet SDK-_", "Instance1", -1)]
        [TestCase("Mac1-", "Dotnet SDK-_", "Instance1", -90)]
        public void TestInValidInstanceIDDisplayNameField(string dataSourceName, string dataSourceGroup, string instanceName, int id)
        {
            string displayName = " .net instance";
            dataSource = new DataSource(name: dataSourceName, group: dataSourceGroup);
            dataSourceInstance = new DataSourceInstance(name: instanceName, instanceId: id, displayName: displayName);
            Assert.AreEqual("DataSourceInstance Id " + id + " should not be negative.Instance display name Should not be empty or have tailing spaces.", metrics.ValidField(dataSource, dataSourceInstance));
        }
        [TestCase("Mac1-", "DotnetSDK", "Instance1")]
        public void TestInValidInstanceDisplayNameField(string dataSourceName, string dataSourceGroup, string instanceName)
        {
            string displayName = " .net instance";
            dataSource = new DataSource(name: dataSourceName, group: dataSourceGroup);
            dataSourceInstance = new DataSourceInstance(name: instanceName, displayName: displayName);
            Assert.AreEqual("Instance display name Should not be empty or have tailing spaces.", metrics.ValidField(dataSource, dataSourceInstance));
        }

        [Test]
        public void TestSingleRequest()
        {
            MetricsV1 input = new MetricsV1(resource, dataSource, dataSourceInstance, dataPoint, value);
            string actualbody = metrics.SingleRequest(input);
            string expectedBody = "[{\"dataSource\":\""+dataSourceName+"\",\"dataSourceGroup\":\""+dataSourceGroup+"\",\"instances\":[{\"dataPoints\":[{\"dataPointAggregationType\":\""+AggType+"\",\"dataPointName\":\""+datapointname+"\",\"values\":{\""+time+ "\":\"1210\"}}],\"instanceName\":\""+ instanceName + "\"}],\"resourceIds\":{\"system.displayname\":\""+ displayName + "\"},\"resourceName\":\""+resourceName+"\",\"singleInstanceDS\":false}]";
            Assert.AreEqual(expectedBody, actualbody);
        }

        [Test]
        public void TestGetInstanceBody()
        {
            string expectedbody= "{\"dataSource\":\"" + dataSourceName + "\",\"instanceName\":\"" + instanceName + "\",\"instanceProperties\":{},\"resourceIds\":{\"system.displayname\":\"" + displayName + "\"},\"singleInstanceDS\":false}";
            string actualbody = metrics.GetInstancePropertyBody(resourceIds,dataSourceName,instanceName,instanceProperties,true);
            Assert.AreEqual(expectedbody, actualbody);

        }
        [Test]
        public void TestgetResourceBody()
        {

            var _resourceIds = new Dictionary<string, string>();
            _resourceIds.Add("someProperty", "someValue");
            string expectedbody = "{\"resourceIds\":{\"system.displayname\":\"" + displayName + "\"},\"resourceProperties\":{\"someProperty\":\"someValue\"},\"singleInstanceDS\":false}";
            string actualbody = metrics.GetResourcePropertyBody(resourceIds,_resourceIds, true);
            Assert.AreEqual(expectedbody, actualbody);
        }

        [Test]
        public void TestMergeRequest()
        {
            MetricsV1 input = new MetricsV1(resource, dataSource, dataSourceInstance, dataPoint, value);
            metrics.AddRequest(input);
            Assert.AreEqual(1, metrics.GetRequest().Count);
            metrics._mergeRequest();
            //After merge count will be 0
            Assert.AreEqual(0, metrics.GetRequest().Count);
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

