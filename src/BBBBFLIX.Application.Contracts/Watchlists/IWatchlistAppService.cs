using BBBBFLIX.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace BBBBFLIX.Watchlists
{
    public interface IWatchlistAppService : IApplicationService
    {
        Task AddSerieAsync(SerieDto serieDto);
        Task<SerieDto[]> ShowSeriesAsync();
        Task DeleteSerieAsync(string imdbId);
    }
}
