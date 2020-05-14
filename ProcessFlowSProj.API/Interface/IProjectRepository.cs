using ProcessFlowSProj.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Interface
{
    public interface IProjectRepository
    {
        Task<IEnumerable<GetProjectDto>> GetAllProjects();

        Task<IEnumerable<GetProjectDto>> GetALlProjectByStaffId(int staffId);

        Task<GetProjectDto> GetProjectById(int projectId);

        Task<GetProjectDto> SaveProject(ProjectForCreationDto project);

        Task<GetProjectDto> ModifyProject(ProjectForUpdateDto project, int projectId);

        Task<bool> CheckIfProjectHasImage(int projectId);

        Task<bool> CheckIfProjectExists(int projectId);

        Task<bool> CheckIfProjectHasSpecificImage(int imageId, int projectId);
        Task<IEnumerable<GetProjectApprovalDto>> GetPendingProjectApprovals(int staffId, int operationId);
        Task<DashboardInfoDto> GetDashboardInfoByStaffId(int id);
    }
}
