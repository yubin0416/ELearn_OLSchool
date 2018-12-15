using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using System.Reflection;
using MediatR;
using Curriculum.Application.Commands.Handlers;
using Curriculum.Application.DomainEventHandlers;
using Curriculum.Application.Behaviors;
using Curriculum.Application.Validators;
using FluentValidation;
using Curriculum.Domain;
using Curriculum.Infrastruction;
using Curriculum.Application.IntegrationEvent;
using Curriculum.Application.IntegrationEvent.Handlers;
using Curriculum.Application.Services;
using HttpClient.Abstraction;
using Microsoft.Extensions.Logging;
using ResilienceHttpClient;
using Microsoft.AspNetCore.Http;

namespace Curriculum.Application
{
    public class ApplicationMoudle:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //注册仓储
            builder.RegisterType(typeof(CurriculumRepository)).As(typeof(ICurriculumRepository)).InstancePerLifetimeScope();

            //注册Mediatr
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            //注册IRequestHandler<>
            builder.RegisterAssemblyTypes(typeof(CreateClassHourCommandHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            //注册INotificationHandler<>
            builder.RegisterAssemblyTypes(typeof(CreateClassHourDomainEventHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(INotificationHandler<>));
            //注册IPipelineBehavior
            builder.RegisterAssemblyTypes(typeof(ValidatorBehavior<,>).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IPipelineBehavior<,>));
            //注册服务工厂
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            //注册FluentValidation
            builder.RegisterAssemblyTypes(typeof(CreateClassHourCommandValidator).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IValidator<>));

        }
    }
}
