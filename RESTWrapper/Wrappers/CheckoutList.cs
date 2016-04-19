using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTWrapper.DataTransferObjects.CheckoutList;
using RESTWrapper.DataTransferObjects;
using RESTWrapper.DataTransferObjects.MyMatters;
using RESTWrapper.Interfaces;
using RESTWrapper.Exceptions;

namespace RESTWrapper.Wrappers
{
    public class CheckoutList : ICheckoutList
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

        public CheckoutList(RESTConnection con)
        {
            mCon = con;
        }

        public async Task GetCheckoutList(string paramDomain, string paramUserid, string paramPassword, string paramServer)
        {
            Authentication = new AuthenticationInfoData
            {
                domain = paramDomain,
                userID = paramUserid,
                password = paramPassword
            };
            BaseInput baseIn = new BaseInput();

            mCon.Initialize(paramServer, Authentication.domain, Authentication.userID, Authentication.password);

            CheckoutListInput input = new CheckoutListInput
            {
                Authentication = Authentication,
                Version = baseIn.Version,
                TimeZoneOffset = baseIn.TimeZoneOffset
            };
            CheckoutListOutput output = await mCon.InvokeMethod<CheckoutListOutput>("CheckoutList", input);

            if (output.statusCode == 0)
            {
                populateContents(output);
            }

            else {
                throw new OutputException(output);
            }

        }

        private void populateContents(CheckoutListOutput output)
        {
            contents = new Content[output.Contents.Length];

            for (int i = 0; i < output.Contents.Length; i++)
            {
                contents[i] = output.Contents[i];
            }
        }
    }
}
