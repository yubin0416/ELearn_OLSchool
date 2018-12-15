using System;
using Microsoft.EntityFrameworkCore;
using DDD.Framework;
using System.Threading;
using System.Threading.Tasks;
using OrderCenter.Domain;
using MediatR;

namespace OrderCenter.Infrastructure
{
    public class OrderContext : DbContext, IUnitwork
    {
        private readonly IMediator _Mediator;

        public OrderContext(DbContextOptions options, IMediator Mediator) : base(options)
        {
            _Mediator = Mediator;
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Order>().HasKey(k => k.ID);
            modelBuilder.Entity<Order>().HasIndex(i => i.UserID);
            modelBuilder.Entity<Order>().Property(p => p.ID).HasColumnType("varchar(36)");
            modelBuilder.Entity<Order>().Property(p => p.ActualPayment).HasColumnType("decimal(18,2)").HasDefaultValue(0);
            modelBuilder.Entity<Order>().Property(p => p.CreateDate).HasColumnType("Datetime").HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Order>().Property(p => p.CurriculumID).HasColumnType("nvarchar(36)").IsRequired();
            modelBuilder.Entity<Order>().Property(p => p.CurriculumPrice).HasColumnType("decimal(18,2)").HasDefaultValue(0);
            modelBuilder.Entity<Order>().Property(p => p.CurriculumTitle).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<Order>().Property(p => p.DiscountsPrice).HasColumnType("decimal(18,2)").HasDefaultValue(0);
            modelBuilder.Entity<Order>().Property(p => p.OrderStatus).HasDefaultValue(OrderStatus.Created);
            modelBuilder.Entity<Order>().Property(p => p.PaymentDate).HasColumnType("datetime");
            modelBuilder.Entity<Order>().Property(p => p.TransationID).HasColumnType("Nvarchar(36)");
            modelBuilder.Entity<Order>().Property(p => p.UserID).HasColumnType("nvarchar(36)").IsRequired();

        }

        public async Task<bool> DomianSaveChangesAnsyc(CancellationToken cancellationToken = default(CancellationToken))
        {
             int result = await base.SaveChangesAsync();
            if (result == 0)
            {
                return false;
            }
             await  _Mediator.DispatchDomainEvent(this);
            return true;
        }
    }
}
