

using RESTWrapper.DataTransferObjects;
using RESTWrapper.DataTransferObjects.MyMatters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTWrapper.Interfaces
{
    public interface IMyMatters
    {

        Folder Folder { get; }


        Task GetMyMatters(string paramDomain,string paramUserid, string paramPassword, string paramServer);

    }
}
