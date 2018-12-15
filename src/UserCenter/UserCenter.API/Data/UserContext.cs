using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserCenter.API.Models;

namespace UserCenter.API.Data
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Student>().HasKey(k => k.ID);
            modelBuilder.Entity<Student>().HasAlternateKey(k=>k.Mobile);
            modelBuilder.Entity<Student>().Property(p => p.ID).HasColumnType("nvarchar(36)").HasValueGenerator(typeof(GuidValueGenerate));
            modelBuilder.Entity<Student>().Property(p => p.Mobile).HasColumnType("nvarchar(11)").IsRequired();
            modelBuilder.Entity<Student>().Property(p => p.NickName).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Student>().Property(p => p.Password).HasColumnType("nvarchar(32)").IsRequired();
            modelBuilder.Entity<Student>().Property(p => p.Picture).HasColumnType("nvarchar(100)").HasDefaultValue("http://www.baidu.com/default.jpg");

            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Teacher>().HasKey(k => k.ID);
            modelBuilder.Entity<Teacher>().HasAlternateKey(k => k.Mobile);
            modelBuilder.Entity<Teacher>().Property(p => p.ID).HasColumnType("nvarchar(36)").HasValueGenerator(typeof(GuidValueGenerate));
            modelBuilder.Entity<Teacher>().Property(p => p.Mobile).HasColumnType("nvarchar(11)").IsRequired();
            modelBuilder.Entity<Teacher>().Property(p => p.Name).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Teacher>().Property(p => p.Password).HasColumnType("nvarchar(32)").IsRequired();
            modelBuilder.Entity<Teacher>().Property(p => p.Picture).HasColumnType("nvarchar(100)").HasDefaultValue("http://www.baidu.com/default.jpg");
            modelBuilder.Entity<Teacher>().Property(p => p.Introduce).HasColumnType("nvarchar(500)").HasDefaultValue("该老师很懒，还没添加自我介绍");
        }


    }
}
