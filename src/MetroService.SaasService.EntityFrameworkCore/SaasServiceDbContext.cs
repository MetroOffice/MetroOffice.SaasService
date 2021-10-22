using MetroService.SaasService.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace MetroService.SaasService.EntityFrameworkCore
{
    [ConnectionStringName(SaasServiceDbProperties.ConnectionStringName)]
    public class SaasServiceDbContext : AbpDbContext<SaasServiceDbContext>, ITenantManagementDbContext
    {
        public DbSet<Tenant>? Tenants { get; set; }
        public DbSet<TenantConnectionString>? TenantConnectionStrings { get; set; }

        public SaasServiceDbContext(DbContextOptions<SaasServiceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.ConfigureTenantManagement();
        }
    }
}