using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class ProjectForApprovalDto
    {
        public int ProjectId { get; set; }
        public int OperationId { get; set; }
        public int FromStaffId { get; set; }
        public int ToStaffId { get; set; }

    }
}
