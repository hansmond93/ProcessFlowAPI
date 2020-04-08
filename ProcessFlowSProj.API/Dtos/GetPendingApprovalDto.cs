using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class GetPendingApprovalDto
    {
        public int StaffId { get; set; }
        public int OperationId { get; set; }
    }
}
