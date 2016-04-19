using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTWrapper.DataTransferObjects.MyMatters;

namespace RESTWrapper.Interfaces
{
    public interface ICheckoutList
    {
        Content[] Contents { get; }

        Task GetCheckoutList(string paramDomain, string paramUserid, string paramPassword, string paramServer);
    }
}
