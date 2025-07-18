using BBBBFLIX.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;


namespace BBBBFLIX.WatchLists
{
    public class Watchlist : AggregateRoot<int>, IMustHaveCreator<Guid>
    {
        public List<Serie> Series { get; set; } = new List<Serie>();
        public Guid Creator {  get; set; }
        public Guid CreatorId { get; set; }
        public DateTime ModificatedDate { get; set; }
    }
}
