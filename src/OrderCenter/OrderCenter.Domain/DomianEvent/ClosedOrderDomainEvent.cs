using System;
using System.Collections.Generic;
using System.Text;

namespace OrderCenter.Domain.DomianEvent
{
    public class ClosedOrderDomainEvent:BaseOrderDomainEvent
    {
        public ClosedOrderDomainEvent(Order order) : base(order) { }
    }
}
