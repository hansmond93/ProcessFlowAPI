using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class DashboardInfoDto
    {
        public int TotalProject { get; set; }
        public decimal TotalProjectAmount { get; set; }
        public decimal? TotalApprovedAmount { get; set; }
        public int TotalCompletedProject { get; set; }
    }
}
