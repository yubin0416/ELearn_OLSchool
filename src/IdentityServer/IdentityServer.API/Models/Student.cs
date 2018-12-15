using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Models
{
    public class Student
    {
        public string ID { get; set; }

        public string Mobile { get; set; }

        public string Password { get; set; }

        public string NickName { get; set; }

        public string Picture { get; set; }
    }
}
