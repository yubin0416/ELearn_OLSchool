using System;
using System.Collections.Generic;
using System.Text;
using Curriculum.Domain;
using Curriculum.Infrastruction.ValueGenerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curriculum.Infrastruction.EntityTypeConfiguration
{
    public class CurriculumConfiguration : IEntityTypeConfiguration<Curriculum.Domain.Curriculum>
    {
        public void Configure(EntityTypeBuilder<Domain.Curriculum> builder)
        {
            builder.ToTable("Curriculums");

            builder.HasKey(k=>k.ID);
            builder.Property(p => p.ID).HasColumnType("NVARCHAR(36)").HasValueGenerator(typeof(StringKeyValueGenerator)).IsRequired();
            
            builder.Property(p => p.CreateTime).HasColumnType("Datetime").HasDefaultValueSql("Getdate()");
            builder.Property(p=>p.EndTime).HasColumnType("Datetime").HasDefaultValueSql("2999-1-1");
            builder.Property(p => p.Introduce).HasColumnType("NVARCHAR(500)").HasDefaultValue("");
            builder.Property(p => p.IsFree).HasDefaultValue(true).ValueGeneratedNever();
            builder.Property(p => p.PictureURL).HasColumnType("NVARCHAR(100)").HasDefaultValue("").IsRequired();
            builder.Property(p => p.Title).HasColumnType("NVARCHAR(100)").IsRequired();

            builder.HasMany(p => p.sections).WithOne(o => o.Curriculum).HasForeignKey(p=>p.CurriculumID).IsRequired();
            builder.HasMany(p => p.Comments).WithOne(o => o.Curriculum).HasForeignKey(p => p.CurriculumID).IsRequired();
            builder.HasMany(p=>p.Coursewares).WithOne(o => o.Curriculum).HasForeignKey(p => p.CurriculumID).IsRequired();

            //设置Teacher
            builder.OwnsOne(ow => ow.Lecturer).HasIndex(o=>o.TeacherID);
            builder.OwnsOne(ow => ow.Lecturer).Property(p => p.TeacherID).HasColumnType("Nvarchar(36)").IsRequired();
            builder.OwnsOne(ow => ow.Lecturer).Property(p=>p.Picture).HasColumnType("Nvarchar(100)").IsRequired();
            builder.OwnsOne(ow => ow.Lecturer).Property(p => p.Name).HasColumnType("Nvarchar(50)").IsRequired();
            builder.OwnsOne(ow => ow.Lecturer).Property(p => p.Introduce).HasColumnType("Nvarchar(500)").HasDefaultValue("");
        }
    }
}
