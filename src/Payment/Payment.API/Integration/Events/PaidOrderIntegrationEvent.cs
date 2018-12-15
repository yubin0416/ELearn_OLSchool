using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Integration.Events
{
    public class PaidOrderIntegrationEvent
    {
        public string OrderID { get; set; }

        public string TranscationID { get; set; }

        public decimal PaidMoney { get; set; }
    }
}
