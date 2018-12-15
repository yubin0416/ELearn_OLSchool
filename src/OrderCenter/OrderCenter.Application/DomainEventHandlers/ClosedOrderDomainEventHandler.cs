using MediatR;
using OrderCenter.Domain.DomianEvent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderCenter.Application.DomainEventHandlers
{
    public class ClosedOrderDomainEventHandler : INotificationHandler<ClosedOrderDomainEvent>
    {
        public Task Handle(ClosedOrderDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
