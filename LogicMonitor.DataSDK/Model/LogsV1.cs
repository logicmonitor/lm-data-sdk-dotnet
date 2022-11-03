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
    [DataContract(Name ="LogsV1")]
    public class LogsV1:IInput
    {


        public LogsV1()
        {
        }

        public LogsV1(string message, Dictionary<string, string> resourceIds, string timeStamp = default, Dictionary<string, string> metaData = default)
        {
            Message = message;
            Timestamp = timeStamp;
            MetaData = new Dictionary<string, string>();
            ResourceId = new Dictionary<string, string>();
            MetaData = metaData;
            ResourceId = resourceIds;
        }

        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        [DataMember(Name = "_lm.resourceId", EmitDefaultValue = false)]
        public Dictionary<string, string> ResourceId { get; set; }

        [DataMember(Name = "Body", EmitDefaultValue = false)]
        public Dictionary<string, string> Body { get; set; }

        [DataMember(Name = "timestamp", EmitDefaultValue = false)]
        public string Timestamp { get; set; }

        [DataMember(Name = "metaData", EmitDefaultValue = false)]
        public Dictionary<string, string> MetaData { get; set; }

        

        
       
    }
    
}