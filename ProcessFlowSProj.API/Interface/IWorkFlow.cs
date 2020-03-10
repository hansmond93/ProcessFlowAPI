using ProcessFlowSProj.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Interface
{
    public interface IWorkFlow
    {
        WorkFlowResponseDto GoForApproval(int operationId, int targetId, int toStaffId, int fromStaffId);
    }
}
