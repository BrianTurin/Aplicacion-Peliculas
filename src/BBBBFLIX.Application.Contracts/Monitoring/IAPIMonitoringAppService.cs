using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace BBBBFLIX.Monitoring
{
    public interface IAPIMonitoringAppService : IApplicationService
    {
        Task<APIStatisticsDto> GetStatisticsAsync();
        Task<APIMonitoringDto[]> GetAPIMonitoringsAsync();
        Task SaveMonitoringsAsync(APIMonitoringDto monitoringAPIDto);
        Task<APIMonitoringDto> StartMonitoring();
        Task<APIMonitoringDto> EndMonitoring(APIMonitoringDto monitoring);
        Task<APIMonitoringDto> AddEvent(APIMonitoringDto monitoring, string eventName);
    }
}
