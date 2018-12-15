using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Curriculum.Application.Commands
{
    /// <summary>
    /// 创建章节
    /// </summary>
    public class CreateSectionCommand:IRequest<bool>
    {
        public string CurriculumID { get; set; }

        public string TeacherID { get; set; }

        public string SectionTitle { get; set; }

        public string SectionIntroduce { get; set; }
    }
}
