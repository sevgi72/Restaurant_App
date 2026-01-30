using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RestaurantApp.DAL.Data
{
    public class RestaurantAppDbContextFactory : IDesignTimeDbContextFactory<RestaurantAppDbContext>
    {
        public RestaurantAppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestaurantAppDbContext>();
            // Server adını SSMS-dəki kimi yazdığınızdan əmin olun (məsələn: .\\SQLEXPRESS)
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Restaurant_AppDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new RestaurantAppDbContext(optionsBuilder.Options);
        }
    }
}
