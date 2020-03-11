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

        private int _operationId;
        private int _targetId;
        private int? _fromlevelId;
        private int? _toLevelId;
        private int? _approvedByStaffId;
        private int _fromStaffId;
        private int _toStaffId;
        private int _requestStaffId;
        private byte _approvalStatusId;
        private byte _statusId;
        private string _comment;
        private readonly string _defaultComment = "Please Kindly Approve Operation";


        public int OperationId { set { _operationId = value; } }
        public int TargetId { set { _targetId = value; } }
        public int? FromLevelId { set { _fromlevelId = value; } }
        public int? ToLevelId { set { _toLevelId = value; } }
        public int? ApprovedByStaffId { set { _approvedByStaffId = value; } }
        public int FromStaffId { set { _fromStaffId = value; } }
        public int ToStaffId { set { _toStaffId = value; } }
        public int RequestStaffId { set { _requestStaffId = value; } }
        public byte ApprovalStatusId { set { _approvalStatusId = value; } }
        public byte StatusId { set { _statusId = value; } }
        public string Comment { set { _comment = value; } }


        public void ProcessRequest()
        {
            var firstLevelOnSetup = _dataContext.ApprovalLevelEntities.Where(x => x.OperationId == _operationId && x.Active == true)
                                                                 .OrderBy(x => x.Position)
                                                                 .Select(x => new { x.Position, x.RoleId })
                                                                 .FirstOrDefault();
            if (firstLevelOnSetup == null)
                throw new CustomException("The Operation has not been Setup");

            var lastWorkFlowTrail = _dataContext.WorkFlowTrailEntities.Where(x => x.OperationId == _operationId
                                                                        && x.TargetId == _targetId)
                                                              .OrderByDescending(x => x.WorkFlowTrailId)
                                                              .FirstOrDefault();


            var roleOfToStaffId = _dataContext.StaffEntities.SingleOrDefault(x => x.StaffId == _toStaffId).RoleId;

            if(lastWorkFlowTrail != null)
            {
                if (firstLevelOnSetup.RoleId == roleOfToStaffId && lastWorkFlowTrail.StatusId == 2)
                {
                    GoForApproval(_operationId, _targetId, _toStaffId, _fromStaffId);
                    return;
                }
                else
                {
                    //Not a new Request implementation here
                }
            }
            else
            {
                if (firstLevelOnSetup.RoleId == roleOfToStaffId)
                {
                    //
                }
                else
                {
                    //Not a new Request implementation here
                }
            }

            

        }

        private void GoForApproval(int operationId, int targetId, int toStaffId, int fromStaffId)
        {
            var level = _dataContext.ApprovalLevelEntities.Where(x => x.OperationId == operationId && x.Active == true)
                                                           .OrderBy(x => x.Position)
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
                StatusId = WorkFLowStatusEntity.Initiation,
                Comment = _defaultComment
            };

            _dataContext.WorkFlowTrailEntities.Add(workFlowTrailEntity);
            _dataContext.SaveChanges();

            //var staff = _dataContext.StaffEntities.Where(x => x.StaffId == toStaffId).SingleOrDefault();

            //var response = new WorkFlowResponseDto
            //{
            //    ApprovalStatusName = ReturnApprovalStatusAsString(ApprovalStatusEntity.Pending),
            //    RoleName = _dataContext.StaffRoleEntities.SingleOrDefault(x => x.RoleId == level.RoleId).RoleName,
            //    WorkFlowStatusName = ReturnWorkFlowStatusAsString(WorkFLowStatusEntity.Initiation),
            //    FirstName = staff.FirstName,
            //    LastName = staff.LastName,
            //    MiddleName = staff.MiddleName

            //};

            //return response;

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
