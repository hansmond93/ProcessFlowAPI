﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class ProcessApprovalDto
    {
        public int OperationId { get; set; }
        public int TargetId { get; set; }
        public int? ToStaffId { get; set; }
        public int FromStaffId { get; set; }
        public string Comment { get; set; }
        public byte ApprovalStatusId { get; set; }
    }
}
