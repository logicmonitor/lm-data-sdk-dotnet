using System;
using LogicMonitor.DataSDK.Model;
using LogicMonitor.DataSDK.Utils;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestDataPoint
    {
        DataPoint dataPoint = new DataPoint();
        ObjectNameValidator objectNameValidator = new ObjectNameValidator();
        public string Name;
        public string AggregationType;
        public string Description;
        public string type;



        [Test]
        public void TestAggregationType()
        {
            DataPoint a = new DataPoint();
            string i = "Aggreaation";
            a.AggregationType = i;
            string msg = a.AggregationType;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestDescription()
        {
            DataPoint a = new DataPoint();
            string i = "Description";
            a.Description = i;
            string msg = a.Description;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestName()
        {
            DataPoint a = new DataPoint();
            string i = "Name";
            a.Name = i;
            string msg = a.Name;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestType()
        {
            DataPoint a = new DataPoint();
            string i = "Type";
            a.Type = i;
            string msg = a.Type;
            Assert.AreEqual(msg, i);
        }
        
    }
}

