/*
 * Copyright, 2022, LogicMonitor, Inc.
 * This Source Code Form is subject to the terms of the 
 * Mozilla Public License, v. 2.0. If a copy of the MPL 
 * was not distributed with this file, You can obtain 
 * one at https://mozilla.org/MPL/2.0/.
 */
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace LogicMonitor.DataSDK.Model
{
    /// <summary>
    /// This Class is used to define the Logs data.
    /// </summary>
    [DataContract]
    public class LogsV1
    {

        
        public LogsV1()
        {
        }

        public LogsV1(string message,Dictionary<string,string> resourceIds, string timeStamp = default, Dictionary<string,string> metaData = default)
        {
            Message = message;
            Timestamp = timeStamp;
            MetaData = new Dictionary<string, string>();
            ResourceId = new Dictionary<string, string>();

            MetaData = metaData;
            ResourceId = resourceIds; 
        }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public Dictionary<string, string> Body { get; set; }

        [DataMember]
        public string Timestamp { get; set; }

        [DataMember]
        public Dictionary<string, string> MetaData { get; set; }

        [DataMember]
        public Dictionary<string,string> ResourceId { get; set; }


        private void CreateBody()
        {
            string resource = JsonConvert.SerializeObject(ResourceId);

            Body = new Dictionary<string, string>();
            Body.Add("message", Message);
            Body.Add("_lm.resourceId", resource);
            Body.Add("timestamp", Timestamp);
            Body.Add("metadata", JsonConvert.SerializeObject(MetaData));
        }
        
        public override string ToString()
        {
            CreateBody();
            var bodyString = JsonConvert.SerializeObject(Body);
            bodyString = bodyString.Replace(@"\", "");
            bodyString = bodyString.Replace("\"{", "{");
            bodyString = bodyString.Replace("}\"", "}");
            bodyString = "[" + bodyString + "]";
            return bodyString;
        }
    }
}