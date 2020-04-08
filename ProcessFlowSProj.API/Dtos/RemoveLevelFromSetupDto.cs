using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class RemoveLevelFromSetupDto
    {
        public int OperationId { get; set; }
        public int ApprovalLevelId { get; set; }
    }
}
