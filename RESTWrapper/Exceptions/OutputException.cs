using System;
using System.Runtime.Serialization;
using RESTWrapper.DataTransferObjects;

namespace RESTWrapper.Exceptions
{
    [Serializable]
    public class OutputException : Exception
    {
        public BaseOutput Output { get; private set; }

        

        public OutputException()
        {
            ;
        }

        public OutputException(string message) : base(message)
        {
            ;
        }

        public OutputException(string message, Exception innerException) : base(message, innerException)
        {
            ;
        }

        protected OutputException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Output = (BaseOutput)info.GetValue("Output", typeof(BaseOutput));
        }

        public OutputException(BaseOutput output) : base(output.statusDetail)
        {
            Output = output;
        }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Output", Output, typeof(BaseOutput));
            base.GetObjectData(info, context);
        }

    }
}