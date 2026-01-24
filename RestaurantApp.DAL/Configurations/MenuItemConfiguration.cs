using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RestaurantApp.DAL.Configurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("MenuItems");
            builder.HasKey(mi => mi.Id);
            builder.Property(mi => mi.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.HasIndex(mi => mi.Name)
                .IsUnique();
            builder.Property(mi => mi.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(mi => mi.Category)
                .IsRequired()
                .HasMaxLength(100);
    
              
           

        }
    }
}
