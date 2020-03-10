using ProcessFlowSProj.API.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class WorkFlowResponseDto: StaffBaseEntity
    {
        public string RoleName { get; set; }
        
        public string ApprovalStatusName { get; set; }

        public string WorkFlowStatusName { get; set; }
        
    }
}
