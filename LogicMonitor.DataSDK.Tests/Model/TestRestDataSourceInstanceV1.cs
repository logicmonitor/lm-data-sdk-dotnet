using System;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestRestDataSourceInstanceV1
    {
        RestDataSourceInstanceV1 restdpv1 = new RestDataSourceInstanceV1();
        List<RestDataPointV1> dataPoints = null;
        string instanceDescription = null;
        string instanceDisplayName = null;
        string instanceGroup = null;
        int instanceId = 0;
        string instanceName = null;
        Dictionary<string, string> instanceProperties = null;
        string instanceWildValue = null;
        RestDataPointV1 item = null;

        [Test]
        public void TestRestDataSourceInstancev1()
        {
            RestDataSourceInstanceV1 a = new RestDataSourceInstanceV1(instanceDisplayName,dataPoints, instanceDescription,instanceGroup,instanceId,instanceName,instanceProperties,instanceWildValue);

            List<RestDataPointV1> test = new List<RestDataPointV1>();
            test.Add(item);
            a.DataPoints = test;
            Assert.AreEqual(test, a.DataPoints);

            a.InstanceDescription = "instanceDescription";
            Assert.AreEqual("instanceDescription", a.InstanceDescription);

            a.InstanceDisplayName = "instanceDisplayName";
            Assert.AreEqual("instanceDisplayName", a.InstanceDisplayName);

            a.InstanceGroup = "instanceGroup";
            Assert.AreEqual("instanceGroup", a.InstanceGroup);

            a.InstanceId = 1;
            Assert.AreEqual(1, a.InstanceId);

            a.InstanceName = "instanceName";
            Assert.AreEqual("instanceName", a.InstanceName);

            Dictionary<string, string> test1 = new Dictionary<string, string>();
            test1.Add("teststring", "testvalues");
            a.InstanceProperties = test1;
            Assert.AreEqual(test1, a.InstanceProperties);

            a.InstanceWildValue = "instanceName";
            Assert.AreEqual("instanceName", a.InstanceWildValue);
        }

        [Test]
        public void TestDataPoints()
        {
            List<RestDataPointV1> i = new List<RestDataPointV1>();
            i.Add(item);
            restdpv1.DataPoints = i;
            List<RestDataPointV1> msg = restdpv1.DataPoints;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestInstanceDescription()
        {
            string i = "InstanceDescription";
            restdpv1.InstanceDescription = i;
            string msg = restdpv1.InstanceDescription;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestInstanceDisplayName()
        {
            string i = "InstanceDisplayName";
            restdpv1.InstanceDisplayName = i;
            string msg = restdpv1.InstanceDisplayName;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestInstanceGroup()
        {
            string i = "InstanceGroup";
            restdpv1.InstanceGroup = i;
            string msg = restdpv1.InstanceGroup;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestInstanceId()
        {
            int i = 1;
            restdpv1.InstanceId = i;
            int msg = restdpv1.InstanceId;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestInstanceName()
        {
            string i = "InstanceGroup";
            restdpv1.InstanceName = i;
            string msg = restdpv1.InstanceName;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestInstanceProperties()
        {
            Dictionary<string, string> i = new Dictionary<string, string>();
            i.Add("testvalues", "testvalues");
            restdpv1.InstanceProperties = i;
            Dictionary<string, string> msg = restdpv1.InstanceProperties;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void RestDataSourceInstanceV1Constructor()
        {
            Assert.AreEqual(restdpv1.DataPoints, null);
            Assert.AreEqual(restdpv1.InstanceDescription, null);
            Assert.AreEqual(restdpv1.InstanceDisplayName, null);
            Assert.AreEqual(restdpv1.InstanceGroup, null);
            Assert.AreEqual(restdpv1.InstanceId, 0);
            Assert.AreEqual(restdpv1.InstanceName, null);
            Assert.AreEqual(restdpv1.InstanceProperties, null);
            Assert.AreEqual(restdpv1.InstanceWildValue, null);
        }
        [Test]
        public void TestToString()
        {
            List<RestDataPointV1> res = new List<RestDataPointV1>();
         
            res.Add(item);
            restdpv1.DataPoints = res;

            string m = "Sample msg for testing";
            restdpv1.InstanceDescription = m;

            string n = "Sample msg for testing";
            restdpv1.InstanceDisplayName = n;

            string o = "Sample msg for testing";
            restdpv1.InstanceGroup = o;

            int i = 12345;
            restdpv1.InstanceId = i;

            string p = "Sample msg for testing";
            restdpv1.InstanceName = p;

            Dictionary<string, string> res1 = new Dictionary<string, string>();
            res1.Add("test", "test");
            restdpv1.InstanceProperties = res1;

            string q = "Sample msg for testing";
            restdpv1.InstanceWildValue = q;

            string expected = "class RestDataSourceInstanceV1 {\n  DataPoints{\n   }\n  InstanceDescription: Sample msg for testing\n  InstanceDisplayName: Sample msg for testing\n  InstanceGroup: Sample msg for testing\n  InstanceId: 12345\n  InstanceName: Sample msg for testing\n  InstanceProperties{\n    test:test\n  }\n  InstanceWildValue: Sample msg for testing\n}\n";
            System.Console.WriteLine(restdpv1.ToString());
            Assert.AreEqual(expected, restdpv1.ToString());

        }
    }
}

