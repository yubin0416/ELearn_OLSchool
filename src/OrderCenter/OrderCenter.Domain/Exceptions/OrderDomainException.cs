using System;
using System.Collections.Generic;
using System.Text;

namespace OrderCenter.Domain.Exceptions
{
    public class OrderDomainException:Exception
    {
        public OrderDomainException() : base() { }

        public OrderDomainException(string message) : base(message) { }

        public OrderDomainException(string message, Exception exception) : base(message, exception) { }
    }
}
