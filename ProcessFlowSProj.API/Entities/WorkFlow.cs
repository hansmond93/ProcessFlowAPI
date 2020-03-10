using ProcessFlowSProj.API.Common;
using ProcessFlowSProj.API.Data;
using ProcessFlowSProj.API.Dtos;
using ProcessFlowSProj.API.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class WorkFlow : IWorkFlow
    {
        private readonly DataContext _dataContext;

        public WorkFlow(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentException("Dependency Injection Failed");
        }
        public WorkFlowResponseDto GoForApproval(int operationId, int targetId, int toStaffId, int fromStaffId)
        {
            var level = _dataContext.ApprovalLevelEntities.Where(x => x.OperationId == operationId && x.Active == true)
                                                           .OrderBy(x => x.Position)
                                                           .ToList()
                                                           .Select(x => new { x.ApprovalLevelId, x.RoleId})
                                                           .FirstOrDefault();
            if (level == null)
                throw new CustomException("The Operation has not been Setup");

            var levelOfToStaffId = _dataContext.StaffEntities.SingleOrDefault(x => x.StaffId == toStaffId).RoleId;

            if (level.RoleId != levelOfToStaffId)
                throw new CustomException("An Error occurred, Please Contact the System Adminstrator");
            

            var workFlowTrailEntity = new WorkFlowTrailEntity
            {
                OperationId = operationId,
                TargetId = targetId,
                ToLevelId = level.ApprovalLevelId,
                FromStaffId = fromStaffId,
                ToStaffId = toStaffId,
                RequestStaffId = fromStaffId,
                ApprovalStatusId = ApprovalStatusEntity.Pending,
                StatusId = WorkFLowStatusEntity.Initiation
            };

            _dataContext.WorkFlowTrailEntities.Add(workFlowTrailEntity);
            _dataContext.SaveChanges();

            var staff = _dataContext.StaffEntities.Where(x => x.StaffId == toStaffId).SingleOrDefault();

            var response = new WorkFlowResponseDto
            {
                ApprovalStatusName = ReturnApprovalStatusAsString(ApprovalStatusEntity.Pending),
                RoleName = _dataContext.StaffRoleEntities.SingleOrDefault(x => x.RoleId == level.RoleId).RoleName,
                WorkFlowStatusName = ReturnWorkFlowStatusAsString(WorkFLowStatusEntity.Initiation),
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                MiddleName = staff.MiddleName

            };

            return response;

        }


        private string ReturnApprovalStatusAsString(byte num)
        {
            switch(num)
            {
                case 0: return "Pending";
                case 1: return "Processing";
                case 2: return "Approved";
                case 3: return "Rejected";
                default: return String.Empty;
            }
        }

        private string ReturnWorkFlowStatusAsString(byte num)
        {
            switch (num)
            {
                case 0: return "Initiation";
                case 1: return "Ongoing";
                case 2: return "Completed";
                default: return String.Empty;
            }
        }

    }
}
