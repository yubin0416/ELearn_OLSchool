using System;
using System.Collections.Generic;
using System.Text;

namespace OrderCenter.Domain.DomianEvent
{
    public class PaidOrderDomainEvent:BaseOrderDomainEvent
    {
        public PaidOrderDomainEvent(Order order) : base(order) { }
    }
}
