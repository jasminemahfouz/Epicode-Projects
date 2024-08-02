// Data/ApplicationDbContext.cs
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pizzacode.Models;

namespace Pizzacode.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pizza>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }

    }
}
