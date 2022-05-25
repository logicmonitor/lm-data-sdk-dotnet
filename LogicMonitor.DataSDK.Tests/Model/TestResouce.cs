using System;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestResource
    {

    public Resource res;
    public Dictionary<string, string> i;
    public Dictionary<string, string> properties;
    [SetUp]
    public void SetUp()
    {
      res = new Resource();
      i = new Dictionary<string, string>();
      i.Add("test", "test");
      res.Ids = i;
      res.Description = "Sample Desc";
      res.Name = "TestName";
      properties = new Dictionary<string, string>();
      properties.Add("resource.type", "local");
      res.Properties = properties;
    }
    [Test]
        public void TestIds()
        {
            Dictionary<string, string> msg = res.Ids;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestName()
        {
            string i = "Name";
            res.Name = i;
            string msg = res.Name;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestDiscription()
        {
            string i = "Discription";
            res.Description = i;
            string msg = res.Description;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestCreate()
        {
            bool i = false;
            res.Create = i;
            bool msg = res.Create;
            Assert.AreEqual(msg, i);
        }
    //issue with properties to string
    [Test]
    public void TesttoString()
    {
      string expected = "class DataPoint {\n  Ids{\n   test:test\n  }\n  Name: TestName\n  Description: Sample Desc\n  Properties: System.Collections.Generic.Dictionary`2[System.String,System.String]\n  Create: False\n}\n";
      string actual = res.ToString();
      Assert.AreEqual(expected, actual);

    }
       
    }
}
