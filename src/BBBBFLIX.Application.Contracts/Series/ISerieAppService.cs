using BBBBFLIX.Ratings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BBBBFLIX.Series
{
    public interface ISerieAppService : ICrudAppService<SerieDto, int, PagedAndSortedResultRequestDto, CreateUpdateSerieDto, CreateUpdateSerieDto>
    {
        Task<SerieDto[]> SearchSerie(string title, string? gender = null);

        // Método para calificar una serie
        Task RateSerieAsync(RatingDto input);
        Task ModificateRateSerieAsync(RatingDto input);
        Task<SerieDto[]> SavedSeriesAsync();
        Task<SerieDto[]> GetAllSeriesAsync();
    }
}