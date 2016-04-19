using Newtonsoft.Json;
using System;

namespace RESTWrapper.DataTransferObjects
{
    [Serializable]
    [JsonObject]
    public class DatabaseData
    {
        public string Description { get; set; }
        public int EmailFieldBcc { get; set; }
        public string Name { get; set; }

        //todo there are more fields returned that are not being handled yet
    }
}
