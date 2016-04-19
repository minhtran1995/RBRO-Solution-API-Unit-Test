using RESTWrapper.DataTransferObjects;
using RESTWrapper.DataTransferObjects.MatterWorkList;
using RESTWrapper.DataTransferObjects.MyMatters;
using RESTWrapper.Exceptions;
using RESTWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWrapper.Wrappers
{
    public class MatterWorkList: IMatterWorkList
    {
        public RESTConnection mCon = null;
        AuthenticationInfoData Authentication { get; set; }

        public int serverVersion { get; set; }

        public Folder[] folders { get; set; }


        public MatterWorkList(RESTConnection con)
        {
            mCon = con;
        }


        public async Task GetMatterWorkList(string paramDomain, string paramUserid, string paramPassword, string paramServer)
        {
            Authentication = new AuthenticationInfoData
            {
                domain = paramDomain,
                userID = paramUserid,
                password = paramPassword
            };
            BaseInput baseIn = new BaseInput();

            mCon.Initialize(paramServer, Authentication.domain, Authentication.userID, Authentication.password);

            MatterWorkListInput input = new MatterWorkListInput
            {
                Authentication = Authentication,
                Version = baseIn.Version,
                TimeZoneOffset = baseIn.TimeZoneOffset
            };
            MatterWorkListOutput output = await mCon.InvokeMethod<MatterWorkListOutput>("MatterWorkList", input);

            if (output.statusCode == 0)
            {
                folders = output.Folders;
                serverVersion = output.ServerVersion;
            }

            else {
                throw new OutputException(output);
            }

        }

        

    }
}
