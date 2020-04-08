using ProcessFlowSProj.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Interface
{
    public interface IWorkFlow
    {
        void GoForApproval(int operationId, int targetId, int toStaffId, int fromStaffId);
        void ProcessApproval(int operationId, int targetId, int? toStaffId, int fromStaffId, string comment, byte approvalStatusId);
        IEnumerable<StaffForApprovalDto> GetNames(int operationId, int staffId, int? targetId = null);
        WorkFlowResponseDto GetWorkFlowOutput();
    }
}
