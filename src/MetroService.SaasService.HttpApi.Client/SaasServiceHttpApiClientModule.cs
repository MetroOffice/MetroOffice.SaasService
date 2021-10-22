using MetroService.SaasService.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace MetroService.SaasService.HttpApi.Client
{
    [DependsOn(
        typeof(SaasServiceApplicationContractsModule),
        typeof(AbpTenantManagementHttpApiClientModule))]
    public class SaasServiceHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(SaasServiceApplicationContractsModule).Assembly,
                SaasServiceRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}