using System;
using System.Collections.Generic;
using System.Text;
using DDD.Framework;

namespace Curriculum.Domain
{
    /// <summary>
    /// 课程-章节
    /// </summary>
    public  class Section:Entity<string>
    {
        /// <summary>
        /// 章节名称
        /// </summary>
        public string SectionTitle { get; set; }

        /// <summary>
        /// 章节介绍
        /// </summary>
        public string SectionIntroduce { get; set; }

        public DateTime CreateTime { get; set; }

        #region 隶属课程
        public string CurriculumID { get; set; }
        public Curriculum Curriculum { get; set; }
        #endregion

        /// <summary>
        /// 包含的课时
        /// </summary>
        public ICollection<ClassHour> ClassHours { get; set; }

        public Section()
        {
            ClassHours = new List<ClassHour>();
        }

        public Section(string _CurriculumID, string _SectionTitle, string _SectionIntroduce):this()
        {
            CurriculumID = _CurriculumID;
            SectionTitle = _SectionTitle;
            SectionIntroduce = _SectionIntroduce;
        }

        public void AddClassHour(ClassHour hour)
        {
            ClassHours.Add(hour);
        }
    }
}
