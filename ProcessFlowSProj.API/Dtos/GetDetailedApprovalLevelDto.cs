using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class GetDetailedApprovalLevelDto
    {
        public int ApprovalLevelId { get; set; }
        public int OperationId { get; set; }
        public int RoleEntityId { get; set; }
        public int Position { get; set; }
        public bool Active { get; set; }
    }
}
