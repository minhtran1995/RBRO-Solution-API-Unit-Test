using RESTWrapper.DataTransferObjects;
using RESTWrapper.DataTransferObjects.MyMatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWrapper.Interfaces
{
    public interface IMatterWorkList
    {       

        Folder[] folders { get; }       

        Task GetMatterWorkList(string paramDomain, string paramUserid, string paramPassword, string paramServer);
    }
}
