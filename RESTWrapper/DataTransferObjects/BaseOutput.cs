using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace RESTWrapper.DataTransferObjects
{
    [Serializable]
    [JsonObject]
    public class BaseOutput
    {
        public int ServerVersion { get; set; }
        public int statusCode { get; set; }
        public string statusDetail { get; set; }
        public int Success { get; set; }
    }

   
}
