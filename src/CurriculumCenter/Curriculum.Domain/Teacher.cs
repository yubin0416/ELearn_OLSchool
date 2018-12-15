using System;
using System.Collections.Generic;
using System.Text;
using DDD.Framework;

namespace Curriculum.Domain
{
    /// <summary>
    /// 讲课老师
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// 老师ID
        /// </summary>
        public string TeacherID { get; set; }

        /// <summary>
        /// 老师姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 老师照片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 老师介绍
        /// </summary>
        public string Introduce { get; set; }
    }
}
