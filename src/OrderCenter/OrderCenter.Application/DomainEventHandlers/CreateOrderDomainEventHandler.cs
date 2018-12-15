using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderCenter.Domain.DomianEvent;
using DotNetCore.CAP;
using OrderCenter.Application.Integration.Events;

namespace OrderCenter.Application.DomainEventHandlers
{
    public class CreateOrderDomainEventHandler : INotificationHandler<CreateOrderDomainEvent>
    {
        private readonly ICapPublisher _CapPublisher;

        public CreateOrderDomainEventHandler(ICapPublisher CapPublisher)
        {
            _CapPublisher = CapPublisher;
        }

        public async Task Handle(CreateOrderDomainEvent notification, CancellationToken cancellationToken)
        {
            CreateOrderIntegrationEvent @event = new CreateOrderIntegrationEvent() {
                ActualPayment = notification.GetOrder.ActualPayment,
                OrderID = notification.GetOrder.ID,
                UserID = notification.GetOrder.UserID
            };

            await  _CapPublisher.PublishAsync("ELearn.OrderCenter.CreateOrder", @event);
        }
    }
}
