using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Integration.Events
{
    public class CreateOrderIntegrationEvent
    {
        public string OrderID { get; set; }

        public string UserID { get; set; }
        
        public decimal ActualPayment { get; set; }
    }
}
