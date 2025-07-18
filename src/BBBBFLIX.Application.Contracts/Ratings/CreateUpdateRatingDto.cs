using System;

namespace BBBBFLIX.Ratings
{
    public class CreateUpdateRatingDto
    {
        public int SeriesId { get; set; } // ID de la serie que está siendo calificada
        public int Score { get; set; } // Puntuación de 1 a 5
        public string? Comment { get; set; } // Comentario opcional
    }
}