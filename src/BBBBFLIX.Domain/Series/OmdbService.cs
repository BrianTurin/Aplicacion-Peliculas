using BBBBFLIX.Episodes;
using BBBBFLIX.Seasons;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace BBBBFLIX.Series
{
    public class OmdbService : ISeriesApiService, ITransientDependency
    {
        private static readonly string apiKey = "a012c4ac"; // Reemplaza con tu clave API de OMDb.
        private static readonly string baseUrl = "http://www.omdbapi.com/";
        private readonly ILogger<OmdbService> _logger;

        public OmdbService(ILogger<OmdbService> logger)
        {
            _logger = logger;
        }
        public async Task<SerieDto[]> GetSeriesAsync(string title, string? gender = null)
        {
            if (title == null)
            {
                _logger.LogError("Error: título = null");
                throw new ArgumentException("Error: título = null");
            }
            else if (title != null && gender == null)
            {
                _logger.LogInformation("Buscando serie solo por título");
                return await SearchByTitleAsinc(title);
            }
            else if (title != null && gender != null)
            {
                _logger.LogInformation("Buscando serie por título y género");
                return await SearchByTitleAndGenderAsync(title, gender);
            }
            else if (title == null && gender != null)
            {
                _logger.LogInformation("Error al buscar por título nulo");
                throw new ArgumentException("Error: título = null, solo ingresó: ", nameof(gender));
            }
            else
            {
                _logger.LogInformation("Error al procesar los datos de búsqueda");
                throw new ArgumentException("Ocurrió un error al procesar los datos de búsqueda");
            }
        }
        private async Task<SerieDto[]> SearchByTitleAsinc(string title)
        {
            var url = $"{baseUrl}?apikey={apiKey}&s={title}&type=series";
            _logger.LogInformation("Buscando serie por título: {title}", title);
            return await GetOmdbSeriesAsync(url);
        }
        private async Task<SerieDto[]> SearchByTitleAndGenderAsync(string titulo, string genero)
        {
            var url = $"{baseUrl}?apikey={apiKey}&s={titulo}&type=series";
            _logger.LogInformation("Buscando serie por título: {titulo} y género: {genero}", titulo, genero);
            var series = await GetOmdbSeriesAsync(url);

            var seriesFiltradas = new List<SerieDto>();
            foreach (var serie in series)
            {
                if (serie.Genre != null && serie.Genre.Contains(genero, StringComparison.OrdinalIgnoreCase))
                {
                    seriesFiltradas.Add(serie);
                }
            }

            return seriesFiltradas.ToArray();
        }
        private async Task<SerieDto[]> GetOmdbSeriesAsync(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    _logger.LogInformation("Obteniendo series desde omdbAPI");
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(jsonResponse);

                    if (json["Response"]?.ToString() == "False")
                    {
                        _logger.LogError("Error al obtener Series desde omdbAPI: {error}", json["Error"]?.ToString());
                        throw new Exception("Error al obtener Series desde omdbAPI: " + json["Error"]?.ToString());
                    }

                    var seriesJson = json["Search"];
                    if (seriesJson == null)
                    {
                        _logger.LogError("La API devolvió null");
                        throw new Exception("La API devolvió null");
                    }

                    var seriesList = new List<SerieDto>();
                    foreach (var serie in seriesJson)
                    {
                        var serieId = serie["imdbID"]?.ToString();
                        var serieDetails = await GetOtherInformationAsync(serieId, json);

                        if (serieDetails != null)
                        {
                            seriesList.Add(serieDetails);
                        }
                    }

                    return seriesList.ToArray();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener Series desde omdbAPI: {error}", ex.Message);
                throw new Exception("Error al obtener Series desde omdbAPI: " + ex.Message, ex);
            }
        }
        private async Task<SerieDto> GetOtherInformationAsync(string imdbID, JObject json)
        {
            var url = $"{baseUrl}?apikey={apiKey}&i={imdbID}";

            try
            {
                _logger.LogInformation("Obteniendo demás datos de Serie");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var jsonDTO = JObject.Parse(jsonResponse);

                    return new SerieDto
                    {
                        Title = jsonDTO["Title"]?.ToString(),
                        Year = jsonDTO["Year"]?.ToString(),
                        Rated = jsonDTO["Rated"]?.ToString(),
                        ReleasedDate = jsonDTO["Released"]?.ToString(),
                        Duration = jsonDTO["Runtime"]?.ToString(),
                        Genre = jsonDTO["Genre"]?.ToString(),
                        Director = jsonDTO["Director"]?.ToString(),
                        Writer = jsonDTO["Writer"]?.ToString(),
                        Actors = jsonDTO["Actors"]?.ToString(),
                        Plot = jsonDTO["Plot"]?.ToString(),
                        Language = jsonDTO["Language"]?.ToString(),
                        Country = jsonDTO["Country"]?.ToString(),
                        Poster = jsonDTO["Poster"]?.ToString(),
                        ImdbRating = jsonDTO["imdbRating"]?.ToString(),
                        ImdbVotes = int.TryParse(jsonDTO["imdbVotes"]?.ToString().Replace(",", ""), out var votes) ? votes : 0,
                        ImdbId = imdbID,
                        Type = jsonDTO["Type"]?.ToString(),
                        numSeasons = int.TryParse(jsonDTO["totalSeasons"]?.ToString(), out var seasons) ? seasons : 0
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener demás datos de Serie: {error}", ex.Message);
                throw new Exception("Error al obtener demás datos de Serie: " + ex.Message, ex);
            }
        }
        public async Task<SeasonDto> GetSeasonsAsync(string imdbID, int seasonNumber)
        {
            _logger.LogInformation("Obteniendo temporadas de la serie con ID: {imdbID}", imdbID);
            if (string.IsNullOrWhiteSpace(imdbID))
            {
                _logger.LogInformation("Error: imdbID de serie nulo o vacío");
                throw new Exception("Error: imdbID de serie nulo o vacío");
            }
            var url = $"{baseUrl}?apikey={apiKey}&i={imdbID}&Season={seasonNumber}";
            _logger.LogInformation("Buscando temporada por ID: {imdbID} y número de temporada: {seasonNumber}", imdbID, seasonNumber);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(jsonResponse);
                    if (json["Response"]?.ToString() == "False")
                    {
                        _logger.LogError("Error al obtener temporadas desde omdbAPI: {error}", json["Error"]?.ToString());
                        throw new Exception("Error al obtener temporadas desde omdbAPI: " + json["Error"]?.ToString());
                    }
                    var episodesJson = json["Episodes"];
                    if (episodesJson == null)
                    {
                        _logger.LogError("La API devolvió null los episodios");
                        throw new Exception("La API devolvió null los episodios");
                    }
                    var episodes = new List<EpisodeDto>();
                    foreach (var episode in episodesJson)
                    {
                        episodes.Add(new EpisodeDto
                        {
                            Title = episode["Title"]?.ToString(),
                            EpisodeNumber = int.TryParse(episode["Episode"]?.ToString(), out var episodioNum) ? episodioNum : 0,
                            ReleasedDate = DateTime.TryParse(episode["Released"]?.ToString(), out var fecha) ? fecha : DateTime.MinValue
                        });
                    }
                    return new SeasonDto
                    {
                        Title = json["Title"]?.ToString(),
                        SeasonNumber = int.TryParse(json["Season"]?.ToString(), out var seasonNum) ? seasonNumber : 0,
                        Episodes = episodes
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al obtener temporadas desde omdbAPI: {error}", ex.Message);
                throw new Exception("Error al obtener temporadas desde omdbAPI: " + ex.Message, ex);

            }
        }
    }
    }