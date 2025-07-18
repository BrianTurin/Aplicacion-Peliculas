using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBBBFLIX.Episodes
{
    public class EpisodeDto
    {
        public required string Title { get; set; }
        public int EpisodeNumber { get; set; }
        public TimeSpan Duration { get; set; }
        public string Synopsis { get; set; }
        public DateTime ReleasedDate { get; set; }
        public string Directors { get; set; }
        public string Writers { get; set; }
        public int SeasonId { get; set; }
    }
}
