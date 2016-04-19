using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.FakeClasses;
using System.Net.Http;
using RESTWrapper;
using RESTWrapper.Interfaces;
using RESTWrapper.DataTransferObjects.MyMatters;
using System.Globalization;
using RESTWrapper.Exceptions;
using RESTWrapper.Wrappers;

namespace UnitTests
{
    /// <summary>
    /// Summary description for MyFavoritesTest
    /// </summary>
    [TestClass]
    public class MyFavoritesTest
    {
        private const bool FAKE = false;
        string server = "rbroserver:85";

        [TestMethod]
        public void TestMyFavorites()
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
                handler.AddFakeResponse(Responses.GetFakeMyFavoritesResponse());

                client = new HttpClient(handler, true);
            }
            RESTConnection con = new RESTConnection(client);

            try
            {
                IMyFavorites myFavorites = new MyFavorites(con);
                myFavorites.GetMyFavorites(domain, userID, password, server).Wait();

                if (FAKE)
                {
                    string rootPath = string.Format(CultureInfo.InvariantCulture, "http://{0}/Mobility2/MobilityService.svc", server);
                    Assert.AreEqual(1, handler.RequestValues.Count);
                    Assert.AreEqual(rootPath + "/MyFavorites", handler.RequestValues[0].Uri);
                }



                //output check
                contentOP[] op = new contentOP[4]
                {
                    new contentOP () { Key=0, Value=true },
                    new contentOP () { Key=1, Value=true },
                    new contentOP () { Key=2, Value=false },
                    new contentOP () { Key=3, Value=true }
                };

                for (int i = 0; i < op.Length; i++)
                {
                    Assert.AreEqual(op[i].Key, myFavorites.Folder.ContentsOp[i].Key);
                    Assert.AreEqual(op[i].Value, myFavorites.Folder.ContentsOp[i].Value);
                }

                Content[] TestContents = new Content[] { new Content {
                    AddlProps = null,
                    DatabaseName = "",
                    Profile = null,
                    Shortcut = new ContentShortcut {
                        Description = "deleteDataQuickRetrive",
                        TargetDatabase = "Active",
                        TargetDocNum = 412,
                        TargetDocType = "ANSI",
                        TargetDocVer = 1
                    }
                }
                };



                Assert.AreEqual(TestContents[0].AddlProps, myFavorites.Folder.Contents[0].AddlProps);
                Assert.AreEqual(TestContents[0].DatabaseName, myFavorites.Folder.Contents[0].DatabaseName);
                Assert.AreEqual(TestContents[0].Profile, myFavorites.Folder.Contents[0].Profile);
                Assert.AreEqual(TestContents[0].Shortcut.Description, myFavorites.Folder.Contents[0].Shortcut.Description);
                Assert.AreEqual(TestContents[0].Shortcut.TargetDatabase, myFavorites.Folder.Contents[0].Shortcut.TargetDatabase);
                Assert.AreEqual(TestContents[0].Shortcut.TargetDocNum, myFavorites.Folder.Contents[0].Shortcut.TargetDocNum);
                Assert.AreEqual(TestContents[0].Shortcut.TargetDocType, myFavorites.Folder.Contents[0].Shortcut.TargetDocType);
                Assert.AreEqual(TestContents[0].Shortcut.TargetDocVer, myFavorites.Folder.Contents[0].Shortcut.TargetDocVer);

                Assert.AreEqual("Active", myFavorites.Folder.DBName);
                Assert.AreEqual(88, myFavorites.Folder.DefaultSecurity);
                Assert.AreEqual("", myFavorites.Folder.Description);
                Assert.AreEqual(1458230737, myFavorites.Folder.EditTime);
                Assert.AreEqual(350, myFavorites.Folder.FolderID);
                Assert.AreEqual(1, myFavorites.Folder.FolderKind);
                Assert.AreEqual(0, myFavorites.Folder.Inherited);
                Assert.AreEqual("My Favorites", myFavorites.Folder.Name);
                Assert.AreEqual(4294967299, myFavorites.Folder.ParentFolderID);



                Folder[] sub = new Folder[1] { new Folder {
                    DBName = "Active",
                    DefaultSecurity=73,
                    EditTime=1458230737,
                    FolderID=940,
                    Inherited =1,
                    Name="TEST",
                    ParentFolderID=350,
                    TargetDatabaseName="Active",
                    TargetFolderID=939,
                    TargetFolderKind=3
                } };




                Assert.AreEqual(sub.Length, myFavorites.Folder.Subfolders.Length);


                Assert.AreEqual(sub[0].DBName, myFavorites.Folder.Subfolders[0].DBName);
                Assert.AreEqual(sub[0].DefaultSecurity, myFavorites.Folder.Subfolders[0].DefaultSecurity);
                Assert.AreEqual(sub[0].EditTime, myFavorites.Folder.Subfolders[0].EditTime);
                Assert.AreEqual(sub[0].FolderID, myFavorites.Folder.Subfolders[0].FolderID);
                Assert.AreEqual(sub[0].Inherited, myFavorites.Folder.Subfolders[0].Inherited);
                Assert.AreEqual(sub[0].Name, myFavorites.Folder.Subfolders[0].Name);
                Assert.AreEqual(sub[0].ParentFolderID, myFavorites.Folder.Subfolders[0].ParentFolderID);
                Assert.AreEqual(sub[0].TargetDatabaseName, myFavorites.Folder.Subfolders[0].TargetDatabaseName);
                Assert.AreEqual(sub[0].TargetFolderID, myFavorites.Folder.Subfolders[0].TargetFolderID);
                Assert.AreEqual(sub[0].TargetFolderKind, myFavorites.Folder.Subfolders[0].TargetFolderKind);


                Assert.AreEqual("", myFavorites.Folder.TargetDatabaseName);
                Assert.AreEqual(0, myFavorites.Folder.TargetFolderID);
                Assert.AreEqual(3, myFavorites.Folder.TargetFolderKind);
            }
            finally
            {
                con.Dispose();

            }
        }

        [TestMethod]
        public void TestMyFavorites_Wrong_IdOrPassword()
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
                handler.AddFakeResponse(Responses.GetFakeMyFavoritesResponse(314159));

                client = new HttpClient(handler, true);
            }
            RESTConnection con = new RESTConnection(client);

            try
            {
                //test
                IMyFavorites myFavorites = new MyFavorites(con);
                try
                {


                    myFavorites.GetMyFavorites(domain, userID, password, server).Wait();
                    Assert.IsTrue(false);//
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
        public void TestMyFavorites_Wrong_Server()
        {
            server = "bad";
            //expected values
            WrongServerException exception = new WrongServerException("Server Is Not Properly Configed", new HttpRequestException());

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
                handler.AddFakeResponse(Responses.GetWrongServerResponse());
                client = new HttpClient(handler, true);
            }

            RESTConnection con = new RESTConnection(client);

            try
            {
                //test
                IMyFavorites myFavorites = new MyFavorites(con);
                try
                {
                    if (System.Diagnostics.Process.GetProcessesByName("Fiddler").Length > 0)
                    {
                        Assert.IsTrue(false, "Fiddler is running, It should be closed for testing HTTPResponse");
                    }

                    myFavorites.GetMyFavorites(domain,userID, password, server).Wait();



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
