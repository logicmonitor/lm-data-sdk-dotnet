using System;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestRestDataPointV1
    {
        RestDataPointV1 restv1 = new RestDataPointV1();
        string dataPointAggregationType = null;
        string  dataPointDescription = null;
        string dataPointName = null;
        string dataPointType = null;
        Dictionary<string, string> values = null;

        [Test]
        public void TestRestDataPoint()
        {
            RestDataPointV1 a = new RestDataPointV1(dataPointAggregationType,dataPointDescription,dataPointName,dataPointType,values);
            a.DataPointAggregationType = "DataPointAggregationType";
            Assert.AreEqual("DataPointAggregationType", a.DataPointAggregationType);

            Dictionary<string, string> test = new Dictionary<string, string>();
            test.Add("teststring", "testvalues");
            a.Values = test;
            Assert.AreEqual(test, a.Values);

            a.DataPointDescription = "dataPointDescription";
            Assert.AreEqual("dataPointDescription", a.DataPointDescription);

            a.DataPointName = "dataPointName";
            Assert.AreEqual("dataPointName", a.DataPointName);

            a.DataPointType = "dataPointType";
            Assert.AreEqual("dataPointType", a.DataPointType);
        }

        [Test]
        public void TestDataPointAggregationType()
        {
            string i = "DataPointAggregationType";
            restv1.DataPointAggregationType = i;
            string msg = restv1.DataPointAggregationType;
            Assert.AreEqual(i, msg);
        }
        [Test]
        public void TestdataPointDescription()
        {
            string i = "dataPointDescription";
            restv1.DataPointDescription = i;
            string msg = restv1.DataPointDescription;
            Assert.AreEqual(i, msg);
        }
        [Test]
        public void TestdataPointName()
        {
            string i = "dataPointName";
            restv1.DataPointName = i;
            string msg = restv1.DataPointName;
            Assert.AreEqual(i, msg);
        }
        [Test]
        public void TestdataPointType()
        {
            string i = "dataPointType";
            restv1.DataPointType = i;
            string msg = restv1.DataPointType;
            Assert.AreEqual(i, msg);
        }
        [Test]
        public void Testvalues()
        {
            Dictionary<string, string> i = new Dictionary<string, string>();
            i.Add("testvalues", "testvalues");
            restv1.Values = i;
            Dictionary<string, string> msg = restv1.Values;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestRestDataPointV1Constructor()
        {
            restv1 = new RestDataPointV1();
            Assert.AreEqual(null, restv1.DataPointAggregationType);
            Assert.AreEqual(null, restv1.DataPointDescription);
            Assert.AreEqual(null, restv1.DataPointName);
            Assert.AreEqual(null, restv1.DataPointType);
            Assert.AreEqual(null, restv1.Values);
        }
        [Test]
        public void TestToString()
        {
            string i = "1683263926";
            restv1.DataPointAggregationType = i;
            Dictionary<string, string> res = new Dictionary<string, string>();
            res.Add("test", "test");
            restv1.Values = res;
            string m = "Sample msg for testing";
            restv1.DataPointDescription = m;

            string n = "Sample msg for testing";
            restv1.DataPointName = n;

            string o = "Sample msg for testing";
            restv1.DataPointType = o;

            Console.WriteLine(restv1.ToString());
            string expected = "class RestDataPointV1 {\n  DataPointAggregationType: 1683263926\n  DataPointDescription: Sample msg for testing\n  DataPointName: Sample msg for testing\n  DataPointType: Sample msg for testing\n  Values{\n    test:test\n  }\n}\n";
            Assert.AreEqual(expected, restv1.ToString());

        }
    }
}
