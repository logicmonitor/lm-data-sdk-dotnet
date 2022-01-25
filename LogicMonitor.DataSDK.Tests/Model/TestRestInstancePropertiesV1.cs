using System;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestRestInstancePropertiesV1
    {
        RestInstancePropertiesV1 restIPv1 = new RestInstancePropertiesV1();
        
        string dataSource = null;
        string dataSourceDisplayName = null;
        int instanceId = 0;
        string instanceName = null;
        Dictionary<string, string> instanceProperties = null;
        Dictionary<string, string> resourceIds = null;

        [Test]
        public void TestRestInstancePropertiesv1()
        {
            RestInstancePropertiesV1 a = new RestInstancePropertiesV1(dataSource,dataSourceDisplayName,instanceId,instanceName,instanceProperties,resourceIds);

            a.DataSource = "datasource";
            Assert.AreEqual("datasource", a.DataSource);

            a.DataSourceDisplayName = "dataSourceDisplayName";
            Assert.AreEqual("dataSourceDisplayName", a.DataSourceDisplayName);

            a.InstanceId = 1;
            Assert.AreEqual(1, a.InstanceId);

            a.InstanceName = "instanceName";
            Assert.AreEqual("instanceName", a.InstanceName);

            Dictionary<string, string> test1 = new Dictionary<string, string>();
            test1.Add("testkey", "testvalues");
            a.InstanceProperties = test1;
            Assert.AreEqual(test1, a.InstanceProperties);

            Dictionary<string, string> test2 = new Dictionary<string, string>();
            test2.Add("testkey", "testvalues");
            a.ResourceIds = test2;
            Assert.AreEqual(test2, a.ResourceIds);

        }

        [Test]
        public void TestDataSource()
        {
            string i = "dataSource";
            restIPv1.DataSource = i;
            string msg = restIPv1.DataSource;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestInstanceDisplayName()
        {
            string i = "dataSourceDisplayName";
            restIPv1.DataSourceDisplayName = i;
            string msg = restIPv1.DataSourceDisplayName;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestInstanceId()
        {
            int i = 123;
            restIPv1.InstanceId = i;
            int msg = restIPv1.InstanceId;
            Assert.AreEqual(msg, i);
        }
        
        [Test]
        public void TestInstanceName()
        {
            string i = "InstanceGroup";
            restIPv1.InstanceName = i;
            string msg = restIPv1.InstanceName;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestInstanceProperties()
        {
            Dictionary<string, string> i = new Dictionary<string, string>();
            i.Add("testvalues", "testvalues");
            restIPv1.InstanceProperties = i;
            Dictionary<string, string> msg = restIPv1.InstanceProperties;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestRestInstancePropertiesV1Constructor()
        {
            restIPv1 = new RestInstancePropertiesV1();
            Assert.AreEqual(restIPv1.DataSource, null);
            Assert.AreEqual(restIPv1.DataSourceDisplayName, null);
            Assert.AreEqual(restIPv1.InstanceId, 0);
            Assert.AreEqual(restIPv1.InstanceName, null);
            Assert.AreEqual(restIPv1.InstanceProperties, null);
            Assert.AreEqual(restIPv1.ResourceIds, null);
        }

        [Test]
        public void TestToString()
        {

            string m = "Sample msg for testing";
            restIPv1.DataSource = m;

            string n = "Sample msg for testing";
            restIPv1.DataSourceDisplayName = n;

            int q = 12345;
            restIPv1.InstanceId = q;

            string k = "Sample msg for testing";
            restIPv1.InstanceName = k;

            Dictionary<string, string> res1 = new Dictionary<string, string>();
            res1.Add("test", "test");
            restIPv1.InstanceProperties = res1;

            Dictionary<string, string> res2 = new Dictionary<string, string>();
            res2.Add("test", "test");
            restIPv1.ResourceIds = res2;

            string expected = "class RestInstancePropertiesV1 {\n  DataSource: Sample msg for testing\n  DataSourceDisplayName: Sample msg for testing\n  InstanceId: 12345\n  InstanceName: Sample msg for testing\n  InstanceProperties{\n   test: test\n  }\n  ResourceIds{\n   test: test\n  }\n}\n";
            System.Console.WriteLine(restIPv1.ToString());
            Assert.AreEqual(expected, restIPv1.ToString());

        }

    }
}


