using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWrapper.DataTransferObjects
{
    class DMSDetailOutput :BaseOutput
    {

        public string DMSServer { get; set; }
        public RoleValueData[] role { get; set; }

   
}
}
