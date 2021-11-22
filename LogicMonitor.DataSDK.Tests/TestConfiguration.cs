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
        Authenticate authentication;

        Configuration config;
        [SetUp]
        public void Setup()
        {

            authentication = new Authenticate();
            authentication.Key = "abckjahgdf7w46rih3kj";
            authentication.Type = "Bearer";
            authentication.Id = "1khgflshdkfuhlsdhf";
            config = new Configuration(company: "lmabcd",authentication);

        }
        [Test]
        public void TestCheckAuthenticationLMv1()
        {
            //Configuration configuration;

            String msg = null;

            Configuration configuration = new Configuration("company", authentication);
            var temp = false;

            try
            {
                temp = configuration.check_authentication(authentication: authentication);
                // Console.WriteLine("temp is " + temp);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            Assert.AreEqual(true, temp);
        }

        [Test]
        public void TestCheckAuthenticationBearer()
        {

            Authenticate auth = new Authenticate();

            String msg = null;
            Configuration configuration = new Configuration("company", authentication);
            var temp = false;

            try
            {
                temp = configuration.check_authentication(authentication: authentication);
                //Console.WriteLine("temp is " + temp);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            Assert.AreEqual(true, temp);
        }
        [Test]
        public void TestNullCompany()
        {
            try
            {
                string Company = "";
                Configuration config = new Configuration(Company, authentication);
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
            Assert.AreEqual(async, config.AsyncRequest);
        }

        [TestCase(true)]
        public void TestAsyncRequestSet(bool async)
        {
            config.AsyncRequest = true;
            Assert.AreEqual(async, config.AsyncRequest);
        }
        [TestCase("lmabcd")]
        public void TestCompanyGet(string company)
        {
            Assert.AreEqual(company, config.company);
        }
        [TestCase("lmabcd")]
        public void TestCompanySet(string company)
        {
            config.company = "lmabcd";
            Assert.AreEqual(company, config.company);
        }

    }
}
