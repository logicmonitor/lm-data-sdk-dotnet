/*
 * Copyright, 2021, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */

using NUnit.Framework;
using LogicMonitor.DataSDK;
using RestSharp;
using System;
using LogicMonitor.DataSDK.Model;
using System.Collections.Generic;

namespace LogicMonitor.DataSDK.Tests
{
    [TestFixture]
    public class Tests
    {
        Configuration configuration;
        ApiClients apiClients;


        [SetUp]
        public void Setup()
        {
            Authenticate authenticate = new Authenticate();
            authenticate.Id = "xz5i6L7Pz44k4wAb4b5r";
            authenticate.Key = "F972B{jWL-JI+Z}M=(aA~~=fcD(y[^993]pCyjS+";
            authenticate.Type = "LMv1";
            configuration = new Configuration(company: "lmaakashkhopade", authentication: authenticate);
            apiClients = new ApiClients(configuration);
        }

        [Test]
        public void TestHeaderContentType()
        {
            string content = "application/json";
            Assert.AreEqual(content, apiClients.SelectHeaderAccept("application/json"));
        }
        [Test]
        public void TestHeaderContentTypeEmpty()
        {
            string content = "application/json";
            Assert.AreEqual(content, apiClients.SelectHeaderContentType(""));
        }

        [Test]
        public void TestHeaderAccept()
        {
            string content = "application/json";
            Assert.AreEqual(content, apiClients.SelectHeaderAccept("application/json"));
        }
        [Test]
        public void TestHeaderAcceptEmpty()
        {
            string content = "application/json";
            Assert.AreEqual(content, apiClients.SelectHeaderContentType(""));
        }

        

        [Test]
        public void TestCallapi()
        {

            Dictionary<string, string> pathParams = new Dictionary<string, string>();
            var queryParams = new Dictionary<string, string>();

            var headersParams = new Dictionary<String, string>();
            headersParams.Add("Accept", "application/json");
            headersParams.Add("Content-Type", "application/json");
            var a = apiClients.Callapi(path: "/metric/ingest", method: "POST",_request_timeout: TimeSpan.FromMinutes(2), pathParams:pathParams,queryParams:queryParams,headerParams:headersParams);
            //Console.WriteLine(a.Content.ToString());
            Assert.Pass();
        }
    }
}
