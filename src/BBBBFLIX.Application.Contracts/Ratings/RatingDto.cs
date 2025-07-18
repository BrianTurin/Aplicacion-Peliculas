using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace BBBBFLIX.Ratings
{
    public class RatingDto : EntityDto<int>
    {
        public Guid UserId { get; set; }
        public int SerieId { get; set; }
        public int Score { get; set; }
        public string? Commentary { get; set; }
        public DateTime CreatedDate { get; set; }
        public float RatingNumber { get; set; }
    }
}