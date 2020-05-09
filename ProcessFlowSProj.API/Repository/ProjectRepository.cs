using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProcessFlowSProj.API.Common;
using ProcessFlowSProj.API.Data;
using ProcessFlowSProj.API.Dtos;
using ProcessFlowSProj.API.Entities;
using ProcessFlowSProj.API.Helpers.Timing;
using ProcessFlowSProj.API.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenDecryptionHelper _tokenHelper;
        private readonly IWorkFlow _workFlow;



        public ProjectRepository(DataContext context, IMapper mapper, ITokenDecryptionHelper tokenHelper, IWorkFlow workFlow)
        {
            _context = context;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
            _workFlow = workFlow;
        }

        public async Task<IEnumerable<GetProjectDto>> GetAllProjects()
        {
            int staffId = _tokenHelper.GetStaffId();    //test StaffId method

            var projects = await _context.ProjectEntities.Where(x => x.IsDeleted != true).ToListAsync();

            var projectsToReturn = _mapper.Map<IEnumerable<GetProjectDto>>(projects);

            return projectsToReturn;
        }

        public async Task<IEnumerable<GetProjectDto>> GetALlProjectByStaffId(int staffId)
        {
            //test staffId in controller to see if it matches the token staffId
            var projects = await _context.ProjectEntities.Where(x => x.IsDeleted != true && x.CreatedBy == staffId).ToListAsync();

            var projectsToReturn = _mapper.Map<IEnumerable<GetProjectDto>>(projects);

            return projectsToReturn;
        }

        public async Task<GetProjectDto> GetProjectById(int projectId)
        {
            var project =  await _context.ProjectEntities.Where(x => x.IsDeleted != true && x.ProjectId == projectId).SingleOrDefaultAsync();

            var projectToReturn = _mapper.Map<GetProjectDto>(project);

            return projectToReturn;
        }

        public async Task<GetProjectDto> SaveProject(ProjectForCreationDto project)
        {
            var projectToBeCreated = _mapper.Map<ProjectEntity>(project);

            projectToBeCreated.DateTimeCreated = Clock.Now;
            projectToBeCreated.CreatedBy = _tokenHelper.GetStaffId();   //Get this staffId from httpContext__checkTimeTracker
            projectToBeCreated.IsDeleted = false;

            await _context.ProjectEntities.AddAsync(projectToBeCreated);

            await _context.SaveChangesAsync();

            var savedProject = _mapper.Map<GetProjectDto>(projectToBeCreated);  //check if this contains the projectId

            return savedProject;

        }

        public async Task<GetProjectDto> ModifyProject(ProjectForUpdateDto project, int projectId)
        {
            var projectFromDb = await _context.ProjectEntities.SingleOrDefaultAsync(x => x.IsDeleted != true && x.ProjectId == projectId);

            if (projectFromDb == null) throw new CustomException("Invalid Project Id");

            _mapper.Map(project, projectFromDb);

            projectFromDb.LastModifiedBy = 4; //Get this staffId from httpContext__checkTimeTracker
            projectFromDb.DateTimeModified = Clock.Now;

            await _context.SaveChangesAsync();

            var updatedProject = _mapper.Map<GetProjectDto>(projectFromDb);

            return updatedProject;

        }

        public async Task<bool> CheckIfProjectHasImage(int projectId)
        {
            bool result = await _context.ProjectEntities.Include(p => p.ImagesEntities).Where(x => x.ProjectId == projectId).AnyAsync(x=> x.ImagesEntities.Count > 0);

            if (result)
                return true;

            return false;
        }

        public async Task<bool> CheckIfProjectExists(int projectId)
        {
            bool result = await _context.ProjectEntities.AnyAsync(x => x.ProjectId == projectId);

            if (result)
                return true;

            return false;
        }

        public async Task<bool> CheckIfProjectHasSpecificImage(int imageId, int projectId)
        {
            bool result = await _context.ProjectEntities.Include(p => p.ImagesEntities).Where(x => x.ProjectId == projectId)
                                                                                       .AnyAsync(x => x.ImagesEntities.FirstOrDefault(f => f.ImageId == imageId).ImageId == imageId);
            
            if (result)
                return true;

            return false;
        }

        public IEnumerable<StaffForApprovalDto> GetApprovalLevelDetails(ApprovalLevelDetailDto approvalLevel)
        {
            var targetId = approvalLevel.TargetId == 0 ? null : approvalLevel.TargetId;

            var result = _workFlow.GetNames(approvalLevel.OperationId, approvalLevel.StaffId, targetId);

            return result;
        }

        public void GoForApproval(ProjectForApprovalDto project)
        {
            //check if peoject exists and has Image in the controller
            //check all the fromStaffId to see if request matches
            _workFlow.GoForApproval( project.OperationId, project.ProjectId, project.ToStaffId, project.FromStaffId);

        }

        public async Task<IEnumerable<GetProjectApprovalDto>> GetPendingProjectApprovals(int staffId, int operationId)
        {
            //var ProjectIds = new List<int>();
            var approvals = new List<GetProjectApprovalDto>();

            var list = await _context.WorkFlowTrailEntities.Where(s => s.OperationId == operationId).GroupBy(x => x.TargetId).ToListAsync();


            foreach(var grouping in list)
            {
                var toStaffId = grouping.Any(x => x.ToStaffId == staffId && x.ApprovalStatusId != ApprovalStatusEntity.Referred);
                var fromStaffId = grouping.Any(x => x.FromStaffId == staffId && x.ToLevelId != null );  //when u reply a referred request we use null for the levels...can modify this when i add workflow approval status
                var finalStaffId = grouping.Any(x => x.ApprovedByStaffId == staffId && x.StatusId == WorkFLowStatusEntity.Completed);     //this will make sure it's not a final request with the staffId as the final staff

                if(toStaffId == true && fromStaffId == false && finalStaffId == false)
                {
                    var approval = await (from wte in _context.WorkFlowTrailEntities
                                          join pe in _context.ProjectEntities on wte.TargetId equals pe.ProjectId
                                          where wte.OperationId == operationId && grouping.Key  ==  pe.ProjectId && wte.ToStaffId == staffId
                                          select new GetProjectApprovalDto
                                          {
                                              ProjectId = pe.ProjectId,
                                              ProjectTitle = pe.ProjectTitle,
                                              Description = pe.Description,
                                              DurationInMonths = pe.DurationInMonths,
                                              ProposedAmount = pe.ProposedAmount,
                                              Location = pe.Location,

                                              Comment = wte.Comment,
                                              FromStaffId = wte.FromStaffId.Value,
                                              RequestStaffId = wte.RequestStaffId,
                                              RequestStaffName = _context.StaffEntities.Where(s => s.Id == wte.RequestStaffId).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault(),
                                              FromStaffName = _context.StaffEntities.Where(s => s.Id == wte.FromStaffId.Value).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault(),
                                              DateApprovedByPreviousLevel = wte.DateTimeApproved


                                          }).FirstOrDefaultAsync();  //this implementation will only work for now when we dont allow same perosn on the Workflow twice

                    approvals.Add(approval);
                }
            }

            return approvals;   //see what it returns when there is nothing to return
        }
    } 
}
