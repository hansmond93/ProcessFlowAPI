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

        public static readonly byte Pending = 0;
        public static readonly byte Procesing = 1;
        public static readonly byte Approved = 2;
        public static readonly byte Rejected = 3;
        public static readonly byte Referred = 4;
        
    }
}
