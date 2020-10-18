using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Onebrb.Data
{
    public class OnebrbContextFactory : IDesignTimeDbContextFactory<OnebrbContext>
    {
        public OnebrbContextFactory()
        {

        }

        private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public OnebrbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<OnebrbContext>();
            builder.UseSqlite(Configuration.GetConnectionString("MSSQLDatabase"));

            return new OnebrbContext(builder.Options);
        }
    }
}
