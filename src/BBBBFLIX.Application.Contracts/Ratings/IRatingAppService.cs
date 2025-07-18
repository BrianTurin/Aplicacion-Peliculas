using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BBBBFLIX.Ratings
{
    public interface IRatingAppService : IApplicationService
    {
        Task<RatingDto> CreateAsync(CreateUpdateRatingDto input, Guid userId);
        Task<List<RatingDto>> GetRatingsBySeriesIdAsync(int seriesId);
        Task<List<RatingDto>> GetRatingsByUserIdAsync(Guid userId);
    }
}
