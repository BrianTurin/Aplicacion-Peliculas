using AutoMapper;
using BBBBFLIX.Monitoring;
using BBBBFLIX.Series;
using Volo.Abp.Application.Dtos;

namespace BBBBFLIX.Application.Mapping
{
    public class SerieMappingProfile : Profile
    {
        public SerieMappingProfile()
        {
            CreateMap<SerieDto, Serie>();
            CreateMap<Serie, SerieDto>();
            CreateMap<APIMonitoringDto, APIMonitoring>();
            CreateMap<APIMonitoring, APIMonitoringDto>();
        }
    }
}