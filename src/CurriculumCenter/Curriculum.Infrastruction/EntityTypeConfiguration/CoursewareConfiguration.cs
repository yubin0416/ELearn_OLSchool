using System;
using System.Collections.Generic;
using System.Text;
using Curriculum.Domain;
using Curriculum.Infrastruction.ValueGenerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curriculum.Infrastruction.EntityTypeConfiguration
{
    public class CoursewareConfiguration : IEntityTypeConfiguration<Courseware>
    {
        public void Configure(EntityTypeBuilder<Courseware> builder)
        {
            builder.ToTable("Coursewares");

            builder.HasKey(k=>k.ID);
            builder.Property(p => p.ID).HasColumnType("NVARCHAR(36)").HasValueGenerator(typeof(StringKeyValueGenerator)).IsRequired();
            builder.Property(p=>p.DownloadUrl).HasColumnType("NVARCHAR(100)").IsRequired();
            builder.Property(p => p.Title).HasColumnType("NVARCHAR(50)").IsRequired();
        }
    }
}
