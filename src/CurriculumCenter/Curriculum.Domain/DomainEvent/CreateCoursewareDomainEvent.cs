using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Curriculum.Domain.DomainEvent
{
    /// <summary>
    /// 新建课件下载领域事件
    /// </summary>
    public class CreateCoursewareDomainEvent:INotification
    {
        public Courseware GetCourseware { get; }

        public CreateCoursewareDomainEvent(Courseware _Courseware)
        {
            GetCourseware = _Courseware;
        }
    }
}
