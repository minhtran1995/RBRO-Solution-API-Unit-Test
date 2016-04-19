using Newtonsoft.Json;
using RESTWrapper.DataTransferObjects.MyMatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWrapper.DataTransferObjects.CheckoutList
{
    [Serializable]
    [JsonObject]

    public class CheckoutListOutput : BaseOutput
    {
       

        public Content[] Contents { get; set; }
    }
}
