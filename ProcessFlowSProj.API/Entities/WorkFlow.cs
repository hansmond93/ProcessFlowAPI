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
        private int _nextLevelRoleId;
        private string _nextLevelRoleName;
        private string _nextLevelFirstname;
        private string _nextLevelLastname;
        private string _nextLevelMiddlename;
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


        //public void ProcessRequest()
        //{
        //    var firstLevelOnSetup = _dataContext.ApprovalLevelEntities.Where(x => x.OperationId == _operationId && x.Active == true)
        //                                                         .OrderBy(x => x.Position)
        //                                                         .Select(x => new { x.Position, x.RoleId })
        //                                                         .FirstOrDefault();
        //    if (firstLevelOnSetup == null)
        //        throw new CustomException("The Operation has not been Setup");

        //    var lastWorkFlowTrail = _dataContext.WorkFlowTrailEntities.Where(x => x.OperationId == _operationId
        //                                                                && x.TargetId == _targetId)
        //                                                      .OrderByDescending(x => x.WorkFlowTrailId)
        //                                                      .FirstOrDefault();


        //    var roleOfToStaffId = _dataContext.StaffEntities.SingleOrDefault(x => x.StaffId == _toStaffId).RoleId;

        //    if(lastWorkFlowTrail != null)
        //    {
        //        if (firstLevelOnSetup.RoleId == roleOfToStaffId && lastWorkFlowTrail.StatusId == 2)
        //        {
        //            GoForApproval(_operationId, _targetId, _toStaffId, _fromStaffId);
        //            return;
        //        }
        //        else
        //        {
        //            //Not a new Request implementation here
        //        }
        //    }
        //    else
        //    {
        //        if (firstLevelOnSetup.RoleId == roleOfToStaffId)
        //        {
        //            //
        //        }
        //        else
        //        {
        //            //Not a new Request implementation here
        //        }
        //    }

            

        //}

        public void GoForApproval(int operationId, int targetId, int toStaffId, int fromStaffId)
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

            var staff = _dataContext.StaffEntities.Where(x => x.StaffId == toStaffId).SingleOrDefault();

            _approvalStatusId = ApprovalStatusEntity.Pending;
            _nextLevelRoleId = level.RoleId;
            _nextLevelRoleName = _dataContext.StaffRoleEntities.SingleOrDefault(x => x.RoleId == level.RoleId).RoleName;
            _nextLevelFirstname = staff.FirstName;
            _nextLevelLastname = staff.LastName;
            _nextLevelMiddlename = staff.MiddleName;



        }

        public void ProcessApproval(int operationId, int targetId, int toStaffId, int fromStaffId, string comment, byte approvalStatusId)
        {
            var roleOfToStaffId = _dataContext.StaffEntities.SingleOrDefault(x => x.StaffId == toStaffId).RoleId;

            if (roleOfToStaffId <= 0) throw new CustomException("An Error Occurred, Please Contact the System Administrator");

            //var levelOfToStaffId = _dataContext.ApprovalLevelEntities.SingleOrDefault(x => x.RoleId == roleOfToStaffId);

            var approvalLevels = _dataContext.ApprovalLevelEntities.Where(x => x.OperationId == operationId && x.Active == true)
                                                                   .OrderBy(x => x.Position)
                                                                   .ToList();

            ApprovalLevelEntity nextLevel = null;

            for (int i = 0; i < approvalLevels.Count(); i++)
            {
                if(approvalLevels[i].RoleId == roleOfToStaffId)
                {
                    var j = i + 1;
                    if (j < approvalLevels.Count())
                    {
                        nextLevel = approvalLevels[i++];    //check if i++ is evaluated first before the array is called
                    }   
                    //more work whe  the role can appear more than once
                    //Also Check if it is the last level on the list so that the process can be finally approved and terminated....Do this later
                    return;
                }
            }

            var requestStaffIdOnTrail = _dataContext.WorkFlowTrailEntities.Where(x => x.OperationId == operationId
                                                                            && x.TargetId == targetId
                                                                            && x.StatusId != WorkFLowStatusEntity.Completed)
                                                                          .Select(x => x.RequestStaffId)
                                                                          .SingleOrDefault();
            //check if approval status Id is not rejected, nor referred or sumthing

            if(nextLevel != null)
            {
                var workFlowTrailEntity = new WorkFlowTrailEntity
                {
                    OperationId = operationId,
                    TargetId = targetId,
                    ToLevelId = nextLevel.ApprovalLevelId,
                    FromStaffId = fromStaffId,
                    ToStaffId = toStaffId,
                    RequestStaffId = requestStaffIdOnTrail,
                    ApprovalStatusId = approvalStatusId,
                    StatusId = WorkFLowStatusEntity.Ongoing,
                    Comment = comment
                };
            }

            //check what would happen if i 


            
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
