using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using System.Reflection;
using OrderCenter.Infrastructure;
using OrderCenter.Domain;
using MediatR;
using OrderCenter.Application.Command;
using OrderCenter.Application.DomainEventHandlers;
using OrderCenter.Application.Behavior;
using OrderCenter.Application.Validator;
using FluentValidation;
using OrderCenter.Application.Integration;
using OrderCenter.Application.Integration.Handlers;

namespace OrderCenter.Application
{
    public class ApplicationMoudel: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //注册仓储
            builder.RegisterType(typeof(OrderRepository)).As(typeof(IOrderRepository)).InstancePerLifetimeScope();

            //注册Mediatr
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            //注册IRequestHandler<>
            builder.RegisterAssemblyTypes(typeof(ClosedOrderCommandHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            //注册INotificationHandler<>
            builder.RegisterAssemblyTypes(typeof(ClosedOrderDomainEventHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(INotificationHandler<>));
            //注册IPipelineBehavior
            builder.RegisterAssemblyTypes(typeof(ValidatorBehavior<,>).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IPipelineBehavior<,>));
            //注册服务工厂
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            //注册FluentValidation
            builder.RegisterAssemblyTypes(typeof(CreateOrderCommandValidator).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IValidator<>));

        }
    }
}
