using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RESTWrapper.DataTransferObjects.MyMatters
{
    public class Profile
    {
        public String DatabaseName { get; set; }
        public Fields[] Fields { get; set; }
        public bool IsEmail { get; set; }

    }
}
