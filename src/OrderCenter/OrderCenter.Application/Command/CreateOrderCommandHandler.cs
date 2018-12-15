using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderCenter.Application.Dtos;
using OrderCenter.Domain;
using AutoMapper;

namespace OrderCenter.Application.Command
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _OrderRepository;

        private readonly ILogger<CreateOrderCommandHandler> _Logger;

        private readonly IMapper _Mapper;

        public CreateOrderCommandHandler(IOrderRepository OrderRepository, ILogger<CreateOrderCommandHandler> Logger, IMapper Mapper)
        {
            _OrderRepository = OrderRepository;
            _Logger = Logger;
            _Mapper = Mapper;
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(request.UserID,request.CurriculumID,request.CurriculumTitle,request.CurriculumPrice,request.DiscountsPrice);
            await _OrderRepository.AddOrderAsync(order);
            await _OrderRepository._Unitwork.DomianSaveChangesAnsyc();
            return _Mapper.Map<OrderDto>(order);
        }
    }
}
