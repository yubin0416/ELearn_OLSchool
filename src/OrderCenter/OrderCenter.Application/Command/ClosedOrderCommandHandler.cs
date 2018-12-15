using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderCenter.Domain;
using OrderCenter.Domain.Exceptions;

namespace OrderCenter.Application.Command
{
    public class ClosedOrderCommandHandler : IRequestHandler<ClosedOrderCommand, bool>
    {
        private readonly IOrderRepository _OrderRepository;

        private readonly ILogger<ClosedOrderCommandHandler> _Logger;

        public ClosedOrderCommandHandler(IOrderRepository OrderRepository, ILogger<ClosedOrderCommandHandler> Logger)
        {
            _OrderRepository = OrderRepository;
            _Logger = Logger;
        }

        public async Task<bool> Handle(ClosedOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _OrderRepository.GetOrderAsync(request.OrderID);
            if (order == null) throw new OrderDomainException("不存在该订单");
            order.CloseOrder();
            await _OrderRepository._Unitwork.DomianSaveChangesAnsyc();
            return true;
        }
    }
}
