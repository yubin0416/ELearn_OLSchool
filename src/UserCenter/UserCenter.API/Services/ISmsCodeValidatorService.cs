using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserCenter.API.Services
{
    public interface ISmsCodeValidatorService
    {
        bool Validate(string mobile, string code);
    }
}
