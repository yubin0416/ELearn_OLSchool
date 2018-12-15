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
    public class CreateCourswareDomainEventHandler : INotificationHandler<CreateCoursewareDomainEvent>
    {
        private readonly ICapPublisher _IntegrationEventBus;

        public CreateCourswareDomainEventHandler(ICapPublisher IntegrationEventBus)
        {
            _IntegrationEventBus = IntegrationEventBus;
        }

        public Task Handle(CreateCoursewareDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
