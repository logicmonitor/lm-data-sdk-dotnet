using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using RestSharp;
namespace LogicMonitor.DataSDK.Model
{
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