using System;
using System.Collections.Generic;
using System.Text;
using Curriculum.Domain;
using Curriculum.Infrastruction.ValueGenerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curriculum.Infrastruction.EntityTypeConfiguration
{
    public class ClassHourConfiguration : IEntityTypeConfiguration<Curriculum.Domain.ClassHour>
    {
        public void Configure(EntityTypeBuilder<ClassHour> builder)
        {
            builder.ToTable("ClassHours");
            builder.HasKey(k=>k.ID);
            builder.Property(p => p.ID).HasColumnType("nvarchar(36)").HasValueGenerator(typeof(StringKeyValueGenerator)).IsRequired();
            builder.Property(p => p.ClassHourTitle).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(p => p.ClassHourType).HasDefaultValue(ClassHourType.Recorded).IsRequired();
            builder.Property(p => p.IsExperience).HasDefaultValue(true).ValueGeneratedNever();
            builder.Property(p => p.IsFree).HasDefaultValue(true).ValueGeneratedNever();
            builder.Property(p => p.VedioDuration).HasColumnType("nvarchar(8)").HasDefaultValue("00:00:00").IsRequired();
            builder.Property(p => p.VedioUrl).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.CreateDate).HasColumnType("DATETIME").HasComputedColumnSql("GETDATE()");
        }
    }
}
