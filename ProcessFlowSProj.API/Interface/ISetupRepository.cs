using ProcessFlowSProj.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Interface
{
    public interface ISetupRepository
    {
        Task<bool> AddLevelToSetup(AddLevelForSetupDto addLevel);
        Task<bool> RemoveLevelFromSetup(int approvalLevelId, int operationId);
        Task<bool> EditApprovalLevel(ApprovalLevelForEditDto editLevel);
        Task<IEnumerable<GetDetailedApprovalLevelDto>> GetApprovallevelsByOperationId(int operationId);
        Task<bool> CheckIfOperationExists(int operationId);
        Task<bool> CheckIfRoleExists(int roleId);
    }
}
