using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BBBBFLIX.Monitoring
{
    [AutoMap(typeof(APIMonitoringDto))]
    public class APIMonitoringDto : EntityDto<int>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float TotalTime { get; set; }
        public List<string> Events { get; set; } = new List<string>();
    }
}
