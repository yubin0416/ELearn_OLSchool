using System;
using System.Collections.Generic;
using System.Text;

namespace OrderCenter.Application.Integration.Events
{
    public class PaidOrderIntegrationEvent
    {
        public string OrderID { get; set; }

        public string TranscationID { get; set; }

        public decimal PaidMoney { get; set; }
    }
}
