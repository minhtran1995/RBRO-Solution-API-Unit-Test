using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTWrapper.DataTransferObjects.MyMatters;
using System.Net.Http;
using UnitTests.FakeClasses;
using RESTWrapper;
using RESTWrapper.Interfaces;
using RESTWrapper.Wrappers;
using System.Globalization;
using RESTWrapper.DataTransferObjects;
using RESTWrapper.Exceptions;

namespace UnitTests
{
    [TestClass]
    public class WorklistTest
    {
        //private vals
        private const bool FAKE = false;
        


        [TestMethod]
        public void TestWorklist()
        {
           string userID = "acmeadmin",
           password = "Pa$$w0rd",
           domain = string.Empty,
           server = "rbroserver:85";



            //setup connection
            HttpClient client = null;
            ResponseHandler handler = null;
            if (!FAKE)
                client = new HttpClient();
            else
            {
                handler = new ResponseHandler();
                handler.AddFakeResponse(Responses.GetFakeWorkListResponse());

                client = new HttpClient(handler, true);
            }
            RESTConnection con = new RESTConnection(client);

            try
            {

                //test
                IWorklist workList = new Worklist(con);
                workList.GetWorklist(domain, userID, password, server).Wait();


                //validate values
                Content[] contents = new Content[] {
                    new Content {
                        AddlProps = null,
                        DatabaseName = "Active",
                        Profile = new Profile {
                            DatabaseName = "Active",
                            Fields = new Fields[] {
                                new Fields { Key=0,Value="Active" },
                                new Fields { Key=1,Value= (long)412 },
                                new Fields { Key=2,Value=(long)1 },
                                new Fields{ Key=3,Value="deleteDataQuickRetrive"},
                                new Fields{ Key=4,Value=""},
                                new Fields{ Key=5,Value="ACMEADMIN"},
                                new Fields{ Key=6,Value="ACMEADMIN"},
                                new Fields{ Key=7,Value="ANSI"},
                                new Fields{ Key=8,Value="COMPARE"},
                                new Fields{ Key=9,Value=""},
                                new Fields{ Key=10,Value=(long)1457633476},
                                new Fields{ Key=11,Value=(long)1457630894},
                                new Fields{ Key=13,Value=(long)244},
                                new Fields{ Key=16,Value="DEFSERVER:\\ACMEACTIVE\\ACMEADMI\\0\\1\\412.1"},
                                new Fields{ Key=17,Value=(long)88},
                                new Fields{ Key=19,Value=""},
                                new Fields{ Key=21,Value=false},
                                new Fields{ Key=22,Value=false},
                                new Fields{ Key=23,Value=false},
                                new Fields{ Key=24,Value=""},
                                new Fields{ Key=25,Value=""},
                                new Fields{ Key=26,Value=""},
                                new Fields{ Key=102,Value=false},
                                new Fields{ Key=111,Value=false},
                                new Fields{ Key=116,Value=(long)1459524125},
                                new Fields{ Key=120,Value=""},
                                new Fields{ Key=40,Value=""},
                                new Fields{ Key=39,Value=""},
                                new Fields{ Key=37,Value=""},
                                new Fields{ Key=38,Value=""},
                                new Fields{ Key=45,Value=null},
                                new Fields{ Key=46,Value=null},
                                new Fields{ Key=82,Value="TXT"}
                            },                            
                            IsEmail = false 
                        },
                        Shortcut = null
                    }
                };

                if (FAKE)
                {
                    //validate requests
                    string rootPath = string.Format(CultureInfo.InvariantCulture, "http://{0}/Mobility2/MobilityService.svc", server);
                    Assert.AreEqual(1, handler.RequestValues.Count);
                    Assert.AreEqual(rootPath + "/WorkList", handler.RequestValues[0].Uri);
                }

                Assert.AreEqual(1, workList.Contents.Length);

                for (int i=0; i < workList.Contents.Length;i++)
                {
                    Assert.AreEqual(contents[i].AddlProps, workList.Contents[i].AddlProps);
                    Assert.AreEqual(contents[i].DatabaseName, workList.Contents[i].DatabaseName);
                    Assert.AreEqual(contents[i].Shortcut, workList.Contents[i].Shortcut);

                    Assert.AreEqual(contents[i].Profile.DatabaseName, workList.Contents[i].Profile.DatabaseName);
                    Assert.AreEqual(contents[i].Profile.IsEmail, workList.Contents[i].Profile.IsEmail);

                    for (int f = 0; f < contents[i].Profile.Fields.Length; f++) {
                        Assert.AreEqual(contents[i].Profile.Fields[f].Key, workList.Contents[i].Profile.Fields[f].Key);
                        Assert.AreEqual(contents[i].Profile.Fields[f].Value, workList.Contents[i].Profile.Fields[f].Value);
                    }
                }
                
            }

            finally
            {
                //clean up
                con.Dispose();
            }
        }


        [TestMethod]
        public void TestWorkList_Wrong_IdOrPassword()
        {
            int errorCode = 314159;

            string userID = "bad",
              password = "pass",
              domain = string.Empty,
              server = "rbroserver:85";


            //setup connection
            HttpClient client = null;
            ResponseHandler handler = null;
            if (!FAKE)
                client = new HttpClient();
            else
            {
                handler = new ResponseHandler();
                handler.AddFakeResponse(Responses.GetFakeWorkListResponse(314159));

                client = new HttpClient(handler, true);
            }
            RESTConnection con = new RESTConnection(client);

            try
            {
                //test
                IWorklist workList = new Worklist(con);
                try
                {

                    workList.GetWorklist(domain, userID, password, server).Wait();
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
        public void TestWorkList_Wrong_Server()
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
                IWorklist workList = new Worklist(con);
                try
                {
                    if (System.Diagnostics.Process.GetProcessesByName("Fiddler").Length > 0)
                    {
                        Assert.IsTrue(false, "Fiddler is running, It should be closed for testing HTTPResponse");
                    }

                    workList.GetWorklist(domain, userID, password, server).Wait();
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

