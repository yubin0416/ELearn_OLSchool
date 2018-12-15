using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OrderCenter.Application.Dtos;
using OrderCenter.Domain;

namespace OrderCenter.Application.AutoMapper
{
    public class OrderMapper: Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, OrderDto>();
        }
    }
}
