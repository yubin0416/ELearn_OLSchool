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
    public class CreateFreeOrderDomainEventHandler : INotificationHandler<CreateFreeOrderDomainEvent>
    {
        private readonly ICapPublisher _CapPublisher;

        public CreateFreeOrderDomainEventHandler(ICapPublisher CapPublisher)
        {
            _CapPublisher = CapPublisher;
        }

        public async Task Handle(CreateFreeOrderDomainEvent notification, CancellationToken cancellationToken)
        {
            DispatchOrderIntegrationEvent @event = new DispatchOrderIntegrationEvent() {
                                                              CurriculumID = notification.GetOrder.CurriculumID,
                                                              OrderID = notification.GetOrder.ID,
                                                              StudentID = notification.GetOrder.UserID};
            await _CapPublisher.PublishAsync("ELearn.OrderCenter.DispatchOrder", @event);
        }
    }
}
