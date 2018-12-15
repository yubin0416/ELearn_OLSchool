using System;
using System.Collections.Generic;
using System.Text;

namespace Curriculum.Application.IntegrationEvent
{
    public class DispatchOrderIntegrationEvent
    {
        public string OrderID { get; set; }

        public string CurriculumID { get; set; }

        public string StudentID { get; set; }
    }
}
