using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebCar.Areas.Admin.Models.EF
{
    public class CarWebDbContextFactory : IDesignTimeDbContextFactory<CarWebDbContext>
    {
        public CarWebDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("WebsiteCar");

            var optionsBuilder = new DbContextOptionsBuilder<CarWebDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new CarWebDbContext(optionsBuilder.Options);
        }
    }
}
