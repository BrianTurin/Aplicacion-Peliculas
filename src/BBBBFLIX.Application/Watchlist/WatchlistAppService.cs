using BBBBFLIX.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using BBBBFLIX.WatchLists;

namespace BBBBFLIX.Watchlists
{
    public class WatchlistAppService : ApplicationService, IWatchlistAppService
    {
        private readonly IRepository<Watchlist, int> _watchlistRepository;
        private readonly IRepository<Serie, int> _serieRepository;
        private readonly ICurrentUser _currentUser;
        private readonly SerieAppService _serieAppService;

        public WatchlistAppService(IRepository<Watchlist, int> watchlistRepository,
            IRepository<Serie, int> serieRepository,
            ICurrentUser currentUser,
            SerieAppService serieAppService)
        {
             _serieRepository = serieRepository;
            _watchlistRepository = watchlistRepository;
            _currentUser = currentUser;
            _serieAppService = serieAppService;
        }

        public async Task AddSerieAsync(SerieDto serieDto)
        {
            //Getting current user
            Guid userId = (Guid)_currentUser.Id;
            //Getting current user's watchlist
            var queryable = await _watchlistRepository.WithDetailsAsync(x => x.Series);
            var watchlist = await AsyncExecuter.FirstOrDefaultAsync(queryable);
            //If Watchlist doesn't exist, create a new
            if (watchlist == null)
            {
                watchlist = new Watchlist()
                {
                    ModificatedDate = DateTime.Now,
                };
                await _watchlistRepository.InsertAsync(watchlist);
            }

            //Looking if the serie already has into watchlist
            if (watchlist.Series.Any(s => s.ImdbId == serieDto.ImdbId))
            {
                throw new Exception("La Serie a agregar ya se encuentra en la watchlist");
            }
            else
            {
                var seriesDto = new List<SerieDto> { serieDto };
                await _serieAppService.SaveSeriesAsync(seriesDto.ToArray());
                var serie = (await _serieRepository.GetListAsync()).LastOrDefault();
                watchlist.Series.Add(serie);
                watchlist.ModificatedDate = DateTime.Now;
            }
            await _watchlistRepository.UpdateAsync(watchlist);
        }
        public async Task<SerieDto[]> ShowSeriesAsync()
        {
            //Getting current user
            Guid userId = (Guid)_currentUser.Id;
            var queryable = await _watchlistRepository.WithDetailsAsync(x => x.Series);
            //Searching a watchlist
            var watchlist = await AsyncExecuter.FirstOrDefaultAsync(queryable);
            if (watchlist == null)
            {
                throw new Exception("Watchlist nula");
            }

            return ObjectMapper.Map<Serie[], SerieDto[]>(watchlist.Series.ToArray());
        }

        public async Task DeleteSerieAsync(string imdbId)
        {
            //Getting current user
            Guid userId = (Guid)_currentUser.Id;
            var queryable = await _watchlistRepository.WithDetailsAsync(x => x.Series);
            //Searching a watchlist
            var watchlist = await AsyncExecuter.FirstOrDefaultAsync(queryable);
            if (watchlist == null)
            {
                throw new Exception("Watchlist nula");
            }

            //First, search if any serie imdb is equal to imdb past as parameter
            if (watchlist.Series.Any(s => s.ImdbId == imdbId))
            {
                var serie = (await _serieRepository.GetListAsync()).LastOrDefault();
                if (serie == null)
                {
                    throw new Exception("La serie a eliminar no puede ser nula");
                }
                watchlist.Series.Remove(serie);
                await _serieRepository.DeleteAsync(serie);
                watchlist.ModificatedDate = DateTime.Now;
            }
            else
            //If doesn't found, throw an exception
            {
                throw new Exception("La Serie que está intentando eliminar no está en la Watchlist");
            }
            await _watchlistRepository.UpdateAsync(watchlist);
        }
    }
}
