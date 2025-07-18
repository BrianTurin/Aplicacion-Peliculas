using Microsoft.AspNetCore.Authorization;
using BBBBFLIX.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using BBBBFLIX.Ratings;
using Volo.Abp;
using BBBBFLIX.Seasons;
using Microsoft.Extensions.Logging;
using BBBBFLIX.Monitoring;

namespace BBBBFLIX.Series
{
    public class SerieAppService : CrudAppService<Serie, SerieDto, int, PagedAndSortedResultRequestDto, CreateUpdateSerieDto, CreateUpdateSerieDto>, ISerieAppService
    {
        private readonly ISeriesApiService _seriesApiService;
        private readonly IRepository<Serie, int> _serieRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IObjectMapper _objectMapper;
        private readonly ILogger<SerieAppService> _logger;
        private readonly IAPIMonitoringAppService _apiMonitoringAppService;

        public SerieAppService(
           IRepository<Serie, int> repository,
           ISeriesApiService seriesApiService,
           ICurrentUserService currentUserService,
           IObjectMapper objectMapper,
           ILogger<SerieAppService> logger,
           IAPIMonitoringAppService apiMonitoringAppService)
       : base(repository)
        {
            _seriesApiService = seriesApiService;
            _serieRepository = repository;
            _currentUserService = currentUserService;
            _objectMapper = objectMapper;
            _logger = logger;
            _apiMonitoringAppService = apiMonitoringAppService;
        }

        public async Task<SerieDto[]> SearchSerie(string title, string? gender = null)
        {
            var monitoring = await _apiMonitoringAppService.StartMonitoring();
            try
            {
                var series = await _seriesApiService.GetSeriesAsync(title, gender);
                monitoring = await _apiMonitoringAppService.EndMonitoring(monitoring);
                await _apiMonitoringAppService.SaveMonitoringsAsync(monitoring);
                _logger.LogInformation("Búsqueda de series exitosa");
                return series;
            }
            catch (Exception ex)
            {
                monitoring = await _apiMonitoringAppService.AddEvent(monitoring, ex.Message);
                await _apiMonitoringAppService.SaveMonitoringsAsync(monitoring);
                _logger.LogError("Error en la búsqueda de series");
                throw;
            }
        }

        public async Task SaveSeriesAsync(SerieDto[] seriesDto)
        {
            try
            {
                var savedSeries = await _serieRepository.GetListAsync();

                if (savedSeries == null)
                {
                    savedSeries = new List<Serie>();
                }

                foreach (var serieDto in seriesDto)
                {
                    if (serieDto == null) continue;

                    if (serieDto.ImdbId == null)
                    {
                        throw new InvalidOperationException("ImdbId es nulo");
                    }

                    var savedSerie = savedSeries.FirstOrDefault(s => s.ImdbId == serieDto.ImdbId);

                    if (savedSerie == null)
                    {
                        var newSerie = MapSerieDtoToSerie(serieDto);
                        await _serieRepository.InsertAsync(newSerie);
                    }
                    else
                    {
                        if (savedSerie.numSeasons == serieDto.numSeasons)
                        {
                            throw new Exception("La Serie ya está guardada");
                        }
                        else
                        {
                            savedSerie.numSeasons = serieDto.numSeasons;
                            UpdateSeasons(savedSerie, serieDto.Seasons.ToList());
                            await _serieRepository.UpdateAsync(savedSerie);
                        }
                    }
                }
                _logger.LogInformation("Series guardadas exitosamente");
            }
            catch (Exception)
            {
                _logger.LogError("Error al guardar series");
                throw;
            }
        }

        [Authorize("BBBBFLIX.Series.Rate")]
        public async Task RateSerieAsync(RatingDto input)
        {
            try
            {
                // 1. Verificar que la serie existe
                var serie = await _serieRepository.GetAsync(input.SerieId);
                if (serie == null)
                {
                    throw new UserFriendlyException("La serie no existe.");
                }

                var userId = _currentUserService.GetCurrentUserId();
                // 2. Verificar que el usuario actual tenga un Id válido
                if (!userId.HasValue)
                {
                    throw new UserFriendlyException("El usuario actual no tiene un ID válido.");
                }

                // 3. Verificar que la serie es del usuario
                if (serie.CreatorId != userId.Value)
                {
                    throw new UserFriendlyException("La serie que intenta calificar no es del mismo usuario");
                }

                // 4. Agregar o actualizar la calificación de la serie
                var existingRating = serie.Ratings.FirstOrDefault(r => r.UserId == userId.Value);

                if (existingRating != null)
                {
                    // Si ya existe una calificación del usuario, la actualizamos
                    throw new UserFriendlyException("Ya haz calificado la serie");
                }
                else
                {
                    // Si no existe una calificación, agregamos una nueva
                    var rating = new Rating
                    {
                        Score = input.Score,
                        Commentary = input.Commentary,
                        CreatedDate = DateTime.Now,
                        SerieId = input.SerieId,
                        UserId = userId.Value,
                        RatingNumber = input.RatingNumber,
                    };
                    serie.Ratings.Add(rating);
                    await _serieRepository.UpdateAsync(serie);
                }
                _logger.LogInformation("Calificación de serie exitosa");
            }
            catch (Exception)
            { 
                _logger.LogError("Error en la calificación de serie");
                throw;
            }
        }
        public async Task ModificateRateSerieAsync(RatingDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("La calificación a modificar es nula");
            }

            try
            {
                var serie = await _serieRepository.GetAsync(input.SerieId);
                if (serie == null)
                {
                    throw new UserFriendlyException("La serie a modificar calificación es nula");
                }

                var userId = _currentUserService.GetCurrentUserId();
                if (!userId.HasValue)
                {
                    throw new UserFriendlyException("Id de Usuario Actual nula");
                }
                if (serie.CreatorId != userId.Value)
                {
                    throw new UserFriendlyException("La serie no es del usuario actual");
                }

                var savedRating = serie.Ratings.FirstOrDefault(r => r.UserId == userId.Value);
                if (savedRating == null)
                {
                    throw new UserFriendlyException("Calificación Guardada es nula");
                }
                savedRating.RatingNumber = input.RatingNumber;
                savedRating.Commentary = input.Commentary;
                savedRating.CreatedDate = input.CreatedDate;
                await _serieRepository.UpdateAsync(serie);
                _logger.LogInformation("Modificación de calificación exitosa");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la modificación de calificación");
                throw;
            }
        }

        public async Task<SerieDto[]> SavedSeriesAsync()
        {
            try
            {
                var series = await _serieRepository.GetListAsync();
                if (series == null)
                {
                    throw new UserFriendlyException("No existen series guardadas");
                }
                _logger.LogInformation("Series guardadas exitosamente");
                return _objectMapper.Map<Serie[], SerieDto[]>(series.ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex,"Error al obtener series guardadas");
                throw;
            }
        }

        private Serie MapSerieDtoToSerie(SerieDto serieDto)
        {
            var serie = _objectMapper.Map<SerieDto, Serie>(serieDto);
            serie.Seasons = new List<Season>();

            if (serieDto.Seasons != null)
            {
                foreach (var temporadaDto in serieDto.Seasons)
                {
                    var temporada = _objectMapper.Map<SeasonDto, Season>(temporadaDto);
                    serie.Seasons.Add(temporada);
                }
            }
            _logger.LogInformation("Mapeo de SerieDto a Serie exitoso");
            return serie;
        }

        private void UpdateSeasons(Serie savedSerie, List<SeasonDto> seasonsDto)
        {
            if (seasonsDto != null)
            { 
                foreach (var seasonDto in seasonsDto)
                {
                    var savedSeason = savedSerie.Seasons.FirstOrDefault(s => s.SeasonNumber == seasonDto.SeasonNumber);
                    if (savedSeason == null)
                    {
                        var newSeason = _objectMapper.Map<SeasonDto,Season>(seasonDto);
                        savedSerie.Seasons.Add(newSeason);
                        _logger.LogInformation("Agregada nueva temporada (savedSeaon==null)");
                    }
                    else
                    {
                        _objectMapper.Map(seasonDto, savedSeason);
                        _logger.LogInformation("Actualización de temporadas exitosa");
                    }
                }
            }
        }

        public async Task<SerieDto[]> GetAllSeriesAsync()
        {
            try
            {
                var series = await _serieRepository.GetListAsync();

                if (series == null)
                {
                    throw new Exception("No hay series guardadas");
                }
                _logger.LogInformation("Obtención de series guardadas exitosa");
                return _objectMapper.Map<Serie[], SerieDto[]>(series.ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error al obtener series guardadas");
                throw;
            }
        }
    }
}