using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTWrapper;
using RESTWrapper.DataTransferObjects;
using RESTWrapper.DataTransferObjects.MyMatters;
using RESTWrapper.DataTransferObjects.CheckoutList;
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
    public class CheckoutListTest
    {
        private const bool FAKE = false;

        
        [TestMethod]
        public void TestCheckoutList()
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
                handler.AddFakeResponse(Responses.GetFakeCheckoutListResponse());

                client = new HttpClient(handler, true);
            }
            RESTConnection con = new RESTConnection(client);

            try
            {

                //test
                ICheckoutList checkoutList = new CheckoutList(con);
                checkoutList.GetCheckoutList(domain, userID, password, server).Wait();


                //validate values
                Content[] contents = new Content[] {
                    new Content {
                        DatabaseName ="Active",
                        AddlProps = null,
                        Profile = new Profile {
                            DatabaseName ="Active",
                            Fields = new Fields[] {
                                new Fields { Key=0,Value="Active" },
                                new Fields { Key=1,Value= (long)264 },
                                new Fields { Key=2,Value=(long)1 },
                                new Fields{ Key=3,Value="Order Granting Application for admission"},
                                new Fields{ Key=4,Value=""},
                                new Fields{ Key=5,Value="ACMEADMIN"},
                                new Fields{ Key=6,Value="ACMEADMIN"},
                                new Fields{ Key=7,Value="ACROBAT"},
                                new Fields{ Key=8,Value="TRIAL"},
                                new Fields{ Key=9,Value=""},
                                new Fields{ Key=10,Value=(long)1451422177},
                                new Fields{ Key=11,Value=(long)1451424760},
                                new Fields{ Key=13,Value=(long)8027},
                                new Fields{ Key=16,Value="DEFSERVER:\\ACMEACTIVE\\ACMEADMI\\0\\1\\264.1"},
                                new Fields{ Key=17,Value=(long)88},
                                new Fields{ Key=19,Value="ACMEADMIN"},
                                new Fields{ Key=21,Value=true},
                                new Fields{ Key=22,Value=true},
                                new Fields{ Key=23,Value=false},
                                new Fields{ Key=24,Value=""},
                                new Fields{ Key=25,Value="1000"},
                                new Fields{ Key=26,Value="9"},
                                new Fields{ Key=102,Value=false},
                                new Fields{ Key=111,Value=false},
                                new Fields{ Key=116,Value=(long)1451436249},
                                new Fields{ Key=120,Value=""},
                                new Fields{ Key=40,Value=""},
                                new Fields{ Key=39,Value=""},
                                new Fields{ Key=37,Value=""},
                                new Fields{ Key=38,Value=""},
                                new Fields{ Key=45,Value=null},
                                new Fields{ Key=46,Value=null},
                                new Fields{ Key=82,Value="PDF"}
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
                    Assert.AreEqual(rootPath + "/CheckoutList", handler.RequestValues[0].Uri);

                }

                Assert.AreEqual(1, checkoutList.Contents.Length);

                for (int i = 0; i < checkoutList.Contents.Length; i++)
                {
                    Assert.AreEqual(contents[i].AddlProps, checkoutList.Contents[i].AddlProps);
                    Assert.AreEqual(contents[i].DatabaseName, checkoutList.Contents[i].DatabaseName);
                    Assert.AreEqual(contents[i].Shortcut, checkoutList.Contents[i].Shortcut);

                    Assert.AreEqual(contents[i].Profile.DatabaseName, checkoutList.Contents[i].Profile.DatabaseName);
                    Assert.AreEqual(contents[i].Profile.IsEmail, checkoutList.Contents[i].Profile.IsEmail);

                    for (int f = 0; f < contents[i].Profile.Fields.Length; f++)
                    {
                        Assert.AreEqual(contents[i].Profile.Fields[f].Key, checkoutList.Contents[i].Profile.Fields[f].Key);
                        Assert.AreEqual(contents[i].Profile.Fields[f].Value, checkoutList.Contents[i].Profile.Fields[f].Value);
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
        public void TestCheckoutList_Wrong_IdOrPassword()
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
                handler.AddFakeResponse(Responses.GetFakeCheckoutListResponse(314159));

                client = new HttpClient(handler, true);
            }
            RESTConnection con = new RESTConnection(client);

            try
            {
                //test
                ICheckoutList checkoutList = new CheckoutList(con);
                try
                {

                    checkoutList.GetCheckoutList(domain, userID, password, server).Wait();
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
        public void TestCheckoutList_Wrong_Server()
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
                ICheckoutList checkoutList = new CheckoutList(con);
                try
                {
                    if (System.Diagnostics.Process.GetProcessesByName("Fiddler").Length > 0)
                    {
                        Assert.IsTrue(false, "Fiddler is running, It should be closed for testing HTTPResponse");
                    }

                    checkoutList.GetCheckoutList(domain, userID, password, server).Wait();
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
