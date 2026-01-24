using Microsoft.EntityFrameworkCore;
using RestaurantApp.DAL.Models;
using RestaurantApp.DAL.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.DAL.Data
{
    public class RestaurantAppDbContext : DbContext
    {
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=SUN10\\MAIN;Database=Restaurant_AppDb;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new MenuItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguation());
        }


    }
}

