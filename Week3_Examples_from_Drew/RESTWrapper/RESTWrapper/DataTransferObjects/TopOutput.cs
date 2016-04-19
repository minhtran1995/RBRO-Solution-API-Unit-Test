using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RESTWrapper.DataTransferObjects
{
    [Serializable]
    [JsonObject]
    public class TopOutput : BaseOutput
    {
        public IList<DatabaseData> Databases { get; set; }
        public string DMSName { get; set; }
        public int FlatSpaceFiling { get; set; }
        public string PreferredDatabaseName { get; set; }
        public int ReplaceOriginal { get; set; }
        public string SessionName { get; set; }
        public string UserId { get; set; }

        //todo there are more fields returned that are not being handled yet
    }
}
