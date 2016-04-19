using RESTWrapper.DataTransferObjects;
using RESTWrapper.DataTransferObjects.CheckoutList;
using RESTWrapper.DataTransferObjects.MatterWorkList;
using RESTWrapper.DataTransferObjects.MyFavorites;
using RESTWrapper.DataTransferObjects.MyMatters;
using RESTWrapper.DataTransferObjects.WorkList;
using RESTWrapper.Exceptions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace UnitTests.FakeClasses
{
    internal static class Responses
    {
        internal static HttpResponseMessage GetFakePingResponse()
        {
            return GetFakePingResponse(0);
        }

        internal static HttpResponseMessage GetFakePingResponse(int statusCode)
        {
            BaseOutput output = new BaseOutput();
            SetBaseOutput(statusCode, output);

            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new ObjectContent<BaseOutput>(output, new JsonMediaTypeFormatter()),
            };

            return response;
        }

        internal static HttpResponseMessage GetFakeTopResponse(string fakeResponse1, string fakeResponse2)
        {
            DatabaseData db1 = new DatabaseData()
            {
                Name = fakeResponse1,
                Description = "Database1",
            };

            DatabaseData db2 = new DatabaseData()
            {
                Name = fakeResponse2,
                Description = "Database2",
            };
            TopOutput output = new TopOutput()
            {
                PreferredDatabaseName = fakeResponse1,
                Databases = new DatabaseData[] { db1, db2 },
            };
            SetBaseOutput(0, output);

            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new ObjectContent<BaseOutput>(output, new JsonMediaTypeFormatter()),
            };

            return response;
        }

        internal static HttpResponseMessage GetWrongServerResponse()
        {
            BaseOutput output = new BaseOutput();


            //bad gate way http response message
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.BadGateway)
            {
                Content = new ObjectContent<BaseOutput>(output, new JsonMediaTypeFormatter()),
            };

            return response;
        }

        //our code start from here

        #region GetFakeMyMattersResponse definition
        internal static HttpResponseMessage GetFakeMyMattersResponse(int statuscode = 0)
        {
            MyMattersOutput output = new MyMattersOutput()
            {
                ContentsOp = new contentOP[] {
                    new contentOP { Key = 0, Value = false },
                    new contentOP { Key = 1, Value = false },
                    new contentOP { Key = 2, Value = false },
                    new contentOP { Key = 3, Value = false }
                },
                DBName = "Active",
                DefaultSecurity = 88,
                Description = string.Empty,
                EditTime = 1458240493,
                FolderID = 349,
                FolderKind = 0,
                Inherited = 0,
                Name = "My Matters",
                ParentFolderID = 4294967299,
                Subfolders = new Folder[] {
                new Folder {
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
                } },
                TargetDatabaseName = string.Empty,
                TargetFolderID = 0,
                TargetFolderKind = 3


            };

            SetBaseOutput(statuscode, output);

            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new ObjectContent<MyMattersOutput>(output, new JsonMediaTypeFormatter()),
            };



            return response;
        }
        #endregion

        #region GetFakeMatterWorkListResponse definition
        internal static HttpResponseMessage GetFakeMatterWorkListResponse(int statusCode = 0)
        {
            MatterWorkListOutput output = new MatterWorkListOutput()
            {

                #region ArrayFolders
                Folders = new Folder[] {
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
                            //There are more fields that are not being handled
                            },
                        IsEmail = false},
                        Subfolders= new Folder[] { },
                        TargetDatabaseName= string.Empty,
                        TargetFolderID=0,
                        TargetFolderKind=3
                    }
                }
                #endregion
            };




            SetBaseOutput(statusCode, output);

            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new ObjectContent<MatterWorkListOutput>(output, new JsonMediaTypeFormatter()),
            };



            return response;
        }
        #endregion

        #region GetFakeMyFavoritesResponse definition
        internal static HttpResponseMessage GetFakeMyFavoritesResponse(int statuscode = 0)
        {
            MyFavoritesOutput output = new MyFavoritesOutput()
            {
                Contents = new Content[] {
                    new Content {
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
                },

                ContentsOp = new contentOP[] {
                    new contentOP { Key = 0, Value = true },
                    new contentOP { Key = 1, Value = true },
                    new contentOP { Key = 2, Value = false },
                    new contentOP { Key = 3, Value = true }
                },
                DBName = "Active",
                DefaultSecurity = 88,
                Description = string.Empty,
                EditTime = 1458230737,
                FolderID = 350,
                FolderKind = 1,
                Inherited = 0,
                Name = "My Favorites",
                ParentFolderID = 4294967299,
                Subfolders = new Folder[] {
                    new Folder {
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
                    }
                },
                TargetDatabaseName = string.Empty,
                TargetFolderID = 0,
                TargetFolderKind = 3


            };

            SetBaseOutput(statuscode, output);

            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new ObjectContent<MyFavoritesOutput>(output, new JsonMediaTypeFormatter()),
            };



            return response;
        }
        #endregion

        #region GetFakeWorkListResponse definition
        internal static HttpResponseMessage GetFakeWorkListResponse(int statuscode = 0)
        {
            WorklistOutput output = new WorklistOutput()
            {
                Contents = new Content[] {
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
                }
        };

            SetBaseOutput(statuscode, output);

            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new ObjectContent<WorklistOutput>(output, new JsonMediaTypeFormatter()),
            };

            return response;
        }
        #endregion

        #region GetFakeCheckoutListResponse definition
        internal static HttpResponseMessage GetFakeCheckoutListResponse(int statusCode = 0)
        {
            CheckoutListOutput output = new CheckoutListOutput()
            {
                Contents = new Content[] {
                    new Content {
                        AddlProps = null,
                        DatabaseName = "Active",
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
                }
            };

            SetBaseOutput(statusCode, output);

            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new ObjectContent<CheckoutListOutput>(output, new JsonMediaTypeFormatter()),
            };

            return response;
        }
        #endregion

        private static void SetBaseOutput(int statusCode, BaseOutput output)
        {
            output.ServerVersion = 3;
            output.statusCode = statusCode;
            output.statusDetail = (statusCode == 0 ? "Success" : "Failed");
            output.Success = (statusCode == 0 ? 0 : 1);
        }
    }
}
