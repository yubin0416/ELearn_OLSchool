using System;
using System.Collections.Generic;
using System.Text;
using DDD.Framework;

namespace Curriculum.Domain
{
    /// <summary>
    /// 视频课时
    /// </summary>
    public class ClassHour:Entity<string>
    {
        /// <summary>
        /// 课时名称
        /// </summary>
        public string ClassHourTitle { get; set; }

        /// <summary>
        /// 课时类型 直播/录播
        /// </summary>
        public ClassHourType ClassHourType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } 

        /// <summary>
        /// 默认视频地址（支持web，ios，android）
        /// </summary>
        public string VedioUrl { get; set; }

        /// <summary>
        /// 视频时长
        /// </summary>
        public string VedioDuration { get; set; }

        /// <summary>
        /// 是否免费
        /// </summary>
        public bool IsFree { get; set; }

        /// <summary>
        /// 是否可以试听
        /// </summary>
        public bool IsExperience { get; set; }

        #region 隶属章节
        public string SectionID { get; set; }

        public Section section { get; set; }
        #endregion
    }
}
