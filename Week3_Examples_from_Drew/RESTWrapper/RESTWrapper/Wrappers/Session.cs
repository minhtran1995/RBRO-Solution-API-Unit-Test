using System.Collections.Generic;
using System.Threading.Tasks;
using RESTWrapper.Interfaces;
using RESTWrapper.DataTransferObjects;
using RESTWrapper.Exceptions;
using System;

namespace RESTWrapper.Wrappers
{
    public class Session : ISession
    {
        private RESTConnection mCon = null;

        private string mServer = null;
        private string mDomain = null;
        private string mUserId = null;
        private string mPassword = null;
        private string mPreferredDatabaseName = null;

        private IList<ILibrary> mLibraries;

        #region "Properties"

        public string Server
        {
            get { return mServer; }
        }

        public string UserId
        {
            get { return mUserId; }
        }

        public string PreferredDatabaseName
        {
            get { return mPreferredDatabaseName; }
        }

        public IList<ILibrary> Libraries
        {
            get { return mLibraries; }
        }
        
        #endregion

        public Session(RESTConnection con)
        {
            mCon = con;
        }

        public async Task Connect(string server, string domain, string userId, string password)
        {
            mServer = server;
            mDomain = domain;
            mUserId = userId;
            mPassword = password;

            mCon.Initialize(mServer, mDomain, mUserId, mPassword);

            BaseInput input = new BaseInput(mDomain, mUserId, mPassword);
            BaseOutput output = await mCon.InvokeMethod<BaseOutput>("Ping", input);
            if (output.statusCode == 0)
                await Initialize();
            else
                throw new OutputException(output);
        }

        private async Task Initialize()
        {
            BaseInput input = new BaseInput(mDomain, mUserId, mPassword);
            TopOutput output = await mCon.InvokeMethod<TopOutput>("Top", input);
            if (output.statusCode == 0)
            {
                mPreferredDatabaseName = output.PreferredDatabaseName;
                InitializeLibraries(output.Databases);

                //todo there are more fields returned that are not being handled yet
            }
            else
                throw new OutputException(output);
        }

        private void InitializeLibraries(IList<DatabaseData> dbOuputArray)
        {
            mLibraries = new List<ILibrary>();

            if (dbOuputArray != null)
            {
                foreach (DatabaseData dbOuput in dbOuputArray)
                {
                    Library library = new Library(mCon, dbOuput);
                    mLibraries.Add(library);
                }
            }
        }

        public ILibrary GetLibrary(string name)
        {
            ILibrary returnLibrary = null;

            if (mLibraries != null && !string.IsNullOrWhiteSpace(name))
            {
                foreach (ILibrary library in mLibraries)
                {
                    if (string.Compare(name, library.Name, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        returnLibrary = library;
                        break;
                    }
                }
            }

            return returnLibrary;
        }

    }
}
