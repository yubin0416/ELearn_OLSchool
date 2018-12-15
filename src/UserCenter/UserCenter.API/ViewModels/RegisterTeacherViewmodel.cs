using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserCenter.API.ViewModels
{
    public class RegisterTeacherViewmodel
    {
        public string Mobile { get; set; }

        public string Code { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public string Introduce { get; set; }
    }
}
