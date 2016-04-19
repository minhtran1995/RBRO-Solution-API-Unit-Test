using Newtonsoft.Json;
using RESTWrapper.DataTransferObjects.MyMatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWrapper.DataTransferObjects.MyFavorites
{
    [Serializable]
    [JsonObject]
   public class MyFavoritesOutput : BaseOutput
    {
        
        public String Owner { get; set; }
        public Int32 ObjectType { get; set; }
        public AdditionalProperty[] AddlProps { get; set; }
        public Int64 EditTime { get; set; }
        public Int32 Inherited { get; set; }
        public String DBName { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Int64 FolderID { get; set; }
        public Int64 ParentFolderID { get; set; }
        public Folder[] Subfolders { get; set; }
        public Content[] Contents { get; set; }
        public String TargetDatabaseName { get; set; }
        public Int64 TargetFolderID { get; set; }
        public Int32 TargetFolderKind { get; set; }
        public Int32 FolderKind { get; set; }
        public Profile Profile { get; set; }
        public Folder Target { get; set; }

        //
        public contentOP[] ContentsOp { get; set; }
        //

        public Int32 DefaultSecurity { get; set; }


    }
}
