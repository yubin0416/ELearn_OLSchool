using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Curriculum.Domain.DomainEvent;
using MediatR;
using DotNetCore.CAP;

namespace Curriculum.Application.DomainEventHandlers
{
    public class CreateSectionDomainEventHandler : INotificationHandler<CreateSectionDomainEvent>
    {
        private readonly ICapPublisher _IntegrationEventBus;

        public CreateSectionDomainEventHandler(ICapPublisher IntegrationEventBus)
        {
            _IntegrationEventBus = IntegrationEventBus;
        }

        public Task Handle(CreateSectionDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
