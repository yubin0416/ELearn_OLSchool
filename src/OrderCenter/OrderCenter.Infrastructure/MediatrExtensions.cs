using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DDD.Framework;
using System.Linq;

namespace OrderCenter.Infrastructure
{
    public static class MediatrExtensions
    {
        public static async Task DispatchDomainEvent(this IMediator mediator, OrderContext ctx)
        {
            var domianEntities = ctx.ChangeTracker.Entries<Entity>().Where(v => v.Entity.DomainEventList != null && v.Entity.DomainEventList.Any());

            var domianEvents = domianEntities.SelectMany(v => v.Entity.DomainEventList).ToList();

            domianEntities.ToList().ForEach(v => v.Entity.Clear());

            var tasks = domianEvents.Select((async (domainevent) => await mediator.Publish(domainevent)));

            await Task.WhenAll(tasks);
        }
    }

}
