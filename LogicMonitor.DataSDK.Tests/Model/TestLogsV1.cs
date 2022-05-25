using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestLogV1
    {

        [Test]
        public void TestLogsV1Constructor()
        {
            LogsV1 a = new LogsV1();

            Assert.AreEqual(null,a.Message);
            Assert.AreEqual(null, a.Body);
            Assert.AreEqual(null, a.Timestamp);
            Assert.AreEqual(null, a.MetaData);
            Assert.AreEqual(null, a.ResourceId);
        }

        [Test]
        public void TestLogsV1()
        {
            Dictionary<string, string> resourceIds = new Dictionary<string, string>();
            Dictionary<string, string> metaData = new Dictionary<string, string>();
            string timeStamp = "1321123432";
            LogsV1 a = new LogsV1("msg", resourceIds, timeStamp, metaData);
            a.Message = "msg";
            Assert.AreEqual("msg", a.Message);

            Dictionary<string, string> test = new Dictionary<string, string>();
            test.Add("test string", "test");
            a.MetaData = test;
            Assert.AreEqual(test, a.MetaData);

            a.Timestamp = "ts";
            Assert.AreEqual("ts", a.Timestamp);

            Dictionary<string, string> test1 = new Dictionary<string, string>();
            test1.Add("test string", "test");
            a.ResourceId = test1;
            Assert.AreEqual(test1, a.ResourceId);
        }

        [Test]
        public void TestMessage()
        {
            LogsV1 a = new LogsV1();
            string i = "Sample msg for testing";
            a.Message = i;
            string msg = a.Message;
            Assert.AreEqual(msg, i);
        }

        [Test]
        public void TestMetadata()
        {
            LogsV1 a = new LogsV1();
            Dictionary<string, string> metadataDict = new Dictionary<string, string>();
            metadataDict.Add("test", "test");
            a.MetaData = metadataDict;
            Dictionary<string, string> msg = a.MetaData;
            Assert.AreEqual(msg, metadataDict);
        }

        [Test]
        public void TestTimestamp()
        {
            LogsV1 a = new LogsV1();
            string i = "1683263926";
            a.Timestamp = i;
            string msg = a.Timestamp;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestResourceID()
        {
            LogsV1 a = new LogsV1();
            Dictionary<string, string> res = new Dictionary<string, string>();
            res.Add("test", "test");
            a.ResourceId = res;
            Dictionary<string, string> msg = a.ResourceId;
            Assert.AreEqual(msg, res);
        }
    }
}
