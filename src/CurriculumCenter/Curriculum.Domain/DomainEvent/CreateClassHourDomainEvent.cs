using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Curriculum.Domain.DomainEvent
{
    /// <summary>
    /// 创建课时领域事件
    /// </summary>
    public class CreateClassHourDomainEvent:INotification
    {
        /// <summary>
        /// 新建的课时
        /// </summary>
        public ClassHour GetClassHour { get; }

        public CreateClassHourDomainEvent(ClassHour _ClassHour)
        {
            GetClassHour = _ClassHour;
        }
    }
}
