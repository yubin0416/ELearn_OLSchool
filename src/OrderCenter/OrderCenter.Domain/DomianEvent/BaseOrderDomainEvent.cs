using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace OrderCenter.Domain.DomianEvent
{
    public abstract class BaseOrderDomainEvent:INotification
    {
        public Order GetOrder { get; }

        public BaseOrderDomainEvent(Order _GetOrder)
        {
            GetOrder = _GetOrder;
        }
    }
}
