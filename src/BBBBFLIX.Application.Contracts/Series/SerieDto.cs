using BBBBFLIX.Notifications;
using BBBBFLIX.Ratings;
using BBBBFLIX.Seasons;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace BBBBFLIX.Series
{
    public class SerieDto : EntityDto<int>
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public ICollection<SeasonDto> Seasons { get; set; }
        public ICollection<RatingDto> Ratings { get; set; }
        public int numSeasons { get; set; }
        public string Plot { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Rated { get; set; }
        public string ReleasedDate { get; set; }
        public string Writer { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Poster { get; set; }
        public string Type { get; set; }
        public string Duration { get; set; }

        //IMDB
        public string ImdbId { get; set; }
        public string ImdbRating { get; set; }
        public int ImdbVotes { get; set; }
    }
}
