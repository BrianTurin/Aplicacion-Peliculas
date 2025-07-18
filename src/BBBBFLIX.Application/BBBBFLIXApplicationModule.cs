using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using BBBBFLIX.Series;
using BBBBFLIX.Workers;
using BBBBFLIX.Monitoring;

namespace BBBBFLIX
{
    [DependsOn(
           typeof(BBBBFLIXDomainModule)
       )]
    public class BBBBFLIXApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BBBBFLIXApplicationModule>();
            });

            // Registrar el servicio de dominio  
            context.Services.AddTransient<SerieUpdateService>();

            // Registrar el worker como un IHostedService  
            context.Services.AddHostedService<SerieUpdateService>();

            context.Services.AddTransient<ISeriesApiService, OmdbService>();

            // Registrar el servicio de MonitoreoApiAppService  
            context.Services.AddTransient<IAPIMonitoringAppService, APIMonitoringAppService>();
        }
    }
}
