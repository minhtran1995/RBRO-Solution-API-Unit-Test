using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTWrapper.DataTransferObjects.MyFavorites;
using RESTWrapper.DataTransferObjects.MyMatters;


namespace RESTWrapper.Interfaces
{
    public interface IMyFavorites
    {
       
        Folder Folder { get; }

        Task GetMyFavorites(string paramDomain, string paramUserid, string paramPassword, string paramServer);
    }
}
