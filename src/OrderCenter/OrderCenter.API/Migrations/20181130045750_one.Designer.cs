﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderCenter.Domain;
using OrderCenter.Infrastructure;

namespace OrderCenter.API.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20181130045750_one")]
    partial class one
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderCenter.Domain.Order", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(36)");

                    b.Property<decimal>("ActualPayment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("CurriculumID")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<decimal>("CurriculumPrice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<string>("CurriculumTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("DiscountsPrice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<int>("OrderStatus")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime");

                    b.Property<string>("TransationID")
                        .HasColumnType("Nvarchar(36)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
