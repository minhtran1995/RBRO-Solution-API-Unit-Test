using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTWrapper;
using RESTWrapper.DataTransferObjects.MyMatters;
using RESTWrapper.Exceptions;
using RESTWrapper.Interfaces;
using RESTWrapper.Wrappers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnitTests.FakeClasses;

namespace UnitTests
{
    [TestClass]
    public class MyMattersTests
    {
        private const bool FAKE = true;

        

        string server = "rbroserver:85";
        [TestMethod]
        public void TestMyMatters()
        {
            string userID = "acmeadmin",
            password = "Pa$$w0rd",
            domain = string.Empty;

            //setup connection
            HttpClient client = null;
            ResponseHandler handler = null;
            if (!FAKE)
                client = new HttpClient();
            else
            {
                handler = new ResponseHandler();
                handler.AddFakeResponse(Responses.GetFakeMyMattersResponse());

                client = new HttpClient(handler, true);
            }
            RESTConnection con = new RESTConnection(client);

            try
            {

                //test
                IMyMatters myMatters = new MyMatters(con);
                myMatters.GetMyMatters(domain,userID,password, server).Wait();


                //validate values

                if (FAKE)
                {
                    //validate requests
                    string rootPath = string.Format(CultureInfo.InvariantCulture, "http://{0}/Mobility2/MobilityService.svc", server);
                    Assert.AreEqual(1, handler.RequestValues.Count);
                    Assert.AreEqual(rootPath + "/MyMatters", handler.RequestValues[0].Uri);

                }

                //output check

                contentOP[] op = new contentOP[4] {
                    new contentOP() { Key=0,Value=false},
                    new contentOP() { Key=1,Value=false},
                    new contentOP() { Key=2,Value=false},
                    new contentOP() { Key=3,Value=false}
                };

                Assert.AreEqual(op[0].Key, myMatters.Folder.ContentsOp[0].Key);
                Assert.AreEqual(op[0].Value, myMatters.Folder.ContentsOp[0].Value);

                Assert.AreEqual(op[1].Key, myMatters.Folder.ContentsOp[1].Key);
                Assert.AreEqual(op[1].Value, myMatters.Folder.ContentsOp[1].Value);
                Assert.AreEqual(op[2].Key, myMatters.Folder.ContentsOp[2].Key);
                Assert.AreEqual(op[2].Value, myMatters.Folder.ContentsOp[2].Value);
                Assert.AreEqual(op[3].Key, myMatters.Folder.ContentsOp[3].Key);
                Assert.AreEqual(op[3].Value, myMatters.Folder.ContentsOp[3].Value);


                Assert.AreEqual("Active", myMatters.Folder.DBName);
                Assert.AreEqual(88, myMatters.Folder.DefaultSecurity);
                Assert.AreEqual("", myMatters.Folder.Description);
                Assert.AreEqual(1458240493, myMatters.Folder.EditTime);
                Assert.AreEqual(349, myMatters.Folder.FolderID);
                Assert.AreEqual(0, myMatters.Folder.FolderKind);
                Assert.AreEqual(0, myMatters.Folder.Inherited);
                Assert.AreEqual("My Matters", myMatters.Folder.Name);
                Assert.AreEqual(4294967299, myMatters.Folder.ParentFolderID);
               


                Folder[] sub = new Folder[1] { new Folder {
                    DBName = "Active",
                    DefaultSecurity=73,
                    EditTime=1458240493,
                    FolderID=941,
                    Inherited =1,
                    Name="CORP - 1090 - Lollipop Incorporated",
                    ParentFolderID=349,
                    TargetDatabaseName="Active",
                    TargetFolderID=591,
                    TargetFolderKind=2
                } };

                Assert.AreEqual(sub.Length, myMatters.Folder.Subfolders.Length);


                Assert.AreEqual(sub.Length, myMatters.Folder.Subfolders.Length);

                Assert.AreEqual(sub[0].DBName, myMatters.Folder.Subfolders[0].DBName);
                Assert.AreEqual(sub[0].DefaultSecurity, myMatters.Folder.Subfolders[0].DefaultSecurity);
                Assert.AreEqual(sub[0].EditTime, myMatters.Folder.Subfolders[0].EditTime);
                Assert.AreEqual(sub[0].FolderID, myMatters.Folder.Subfolders[0].FolderID);
                Assert.AreEqual(sub[0].Inherited, myMatters.Folder.Subfolders[0].Inherited);
                Assert.AreEqual(sub[0].Name, myMatters.Folder.Subfolders[0].Name);
                Assert.AreEqual(sub[0].ParentFolderID, myMatters.Folder.Subfolders[0].ParentFolderID);
                Assert.AreEqual(sub[0].TargetDatabaseName, myMatters.Folder.Subfolders[0].TargetDatabaseName);
                Assert.AreEqual(sub[0].TargetFolderID, myMatters.Folder.Subfolders[0].TargetFolderID);
                Assert.AreEqual(sub[0].TargetFolderKind, myMatters.Folder.Subfolders[0].TargetFolderKind);




                Assert.AreEqual("", myMatters.Folder.TargetDatabaseName);
                Assert.AreEqual(0, myMatters.Folder.TargetFolderID);
                Assert.AreEqual(3, myMatters.Folder.TargetFolderKind);
            }
            finally
            {
                //clean up
                con.Dispose();
            }
        }

        [TestMethod]
        public void TestMyMatters_Wrong_IdOrPassword()
        {
            int errorCode = 314159;

            string userID = "bad",
             password = "pass",
             domain = string.Empty;
            

            //setup connection
            HttpClient client = null;
            ResponseHandler handler = null;
            if (!FAKE)
                client = new HttpClient();
            else
            {
                handler = new ResponseHandler();
                handler.AddFakeResponse(Responses.GetFakeMyMattersResponse(314159));

                client = new HttpClient(handler, true);
            }
            RESTConnection con = new RESTConnection(client);

            try
            {
                //test
                IMyMatters myMatters = new MyMatters(con);
                try
                {
                    myMatters.GetMyMatters(domain,userID,password, server).Wait();
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
        public void TestMyMatters_Wrong_Server()
        {
            
            //expected values
            WrongServerException exception = new WrongServerException("Server Is Not Properly Configed", new HttpRequestException());

            
                string userID = "bad",
                password = "pass",
                domain = string.Empty,
                server = "bad";


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
                IMyMatters myMatters = new MyMatters(con);
                try
                {
                    if (System.Diagnostics.Process.GetProcessesByName("Fiddler").Length > 0)
                    {
                        Assert.IsTrue(false, "Fiddler is running, It should be closed for testing HTTPResponse");
                    }

                    myMatters.GetMyMatters(domain,userID,password, server).Wait();



                    Assert.IsTrue(false);
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

