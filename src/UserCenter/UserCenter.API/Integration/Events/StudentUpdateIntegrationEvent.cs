using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserCenter.API.Integration.Events
{
    public class StudentUpdateIntegrationEvent
    {
        public string StudentID { get; set; }

        public string NickName { get; set; }

        public StudentUpdateIntegrationEvent() { }

        public StudentUpdateIntegrationEvent(string studentid,string nickname) {
            StudentID = studentid;
            NickName = nickname;
        }
    }
}
