using Microsoft.EntityFrameworkCore;
using ProcessFlowSProj.API.Common;
using ProcessFlowSProj.API.Data;
using ProcessFlowSProj.API.Dtos;
using ProcessFlowSProj.API.Helpers.Timing;
using ProcessFlowSProj.API.Interface;
using Services.EmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class WorkFlow : IWorkFlow
    {
        private readonly DataContext _dataContext;
        private readonly IEmailService _emailService;

        public WorkFlow(DataContext dataContext, IEmailService emailService)
        {
            _dataContext = dataContext ?? throw new ArgumentException("Dependency Injection Failed");
            _emailService = emailService ?? throw new ArgumentException("Dependency Injection Failed");
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
        private int? _nextLevelRoleId;
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

            //check if there is a similar request in the db that is still ongoing
            var checkSimilarRequest = _dataContext.WorkFlowTrailEntities.Where(x => x.OperationId == operationId &&
                                                                                    x.TargetId == targetId &&
                                                                                    x.StatusId != WorkFLowStatusEntity.Completed)
                                                                        .Any();

            if (checkSimilarRequest) throw new CustomException("This Operation is currently Undergoing approval");

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
                Comment = _defaultComment,
                DateTimeApproved = Clock.Now
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

            var toStaff = _dataContext.StaffEntities.Where(x => x.StaffId == toStaffId).SingleOrDefault();
            var fromStaff = _dataContext.StaffEntities.Where(x => x.StaffId == fromStaffId).SingleOrDefault();


            _nextLevelRoleId = level.RoleId;
            _nextLevelRoleName = _dataContext.StaffRoleEntities.SingleOrDefault(x => x.RoleId == level.RoleId).RoleName;
            _nextLevelFirstname = toStaff.FirstName;
            _nextLevelLastname = toStaff.LastName;
            _nextLevelMiddlename = toStaff.MiddleName ?? "";
            _emailService.SendEmail(fromStaff.EmailAddress, "Approval Notification", $"Dear {fromStaff.FirstName}\n Your request has been successfully sent to {toStaff.FirstName} {toStaff.LastName} for approval. ");
            _emailService.SendEmail(toStaff.EmailAddress, "Approval Notification", $"Dear {toStaff.FirstName}\n Your have an Approval Request from {fromStaff.FirstName} {fromStaff.LastName}, please kindly review. ");


        }

        public void ProcessApproval(int operationId, int targetId, int? toStaffId, int fromStaffId, string comment, byte approvalStatusId)
        {
            if(toStaffId == null)   //liklely A final Approval
            {
                //liklely A final Approval
                ProcessFinalApproval(operationId, targetId, fromStaffId, comment, approvalStatusId);
                return;
            }

            if(approvalStatusId == ApprovalStatusEntity.Referred)   //check for referred approval status
            {
                ProcessReferredApproval(operationId, targetId, toStaffId, fromStaffId, comment, approvalStatusId);
                return;

            }

            if (approvalStatusId == ApprovalStatusEntity.Rejected)   //check for referred approval status
            {
                //think about if this request was referred back to this current user and he decides to reject even though it is unlikley, do not give chance to reject if its a referred request
                ProcessRejectedApproval(operationId, targetId, toStaffId, fromStaffId, comment, approvalStatusId);
                return;
            }

            var roleOfToStaffId = _dataContext.StaffEntities.SingleOrDefault(x => x.StaffId == toStaffId).RoleId;

            if (roleOfToStaffId <= 0) throw new CustomException("An Error Occurred, Please Contact the System Administrator");

            //var levelOfToStaffId = _dataContext.ApprovalLevelEntities.SingleOrDefault(x => x.RoleId == roleOfToStaffId);

            //check if this request was not referred to this current staff that is making this request by approving it
            var levelsss = _dataContext.WorkFlowTrailEntities.Where(x => x.OperationId == operationId &&
                                                                         x.TargetId == targetId &&
                                                                         x.StatusId != WorkFLowStatusEntity.Completed)
                                                             .Select(x => new { x.FromStaffId, x.ApprovalStatusId, x.WorkFlowTrailId})
                                                             .OrderByDescending(x => x.WorkFlowTrailId)
                                                             .ToArray();
            //this query takes time while debugging, maybe cuz am selecting into an anonyous type
            if(levelsss[0].ApprovalStatusId ==ApprovalStatusEntity.Referred)
            {
                //once you are replying a referred request, u must automatically approve it.
                if(approvalStatusId != ApprovalStatusEntity.Approved) throw new CustomException("An Error Occurred, Please Contact the System Administrator");

                //this means that this request was referred to the current user 
                //and he has to send it back to the staff that referred it to him and not follow the normal setup check
                var requestStafIdOnTrail = _dataContext.WorkFlowTrailEntities.Where(x => x.OperationId == operationId
                                                                            && x.TargetId == targetId
                                                                            && x.StatusId != WorkFLowStatusEntity.Completed)
                                                                          .Select(x => x.RequestStaffId)
                                                                          .FirstOrDefault();

                var workFlowTrailEntity = new WorkFlowTrailEntity
                {
                    OperationId = operationId,
                    TargetId = targetId,
                    //ToLevelId = , we are not putting it because it is a request that is being sent to the referer
                    FromStaffId = fromStaffId,
                    ToStaffId = levelsss[0].FromStaffId,
                    RequestStaffId = requestStafIdOnTrail,
                    ApprovalStatusId = ApprovalStatusEntity.Procesing,  //we pick this cuz at this point the request can only be an approved request
                    StatusId = WorkFLowStatusEntity.Ongoing,
                    Comment = comment
                };

                _dataContext.WorkFlowTrailEntities.Add(workFlowTrailEntity);
                _dataContext.SaveChanges();
                //Send mails here

                var staff = _dataContext.StaffEntities.FirstOrDefault(s => s.StaffId == levelsss[0].FromStaffId);

                _nextLevelRoleId = staff.RoleId;
                _nextLevelRoleName = _dataContext.StaffRoleEntities.FirstOrDefault(r => r.RoleId == staff.RoleId).RoleName;
                _nextLevelFirstname = staff.FirstName;
                _nextLevelMiddlename = staff.MiddleName;
                _nextLevelLastname = staff.LastName;

                return;
            }

            var approvalLevels = _dataContext.ApprovalLevelEntities.Where(x => x.OperationId == operationId && x.Active == true)
                                                                   .OrderBy(x => x.Position)
                                                                   .ToList();

            ApprovalLevelEntity nextLevel = null;
            ApprovalLevelEntity previousLevel = null;

            for (int i = 0; i < approvalLevels.Count(); i++)
            {
                if(approvalLevels[i].RoleId == roleOfToStaffId)
                {
                    nextLevel = approvalLevels[i];          //check if i++ is evaluated first before the array is called
                    previousLevel = approvalLevels[--i];    //check if this truly picks the previous level
                       
                    //more work whe  the role can appear more than once
                    break;
                }
            }

            if(nextLevel == null)
            {
                throw new CustomException("An Error Occurred, Please Contact the System Administrator");
            }

            var requestStaffIdOnTrail = _dataContext.WorkFlowTrailEntities.Where(x => x.OperationId == operationId
                                                                            && x.TargetId == targetId
                                                                            && x.StatusId != WorkFLowStatusEntity.Completed)
                                                                          .Select(x => x.RequestStaffId)
                                                                          .FirstOrDefault();
            //check if approval status Id is not rejected, nor referred or sumthing

            if(nextLevel != null)
            {
                var workFlowTrailEntity = new WorkFlowTrailEntity
                {
                    OperationId = operationId,
                    TargetId = targetId,
                    ToLevelId = nextLevel.ApprovalLevelId,
                    FromStaffId = fromStaffId,
                    FromLevelId = previousLevel.ApprovalLevelId,
                    ToStaffId = toStaffId,
                    RequestStaffId = requestStaffIdOnTrail,
                    ApprovalStatusId = ApprovalStatusEntity.Procesing,
                    StatusId = WorkFLowStatusEntity.Ongoing,
                    Comment = comment,
                    DateTimeApproved = Clock.Now
                };

                _dataContext.WorkFlowTrailEntities.Add(workFlowTrailEntity);
                _dataContext.SaveChanges();
                //Send mails here

                var staff = _dataContext.StaffEntities.FirstOrDefault(s => s.StaffId == toStaffId);

                _nextLevelRoleId = staff.RoleId;
                _nextLevelRoleName = _dataContext.StaffRoleEntities.FirstOrDefault(r => r.RoleId == staff.RoleId).RoleName;
                _nextLevelFirstname = staff.FirstName;
                _nextLevelMiddlename = staff.MiddleName ?? String.Empty;
                _nextLevelLastname = staff.LastName;
            }
            
        }

        private void ProcessReferredApproval(int operationId, int targetId, int? toStaffId, int fromStaffId, string comment, byte approvalStatusId)
        {
            var roleOfRefferdStaffId = _dataContext.StaffEntities.SingleOrDefault(x => x.StaffId == toStaffId).RoleId;

            if (roleOfRefferdStaffId <= 0) throw new CustomException("An Error Occurred, Please Contact the System Administrator");


            //I can also check the setup table to be double sure the person that the task was referred to is in the set up
            var trailStaffIds = _dataContext.WorkFlowTrailEntities.Where(x => x.OperationId == operationId &&
                                                                                        x.TargetId == targetId &&
                                                                                        x.StatusId != WorkFLowStatusEntity.Completed)
                                                                            .Select(x => x.FromStaffId) //select the staffId of staffs that created requests in the DB
                                                                            .ToList();                 //i.e. staffs that previously worked on the request

            if (trailStaffIds.Contains(toStaffId)) //check if the referred back staffId is in the list of staffIds that have worked on the list before
            {
                var requestStafIdOnTrail = _dataContext.WorkFlowTrailEntities.Where(x => x.OperationId == operationId
                                                                        && x.TargetId == targetId
                                                                        && x.StatusId != WorkFLowStatusEntity.Completed)
                                                                      .Select(x => x.RequestStaffId)
                                                                      .FirstOrDefault();

                var workFlowTrailEntity = new WorkFlowTrailEntity
                {
                    OperationId = operationId,
                    TargetId = targetId,
                    //ToLevelId = nextLevel.ApprovalLevelId,    //we are not adding a level because it is been referred
                    FromStaffId = fromStaffId,
                    ToStaffId = toStaffId,
                    //FromLevelId =                             //we are dismissing levels when it comes to referred requests tho we can choose to use it 
                    RequestStaffId = requestStafIdOnTrail,
                    ApprovalStatusId = approvalStatusId,
                    StatusId = WorkFLowStatusEntity.Ongoing,
                    Comment = comment,
                    DateTimeApproved = Clock.Now
                };

                var staff = _dataContext.StaffEntities.FirstOrDefault(s => s.StaffId == toStaffId);
                _nextLevelRoleId = staff.RoleId;
                _nextLevelRoleName = _dataContext.StaffRoleEntities.FirstOrDefault(r => r.RoleId == staff.RoleId).RoleName;
                _nextLevelFirstname = staff.FirstName;
                _nextLevelMiddlename = staff.MiddleName ?? String.Empty;
                _nextLevelLastname = staff.LastName;

            }
            else
            {
                throw new CustomException("An Error Occurred, Please Contact the System Administrator");
            }
        }

        private void ProcessRejectedApproval(int operationId, int targetId, int? toStaffId, int fromStaffId, string comment, byte approvalStatusId)
        {
            //check for the staffId the this request was actually sent to
            var toStaffIdFromWorkflowTrail = _dataContext.WorkFlowTrailEntities.Where(x => x.OperationId == operationId &&
                                                                                   x.TargetId == targetId &&
                                                                                   x.StatusId != WorkFLowStatusEntity.Completed)
                                                                             .OrderByDescending(x => x.WorkFlowTrailId)
                                                                             .ToArray();
            if (toStaffIdFromWorkflowTrail == null)
            {
                throw new CustomException("An Error Occurred, Please Contact the System Administrator");
            }

            if (toStaffIdFromWorkflowTrail[0].ToStaffId == fromStaffId)    //check if this staffId matches the current staffId that is rejecting the request
            {
                var levelOfSender = (from ale in _dataContext.ApprovalLevelEntities
                                    join se in _dataContext.StaffEntities on ale.RoleId equals se.RoleId
                                    where se.StaffId == fromStaffId
                                    select ale.ApprovalLevelId).FirstOrDefault();

                var workFlowTrailEntity = new WorkFlowTrailEntity
                {
                    OperationId = operationId,
                    TargetId = targetId,
                    FromStaffId = fromStaffId,
                    ToStaffId = toStaffId,
                    RequestStaffId = toStaffIdFromWorkflowTrail[0].RequestStaffId,
                    ApprovalStatusId = approvalStatusId,
                    StatusId = WorkFLowStatusEntity.Completed,
                    Comment = comment,
                    FromLevelId = levelOfSender,
                    DateTimeApproved = Clock.Now
                };

                _dataContext.WorkFlowTrailEntities.Add(workFlowTrailEntity);
                _dataContext.SaveChanges();

                _nextLevelRoleId = null;
                _nextLevelRoleName = "n/a";
                _nextLevelFirstname = "n/a";
                _nextLevelMiddlename = "n/a";
                _nextLevelLastname = "n/a";
                _approvalStatusId = approvalStatusId;
            }
            else
            {
                throw new CustomException("An Error Occurred, Please Contact the System Administrator");
            }
        }

        private void ProcessFinalApproval(int operationId, int targetId, int fromStaffId, string comment, byte approvalStatusId)
        {
            var roleOfFromStaffId = _dataContext.StaffEntities.SingleOrDefault(x => x.StaffId == fromStaffId).RoleId;   //get role of the fromStaffId

            if (roleOfFromStaffId <= 0) throw new CustomException("An Error Occurred, Please Contact the System Administrator");

            var approvaLevels = _dataContext.ApprovalLevelEntities.Where(x => x.OperationId == operationId && x.Active == true)
                                                               .OrderByDescending(x => x.Position)
                                                               .ToArray();

            if (approvaLevels[0].RoleId == roleOfFromStaffId)    //if the roleId of the fromStaffId matches the role of the final approval in the setup
            {
                var requestStafIdOnTrail = _dataContext.WorkFlowTrailEntities.Where(x => x.OperationId == operationId
                                                                        && x.TargetId == targetId
                                                                        && x.StatusId != WorkFLowStatusEntity.Completed)
                                                                      .Select(x => x.RequestStaffId)
                                                                      .FirstOrDefault();

                var workFlowTrailEntity = new WorkFlowTrailEntity
                {
                    OperationId = operationId,
                    TargetId = targetId,
                    RequestStaffId = requestStafIdOnTrail,
                    ApprovalStatusId = approvalStatusId,
                    StatusId = WorkFLowStatusEntity.Completed,
                    FromLevelId = approvaLevels[1].ApprovalLevelId, //level of the previous role on the setup
                    Comment = comment,
                    DateTimeApproved = Clock.Now,
                    ApprovedByStaffId = fromStaffId
                };

                _dataContext.WorkFlowTrailEntities.Add(workFlowTrailEntity);
                _dataContext.SaveChanges();
                //can probably send a mail here to the request staff id here

                _nextLevelRoleId = null;
                _nextLevelRoleName = "n/a";
                _nextLevelFirstname = "n/a";
                _nextLevelLastname = "n/a";
                _nextLevelMiddlename = "n/a";
                _approvalStatusId = approvalStatusId;

            }
            else
            {
                throw new CustomException("An Error Occurred, Contact the System Admin");
            }
        }

        public IEnumerable<StaffForApprovalDto> GetNames(int operationId, int staffId, int? targetId = null)
        {
            if(targetId == null)    //this means that it is a first request and it is meant to go to the first person on the setup table
            {
                var data = _dataContext.ApprovalLevelEntities.Where(x => x.Active != false &&
                                                                         x.OperationId == operationId)
                                                             .OrderBy(x => x.Position)
                                                             .ToArray();

                if (data == null) throw new CustomException("Operation has not been setup");

                var staffs = (from se in _dataContext.StaffEntities
                             join sre in _dataContext.StaffRoleEntities on se.RoleId equals sre.RoleId
                             where se.RoleId == data[0].RoleId
                             select new StaffForApprovalDto
                             {
                                 StaffId = se.StaffId,
                                 StaffName = se.FirstName + " " +se.LastName,
                                 StaffRole = sre.RoleName

                             }).AsEnumerable();

                return staffs;
            }
            else //this means that the request is currently undergoing approval so we check from the workflow table
            {

                var result = checkIfItsFinalApproval(operationId, staffId, targetId);
                if (result)
                    return null;    //if result == null then we know at the controller it's a final Approval

                var trailData = _dataContext.WorkFlowTrailEntities.Where(x => x.OperationId == operationId &&
                                                                         x.TargetId == targetId &&
                                                                         x.StatusId != WorkFLowStatusEntity.Completed)
                                                                   .OrderByDescending(x => x.WorkFlowTrailId)
                                                                   .ToArray();

                if (trailData == null) throw new CustomException("An Error Occurred, Please Contact the System Admin");

                //check if the last request was referred on the trail table
                if(trailData[0].ApprovalStatusId == ApprovalStatusEntity.Referred)
                {
                    var staff = (from se in _dataContext.StaffEntities
                                 join sre in _dataContext.StaffRoleEntities on se.RoleId equals sre.RoleId
                                 where se.StaffId == trailData[0].FromStaffId
                                 select new StaffForApprovalDto
                                 {
                                     StaffId = se.StaffId,
                                     StaffName = se.FirstName + se.LastName,
                                     StaffRole = sre.RoleName

                                 }).AsEnumerable();

                    return staff;
                }

                //I can also do more checks to be sure by using the staffId that was passed in

                var levelOfCurrentUser = trailData[0].ToLevelId;

                var setupData = _dataContext.ApprovalLevelEntities.Where(x => x.OperationId == operationId &&
                                                                              x.Active != false)
                                                                  .OrderBy(x => x.Position)
                                                                  .ToArray();
                ApprovalLevelEntity nextlevel = null;

                for(int i = 0; i < setupData.Length; i++)
                {
                    if (setupData[i].ApprovalLevelId == levelOfCurrentUser)
                    {
                        var j = i + 1;
                        nextlevel = setupData[j];
                        break;
                    }
                }

                var staffsEnumerable = (from se in _dataContext.StaffEntities
                                         join sre in _dataContext.StaffRoleEntities on se.RoleId equals sre.RoleId
                                         join ale in _dataContext.ApprovalLevelEntities on se.RoleId equals ale.RoleId
                                         where se.RoleId == nextlevel.RoleId
                                         select new StaffForApprovalDto
                                         {
                                             StaffId = se.StaffId,
                                             StaffName = se.FirstName + " " + se.LastName,
                                             StaffRole = sre.RoleName

                                         }).AsEnumerable();

                return staffsEnumerable;

            }
        }

        public bool checkIfItsFinalApproval(int operationId, int staffId, int? targetId)
        {
            //throw new NotImplementedException();

            var roleIdOfStaffId = (from se in _dataContext.StaffEntities
                                   join sre in _dataContext.StaffRoleEntities on se.RoleId equals sre.RoleId
                                   where se.StaffId == staffId
                                   select new
                                   {
                                       sre.RoleId
                                   }).SingleOrDefault();

            if (roleIdOfStaffId == null) throw new CustomException("An Error Occurred, Please Contact the System Admin");

            var setUpData = _dataContext.ApprovalLevelEntities.Where(x => x.OperationId == operationId && x.Active != false)
                                                              .OrderByDescending(x => x.Position)
                                                              .ToArray();

            if (setUpData == null) throw new CustomException("Operation has not ben setup");

            if (setUpData[0].RoleId == roleIdOfStaffId.RoleId) 
                return true;

            return false;
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

        public WorkFlowResponseDto GetWorkFlowOutput()
        {
            if(_nextLevelRoleId == null)
            {
                if(_approvalStatusId > 0)
                {
                    var output = new WorkFlowResponseDto
                    {
                        ApprovalStatusName = ReturnApprovalStatusAsString(_approvalStatusId)
                    };
                    return output;
                }
            }

            var response = new WorkFlowResponseDto
            {
                RoleName = _dataContext.StaffRoleEntities.SingleOrDefault(x => x.RoleId == _nextLevelRoleId).RoleName,
                RoleId = _nextLevelRoleId.Value,
                FullName = _nextLevelFirstname + " " + _nextLevelMiddlename + " " + _nextLevelLastname,
                //Add staff Id later
            };

            return response;
        }

    }
}
