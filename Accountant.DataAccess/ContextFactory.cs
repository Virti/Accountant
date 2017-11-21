using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Accountant.DataAccess
{
    public class UsersContextFactory : IDesignTimeDbContextFactory<UsersContext>
    {
        public UsersContext CreateDbContext(string[] args)
        {
            return Create(
                Directory.GetCurrentDirectory(),
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }

        public UsersContext CreateDbContext(DbContextFactoryOptions options)
        {
            return CreateDbContext(new string[] {});
        }

        private UsersContext Create(string basePath, string environmentName)
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            
            var connectionStringName = nameof(UsersContext);
            var connstr = config.GetConnectionString(connectionStringName);

            if (string.IsNullOrWhiteSpace(connstr) == true)
            {
                throw new InvalidOperationException(
                    $"Could not find a connection string named '{connectionStringName}'.");
            }
            else
            {
                return Create(connstr);
            }
        }
        
        private UsersContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
                $"{nameof(connectionString)} is null or empty.",
                nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<UsersContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new UsersContext(optionsBuilder.Options);
        }
    }
}