using System.Collections.Generic;
using BBBBFLIX.Notifications;
using BBBBFLIX.Seasons;

namespace BBBBFLIX.Series
{
    public class CreateUpdateSerieDto
    {
        public required string Title { get; set; }
        public required string Year { get; set; }
        public required string Genre { get; set; }
        public required string Plot { get; set; }
        public required string Director { get; set; }
        public required string Actors { get; set; }
        public List<SeasonDto> Seasons { get; set; } = new(); // Permitir agregar temporadas al crear/actualizar la serie
        public List<NotificationDto> Notifications { get; set; } = new();
    }
}