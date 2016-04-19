using Newtonsoft.Json;
using RESTWrapper.DataTransferObjects.MyMatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWrapper.DataTransferObjects.WorkList
{
    [Serializable]
    [JsonObject]
    public class WorklistOutput: BaseOutput
    {
        public Content[] Contents { get; set; }
    }
}
