using System.IO;
using MetroService.SaasService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MetroService.SaasService.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands)
     * */
    public class SaasServiceDbContextFactory : IDesignTimeDbContextFactory<SaasServiceDbContext>
    {
        public SaasServiceDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SaasServiceDbContext>()
                .UseSqlServer(GetConnectionStringFromConfiguration(), b =>
                {
                    b.MigrationsHistoryTable("__SaasService_Migrations");
                });

            return new SaasServiceDbContext(builder.Options);
        }

        private static string GetConnectionStringFromConfiguration()
        {
            return BuildConfiguration()
                .GetConnectionString(SaasServiceDbProperties.ConnectionStringName);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        $"..{Path.DirectorySeparatorChar}MetroService.SaasService.HttpApi.Host"
                    )
                )
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}