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
using System.Net;

namespace UnitTests
{
    [TestClass]
    public class SessionTests
    {
        private const bool FAKE = false;

        [TestMethod]
        public void TestConnect()
        {
            string server = "rbroserver:85";
            string domain = string.Empty;
            string userId = "acmeadmin";
            string password = "Pa$$w0rd";
            string libraryName1 = "Active";
            string libraryName2 = "Archive";

            //setup connection
            HttpClient client = null;
            ResponseHandler handler = null;
            if (!FAKE)
                client = new HttpClient();
            else
            {
                handler = new ResponseHandler();
                handler.AddFakeResponse(Responses.GetFakePingResponse());
                handler.AddFakeResponse(Responses.GetFakeTopResponse("Active", "Archive"));
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
                Assert.AreEqual(libraryName1, session.PreferredDatabaseName);


                Assert.AreEqual(2, session.Libraries.Count, "Lib count");
                Assert.AreEqual(libraryName1, session.Libraries[0].Name);
                Assert.AreEqual(libraryName2, session.Libraries[1].Name);
            }
            finally
            {
                //clean up
                con.Dispose();
            }
        }

        [TestMethod]
        public void TestConnectFail_WrongID_password()
        {
            string server = "rbroserver:85";
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
                handler.AddFakeResponse(Responses.GetFakeTopResponse("WorkSite9a", "Archive"));
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


        [TestMethod]
        public void TestConnectFail_WrongServer()
        {
            string server = "wrong:1080";
            string domain = string.Empty;
            string userId = "bad";
            string password = "password";

            //expected values
            WrongServerException exception = new WrongServerException("Server Is Not Properly Configed", new HttpRequestException());



            //setup connection
            HttpClient client = null;
            ResponseHandler handler = null;
            if (!FAKE)
                client = new HttpClient();
            else
            {
                handler = new ResponseHandler();
                handler.AddFakeResponse(Responses.GetWrongServerResponse());               
                client = new HttpClient(handler, true);
            }


            RESTConnection con = new RESTConnection(client);

            try
            {
                //test
                ISession session = new Session(con);
                try
                {
                    
                    if (System.Diagnostics.Process.GetProcessesByName("Fiddler").Length > 0)
                    {
                        Assert.IsTrue(false, "Fiddler is running, It should be closed for testing HTTPResponse");
                    }

                    session.Connect(server, domain, userId, password).Wait();

                    Assert.IsTrue(false, "");
                }
                catch (AggregateException ex)
                {

                    Assert.IsNotNull(ex.InnerException);
                    Assert.IsInstanceOfType(ex.InnerException, typeof(WrongServerException));

                    Assert.IsInstanceOfType(ex.InnerException.InnerException, typeof(HttpRequestException));

                    Assert.AreEqual(exception.Message, ((WrongServerException)ex.InnerException).Message);


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
