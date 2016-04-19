using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using RESTWrapper;
using RESTWrapper.Interfaces;
using RESTWrapper.Wrappers;
using RESTWrapper.DataTransferObjects;
using UnitTests.FakeClasses;
using System;
using RESTWrapper.Exceptions;
using System.Globalization;

namespace UnitTests
{
    [TestClass]
    public class SessionTests
    {
        private const bool FAKE = true;

        [TestMethod]
        public void TestConnect()
        {
            string server = "169.254.80.90";
            string domain = string.Empty;
            string userId = "admin";
            string password = "password";
            string libraryName = "WorkSite9a";

            //setup connection
            HttpClient client = null;
            ResponseHandler handler = null;
            if (!FAKE)
                client = new HttpClient();
            else
            {
                handler = new ResponseHandler();
                handler.AddFakeResponse(Responses.GetFakePingResponse());
                handler.AddFakeResponse(Responses.GetFakeTopResponse());
                client = new HttpClient(handler, true);
            }
            RESTConnection con = new RESTConnection(client);

            try
            {
                //test
                ISession session = new Session(con);
                session.Connect(server, domain, userId, password).Wait();

                if (FAKE)
                {
                    //validate requests
                    string rootPath = string.Format(CultureInfo.InvariantCulture, "http://{0}/Mobility2/MobilityService.svc", server);
                    Assert.AreEqual(2, handler.RequestValues.Count);
                    Assert.AreEqual(rootPath + "/Ping", handler.RequestValues[0].Uri);
                    Assert.AreEqual(rootPath + "/Top", handler.RequestValues[1].Uri);
                }

                //validate values
                Assert.AreEqual(server, session.Server);
                Assert.AreEqual(userId, session.UserId);
                Assert.AreEqual(libraryName, session.PreferredDatabaseName);
                Assert.AreEqual(1, session.Libraries.Count);
                Assert.AreEqual(libraryName, session.Libraries[0].Name);
            }
            finally
            {
                //clean up
                con.Dispose();
            }               
        }

        [TestMethod]
        public void TestConnectFail()
        {
            string server = "169.254.80.90";
            string domain = string.Empty;
            string userId = "bad";
            string password = "password";
            int errorCode = 314159;

            //setup connection
            HttpClient client = null;
            ResponseHandler handler = null;
            if (!FAKE)
                client = new HttpClient();
            else
            {
                handler = new ResponseHandler();
                handler.AddFakeResponse(Responses.GetFakePingResponse(errorCode));
                handler.AddFakeResponse(Responses.GetFakeTopResponse());
                client = new HttpClient(handler, true);
            }
            RESTConnection con = new RESTConnection(client);

            try
            {
                //test
                ISession session = new Session(con);
                try
                {
                    session.Connect(server, domain, userId, password).Wait();
                    Assert.IsTrue(false);
                }
                catch (AggregateException ex)
                {
                    Assert.IsNotNull(ex.InnerException);
                    Assert.IsInstanceOfType(ex.InnerException, typeof(OutputException));
                    Assert.AreEqual(((OutputException)ex.InnerException).Output.statusCode, errorCode);
                }
            }
            finally
            {
                //clean up
                con.Dispose();
            }
        }
    }
}
