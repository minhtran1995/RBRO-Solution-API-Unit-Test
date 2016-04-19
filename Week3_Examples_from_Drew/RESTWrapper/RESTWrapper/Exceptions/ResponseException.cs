using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace RESTWrapper.Exceptions
{
    [Serializable]
    public class ResponseException : Exception
    {
        public HttpResponseMessage Response { get; private set; }

        public ResponseException()
        {
            ;
        }

        public ResponseException(string message) : base(message)
        {
            ;
        }

        public ResponseException(string message, Exception innerException) : base(message, innerException)
        {
            ;
        }

        protected ResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Response = (HttpResponseMessage)info.GetValue("Response", typeof(HttpResponseMessage)); 
        }

        public ResponseException(HttpResponseMessage response) : base(response.ReasonPhrase)
        {
            Response = response;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Response", Response, typeof(HttpResponseMessage));
            base.GetObjectData(info, context);
        }
    }
}
