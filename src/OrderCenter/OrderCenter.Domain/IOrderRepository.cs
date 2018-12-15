using System;
using System.Collections.Generic;
using System.Text;
using DDD.Framework;
using System.Threading.Tasks;

namespace OrderCenter.Domain
{
    public interface IOrderRepository:IResposity<Order>
    {
        Task<Order> GetOrderAsync(string OrderID);

        Task<Order> AddOrderAsync(Order order);
    }
}
