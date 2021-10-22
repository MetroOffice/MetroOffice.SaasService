using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Volo.Abp.TenantManagement;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using MetroService.SaasService.Domain.Shared.Localization;

namespace MetroService.SaasService.Domain.Shared
{
    [DependsOn(
        typeof(AbpTenantManagementDomainSharedModule)
    )]
    public class SaasServiceDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<SaasServiceDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<SaasServiceResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/SaasService");
            });
        }
    }
}
