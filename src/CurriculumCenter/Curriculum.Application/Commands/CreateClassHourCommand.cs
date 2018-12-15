using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Curriculum.Domain;

namespace Curriculum.Application.Commands
{
    /// <summary>
    /// 创建课时命令
    /// </summary>
    public class CreateClassHourCommand:IRequest<bool>
    {
        public string CurriculumID { get; set; }

        public string SectionID { get; set; }

        public string TeacherID { get; set; }
        
        public ClassHourType ClassHourType { get; set; }

        public string ClassHourTitle { get; set; }

        public string VedioUrl { get; set; }

        public string VedioDuration { get; set; }

        public bool IsFree { get; set; }

        public bool IsExperience { get; set; }
    }
}
