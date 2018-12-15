using System;
using System.Collections.Generic;
using System.Text;

namespace OrderCenter.Domain.DomianEvent
{
    public class CreateOrderDomainEvent:BaseOrderDomainEvent
    {
        public CreateOrderDomainEvent(Order order) : base(order) { }
    }
}
