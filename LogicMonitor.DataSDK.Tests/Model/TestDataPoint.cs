using System;
using LogicMonitor.DataSDK.Model;
using LogicMonitor.DataSDK.Utils;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestDataPoint
    {
        DataPoint a = new DataPoint(name:"name");
        ObjectNameValidator objectNameValidator = new ObjectNameValidator();
        public string Name;
        public string AggregationType;
        public string Description;
        public string type;



        [Test]
        public void TestAggregationType()
        {
            string i = "Aggreaation";
            a.AggregationType = i;
            string msg = a.AggregationType;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestDescription()
        {
            string i = "Description";
            a.Description = i;
            string msg = a.Description;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestName()
        {
            string i = "Name";
            a.Name = i;
            string msg = a.Name;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestType()
        {
            string i = "Type";
            a.Type = i;
            string msg = a.Type;
            Assert.AreEqual(msg, i);
        }

        [Test]
        public void TestToString()
        {
            string i = "1683263926";
            a.AggregationType = i;


            string m = "Sample msg for testing";
            a.Description = m;

            string n = "Sample msg for testing";
            a.Name = n;

            string o = "Sample msg for testing";
            a.Type = o;

            string expected = "class DataPoint {\n  Aggregation: 1683263926\n  Description: Sample msg for testing\n  Name: Sample msg for testing\n  Type: Sample msg for testing\n}\n";
            Assert.AreEqual(expected, a.ToString());

        }
    }
}

