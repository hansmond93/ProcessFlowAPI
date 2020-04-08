using ProcessFlowSProj.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Interface
{
    public interface IImagesRepository
    {
        Task<IEnumerable<GetImageDto>> GetAllImageByProject(int projectId);
        Task<GetImageDto> GetImageById(int imageId);
        Task<GetImageDto> AddImageToProject(ImageForCreationDto imageForCreation, int projectId);
        Task DeleteImage(int imageId);
    }
}
