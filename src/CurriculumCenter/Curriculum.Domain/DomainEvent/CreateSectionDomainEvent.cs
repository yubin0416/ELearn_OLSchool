using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Curriculum.Domain.DomainEvent
{
    /// <summary>
    /// 创建章节领域事件
    /// </summary>
    public class CreateSectionDomainEvent:INotification
    {   
        public Section GetSection { get; }

        public CreateSectionDomainEvent(Section _GetSection)
        {
            GetSection = _GetSection;
        }
    }
}
