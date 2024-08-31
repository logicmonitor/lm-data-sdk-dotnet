using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LogicMonitor.DataSDK.Model;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using LogicMonitor.DataSDK;
using RestSharp;

namespace LogicMonitor.DataSDK.Tests
{
   [TestFixture]
   public class TestRest
   {

       public static Configuration configuration;
       public static string method = "GET";
       public static string url = "/v2/metric/ingest";
       public static string body = "body";
       public static Dictionary<string, string> headers = new Dictionary<string, string>();
       public static Dictionary<string, string> queryParams = new Dictionary<string, string>();
       public static TimeSpan requestTimeout = TimeSpan.Zero;
       public static Dictionary<string, string> postParams = new Dictionary<string, string>();
       [SetUp]
       public void Setup()
        {
            string AccessID = "DummyLmAccessID";
            string AccessKey = "DummyAccessKey";
            Configuration config = new Configuration(company: "lmabcd", accessID: AccessID, accessKey: AccessKey);

        }
       //[TestCase("post","/metric/ingest","body",typeof(resourceId), typeof(Dictionary<string, string>),typeof(TimeSpan), typeof(Dictionary<string, string>))]

       [TestCase("pos")]
       [TestCase("http")]
       [TestCase("cos")]
       public void TestInvalidRequest(string method)
       {

           string url = "/metric/ingest";
           string body = "body";
           Dictionary<string, string> headers = new Dictionary<string, string>();
           Dictionary<string, string> queryParams = new Dictionary<string, string>();
           TimeSpan requestTimeout = TimeSpan.Zero;
           Dictionary<string, string> postParams = new Dictionary<string, string>();
           Rest r = new Rest();
           Assert.Throws<ArgumentException>(() => r.Request(method, url, body, headers, queryParams, requestTimeout, postParams));
       }

       [TestCase("POST")]
       [TestCase("GET")]
       [TestCase("HEAD")]
       public void TestValidRequest(string method)
       {

           string url = "/metric/ingest";
           string body = "body";
           Dictionary<string, string> headers = new Dictionary<string, string>();
           Dictionary<string, string> queryParams = new Dictionary<string, string>();
           TimeSpan requestTimeout = TimeSpan.Zero;
           Dictionary<string, string> postParams = new Dictionary<string, string>();
           Rest r = new Rest();
           Assert.DoesNotThrow(() => r.Request(method, url, body, headers, queryParams, requestTimeout, postParams));
       }
       public static IRestClient MockRestClient<T>(HttpStatusCode httpStatusCode, string json)
       where T : new()
       {
           var data = JsonConvert.DeserializeObject<T>(json);
           var response = new Mock<IRestResponse<T>>();
           response.Setup(_ => _.StatusCode).Returns(httpStatusCode);
           response.Setup(_ => _.Data).Returns(data);

           var mockIRestClient = new Mock<IRestClient>();
           mockIRestClient.Setup(x => x.Execute<T>(It.IsAny<IRestRequest>())).Returns(response.Object);
           return mockIRestClient.Object;
       }
       [Test]
       public async Task TestGet()
       {
           Mock<RestClient> restClient = new Mock<RestClient>();

           restClient.Setup(c => c.Execute(It.IsAny<RestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.OK, Content = "Test" });
           Rest r = new Rest(restClient.Object);

           var Response = await r.Get(method, url, body, headers, queryParams, requestTimeout);
           Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
       }

       [Test]
       public async Task TestPost()
       {
           Mock<RestClient> restClient = new Mock<RestClient>();

           restClient.Setup(c => c.Execute(It.IsAny<RestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.OK, Content = "Test" });
           Rest r = new Rest(restClient.Object);

           var Response = await r.Post(method, url, body, headers, queryParams, requestTimeout);
           Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
       }

       [Test]
       public async Task TestHead()
       {
           Mock<RestClient> restClient = new Mock<RestClient>();

           restClient.Setup(c => c.Execute(It.IsAny<RestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.OK, Content = "Test" });
           Rest r = new Rest( restClient.Object);

           var Response = await r.Head(method, url, body, headers, queryParams, requestTimeout);
           Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
       }
       [Test]
       public async Task TestOptions()
       {
           Mock<RestClient> restClient = new Mock<RestClient>();

           restClient.Setup(c => c.Execute(It.IsAny<RestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.OK, Content = "Test" });
           Rest r = new Rest( restClient.Object);

           var Response = await r.Options(method, url, body, headers, queryParams, requestTimeout);
           Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
       }
       [Test]
       public async Task TestDelete()
       {
           Mock<RestClient> restClient = new Mock<RestClient>();

           restClient.Setup(c => c.Execute(It.IsAny<RestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.OK, Content = "Test" });
           Rest r = new Rest(restClient.Object);

           var Response = await r.Delete(method, url, body, headers, queryParams, requestTimeout);
           Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
       }
       [Test]
       public async Task TestPut()
       {
           Mock<RestClient> restClient = new Mock<RestClient>();

           restClient.Setup(c => c.Execute(It.IsAny<RestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.OK, Content = "Test" });
           Rest r = new Rest(restClient.Object);

           var Response = await r.Put(method, url, body, headers, queryParams, requestTimeout);
           Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
       }
       [Test]
       public async Task TestPatch()
       {
           Mock<RestClient> restClient = new Mock<RestClient>();

           restClient.Setup(c => c.Execute(It.IsAny<RestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.OK, Content = "Test" });
           Rest r = new Rest(restClient.Object);

           var Response = await r.Patch(method, url, body, headers, queryParams, requestTimeout);
           Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
       }

   }
}
