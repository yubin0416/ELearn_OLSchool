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
    public class CreateCommentDomainEventHandler : INotificationHandler<CreateCommentDomainEvent>
    {
        private readonly ICapPublisher _IntegrationEventBus;

        public CreateCommentDomainEventHandler(ICapPublisher IntegrationEventBus)
        {
            _IntegrationEventBus = IntegrationEventBus;
        }

        public Task Handle(CreateCommentDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
