using System;
using System.Collections.Generic;
using LogicMonitor.DataSDK.Model;
using NUnit.Framework;
namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestRestResourcePropertiesV1
    {
        RestResourcePropertiesV1 restrpv1 = new RestResourcePropertiesV1();        

        [Test]
        public void TestRestResourcePropertiesv1()
        {            
            Dictionary<string, string> test1 = new Dictionary<string, string>();
            test1.Add("testkey", "testvalues");
            Dictionary<string, string> test2 = new Dictionary<string, string>();
            test2.Add("testkey", "testvalues");
            RestResourcePropertiesV1 a = new RestResourcePropertiesV1(test1, "resourceName", test2);
            Assert.AreEqual("resourceName", a.ResourceName);
            Assert.AreEqual(test1, a.ResourceIds);
            Assert.AreEqual(test2, a.ResourceProperties);

        }
        [Test]
        public void TestResourceIds()
        {
            Dictionary<string, string> i = new Dictionary<string, string>();
            i.Add("testvalues", "testvalues");
            restrpv1.ResourceIds = i;
            Dictionary<string, string> msg = restrpv1.ResourceIds;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestResourceName()
        {
            string i = "ResourceName";
            restrpv1.ResourceName = i;
            string msg = restrpv1.ResourceName;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestResourceProperties()
        {
            Dictionary<string, string> i = new Dictionary<string, string>();
            i.Add("testvalues", "testvalues");
            restrpv1.ResourceProperties = i;
            Dictionary<string, string> msg = restrpv1.ResourceProperties;
            Assert.AreEqual(msg, i);
        }
        [Test]
        public void TestRestResourcePropertiesv1Constructor()
        {
            RestResourcePropertiesV1 r = new RestResourcePropertiesV1();
            Assert.AreEqual(r.ResourceIds, null);
            Assert.AreEqual(r.ResourceName, null);
            Assert.AreEqual(r.ResourceProperties, null);
        }

        [Test]
        public void TestToString()
        {
            Dictionary<string, string> property = new Dictionary<string, string>();
            property.Add("property", "propertyvalue");
            string name = "ResourceName";
            Dictionary<string, string> id = new Dictionary<string, string>();
            id.Add("testvalues", "testvalues");
            RestResourcePropertiesV1 r = new RestResourcePropertiesV1(id,name,property);
            Console.WriteLine(r.ToString());
            string expected = "class RestResourcePropertiesV1 {\n  RestResourcePropertiesV1{\n   property: propertyvalue\n  }\n  ResourceIds{\n   testvalues: testvalues\n  }\n  ResourceName: ResourceName\n}\n";
            Assert.AreEqual(expected, r.ToString());
        }

    }
}



