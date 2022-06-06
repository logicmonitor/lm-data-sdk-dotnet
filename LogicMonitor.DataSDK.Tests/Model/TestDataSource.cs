using System;
using LogicMonitor.DataSDK.Model;
using LogicMonitor.DataSDK.Utils;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestDataSource
    {
        DataSource dataSource = new DataSource("nameold","displayname","groupname",1);

        [Test]
        public void TestName()
        {
            string i = "name";
            dataSource.Name = i;
            string msg = dataSource.Name;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestDisplayName()
        {
            string i = "Displayname";
            dataSource.DisplayName = i;
            string msg = dataSource.DisplayName;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestGroup()
        {
            string i = "group";
            dataSource.Group = i;
            string msg = dataSource.Group;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestId()
        {
            int i = 0;
            dataSource.Id = i;
            int msg = dataSource.Id;
            Assert.AreEqual(msg, i);
        }

        [Test]
        public void TestToString()
        {
            string i = "1683263926";
            dataSource.Name = i;


            string m = "Sample msg for testing";
            dataSource.DisplayName = m;

            string n = "Sample msg for testing";
            dataSource.Group = n;

            int o = 12345;
            dataSource.Id = o;

            string expected = "class DataSource {\n  Name: 1683263926\n  DisplayName: Sample msg for testing\n  Group: Sample msg for testing\n  Id: 12345\n  SingleInstanceDS: False\n}\n";
            Assert.AreEqual(expected, dataSource.ToString());

        }

        [Test]
        public void TestValidFields()
        {
          int o = -12345;
          dataSource.Id = o;
          string expected = "DataSource Id " + o + " should not be negative.";
          string actual = dataSource.ValidField();
          Assert.AreEqual(expected, actual);
        }
    }
}
