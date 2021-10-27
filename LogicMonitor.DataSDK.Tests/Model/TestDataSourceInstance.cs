using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using LogicMonitor.DataSDK.Utils;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestDataSourceInstance
    {
        DataSourceInstance dataSourceIns = new DataSourceInstance();

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
            string i = "Displayname";
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
    }
}

