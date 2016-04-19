using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTWrapper.Interfaces
{
    public interface ISession
    {
        string Server { get; }
        string UserId { get; }
        string PreferredDatabaseName { get; }
        IList<ILibrary> Libraries { get; }

        Task Connect(string server, string domain, string userId, string password);

        ILibrary GetLibrary(string name);


    }
}
