using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTWrapper;
using RESTWrapper.DataTransferObjects;
using RESTWrapper.DataTransferObjects.MyMatters;
using RESTWrapper.Exceptions;
using RESTWrapper.Interfaces;
using RESTWrapper.Wrappers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using UnitTests.FakeClasses;

namespace UnitTests
{
    [TestClass]
    public class MatterWorkListTest
    {
        private const bool FAKE = false;

        string server = "rbroserver:85";

        [TestMethod]
        public void TestMatterWorkList()
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
                handler.AddFakeResponse(Responses.GetFakeMatterWorkListResponse());

                client = new HttpClient(handler, true);
            }
            RESTConnection con = new RESTConnection(client);

            try
            {

                //test
                IMatterWorkList matterWorkList = new MatterWorkList(con);
                matterWorkList.GetMatterWorkList(domain,userID,password,server).Wait();


                //validate values

                if (FAKE)
                {
                    //validate requests
                    string rootPath = string.Format(CultureInfo.InvariantCulture, "http://{0}/Mobility2/MobilityService.svc", server);
                    Assert.AreEqual(1, handler.RequestValues.Count);
                    Assert.AreEqual(rootPath + "/MatterWorkList", handler.RequestValues[0].Uri);

                }

                #region Declare TestValue
                Folder[] testValue = new Folder[] {
                    new Folder {
                        DBName ="Active",
                        DefaultSecurity =88,
                        Description="",
                        EditTime =1458232697,
                        FolderID =591,
                        FolderKind =2,
                        Inherited =0,
                        Name ="CORP - 1090 - Lollipop Incorporated",
                        ParentFolderID = 4294967300,
                        Profile = new Profile {
                            DatabaseName ="Active",
                            Fields = new Fields[] {
                            new Fields { Key=0,Value="Active" },
                            new Fields { Key=1,Value= (long)81 },
                            new Fields { Key=2,Value=(long)1 },
                            new Fields{ Key=3,Value=""},
                            new Fields{ Key=4,Value="CORP - 1090 - Lollipop Incorporated"},
                            new Fields{ Key=5,Value="ACMEADMIN"},
                            new Fields{ Key=6,Value="ACMEADMIN"},
                            new Fields{ Key=7,Value="XML"},
                            new Fields{ Key=8,Value="WEBDOC"},
                            new Fields{ Key=9,Value=""},
                            new Fields{ Key=10,Value=(long)1451427200},
                            new Fields{ Key=11,Value=(long)1451427200},
                            new Fields{ Key=13,Value=(long)4200},
                            new Fields{ Key=16,Value="DEFSERVER:\\ACMEACTIVE\\ACMEADMI\\0\\1\\81.1"},
                            new Fields{ Key=17,Value=(long)88},
                            new Fields{ Key=19,Value=""},
                            new Fields{ Key=21,Value=false},
                            new Fields{ Key=22,Value=false},
                            new Fields{ Key=23,Value=false},
                            new Fields{ Key=24,Value=""},
                            new Fields{ Key=25,Value="1090"},
                            new Fields{ Key=26,Value="1"},
                            new Fields{ Key=102,Value=false},
                            new Fields{ Key=111,Value=false},
                            new Fields{ Key=116,Value=(long)1451427200},
                            new Fields{ Key=120,Value=""},
                            new Fields{ Key=40,Value=""},
                            new Fields{ Key=39,Value=""},
                            new Fields{ Key=37,Value=""},
                            new Fields{ Key=38,Value=""},
                            new Fields{ Key=45,Value=null},
                            new Fields{ Key=46,Value=null},
                            new Fields{ Key=82,Value="XML"}
                            },

                            //There are more fields that are not being handled
        
                        IsEmail = false},
                        Subfolders= new Folder[] { },
                        TargetDatabaseName= string.Empty,
                        TargetFolderID=0,
                        TargetFolderKind=3
                    }
                };
                #endregion

                Assert.AreEqual(testValue.Length, matterWorkList.folders.Length);


                Assert.AreEqual(testValue[0].FolderID, matterWorkList.folders[0].FolderID);
                Assert.AreEqual(testValue[0].DBName, matterWorkList.folders[0].DBName);
                Assert.AreEqual(testValue[0].DefaultSecurity, matterWorkList.folders[0].DefaultSecurity);
                Assert.AreEqual(testValue[0].Description, matterWorkList.folders[0].Description);
                Assert.AreEqual(testValue[0].EditTime, matterWorkList.folders[0].EditTime);
                Assert.AreEqual(testValue[0].FolderID, matterWorkList.folders[0].FolderID);
                Assert.AreEqual(testValue[0].FolderKind, matterWorkList.folders[0].FolderKind);
                Assert.AreEqual(testValue[0].Inherited, matterWorkList.folders[0].Inherited);
                Assert.AreEqual(testValue[0].Name, matterWorkList.folders[0].Name);
                Assert.AreEqual(testValue[0].ParentFolderID, matterWorkList.folders[0].ParentFolderID);

                Assert.AreEqual(testValue[0].Profile.DatabaseName, matterWorkList.folders[0].Profile.DatabaseName);
                Assert.AreEqual(testValue[0].Profile.IsEmail, matterWorkList.folders[0].Profile.IsEmail);

                for (int i = 0; i < testValue[0].Profile.Fields.Length; i++)
                {
                    Assert.AreEqual(testValue[0].Profile.Fields[i].Key, matterWorkList.folders[0].Profile.Fields[i].Key);
                    Assert.AreEqual(testValue[0].Profile.Fields[i].Value, matterWorkList.folders[0].Profile.Fields[i].Value);
                }


                Assert.AreEqual(testValue[0].TargetDatabaseName, matterWorkList.folders[0].TargetDatabaseName);
                Assert.AreEqual(testValue[0].TargetFolderID, matterWorkList.folders[0].TargetFolderID);
                Assert.AreEqual(testValue[0].TargetFolderKind, matterWorkList.folders[0].TargetFolderKind);






            }
            finally
            {
                //clean up
                con.Dispose();
            }
        }



        [TestMethod]
        public void TestMatterWorkList_Wrong_IdOrPassword()
        {
            int errorCode = 314159;

            string userID = "wrong",
                password = "wrong",
                domain = string.Empty;            

            //setup connection
            HttpClient client = null;
            ResponseHandler handler = null;
            if (!FAKE)
                client = new HttpClient();
            else
            {
                handler = new ResponseHandler();
                handler.AddFakeResponse(Responses.GetFakeMatterWorkListResponse(314159));

                client = new HttpClient(handler, true);
            }
            RESTConnection con = new RESTConnection(client);

            try
            {
                //test
                IMatterWorkList matterWorkList = new MatterWorkList(con);
                try
                {

                    matterWorkList.GetMatterWorkList(domain,userID,password,server).Wait();
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
        public void TestMatterWorkList_Wrong_Server()
        {
           
            //expected values
            WrongServerException exception = new WrongServerException("Server Is Not Properly Configed", new HttpRequestException());


            string userID = "wrong",
                password = "wrong",
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
                IMatterWorkList matterWorkList = new MatterWorkList(con);
                try
                {
                    
                    if (System.Diagnostics.Process.GetProcessesByName("Fiddler").Length > 0)
                    {
                        Assert.IsTrue(false, "Fiddler is running, It should be closed for testing HTTPResponse");
                    }

                    matterWorkList.GetMatterWorkList(domain,userID,password,server).Wait();
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
