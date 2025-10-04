using FirstApiTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace FirstApiTask.Infrastructure.Data
{
    public class StorageDbContext : DbContext
    {
        public StorageDbContext(DbContextOptions<StorageDbContext> options) : base(options) { }

        public DbSet<RequestEntity> Requests { get; set; }
        public DbSet<CombinationEntity> Combinations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestEntity>()
                .Property(r => r.InputItems)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<CombinationEntity>()
                .Property(c => c.Items)
                .HasColumnType("nvarchar(max)");
        }
    }

}
