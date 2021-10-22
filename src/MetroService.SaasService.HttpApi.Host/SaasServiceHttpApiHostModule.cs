using Microsoft.AspNetCore.Cors;
using Volo.Abp;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Swashbuckle;
using Volo.Abp.Threading;
using MetroService.SaasService.Application;
using MetroService.SaasService.EntityFrameworkCore;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Swashbuckle;
using Volo.Abp.Autofac;

namespace MetroService.SaasService.HttpApi.Host
{
    [DependsOn(
        typeof(SaasServiceHttpApiModule),
        typeof(SaasServiceApplicationModule),
        typeof(SaasServiceEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpAutofacModule))]
    public class SaasServiceHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            JwtBearerConfigurationHelper.Configure(context, "SaasService");

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = true;
            });

            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.Trim().RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            SwaggerConfigurationHelper.Configure(context, "Saas Service API");
            //SwaggerWithAuthConfigurationHelper.Configure(
            //    context: context,
            //    authority: configuration["AuthServer:Authority"],
            //    scopes: new Dictionary<string, string> /* Requested scopes for authorization code request and descriptions for swagger UI only */
            //    {
            //        {"SaasService", "Saas Service API"}
            //    },
            //    apiTitle: "SaasService Gateway API"
            //);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCorrelationId();
            app.UseCors();
            app.UseAbpRequestLocalization();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAbpClaimsMap();
            app.UseMultiTenancy();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json",
                "Saas Service API"); });
            app.UseAbpSerilogEnrichers();
            app.UseAuditing();
            app.UseUnitOfWork();
            app.UseConfiguredEndpoints();
        }
    }
}