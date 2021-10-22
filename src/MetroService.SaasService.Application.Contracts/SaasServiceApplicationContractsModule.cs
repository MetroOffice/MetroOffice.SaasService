using MetroService.SaasService.Domain.Shared;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace MetroService.SaasService.Application.Contracts
{
    [DependsOn(
        typeof(SaasServiceDomainSharedModule),
        typeof(AbpTenantManagementApplicationContractsModule)
        )]
    public class SaasServiceApplicationContractsModule : AbpModule
    {

    }
}
