using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Curriculum.Domain.DomainEvent
{
    /// <summary>
    ///  新建课程领域事件
    /// </summary>
    public class CreateCurriculumDomainEvent:INotification
    {
        public Curriculum GetCurriculum { get; }

        public CreateCurriculumDomainEvent(Curriculum Curriculum)
        {
            GetCurriculum = Curriculum;
        }
    }
}
