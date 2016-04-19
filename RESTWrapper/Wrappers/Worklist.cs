using RESTWrapper.DataTransferObjects;
using RESTWrapper.DataTransferObjects.MyMatters;
using RESTWrapper.DataTransferObjects.WorkList;
using RESTWrapper.Exceptions;
using RESTWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWrapper.Wrappers
{
    public class Worklist : IWorklist
    {
        public RESTConnection mCon = null;

        AuthenticationInfoData Authentication { get; set; }

        Content[] contents = null;

        public Content[] Contents
        {
            get
            {
                return contents;
            }
        }

        public Worklist(RESTConnection con)
        {
            mCon = con;
        }

        public async Task GetWorklist(string paramDomain, string paramUserid, string paramPassword, string paramServer)
        {
            Authentication = new AuthenticationInfoData
            {
                domain = paramDomain,
                userID = paramUserid,
                password = paramPassword
            };
            BaseInput baseIn = new BaseInput();

            mCon.Initialize(paramServer, Authentication.domain, Authentication.userID, Authentication.password);

            WorklistInput input = new WorklistInput
            {
                Authentication = Authentication,
                Version = baseIn.Version,
                TimeZoneOffset = baseIn.TimeZoneOffset
            };
            WorklistOutput output = await mCon.InvokeMethod<WorklistOutput>("WorkList", input);

            if (output.statusCode == 0)
            {
                populateContents(output);
            }

            else {
                throw new OutputException(output);
            }

        }

        private void populateContents(WorklistOutput output) {
            contents = new Content[output.Contents.Length];

            for (int i = 0; i < output.Contents.Length; i++) {
                contents[i] = output.Contents[i];
            }
        }
    }
}
