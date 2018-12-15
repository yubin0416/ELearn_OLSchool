using System;
using Microsoft.EntityFrameworkCore;
using DDD.Framework;
using System.Threading;
using System.Threading.Tasks;
using Curriculum.Domain;
using CurriculumModel = Curriculum.Domain.Curriculum;
using Curriculum.Infrastruction.EntityTypeConfiguration;
using MediatR;

namespace Curriculum.Infrastruction
{
    public class CurriculumContext : DbContext, IUnitwork
    {
        private readonly IMediator _Mediator;

        public CurriculumContext(DbContextOptions options, IMediator Mediator) : base(options)
        {
            _Mediator = Mediator;
        }

        public DbSet<CurriculumModel> Curriculums { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ClassHour> ClassHours { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Courseware> Coursewares { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CurriculumConfiguration());
            modelBuilder.ApplyConfiguration(new SectionConfiguration());
            modelBuilder.ApplyConfiguration(new ClassHourConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new CoursewareConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new CurriculumStudentConfiguration());
        }

        public async Task<bool> DomianSaveChangesAnsyc(CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.SaveChangesAsync();
            await _Mediator.DispatchDomainEvent(this);
            return true;
        }
    }
}
