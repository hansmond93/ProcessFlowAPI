using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcessFlowSProj.API.Entities
{
    public class ApprovalLevelEntity
    {
        [Key]
        public int ApprovalLevelId { get; set; }

        [ForeignKey("OperationEntity")]
        public int OperationId { get; set; }
        public OperationEntity OperationEntity { get; set; }


        [ForeignKey("StaffRoleEntity")]
        public int RoleId { get; set; }
        public StaffRoleEntity StaffRoleEntity { get; set; }



        public WorkFlowTrailEntity WorkFlowTrailEntityToLevel { get; set; }
        public WorkFlowTrailEntity WorkFlowTrailEntityFromLevel { get; set; }

        public int Position { get; set; }

        public bool Active { get; set; }
    }
}
