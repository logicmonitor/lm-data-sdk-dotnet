using System;
using LogicMonitor.DataSDK.Model;
using LogicMonitor.DataSDK.Utils;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestDataSource
    {
        DataSource dataSource = new DataSource();

        [Test]
        public void TestName()
        {
            //DataPoint a = new DataPoint();
            string i = "name";
            dataSource.Name = i;
            string msg = dataSource.Name;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestDisplayName()
        {
            //DataPoint a = new DataPoint();
            string i = "Displayname";
            dataSource.DisplayName = i;
            string msg = dataSource.DisplayName;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestGroup()
        {
            //DataPoint a = new DataPoint();
            string i = "group";
            dataSource.Group = i;
            string msg = dataSource.Group;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestId()
        {
            //DataPoint a = new DataPoint();
            int i = 0;
            dataSource.Id = i;
            int msg = dataSource.Id;
            Assert.AreEqual(msg, i);
        }
    }
}
