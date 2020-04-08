using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class GetApprovalLevelDto
    {
        public int OperationId { get;  set; }
        public int FromStaffId { get;  set; }
        public int? TargetId { get;  set; }
    }
}
