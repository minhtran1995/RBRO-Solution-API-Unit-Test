using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWrapper.Exceptions
{
    [Serializable]
    public class WrongServerException: Exception
    {

        public WrongServerException()
        {
            ;
        }

        public WrongServerException(string message) : base(message)
        {
            ;
        }

        public WrongServerException(string message, Exception innerException) : base(message, innerException)
        {
            ;
        }


    }
}
