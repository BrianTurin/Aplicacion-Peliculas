using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace BBBBFLIX.Monitoring
{
    public class APIMonitoring : Entity<int>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float TotalTime { get; set; }
        public List<string> Events { get; set; } = new List<string>();
    }
}
