using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using OrderCenter.Application.Dtos;

namespace OrderCenter.Application.Command
{
    public class CreateOrderCommand:IRequest<OrderDto>
    {
        public string UserID { get; set; }

        public string CurriculumID { get; set; }

        public string CurriculumTitle { get; set; }

        public decimal CurriculumPrice { get; set; }

        public decimal DiscountsPrice { get; set; }
    }
}
