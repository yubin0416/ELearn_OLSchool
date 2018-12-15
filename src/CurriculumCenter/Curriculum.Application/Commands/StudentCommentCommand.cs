using System;
using System.Collections.Generic;
using System.Text;
using Curriculum.Domain;
using MediatR;

namespace Curriculum.Application.Commands
{
    /// <summary>
    /// 学生评价
    /// </summary>
    public class StudentCommentCommand:IRequest<bool>
    {
        public string CurriculumID { get; set; }

        public string StudentID { get; set; }

        public CommentStarType CommentStar { get; set; }

        public string Context { get; set; }
    }
}
