using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using LogicMonitor.DataSDK.Utils;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestDataSourceInstance
    {
        Dictionary<string, string> res = new Dictionary<string, string>();
        DataSourceInstance dataSourceIns;

        [SetUp]
        public void SetUp()
        {
            dataSourceIns = new DataSourceInstance(name: "Instance1");
        }
        [Test]
        public void TestName()
        {
            string i = "Description";
            dataSourceIns.Description = i;
            string msg = dataSourceIns.Description;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestDisplayName()
        {
            string i = "logicmonitor.host";
            dataSourceIns.DisplayName = i;
            string msg = dataSourceIns.DisplayName;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestGroup()
        {
            string i = "name";
            dataSourceIns.Name = i;
            string msg = dataSourceIns.Name;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestId()
        {
            Dictionary<string, string> i = new Dictionary<string, string>();
            i.Add("test", "test");
            dataSourceIns.Properties = i;
            Dictionary<string, string> msg = dataSourceIns.Properties;
            Assert.AreEqual(msg, i);
        }

        [Test]
        public void TestToString()
        {
            string i = "1683263926";
            dataSourceIns.Description = i;


            string m = "Sample msg for testing";
            dataSourceIns.DisplayName = m;

            string n = "Sample msg for testing";
            dataSourceIns.Name = n;

            Dictionary<string, string> res = new Dictionary<string, string>();
            res.Add("test", "test");
            dataSourceIns.Properties = res;

            string expected = "class DataPoint {\n  Description: 1683263926\n  DisplayName: Sample msg for testing\n  Name: Sample msg for testing\n  Properties: test:test\n}\n";
            Assert.AreEqual(expected, dataSourceIns.ToString());

        }
    }
}

