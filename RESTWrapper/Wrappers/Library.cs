using System.Threading.Tasks;
using RESTWrapper.Interfaces;
using RESTWrapper.DataTransferObjects;
using RESTWrapper.Exceptions;
using System.Collections.Generic;

namespace RESTWrapper.Wrappers
{
    public class Library : ILibrary
    {
        private RESTConnection mCon = null;
        private string mName = null;

        public string Name
        {
            get { return mName; }
        }

        internal Library(RESTConnection con, DatabaseData output)
        {
            mCon = con;
            mName = output.Name;
        }

    }
}
