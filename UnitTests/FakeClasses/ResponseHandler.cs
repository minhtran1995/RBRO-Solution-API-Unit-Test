using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace UnitTests.FakeClasses
{
    class ResponseHandler : DelegatingHandler
    {
        private readonly IList<HttpResponseMessage> mFakeResponses = new List<HttpResponseMessage>();
        private readonly IList<RequestValue> mRequestValues = new List<RequestValue>();
        private int mNextResponse = 0;

        public IList<RequestValue> RequestValues
        {
            get { return mRequestValues; }
        }

        public void AddFakeResponse(HttpResponseMessage responseMessage)
        {
            mFakeResponses.Add(responseMessage);
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            RequestValue req = new RequestValue(request);
            mRequestValues.Add(req);

            HttpResponseMessage response = null;
            if (mFakeResponses.Count > mNextResponse)
            {
                response = mFakeResponses[mNextResponse];
                mNextResponse++;
            }
            else
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
            response.RequestMessage = request;

            return response;
        }
    }
}
