using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using DotNetCore.CAP;
using Curriculum.Domain.DomainEvent;
using System.Threading;
using System.Threading.Tasks;

namespace Curriculum.Application.DomainEventHandlers
{
    public class CreateClassHourDomainEventHandler : INotificationHandler<CreateClassHourDomainEvent>
    {
        private readonly ICapPublisher _IntegrationEventBus;

        public CreateClassHourDomainEventHandler(ICapPublisher IntegrationEventBus)
        {
            _IntegrationEventBus = IntegrationEventBus;
        }

        public Task Handle(CreateClassHourDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
