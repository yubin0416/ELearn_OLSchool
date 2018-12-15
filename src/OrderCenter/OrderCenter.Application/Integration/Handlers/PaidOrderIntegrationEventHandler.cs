using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;
using OrderCenter.Application.Integration.Events;
using OrderCenter.Domain;
using OrderCenter.Domain.Exceptions;

namespace OrderCenter.Application.Integration.Handlers
{
    /// <summary>
    /// 付款成功
    /// </summary>
    public class PaidOrderIntegrationEventHandler : IIntegrationHandler<PaidOrderIntegrationEvent>, ICapSubscribe
    {
        private readonly IOrderRepository _OrderRepository;

        public PaidOrderIntegrationEventHandler(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }

        [CapSubscribe("ELearn.Payment.PaidOrder")]
        public async Task Handler(PaidOrderIntegrationEvent Event)
        {
            var Order = await _OrderRepository.GetOrderAsync(Event.OrderID);
            if (Order == null) throw new OrderDomainException("PaidOrderIntegrationEventHandler order is null ");
            Order.Paymented(Event.TranscationID,Event.PaidMoney);
            await _OrderRepository._Unitwork.DomianSaveChangesAnsyc();
        }
    }
}
