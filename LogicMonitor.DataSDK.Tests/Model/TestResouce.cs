using System;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestResource
    {
        
        Resource res=new Resource();

        [Test]
        public void TestIds()
        {
            Dictionary<string, string> i = new Dictionary<string, string>();
            i.Add("test", "test");
            res.Ids = i;
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

       
    }
}
