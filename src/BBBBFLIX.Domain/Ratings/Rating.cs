using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace BBBBFLIX.Series
{
    public class Rating : Entity<int>
    {
        public Guid UserId { get; set; }
        public int SerieId { get; set; }
        public Serie Serie { get; set; }
        public int Score { get; set; } // Puntuación de 1 a 5
        public string? Commentary { get; set; } // Comentario opcional
        public DateTime CreatedDate { get; set; }
        public float RatingNumber { get; set; }
    }
}