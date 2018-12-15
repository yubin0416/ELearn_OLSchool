using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Curriculum.Domain.DomainEvent
{
    /// <summary>
    /// 新学生报名参加课程领域事件
    /// </summary>
    public class StudentLearnCurriculumDomainEvent:INotification
    {
        public Curriculum GetCurriculum { get; }

        public Student GetStudent { get; }

        public StudentLearnCurriculumDomainEvent(Curriculum _Curriculum, Student _Student)
        {
            GetCurriculum = _Curriculum;
            GetStudent = _Student;
        }
    }
}
