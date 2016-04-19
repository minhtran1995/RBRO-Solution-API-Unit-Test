using System.Net.Http;

namespace UnitTests.FakeClasses
{
    class RequestValue
    {
        public string Uri { get; private set; }

        public RequestValue(HttpRequestMessage request)
        {
            Uri = request.RequestUri.ToString();
        }
    }
}
