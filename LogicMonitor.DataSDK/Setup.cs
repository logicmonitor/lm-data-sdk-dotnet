/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */
using System;
namespace LogicMonitor.DataSDK
{
    public class Setup
    {
        public const string PackageID = "LogicMonitor.DataSDK";
        public const string PackageVersion = "0.0.7-alpha";
        public const string Author = "LogicMonitor";
        public const string AuthorEmail = "support@logicmonior.com";

        public struct Path
        {
            public static readonly string IngestPath = "/v2/metric/ingest";
            public static readonly string UpdateResourcePropertyPath = "/resource_property/ingest";
            public static readonly string UpdateInsatancePropertyPath = "/instance_property/ingest";
        };
    
    }
}