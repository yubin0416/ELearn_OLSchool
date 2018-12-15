using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using CurriculumModel = Curriculum.Domain.Curriculum;

namespace Curriculum.Application.Commands
{
    /// <summary>
    /// 创建课程命令
    /// </summary>
    public class CreateCurriculumCommand:IRequest<CurriculumModel>
    {
        public string Title { get; set; }

        public string PictureURL { get; set; }

        public string Introduce { get; set; }

        public bool IsFree { get; set; }

        public DateTime? EndTime { get; set; }

        public string TeacherID { get; set; }

        public string TeacherPicture { get; set; }

        public string TeacherName { get; set; }

        public string TeacherIntroduce { get; set; }
    }
}
