using System;
using System.Collections.Generic;
using System.Text;
using Curriculum.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curriculum.Infrastruction.EntityTypeConfiguration
{
    public class CurriculumStudentConfiguration : IEntityTypeConfiguration<CurriculumStudent>
    {
        public void Configure(EntityTypeBuilder<CurriculumStudent> builder)
        {
            builder.ToTable("CurriculumStudents");
            builder.HasKey(k=>new { k.CurriculumID,k.StudentID});
        }
    }
}
