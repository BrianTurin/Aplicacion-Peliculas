using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using BBBBFLIX.Series;
using BBBBFLIX.Notificaciones;
using System.Collections.Generic;
using Volo.Abp.Users;
using System;
using Volo.Abp.ObjectMapping;
using Microsoft.AspNetCore.Authorization;
using BBBBFLIX.Notifications;
using BBBBFLIX.Episodes;
using BBBBFLIX.Notificaciones.Strategies;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace BBBBFLIX.Workers
{
    public class SerieUpdateService : DomainService, ISerieUpdateService
    {
        private readonly ISeriesApiService _seriesApiService;
        private readonly IRepository<Serie, int> _serieRepository;
        private readonly INotificationAppService _notificationService;
        private readonly IObjectMapper _objectMapper;


        public SerieUpdateService(
            ISeriesApiService seriesApiService,
            INotificationAppService notificationService,
            IRepository<Serie, int> serieRepository,
            IObjectMapper objectMapper)
        {
            _seriesApiService = seriesApiService;
            _notificationService = notificationService;
            _serieRepository = serieRepository;
            _objectMapper = objectMapper;
        }
        public async Task VerifyAndUpdateSeriesAsync()
        {
            var series = await _serieRepository.GetListAsync();
            foreach (var serie in series)
            {
                var seriesApi = await _seriesApiService.GetSeriesAsync(serie.Title, serie.Genre);
                if (seriesApi != null && seriesApi.Length > 0)
                {
                    var serieApi = seriesApi.FirstOrDefault();
                    if (serieApi.numSeasons > serie.numSeasons)
                    {
                        var newSeasonNumber = serie.numSeasons + 1;
                        var newSeasonApi = await _seriesApiService.GetSeasonsAsync(serieApi.ImdbId, newSeasonNumber);
                        if (newSeasonApi != null)
                        {
                            var newSeason = new Season
                            {
                                SeasonNumber = newSeasonNumber,
                                Episodes = newSeasonApi.Episodes.Select(e => new Episode
                                {
                                    Title = e.Title,
                                    EpisodeNumber = e.EpisodeNumber,
                                    ReleasedTime = e.ReleasedDate,
                                }).ToList()
                            };
                            serie.Seasons.Add(newSeason);
                            serie.numSeasons = serieApi.numSeasons;
                            await _serieRepository.UpdateAsync(serie);
                            var notificationSeasonTitle = $"Nueva temporada de {serie.Title}";
                            var notificationSeasonMessage = $"Se ha añadido la temporada {newSeasonNumber} de {serie.Title}";
                            var userId = 001;
                            await _notificationService.CreateSendNotificationsAsync(userId, notificationSeasonTitle, notificationSeasonMessage, Notificaciones.Strategies.NotificationType.Email);
                            await _notificationService.CreateSendNotificationsAsync(userId, notificationSeasonTitle, notificationSeasonMessage, Notificaciones.Strategies.NotificationType.Push);
                        }
                    }
                    var lastSeasonLocal = serie.Seasons.OrderByDescending(s => s.SeasonNumber).FirstOrDefault();
                    if (lastSeasonLocal != null)
                    {
                        var lastSeasonApi = await _seriesApiService.GetSeasonsAsync(serieApi.ImdbId, lastSeasonLocal.SeasonNumber);
                    if (lastSeasonApi != null)
                        {
                            if (lastSeasonApi.Episodes.Count > lastSeasonLocal.Episodes.Count)
                            {
                                var localEpisodes = lastSeasonLocal.Episodes.Select(e => e.EpisodeNumber).ToHashSet();
                                var newEpisodes = lastSeasonApi.Episodes
                                    .Where(e => !localEpisodes.Contains(e.EpisodeNumber))
                                    .ToList();
                                if (newEpisodes.Any())
                                {
                                    foreach (var newEpisode in newEpisodes)
                                    {
                                        var newEpisodeToAdd = _objectMapper.Map<EpisodeDto, Episode>(newEpisode);
                                        lastSeasonLocal.Episodes.Add(newEpisodeToAdd);
                                    }
                                    var lastSerieSeason = serie.Seasons.OrderByDescending(t => t.SeasonNumber).FirstOrDefault();
                                    var seasons = serie.Seasons.ToList();
                                    var indexLastSerieSeason = seasons.IndexOf(lastSerieSeason);
                                    seasons[indexLastSerieSeason] = lastSeasonLocal;
                                    serie.Seasons = seasons;
                                    await _serieRepository.UpdateAsync(serie);
                                    var notificationTitle = $"Nuevos episodios en {serie.Title}";
                                    var notificationMessage = $"Se han añadido {newEpisodes.Count} nuevos episodios en la serie {serie.Title}.";
                                    var userId = 001;
                                    await _notificationService.CreateSendNotificationsAsync(userId, notificationTitle, notificationMessage, NotificationType.Email);
                                    await _notificationService.CreateSendNotificationsAsync(userId, notificationTitle, notificationMessage, NotificationType.Push);
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}

