using BBBBFLIX.Seasons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBBBFLIX.Series
{
    public interface ISeriesApiService  
    {
        Task<SerieDto[]> GetSeriesAsync(string title, string? gender = null);
        Task<SeasonDto> GetSeasonsAsync(string imdbID, int seasonNumber);
    }
}