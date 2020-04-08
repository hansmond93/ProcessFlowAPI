using AutoMapper;
using ProcessFlowSProj.API.Dtos;
using ProcessFlowSProj.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProjectEntity, GetProjectDto>();
            CreateMap<ProjectForCreationDto, ProjectEntity>();
            CreateMap<ImagesEntity, GetImageDto>();
            CreateMap<ImageForCreationDto, ImagesEntity>();
            CreateMap<ApprovalLevelForEditDto, ApprovalLevelEntity>()
                .ForMember(dest => dest.ApprovalLevelId, option =>
                {
                    option.Ignore();
                });
            CreateMap<ApprovalLevelEntity, GetDetailedApprovalLevelDto>();


        }
        
    }
}
