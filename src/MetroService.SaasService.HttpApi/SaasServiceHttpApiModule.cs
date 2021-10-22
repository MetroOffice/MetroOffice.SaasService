using MetroService.SaasService.Application.Contracts;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace MetroService.SaasService.HttpApi
{
    [DependsOn(
        typeof(SaasServiceApplicationContractsModule),
        typeof(AbpTenantManagementHttpApiModule)
        )]
    public class SaasServiceHttpApiModule : AbpModule
    {
    }
}
