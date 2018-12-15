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
    public class PaidOrderDomainEventHandler : INotificationHandler<PaidOrderDomainEvent>
    {
        private readonly ICapPublisher _CapPublisher;

        public PaidOrderDomainEventHandler(ICapPublisher CapPublisher)
        {
            _CapPublisher = CapPublisher;
        }

        public async Task Handle(PaidOrderDomainEvent notification, CancellationToken cancellationToken)
        {
            DispatchOrderIntegrationEvent @event = new DispatchOrderIntegrationEvent()
            {
                CurriculumID = notification.GetOrder.CurriculumID,
                OrderID = notification.GetOrder.ID,
                StudentID = notification.GetOrder.UserID
            };
            await _CapPublisher.PublishAsync("ELearn.OrderCenter.DispatchOrder", @event);
        }
    }
}
