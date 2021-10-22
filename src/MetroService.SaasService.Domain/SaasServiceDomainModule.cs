using MetroService.SaasService.Domain.Shared;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace MetroService.SaasService.Domain
{
    [DependsOn(
        typeof(AbpTenantManagementDomainModule),
        typeof(SaasServiceDomainSharedModule)
    )]
    public class SaasServiceDomainModule : AbpModule
    {

    }
}
