/*
* Copyright, 2021, LogicMonitor, Inc.
* This Source Code Form is subject to the terms of the 
* Mozilla Public License, v. 2.0. If a copy of the MPL 
* was not distributed with this file, You can obtain 
* one at https://mozilla.org/MPL/2.0/.
*/

using LogicMonitor.DataSDK.Model;
using NUnit.Framework;
using System;


namespace LogicMonitor.DataSDK.Tests
{
   [TestFixture]
   public class TestConfiguration
   {
       Configuration config;
       
       [Test]
       public void TestCheckAuthenticationLMv1()
        {
           string AccessID  = "Shkjafhdfhjshs";
           string AccessKey ="sdkHfi924urlasfd";
           string msg = null;
           try
           {
               config = new Configuration(company: "lmabcd", accessID: AccessID, accessKey: AccessKey);
           }
           catch (Exception e)
           {
               msg = e.Message;
           }

           Assert.AreEqual(null, msg);

       }

        [Test]
        public void TestCheckAuthenticationBearer()
        {
            string msg = null;
            string bearToken = "SDAASDadasihu83ir510{}[]";
            try
            {
                config = new Configuration(company: "lmabcd", bearerToken:bearToken);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            Assert.AreEqual(null, msg);
        }
       [Test]
       public void TestNullCompany()
       {
           try
           {
               string Company = "";

                Configuration configration = new Configuration(Company,accessID:"sfdfaFdasdfdgfdag",accessKey:"DASDASDASDyt5y54");
           }
           catch (Exception e)
           {
               Assert.AreEqual("Company must have your account name", e.Message);
           }
       }
       [TestCase("https://lmabcd.logicmonitor.com/rest")]
       public void TestHost(string expected)
       {
           Assert.AreEqual(expected, config.host);
       }
       [TestCase(false)]
       public void TestAsyncRequestGet(bool async)
       {

            string AccessID = "Shkjafhdfhjshs";
            string AccessKey = "sdkHfi924urlasfd";
            config = new Configuration(company: "lmabcd", accessID: AccessID, accessKey: AccessKey);
            Assert.AreEqual(async, config.AsyncRequest);
       }

       [TestCase(true)]
       public void TestAsyncRequestSet(bool async)
       {
           string AccessID = "Shkjafhdfhjshs";
           string AccessKey = "sdkHfi924urlasfd";
           config = new Configuration(company: "lmabcd", accessID: AccessID, accessKey: AccessKey);
           config.AsyncRequest = true;
           Assert.AreEqual(async, config.AsyncRequest);
       }
       [TestCase("lmabcd")]
       public void TestCompanyGet(string company)
       {
           Assert.AreEqual(company, config.Company);
       }
       [TestCase("lmabcd")]
       public void TestCompanySet(string company)
       {
           config.Company = "lmabcd";
           Assert.AreEqual(company, config.Company);
       }

   }
}
