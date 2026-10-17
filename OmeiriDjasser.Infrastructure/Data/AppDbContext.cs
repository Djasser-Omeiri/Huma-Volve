using Microsoft.EntityFrameworkCore;
using OmeiriDjasser.Domain.Entities;

namespace OmeiriDjasser.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.JobTitle).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Salary).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Age).IsRequired();
                entity.ToTable("Employees");
            });
        }
    }
}
