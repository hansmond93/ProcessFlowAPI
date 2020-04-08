using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class GetProjectApprovalDto : GetProjectDto
    {
        public int FromStaffId { get; set; }
        public string Comment { get; set; }
        public DateTime DateApprovedByPreviousLevel { get; set; }
        public int RequestStaffId { get; set; }
        public string FromStaffName { get; set; }
        public string RequestStaffName { get; set; }
    }
}
