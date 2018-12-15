using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Curriculum.Application.Commands
{
    /// <summary>
    /// 新建课件下载命令
    /// </summary>
    public class CreateCoursewareCommand:IRequest<bool>
    {
        public string CurriculumID { get; set; }

        public string CoursewareTitle { get; set; }

        public string DownloadUrl { get; set; }
    }
}
