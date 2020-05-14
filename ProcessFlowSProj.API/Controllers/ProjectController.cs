using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessFlowSProj.API.Common;
using ProcessFlowSProj.API.Dtos;
using ProcessFlowSProj.API.Entities;
using ProcessFlowSProj.API.Interface;

namespace ProcessFlowSProj.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IWorkFlow _workFlow;
        private readonly IProjectRepository _projectRepo;
        private readonly ITokenDecryptionHelper _token;

        public ProjectController(IWorkFlow workFlow, IProjectRepository projectRepo, ITokenDecryptionHelper token)
        {
            _workFlow = workFlow;
            _projectRepo = projectRepo;
            _token = token;
        }


        [HttpGet("dashboard/information/{staffId}")]
        public async Task<IActionResult> GetDashbordInformationByStaffId(int staffId)
        {
            if (_token.GetStaffId() != staffId)
                return Unauthorized();

            var data = await _projectRepo.GetDashboardInfoByStaffId(staffId);

            return Ok(data);
        }

        //api/test/goforapproval
        [HttpPost("saveProject/{staffId}")]
        public async Task<IActionResult> CreateProject(ProjectForCreationDto project, int staffId)
        {
            if (project == null)
                return BadRequest();

            if (_token.GetStaffId() != staffId)
                return Unauthorized();


            try
            {
                var result = await _projectRepo.SaveProject(project);

                return CreatedAtRoute("GetProject", new { result.ProjectId }, result);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet("getProject/{projectId}", Name = "GetProject")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            try
            {
                var projectFromRepo = await _projectRepo.GetProjectById(projectId);

                if (projectFromRepo == null)
                    return BadRequest("Project does not exist");

                return Ok(projectFromRepo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("{staffId}", Name = "GetProjectByStaffId")]
        public async Task<IActionResult> GetProjectByStaffId(int staffId)
        {
            try
            {
                var projectFromRepo = await _projectRepo.GetALlProjectByStaffId(staffId);

                return Ok(projectFromRepo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("editProject/{projectId}")]
        public async Task<IActionResult> UpdateProject(ProjectForUpdateDto project, int projectId)
        {
            try
            {
                var updatedProject = await _projectRepo.ModifyProject(project, projectId);

                return Ok(updatedProject); //confirm the status code that created at route returns

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        [HttpPost("goForApproval")]
        public async Task<IActionResult> SendProjectForApproval(ProjectForApprovalDto project)
        {
            try
            {

                var staffId = _token.GetStaffId();
                //check if from staffId matches the staffId from token
                if (project.FromStaffId != int.Parse(User.FindFirst( x => x.Type == "StaffId").Value))
                    return Unauthorized();

                //check if project exists and has image
                bool result = await _projectRepo.CheckIfProjectHasImage(project.ProjectId);

                if (!result)
                    return BadRequest("Project does not have an Image");

                _workFlow.GoForApproval(project.OperationId, project.ProjectId, project.ToStaffId, project.FromStaffId);
                var output = _workFlow.GetWorkFlowOutput();

                return Ok(output);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        
        [HttpPost("getApprovalLevel")]
        public IActionResult GetApprovalLevel(GetApprovalLevelDto getLevel)
        {
            try
            {
                var staffId = _token.GetStaffId();
                //check if from staffId matches the staffId from token
                if (getLevel.FromStaffId != int.Parse(User.FindFirst( x => x.Type == "StaffId").Value))
                    return Unauthorized();

                var targetId = getLevel.TargetId == 0 ? null : getLevel.TargetId;
                var output = _workFlow.GetNames(getLevel.OperationId, getLevel.FromStaffId, targetId);

                if (output == null)
                    return Ok("This is the Final Level");

                return Ok(output);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost("getPendingApproval")]
        public async Task<IActionResult> GetPendingApprovals(GetPendingApprovalDto getPendingApproval)
        {
            try
            {
                var staffId = _token.GetStaffId();

                if (staffId != getPendingApproval.StaffId)
                    return Unauthorized();

                var result = await _projectRepo.GetPendingProjectApprovals(getPendingApproval.StaffId, getPendingApproval.OperationId);
                if (result == null)
                    return Ok("No Pending Projects");

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("ProcessApproval")]
        public IActionResult ProcessProjectApproval(ProcessApprovalDto processApproval)
        {
            try
            {
                var staffId = _token.GetStaffId();

                if (staffId != processApproval.FromStaffId)
                    return Unauthorized();

                if (processApproval.ApprovalStatusId == ApprovalStatusEntity.Pending || processApproval.ApprovalStatusId == ApprovalStatusEntity.Procesing)
                    return BadRequest("Invalid Approval Request");

                _workFlow.ProcessApproval(processApproval.OperationId, processApproval.TargetId, processApproval.ToStaffId,
                                                                processApproval.FromStaffId, processApproval.Comment, processApproval.ApprovalStatusId);
                var result = _workFlow.GetWorkFlowOutput();

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}