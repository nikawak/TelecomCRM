
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TelecomCRM.Infrastructure.Data
{
    public class TelecomDbContextFactory : IDesignTimeDbContextFactory<TelecomDbContext>
    {
        public TelecomDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // путь можно подкорректировать
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TelecomDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new TelecomDbContext(optionsBuilder.Options);
        }
    }
}


