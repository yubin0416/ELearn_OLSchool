using System;
using System.Collections.Generic;
using System.Text;
using DDD.Framework;

namespace Curriculum.Domain
{
    /// <summary>
    /// 学生
    /// </summary>
    public class Student:Entity<string>
    {
        public string NickName { get; set; }

        public string Picture { get; set; }

        public ICollection<CurriculumStudent> Curriculums { get; set; }

        public Student()
        {

        }

        public Student(string _ID,string _NickName,string _Picture)
        {
            ID = _ID;
            NickName = _NickName;
            Picture = _Picture;
        }
    }
}
