using System;
using System.Collections.Generic;
using System.Text;
using DDD.Framework;

namespace Curriculum.Domain
{
    /// <summary>
    /// 评论
    /// </summary>
    public class Comment:Entity<string>
    {
        /// <summary>
        /// 满意度
        /// </summary>
        public CommentStarType CommentStar { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        #region 隶属于课程
        public string CurriculumID { get; set; }
        public Curriculum Curriculum { get; set; }
        #endregion

        #region 学生
        public string StudentID { get; set; }
        public Student Student { get; set; }
        #endregion
    }
}
