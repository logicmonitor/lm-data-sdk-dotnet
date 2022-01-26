using System;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestRestMetricsV1
    {
        RestMetricsV1 restmv1 = new RestMetricsV1();

        string dataSource = null;
        string dataSourceDisplayName = null;
        string dataSourceGroup = null;
        int dataSourceId = 0;
        List<RestDataSourceInstanceV1> instances = null;
        string resourceDescription = null;
        Dictionary<string, string> resourceIds = null;
        string resourceName = null;
        Dictionary<string, string> resourceProperties = null;
        RestDataSourceInstanceV1 item = null;

        [Test]
        public void TestRestMetricsv1()
        {
            RestMetricsV1 a = new RestMetricsV1(dataSource, dataSourceDisplayName,dataSourceGroup,dataSourceId,instances,resourceDescription,resourceIds,resourceName, resourceProperties);

            a.DataSource = "datasource";
            Assert.AreEqual("datasource", a.DataSource);

            a.DataSourceDisplayName = "dataSourceDisplayName";
            Assert.AreEqual("dataSourceDisplayName", a.DataSourceDisplayName);

            a.DataSourceGroup = "dataSourceGroup";
            Assert.AreEqual("dataSourceGroup", a.DataSourceGroup);

            a.DataSourceId = 1;
            Assert.AreEqual(1, a.DataSourceId);

            List<RestDataSourceInstanceV1> test = new List<RestDataSourceInstanceV1>();
            test.Add(item);
            a.Instances = test;
            Assert.AreEqual(test, a.Instances);

            a.ResourceDescription = "resourceDescription";
            Assert.AreEqual("resourceDescription", a.ResourceDescription);

            Dictionary<string, string> test1 = new Dictionary<string, string>();
            test1.Add("testkey", "testvalues");
            a.ResourceIds = test1;
            Assert.AreEqual(test1, a.ResourceIds);

            a.ResourceName = "resourceName";
            Assert.AreEqual("resourceName", a.ResourceName);

            Dictionary<string, string> test2 = new Dictionary<string, string>();
            test2.Add("testkey", "testvalues");
            a.ResourceProperties = test2;
            Assert.AreEqual(test2, a.ResourceProperties);

        }

        [Test]
        public void TestDataSource()
        {
            string i = "dataSource";
            restmv1.DataSource = i;
            string msg = restmv1.DataSource;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestDataSourceDisplayName()
        {
            string i = "dataSourceDisplayName";
            restmv1.DataSourceDisplayName = i;
            string msg = restmv1.DataSourceDisplayName;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestDataSourceGroup()
        {
            string i = "dataSourceGroup";
            restmv1.DataSourceGroup = i;
            string msg = restmv1.DataSourceGroup;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestDataSourceId()
        {
            int i = 123;
            restmv1.DataSourceId = i;
            int msg = restmv1.DataSourceId;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestInstances()
        {
            List<RestDataSourceInstanceV1> i = new List<RestDataSourceInstanceV1>();
            i.Add(item);
            restmv1.Instances = i;
            List<RestDataSourceInstanceV1> msg = restmv1.Instances;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestResourceDescription()
        {
            string i = "ResourceDescription";
            restmv1.ResourceDescription = i;
            string msg = restmv1.ResourceDescription;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestResourceIds()
        {
            Dictionary<string, string> i = new Dictionary<string, string>();
            i.Add("testvalues", "testvalues");
            restmv1.ResourceIds = i;
            Dictionary<string, string> msg = restmv1.ResourceIds;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestResourceName()
        {
            string i = "ResourceName";
            restmv1.ResourceName = i;
            string msg = restmv1.ResourceName;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestResourceProperties()
        {
            Dictionary<string, string> i = new Dictionary<string, string>();
            i.Add("testvalues", "testvalues");
            restmv1.ResourceProperties = i;
            Dictionary<string, string> msg = restmv1.ResourceProperties;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestRestMetricsv1Constructor()
        {
            restmv1 = new RestMetricsV1();
            Assert.AreEqual(restmv1.DataSource, null);
            Assert.AreEqual(restmv1.DataSourceDisplayName, null);
            Assert.AreEqual(restmv1.DataSourceGroup, null);
            Assert.AreEqual(restmv1.DataSourceId, 0);
            Assert.AreEqual(restmv1.Instances, null);
            Assert.AreEqual(restmv1.ResourceDescription, null);
            Assert.AreEqual(restmv1.ResourceIds, null);
            Assert.AreEqual(restmv1.ResourceName, null);
            Assert.AreEqual(restmv1.ResourceProperties, null);
        }

        [Test]
        public void TestToString()
        {

            string m = "Sample msg for testing";
            restmv1.DataSource = m;

            string n = "Sample msg for testing";
            restmv1.DataSourceDisplayName = n;

            string q = "Sample msg for testing";
            restmv1.DataSourceGroup = q;

            int k = 12345;
            restmv1.DataSourceId = k;

            List<RestDataSourceInstanceV1> res = new List<RestDataSourceInstanceV1>();
            res.Add(item);
            restmv1.Instances = res;

            string s = "Sample msg for testing";
            restmv1.ResourceDescription = s;

            Dictionary<string, string> res1 = new Dictionary<string, string>();
            res1.Add("test", "test");
            restmv1.ResourceIds = res1;

            string t = "Sample msg for testing";
            restmv1.ResourceName = t;

            Dictionary<string, string> res2 = new Dictionary<string, string>();
            res2.Add("test", "test");
            restmv1.ResourceProperties = res2;

            string expected = "class RestMetricsV1 {\n  DataSource: Sample msg for testing\n  DataSourceDisplayName: Sample msg for testing\n  DataSourceGroup: Sample msg for testing\n  DataSourceId: 12345\n  Instances{ }\n  ResourceDescription: Sample msg for testing\n  ResourceIds{\n   test: test\n  }\n  ResourceName: Sample msg for testing\n  ResourceProperties{\n   test: test\n  }\n}\n";
            System.Console.WriteLine(restmv1.ToString());
            Assert.AreEqual(expected, restmv1.ToString());

        }

    }
}



