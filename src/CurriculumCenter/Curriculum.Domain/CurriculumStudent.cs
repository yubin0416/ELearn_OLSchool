using System;
using System.Collections.Generic;
using System.Text;

namespace Curriculum.Domain
{
    /// <summary>
    /// 课程-学生 多对多约定
    /// </summary>
    public class CurriculumStudent
    {
        public string CurriculumID { get; set; }
        public Curriculum Curriculum { get; set; }

        public string StudentID { get; set; }
        public Student Student { get; set; }
    }
}
