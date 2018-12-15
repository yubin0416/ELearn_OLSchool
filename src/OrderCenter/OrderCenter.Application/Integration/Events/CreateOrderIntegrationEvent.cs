using System;
using System.Collections.Generic;
using System.Text;

namespace OrderCenter.Application.Integration.Events
{
    public class CreateOrderIntegrationEvent
    {
        public string OrderID { get; set; }

        public string UserID { get; set; }

        public decimal ActualPayment { get; set; }
    }
}
