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
    public class StudentLearnCurriculumDomainEventHandler : INotificationHandler<StudentLearnCurriculumDomainEvent>
    {
        private readonly ICapPublisher _IntegrationEventBus;

        public StudentLearnCurriculumDomainEventHandler(ICapPublisher IntegrationEventBus)
        {
            _IntegrationEventBus = IntegrationEventBus;
        }

        public Task Handle(StudentLearnCurriculumDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
