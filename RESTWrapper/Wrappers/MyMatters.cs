using System;
using System.Collections.Generic;

using RESTWrapper.DataTransferObjects;
using System.Threading.Tasks;
using RESTWrapper.Exceptions;
using RESTWrapper.DataTransferObjects.MyMatters;
using RESTWrapper.Interfaces;


namespace RESTWrapper.Wrappers
{
    public class MyMatters : IMyMatters
    {
        public RESTConnection mCon = null;

        AuthenticationInfoData Authentication { get; set; }

        private Folder mFolder { get; set; }

        public Folder Folder
        {
            get
            {
                return mFolder;
            }
        }

        public MyMatters(RESTConnection con)
        {
            mCon = con;
        }
        
        public async Task GetMyMatters(string paramDomain,string paramUserid, string paramPassword, string paramServer)
        {
            Authentication = new AuthenticationInfoData {
                domain = paramDomain,
                userID = paramUserid,
                password = paramPassword
            };
            BaseInput baseIn = new BaseInput();

            mCon.Initialize(paramServer, Authentication.domain, Authentication.userID, Authentication.password);

            MyMattersInput input = new MyMattersInput
            {
                Authentication = Authentication,
                Version = baseIn.Version,
                TimeZoneOffset = baseIn.TimeZoneOffset
            };
            MyMattersOutput output = await mCon.InvokeMethod<MyMattersOutput>("MyMatters", input);

            if (output.statusCode == 0)
            {
                InitializeFolderInfo(output);
            }

            else {
                throw new OutputException(output);
            }

        }

        public void InitializeFolderInfo(MyMattersOutput folderInfo)
        {

            if (folderInfo != null)
            {
                mFolder = new Folder
                {
                    Owner = folderInfo.Owner,
                    ObjectType = folderInfo.ObjectType,
                    AddlProps = folderInfo.AddlProps,
                    EditTime = folderInfo.EditTime,
                    Inherited = folderInfo.Inherited,
                    DBName = folderInfo.DBName,
                    Name = folderInfo.Name,
                    Description = folderInfo.Description,
                    FolderID = folderInfo.FolderID,
                    ParentFolderID = folderInfo.ParentFolderID,
                    Subfolders = folderInfo.Subfolders,
                    Contents = folderInfo.Contents,
                    TargetDatabaseName = folderInfo.TargetDatabaseName,
                    TargetFolderID = folderInfo.TargetFolderID,
                    TargetFolderKind = folderInfo.TargetFolderKind,
                    FolderKind = folderInfo.FolderKind,
                    Profile = folderInfo.Profile,
                    Target = folderInfo.Target,
                    ContentsOp = folderInfo.ContentsOp,
                    DefaultSecurity = folderInfo.DefaultSecurity
                };
            }


        }

        public Folder getMyMattersFolder() {            
            return mFolder;
        }
    }
}
