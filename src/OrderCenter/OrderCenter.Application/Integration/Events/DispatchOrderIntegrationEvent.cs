using System;
using System.Collections.Generic;
using System.Text;

namespace OrderCenter.Application.Integration.Events
{
    /// <summary>
    /// 派发订单
    /// </summary>
    public class DispatchOrderIntegrationEvent
    {
        public string OrderID { get; set; }

        public string CurriculumID { get; set; }

        public string StudentID { get; set; }

    }
}
