using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace OrderCenter.Application.Command
{
    public class ClosedOrderCommand:IRequest<bool>
    {
        public string OrderID { get; set; }
    }
}
