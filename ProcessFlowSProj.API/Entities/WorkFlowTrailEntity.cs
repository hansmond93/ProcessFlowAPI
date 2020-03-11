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

        [ForeignKey("OperationEntity")]
        public int OperationId { get; set; }
        
        public int TargetId { get; set; }

        [ForeignKey("ApprovalLevelEntity")]
        public int? FromLevelId { get; set; }

        [ForeignKey("ApprovalLevelEntity")]
        public int? ToLevelId { get; set; }

        [ForeignKey("StaffEntity")]
        public int? ApprovedByStaffId { get; set; }  //the staffId that finally Approves the request in case the assignee is AWAY e.g LEAVE

        [ForeignKey("StaffEntity")]
        public int FromStaffId { get; set; }

        [ForeignKey("StaffEntity")]
        public int? ToStaffId { get; set; }

        public int RequestStaffId { get; set; }

        [ForeignKey("ApprovalStatusEntity")]
        public byte ApprovalStatusId { get; set; }

        [ForeignKey("WorkFlowStatusEntity")]
        public byte StatusId { get; set; }

        public string Comment { get; set; }

        //public ApprovalLevelEntity ApprovalLevelEntity { get; set; }

        //public StaffEntity StaffEntity { get; set; }

        public OperationEntity OperationEntity { get; set; }
    }
}
