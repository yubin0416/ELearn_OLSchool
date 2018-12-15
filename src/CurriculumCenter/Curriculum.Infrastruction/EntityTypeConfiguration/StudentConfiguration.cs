using System;
using System.Collections.Generic;
using System.Text;
using Curriculum.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curriculum.Infrastruction.EntityTypeConfiguration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(k => k.ID);
            builder.Property(p => p.ID).HasColumnType("Nvarchar(36)").ValueGeneratedNever();
            builder.Property(p => p.NickName).HasColumnType("Nvarchar(50)").IsRequired();
            builder.Property(p => p.Picture).HasColumnType("Nvarchar(100)").IsRequired();
        }
    }
}
