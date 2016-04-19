using Newtonsoft.Json;
using RESTWrapper.DataTransferObjects.MyMatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWrapper.DataTransferObjects.MatterWorkList
{
    [Serializable]
    [JsonObject]
    public class MatterWorkListOutput : BaseOutput
    {

        public int serverVersion;

        public Folder[] Folders;

    }
}
