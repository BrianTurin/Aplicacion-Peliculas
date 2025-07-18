using BBBBFLIX.Ratings;
using BBBBFLIX.Seasons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace BBBBFLIX.Series
{
    public class Serie : AggregateRoot<int>, ISoftDelete
    {
        public  string Title { get; set; }
        public  string Genre { get; set; }
        public  string Year { get; set; }
        public ICollection<Season> Seasons { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public int numSeasons { get; set; }
        public  string Plot { get; set; }
        public  string Director { get; set; }
        public  string Actors { get; set; }
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

        //User
        public Guid Creator { get; set; }
        public Guid CreatorId;

        public Serie()
        {
            Ratings = new List<Rating>();
        }

        //is deleted?
        public Boolean IsDeleted { get; set; }
    }
}

