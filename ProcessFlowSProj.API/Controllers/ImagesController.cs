using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessFlowSProj.API.Dtos;
using ProcessFlowSProj.API.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesRepository _imageRepository;
        private readonly IProjectRepository _projectRepo;

        public ImagesController(IImagesRepository imageRepo, IProjectRepository projectRepo)
        {
            _imageRepository = imageRepo;
            _projectRepo = projectRepo;
        }


        [HttpGet("project/{projectId}", Name = "GetImageForProject")]
        public async Task<IActionResult> GetImageByProject(int projectId)
        {
            try
            {
                var result = await _projectRepo.CheckIfProjectHasImage(projectId);

                if (!result)
                    return BadRequest("Project does not have any image");

                var imageFromRepo = _imageRepository.GetAllImageByProject(projectId);

                return Ok(imageFromRepo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpGet("{imageId}", Name = "GetImage")]
        public async Task<IActionResult> GetImage(int imageId)
        {
            try
            {
                var image = await _imageRepository.GetImageById(imageId);

                if (image == null)
                    return BadRequest("Image does not exist");

                return Ok(image);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost("project/{projectId}")]
        public async Task<IActionResult> AddImageToProject(int projectId, [FromForm]ImageForCreationDto imageForCreation)
        {
            try
            {
                var check = await _projectRepo.CheckIfProjectExists(projectId);

                if (!check)
                    return BadRequest("Invalid Project");

                var image = await _imageRepository.AddImageToProject(imageForCreation, projectId);

                if (image == null)
                    return BadRequest("Could not add Image");

                return CreatedAtRoute("GetImage", new { imageId = image.ImageId }, image);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpDelete()]
        public async Task<IActionResult> RemoveImage(int projectId, int imageId)
        {
            var check = await _projectRepo.CheckIfProjectHasSpecificImage(imageId, projectId);

            if (!check)
                return BadRequest("Project does not contain this Image");

            return Ok();
        }




    }
}
