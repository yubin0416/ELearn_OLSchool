using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserCenter.API.Services
{
    public class SmsCodeValidatorService : ISmsCodeValidatorService
    {
        public bool Validate(string mobile, string code)
        {
            return true;
        }
    }
}
