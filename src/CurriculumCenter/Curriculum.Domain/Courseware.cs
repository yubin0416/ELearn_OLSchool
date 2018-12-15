using System;
using System.Collections.Generic;
using System.Text;
using DDD.Framework;

namespace Curriculum.Domain
{
    /// <summary>
    /// 课件
    /// </summary>
    public  class Courseware:Entity<string>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadUrl { get; set; }

        #region 隶属课程
        public string CurriculumID { get; set; }
        public Curriculum Curriculum { get; set; }
        #endregion
    }
}
