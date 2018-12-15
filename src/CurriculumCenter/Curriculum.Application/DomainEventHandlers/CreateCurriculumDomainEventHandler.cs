using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Curriculum.Domain;
using Curriculum.Domain.Exception;
using Curriculum.Domain.DomainEvent;
using System.Threading;
using System.Threading.Tasks;
using DotNetCore.CAP;

namespace Curriculum.Application.DomainEventHandlers
{
    /// <summary>
    /// 新建课程领域事件处理器
    /// </summary>
    public class CreateCurriculumDomainEventHandler : INotificationHandler<CreateCurriculumDomainEvent>
    {
        private readonly ICapPublisher _IntegrationEventBus;

        public CreateCurriculumDomainEventHandler(ICapPublisher IntegrationEventBus)
        {
            _IntegrationEventBus = IntegrationEventBus;
        }

        public Task Handle(CreateCurriculumDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
