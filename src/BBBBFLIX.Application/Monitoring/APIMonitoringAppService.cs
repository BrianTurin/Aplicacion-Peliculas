using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace BBBBFLIX.Monitoring
{
    public class APIMonitoringAppService : ApplicationService, IAPIMonitoringAppService
    {
        private readonly IRepository<APIMonitoring, int> _apiMonitoringRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly ILogger<APIMonitoringAppService> _logger;

        public APIMonitoringAppService(IRepository<APIMonitoring, int> repository,
            IObjectMapper objectMapper,
            ILogger<APIMonitoringAppService> logger)
        {
            _apiMonitoringRepository = repository;
            _objectMapper = objectMapper;
            _logger = logger;
        }

        public async Task<APIStatisticsDto> GetStatisticsAsync()
        {
            try
            {
                var monitoringsDto = await GetAPIMonitoringsAsync();
                var numMonitorings = monitoringsDto.Length;
                var averageDuration = monitoringsDto.Average(am => am.TotalTime);
                var numEvents = monitoringsDto.Sum(am => am.Events.Count);
                var statisticsDto = new APIStatisticsDto
                {
                    NumMonitorings = numMonitorings,
                    AverageDuration = averageDuration,
                    NumEvents = numEvents,
                };
                _logger.LogInformation("Estadísticas de Monitoreos de API obtenidas correctamente");
                return statisticsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estadísticas de Monitoreos de API");
                throw;
            }
        }

        public async Task<APIMonitoringDto[]> GetAPIMonitoringsAsync()
        {
            try
            {
                var monitorings = await _apiMonitoringRepository.GetListAsync();
                if (monitorings == null)
                {
                    _logger.LogError("Lista de monitoreos nula");
                    throw new Exception("Lista de monitoreos nula");
                }
                else
                {
                    _logger.LogInformation("Lista de Monitoreos de API obtenida correctamente");
                    return _objectMapper.Map<APIMonitoring[], APIMonitoringDto[]>(monitorings.ToArray());
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener lista de Monitoreos de API");
                throw;
            }

        }

        public async Task SaveMonitoringsAsync(APIMonitoringDto monitoringAPIDto)
        {
            try
            {
                if (monitoringAPIDto == null)
                {
                    throw new ArgumentNullException(nameof(monitoringAPIDto), "El objeto monitoringAPIDto no puede ser nulo.");
                }

                var monitoringAPI = _objectMapper.Map<APIMonitoringDto, APIMonitoring>(monitoringAPIDto);
                if (CurrentUnitOfWork == null)
                {
                    throw new InvalidOperationException("CurrentUnitOfWork no puede ser nulo.");
                }

                await _apiMonitoringRepository.InsertAsync(monitoringAPI);
                await CurrentUnitOfWork.SaveChangesAsync();
                _logger.LogInformation("Monitoreo de API guardado correctamente. ID: {Id}", monitoringAPI.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar Monitoreos de API");
                throw;
            }
        }

        public async Task<APIMonitoringDto> StartMonitoring()
        {
            var monitoring = new APIMonitoringDto
            {
                StartTime = DateTime.Now,
            };
            return monitoring;

        }

        public async Task<APIMonitoringDto> EndMonitoring(APIMonitoringDto monitoring)
        {
            monitoring.EndTime = DateTime.Now;
            monitoring.TotalTime = (float)(monitoring.EndTime - monitoring.StartTime).TotalSeconds;
            return monitoring;
        }

        public async Task<APIMonitoringDto> AddEvent(APIMonitoringDto monitoring, string eventName)
        {
            monitoring.EndTime = DateTime.Now;
            monitoring.TotalTime = (float)(monitoring.EndTime - monitoring.StartTime).TotalSeconds;
            monitoring.Events.Add("Event: " + eventName);
            return monitoring;
        }
    }
}
