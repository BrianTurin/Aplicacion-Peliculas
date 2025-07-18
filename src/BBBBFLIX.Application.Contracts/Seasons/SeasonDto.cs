using BBBBFLIX.Episodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BBBBFLIX.Seasons
{
    public class SeasonDto : EntityDto<int>
    {
        public int SeasonNumber { get; set; }
        public int Year { get; set; }
        public ICollection<EpisodeDto> Episodes { get; set; }
        public string Title { get; set; }
        public DateTime ReleasedDate { get; set; }

        public int SerieID { get; set; }
    }
}
