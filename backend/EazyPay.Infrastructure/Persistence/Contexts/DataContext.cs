using EazyPay.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EazyPay.Infrastructure.Persistence.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public virtual DbSet<Bank> Banks { get; set; }
    public virtual DbSet<Merchant> Merchants { get; set; }
    public virtual DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Payment>().Property(p => p.Amount)
            .HasPrecision(10, 2);
    }
}