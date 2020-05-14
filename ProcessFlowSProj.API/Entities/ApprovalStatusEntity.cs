using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class ApprovalStatusEntity
    {
        [Key]
        public int ApprovalStatusId { get; set; }
        public string ApprovalStatusName { get; set; }
        public WorkFlowTrailEntity WorkFlowTrailEntity { get; set; }
        public ICollection<ProjectEntity> ProjectEntities { get; set; }

        public static readonly int Pending = 0;
        public static readonly int Procesing = 1;
        public static readonly int Approved = 2;
        public static readonly int Rejected = 3;
        public static readonly int Referred = 4;
        
    }
}
