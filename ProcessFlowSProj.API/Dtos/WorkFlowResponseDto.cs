using ProcessFlowSProj.API.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class WorkFlowResponseDto
    {
        public int StaffId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public  string FullName { get; set; }
        public string ApprovalStatusName { get; set; }
    }
}
