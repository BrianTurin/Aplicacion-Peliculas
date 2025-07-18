using AutoMapper;
using BBBBFLIX.Episodes;
using BBBBFLIX.Series;

namespace BBBBFLIX;

public class BBBBFLIXApplicationAutoMapperProfile : Profile
{
    public BBBBFLIXApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Serie, SerieDto>();
        CreateMap<SerieDto, Serie>();
        CreateMap<CreateUpdateSerieDto, Serie>();
        CreateMap<Serie, CreateUpdateSerieDto>();
        CreateMap<Episode, EpisodeDto>();
        CreateMap<EpisodeDto, Episode>();
    }
}
