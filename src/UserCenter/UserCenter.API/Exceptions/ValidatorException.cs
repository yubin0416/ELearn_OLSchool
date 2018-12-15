using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserCenter.API.Exceptions
{
    public class ValidatorException:Exception
    {
        public ValidatorException() : base() { }

        public ValidatorException(string message) : base(message) { }

        public ValidatorException(string message, Exception exception) : base(message, exception) { }
    }
}
