using System;
using System.Collections.Generic;
using System.Text;
using Curriculum.Domain;
using Curriculum.Infrastruction.ValueGenerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curriculum.Infrastruction.EntityTypeConfiguration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Curriculum.Domain.Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(k => k.ID);
            builder.Property(p => p.ID).HasColumnType("NVARCHAR(36)").HasValueGenerator(typeof(StringKeyValueGenerator)).IsRequired();
            builder.Property(p => p.CreateDate).HasColumnType("Datetime").HasComputedColumnSql("Getdate()");
            builder.Property(p => p.Context).HasColumnType("NVARCHAR(500)").IsRequired();
            builder.Property(p => p.CommentStar).IsRequired();

            builder.HasOne(p => p.Student).WithMany().HasForeignKey(p=>p.StudentID);
        }
    }
}
