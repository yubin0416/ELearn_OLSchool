using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DDD.Framework;
using OrderCenter.Domain;
using System.Linq;

namespace OrderCenter.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext context;

        public OrderRepository(OrderContext _context)
        {
            context = _context;
        }

        public IUnitwork _Unitwork => context;

        public async Task<Order> AddOrderAsync(Order order)
        {
            return (await context.Orders.AddAsync(order)).Entity;
        }

        public async Task<Order> GetOrderAsync(string OrderID)
        {
            return await context.Orders.FindAsync(OrderID);
        }
    }
}
