using System;
using LogicMonitor.DataSDK.Model;
using NUnit.Framework;

namespace LogicMonitor.DataSDK.Tests.Model
{
    [TestFixture]
    public class TestAuthenticate
    {
        [Test]
        public void TestId()
        {
            Authenticate a = new Authenticate();
            string i= "12334546586";
            a.Id = i;
            string id = a.Id;
            Assert.AreEqual(id, i);
        }

        [Test]
        public void Testkey()
        {
            Authenticate a = new Authenticate();
            string k = "12334546586";
            a.Key = k;
            string key = a.Key;
            Assert.AreEqual(key, k);
        }

        [Test]
        public void TestAuthenticateConstructor()
        {
            Authenticate a = new Authenticate();
           
            Assert.AreEqual(null,a.Id);
            Assert.AreEqual(null,a.Id);

        }

        [TestCase("LMv1")]
        [TestCase("Bearer")]
        public void TestValidType(string type)
        {
            Authenticate a = new Authenticate();
            a.Type = type;
            Assert.AreEqual(type, a.Type);
        }

        [TestCase("LMlv1")]
        [TestCase("auth")]
        public void TestInValidType(string type)
        {
            Authenticate a = new Authenticate();
            try
            {
                a.Type = type;

            }
            catch (Exception e)
            {
                Assert.AreEqual("Type must be 'LMv1' or 'Bearer' ",e.Message);
            }
        }
    }


}
