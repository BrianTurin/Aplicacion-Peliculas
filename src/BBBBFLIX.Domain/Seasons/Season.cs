using System.Collections.Generic;
using System;
using Volo.Abp.Domain.Entities;
using BBBBFLIX.Episodes;
using BBBBFLIX.Series;
namespace BBBBFLIX.Series
{
    public class Season : Entity<int>
    {
        public int SeasonNumber { get; set; }
        public int Year { get; set; }
        public ICollection<Episode> Episodes { get; set; }
        public string Title { get; set; }
        public DateTime ReleasedDate { get; set; }

        //DB

        public int IdSerie { get; set; }
        public Serie Serie { get; set; }
    }
}