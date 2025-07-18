using System;
using Volo.Abp.Domain.Entities;
namespace BBBBFLIX.Series
{
    public class Episode : Entity<Guid>
    {
        public required string Title { get; set; }
        public int EpisodeNumber { get; set; }
        public TimeSpan Duration { get; set; }
        public string Synopsis { get; set; }
        public DateTime ReleasedTime { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }

        public int IdSeason { get; set; }
        public Season Season { get; set; }
    }
}