using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Data
{
    public class ApiContextFactory : IDesignTimeDbContextFactory<ApiContext>
    {
        public ApiContext CreateDbContext(string[]? args)
        {
            string connectionstring = "server=localhost;database=veloce_dev;user=SA;password=Fantaer=h2o&brus";
            var optionsBuilder = new DbContextOptionsBuilder<ApiContext>();
            optionsBuilder.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring));

            //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Veloce_Dev;Trusted_Connection=True;TrustServerCertificate=True;");

            return new ApiContext(optionsBuilder.Options);
        }
    }
}
