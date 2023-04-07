using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Doamin.Entities
{
    public  class CarBDContext :DbContext
    {
        public CarBDContext(DbContextOptions<CarBDContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderCase>()
            .HasOne<CarNumbers>(s => s.carNumbers)
            .WithMany(g => g.OrderCases)
            .HasForeignKey(s => s.CarNumbersId);
        }
        public DbSet<OrderCase> OrderCases { get; set; }
        public DbSet<CarNumbers> CarNumber { get; set; }
    }
}
