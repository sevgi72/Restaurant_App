using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.DAL.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.TotalAmount)  
                   .HasColumnType("decimal(18,2)");
            builder.Property(o => o.Date)
                     .IsRequired();

            builder.HasMany(o => o.OrderItems)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(o => o.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
