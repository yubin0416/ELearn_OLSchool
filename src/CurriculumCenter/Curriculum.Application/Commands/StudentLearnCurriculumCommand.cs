using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Curriculum.Domain;

namespace Curriculum.Application.Commands
{
    public class StudentLearnCurriculumCommand : IRequest<bool>
    {
        public string CurriculumID { get; set; }

        public string StudentID { get; set; }

        public string StudentNickName { get; set; }

        public string StudentPicture { get; set; }
    }
}
