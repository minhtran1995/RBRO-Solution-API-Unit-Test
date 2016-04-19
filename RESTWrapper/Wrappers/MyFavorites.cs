using RESTWrapper.DataTransferObjects;
using RESTWrapper.DataTransferObjects.MyFavorites;
using RESTWrapper.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTWrapper.Interfaces;
using RESTWrapper.DataTransferObjects.MyMatters;

namespace RESTWrapper.Wrappers
{
    public class MyFavorites: IMyFavorites
    {
        private RESTConnection mCon = null;
        AuthenticationInfoData Authentication { get; set; }

        private Folder mFolder { get; set; }


        public Folder Folder
        {
            get
            {
                return mFolder;
            }
        }

        public MyFavorites(RESTConnection con)
        {
            mCon = con;
        }

        public async Task GetMyFavorites(string paramDomain, string paramUserid, string paramPassword, string paramServer)
        {
            Authentication = new AuthenticationInfoData
            {
                domain = paramDomain,
                userID = paramUserid,
                password = paramPassword
            };


            BaseInput baseIn = new BaseInput();

            mCon.Initialize(paramServer, Authentication.domain, Authentication.userID, Authentication.password);


            MyFavoritesInput input = new MyFavoritesInput
            {
                Authentication = Authentication,
                Version = baseIn.Version,
                TimeZoneOffset = baseIn.TimeZoneOffset
            };
            MyFavoritesOutput output = await mCon.InvokeMethod<MyFavoritesOutput>("MyFavorites", input);
              

            if (output.statusCode == 0)
            {
                InitializeFolderInfo(output);
            }

            else {
                throw new OutputException(output);
            }


        }

        public void InitializeFolderInfo(MyFavoritesOutput folderInfo)
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
        public Folder getMyFavoritesFolder()
        {
            return mFolder;
        }
    }
}