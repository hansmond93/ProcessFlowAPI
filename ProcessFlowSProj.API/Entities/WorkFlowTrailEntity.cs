using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class WorkFlowTrailEntity
    {
        [Key]
        public int WorkFlowTrailId { get; set; }

        public int OperationId { get; set; }
        public OperationEntity OperationEntity { get; set; }

        public int TargetId { get; set; }

        public int? FromLevelId { get; set; }
        public ApprovalLevelEntity FromLevel { get; set; }

        
        public int? ToLevelId { get; set; }
        public ApprovalLevelEntity ToLevel { get; set; }

        
        public int? ApprovedByStaffId { get; set; }  //the staffId that finally Approves the request in case the assignee is AWAY e.g LEAVE
        public StaffEntity ApprovedByStaff { get; set; }

        
        public int? FromStaffId { get; set; }
        public StaffEntity FromStaff { get; set; }

        
        public int? ToStaffId { get; set; }
        public StaffEntity ToStaff { get; set; }

        public int RequestStaffId { get; set; }
        public StaffEntity RequestStaff { get; set; }

        
        public int ApprovalStatusId { get; set; }
        public ApprovalStatusEntity ApprovalStatus { get; set; }


        public int StatusId { get; set; }
        public WorkFLowStatusEntity Status { get; set; }

        public string Comment { get; set; }

        public DateTime DateTimeApproved { get; set; }
    }
}
