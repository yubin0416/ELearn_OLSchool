using System;
using System.Collections.Generic;
using DDD.Framework;
using Curriculum.Domain.DomainEvent;
using System.Linq;
using Curriculum.Domain.Exception;

namespace Curriculum.Domain
{
    /// <summary>
    /// 课程
    /// </summary>
    public class Curriculum:Entity<string>,IAggregateRoot
    {
        #region 成员变量
        /// <summary>
        /// 课程标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 课程图片
        /// </summary>
        public string PictureURL { get; set; }

        /// <summary>
        /// 课程介绍
        /// </summary>
        public string Introduce { get; set; }
        
        /// <summary>
        /// 是否免费
        /// </summary>
        public bool IsFree { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 结束时间，之后不能被观看
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 主讲人老师
        /// </summary>
        public Teacher Lecturer { get; set; }

        /// <summary>
        /// 学习中的学生
        /// </summary>
        public ICollection<CurriculumStudent> LearningStudents { get; set; }

        /// <summary>
        /// 章节
        /// </summary>
        public ICollection<Section> sections { get; set; }

        /// <summary>
        /// 评论
        /// </summary>
        public ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// 课件下载
        /// </summary>
        public ICollection<Courseware> Coursewares { get; set; }

        #endregion

        #region 创建课程
        private Curriculum():base()
        {
            LearningStudents = new List<CurriculumStudent>();
            sections = new List<Section>();
            Comments = new List<Comment>();
            Coursewares = new List<Courseware>();
        }

        public Curriculum(Teacher _Teacher, string _Title, string _Introduce, string _Picture) : this(_Teacher, _Title, _Introduce, _Picture, true, DateTime.Parse("2099-1-1")) { }

        public Curriculum(Teacher _Teacher,string _Title,string _Introduce,string _Picture,bool _IsFree,DateTime _EndTime):this()
        {
            Lecturer = _Teacher;
            Title = _Title;
            Introduce = _Introduce;
            PictureURL = _Picture;
            IsFree = _IsFree;
            CreateTime = DateTime.Now;
            EndTime = _EndTime;
            AddDomianEvent(new CreateCurriculumDomainEvent(this));
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加章节
        /// </summary>
        public void AddSection(string SectionTitle,string SectionIntroduce)
        {
            var SectionModel = new Section(ID, SectionTitle, SectionIntroduce);
            sections.Add(SectionModel);
            AddDomianEvent(new CreateSectionDomainEvent(SectionModel));
        }

        /// <summary>
        /// 添加课时
        /// </summary>
        public void AddClassHour(string SectionID,ClassHour NewClassHour)
        {
            var SectionModel = sections.Where(vn => vn.ID == SectionID).FirstOrDefault()
                                             ?? throw new CurriculumExecption("SectionID Not In Sections");
            
            if (IsFree)
            {
                NewClassHour.IsFree = IsFree;
            }
            NewClassHour.SectionID = SectionID;
            NewClassHour.section = SectionModel;
            SectionModel.AddClassHour(NewClassHour);
            AddDomianEvent(new CreateClassHourDomainEvent(NewClassHour));
        }

        /// <summary>
        /// 添加听课学生
        /// </summary>
        public void AddStudent(Student NewLearner)
        {
            if (LearningStudents.Where(stu => stu.StudentID == NewLearner.ID).FirstOrDefault() != null)
                return;
            LearningStudents.Add(new CurriculumStudent() {  Curriculum = this,CurriculumID=ID,Student = NewLearner, StudentID = NewLearner.ID });
            AddDomianEvent(new StudentLearnCurriculumDomainEvent(this, NewLearner));
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        public void AddComment(Comment NewComment)
        {
            if (Comments.Where(vn => vn.Student.ID == NewComment.Student.ID).FirstOrDefault() != null)
            {
                return;
            }
            NewComment.Curriculum = this;
            NewComment.CurriculumID = ID;
            Comments.Add(NewComment);
            AddDomianEvent(new CreateCommentDomainEvent(NewComment));
        }

        /// <summary>
        /// 添加课件下载
        /// </summary>
        public void AddCourseware(Courseware courseware)
        {
            courseware.Curriculum = this;
            courseware.CurriculumID = ID;
            Coursewares.Add(courseware);
            AddDomianEvent(new CreateCoursewareDomainEvent(courseware));
        }
        #endregion

    }
}
