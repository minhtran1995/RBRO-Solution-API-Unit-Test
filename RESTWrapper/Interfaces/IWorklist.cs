using RESTWrapper.DataTransferObjects.MyMatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWrapper.Interfaces
{
    public interface IWorklist
    {
        Content[] Contents { get; }

        Task GetWorklist(string paramDomain, string paramUserid, string paramPassword, string paramServer);
    }
}
