using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BBBBFLIX.Watchlists
{
    public class WatchlistDto : EntityDto<int>
    {
        public int Id { get; set; } // ID de la lista de seguimiento
        public List<int> SerieIds { get; set; } = new List<int>(); // Lista de IDs de series

        // Constructor opcional
        public WatchlistDto()
        {
        }
    }
}
