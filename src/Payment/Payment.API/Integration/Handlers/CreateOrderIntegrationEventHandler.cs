using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Payment.API.Integration.Events;

namespace Payment.API.Integration.Handlers
{
    public class CreateOrderIntegrationEventHandler : ICapSubscribe, IIntegrationEventHandler<CreateOrderIntegrationEvent>
    {
        private readonly ICapPublisher _CapPublisher;

        public CreateOrderIntegrationEventHandler(ICapPublisher CapPublisher)
        {
            _CapPublisher = CapPublisher;
        }

        [CapSubscribe("ELearn.OrderCenter.CreateOrder")]
        public async Task Handler(CreateOrderIntegrationEvent model)
        {
            PaidOrderIntegrationEvent @event = new PaidOrderIntegrationEvent() {  OrderID = model.OrderID, TranscationID= Guid.NewGuid().ToString(), PaidMoney =model.ActualPayment};
            await _CapPublisher.PublishAsync("ELearn.Payment.PaidOrder", @event);
        }
    }
}
