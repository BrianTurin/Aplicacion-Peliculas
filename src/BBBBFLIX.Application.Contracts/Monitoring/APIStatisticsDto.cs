using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BBBBFLIX.Monitoring
{
    public class APIStatisticsDto : EntityDto<int>
    {
        public int NumMonitorings { get; set; }
        public int NumEvents { get; set; }
        public float AverageDuration { get; set; }
    }
}
