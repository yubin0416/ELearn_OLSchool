using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace OrderCenter.Domain.DomianEvent
{
    public class CreateFreeOrderDomainEvent:BaseOrderDomainEvent
    {
        public CreateFreeOrderDomainEvent(Order order) : base(order) { }
    }
}
