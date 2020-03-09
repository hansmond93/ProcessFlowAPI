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

        private static readonly byte Pending = 0;
        private static readonly byte Procesing = 1;
        private static readonly byte Approved = 2;
        private static readonly byte Rejected = 3;
        
    }
}
