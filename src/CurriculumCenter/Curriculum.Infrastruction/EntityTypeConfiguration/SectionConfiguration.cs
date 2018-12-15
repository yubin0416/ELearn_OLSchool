using System;
using System.Collections.Generic;
using System.Text;
using Curriculum.Domain;
using Curriculum.Infrastruction.ValueGenerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curriculum.Infrastruction.EntityTypeConfiguration
{
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.ToTable("Sections");
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).HasColumnType("NVARCHAR(36)").HasValueGenerator(typeof(StringKeyValueGenerator)).IsRequired();
            builder.Property(p => p.SectionIntroduce).HasColumnType("NVARCHAR(1000)").HasDefaultValue("SectionIntroduce");
            builder.Property(p => p.SectionTitle).HasColumnType("NVARCHAR(100)").IsRequired();
            builder.Property(p => p.CreateTime).HasColumnType("Datetime").HasDefaultValueSql("Getdate()");

            builder.HasMany(p => p.ClassHours).WithOne(p => p.section).HasForeignKey(p=>p.SectionID);
        }
    }
}
