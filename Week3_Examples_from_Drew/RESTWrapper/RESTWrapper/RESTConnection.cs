using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using RESTWrapper.DataTransferObjects;
using RESTWrapper.Exceptions;

namespace RESTWrapper
{
    public class RESTConnection : IDisposable
    {
        private const string BasePath = "/Mobility2/MobilityService.svc";

        private HttpClient mClient = null;
        private bool mInitialized = false;
        private string mDomain = null;
        private string mUserId = null;
        private string mPassword = null;

        #region "Properties"

        internal string Domain
        {
            get { return mDomain; }
        }

        internal string UserId
        {
            get { return mUserId; }
        }

        internal string Password
        {
            get { return mPassword; }
        }

        #endregion

        #region IDisposable Support

        private bool mDisposedValue = false; // To detect redundant calls

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!mDisposedValue)
            {
                if (disposing)
                {
                    if (mClient != null)
                        mClient.Dispose();
                }

                mClient = null;
                mDisposedValue = true;
            }
        }

        #endregion

        public RESTConnection(HttpClient client)
        {
            mClient = client;
        }

        internal void Initialize(string server, string domain, string userId, string password)
        {
            if (!mInitialized)
            {
                mClient.BaseAddress = new Uri("http://" + server);
                mClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                mClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Encrypt(domain, userId, password));

                mDomain = domain;
                mUserId = userId;
                mPassword = password;

                mInitialized = true;
            }
            else
                throw new InvalidOperationException("RESTConnection already initialized");
        }

        private static string Encrypt(string domain, string userId, string password)
        {
            string userName = userId;
            if (!string.IsNullOrWhiteSpace(domain))
                userName = domain + "\\" + userId;

            return Convert.ToBase64String(Encoding.Default.GetBytes(userName + ":" + password));
        }

        internal async Task<T> InvokeMethod<T>(string name, BaseInput input)
        {
            T output = default(T);

            if (mInitialized) {
                using (HttpResponseMessage response = await mClient.PostAsJsonAsync(BasePath + '/' + name, input))
                {
                    if (response.IsSuccessStatusCode)
                        output = await response.Content.ReadAsAsync<T>();
                    else
                        throw new ResponseException(response);
                }
            }
            else
                throw new InvalidOperationException("RESTConnection not initialized");

            return output;
        }

    }
}
