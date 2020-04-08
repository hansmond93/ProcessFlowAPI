using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProcessFlowSProj.API.Data;
using ProcessFlowSProj.API.Dtos;
using ProcessFlowSProj.API.Entities;
using ProcessFlowSProj.API.Helpers;
using ProcessFlowSProj.API.Helpers.Timing;
using ProcessFlowSProj.API.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Repository
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IProjectRepository _projectRepo;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly Cloudinary _cloudinary;

        public ImagesRepository(IMapper mapper, DataContext context, IOptions<CloudinarySettings> cloudinaryConfig, IProjectRepository projectRepo)
        {
            _mapper = mapper;
            _context = context;
            _projectRepo = projectRepo;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account
            (
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<IEnumerable<GetImageDto>> GetAllImageByProject(int projectId)
        {
            var image = await _context.ProjectEntities.Include(x => x.ImagesEntities).Where(x => x.ProjectId == projectId).Select(x => x.ImagesEntities).ToListAsync();

            var imageToReturn = _mapper.Map<IEnumerable<GetImageDto>>(image);

            return imageToReturn;
        }

        public async Task<GetImageDto> GetImageById(int imageId)
        {
            var image = await _context.ImagesEntities.FirstOrDefaultAsync(x => x.ImageId == imageId);

            var imageToReturn = _mapper.Map<GetImageDto>(image);

            return imageToReturn;
        }

        public async Task<GetImageDto> AddImageToProject(ImageForCreationDto imageForCreation, int projectId)
        {
            var file = imageForCreation.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)

                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            var image = _mapper.Map<ImagesEntity>(imageForCreation);

            image.Url = uploadResult.Uri.ToString();
            image.PublicId = uploadResult.PublicId;
            image.DateTimeAdded = Clock.Now;
            image.ProjectId = projectId;

            await _context.ImagesEntities.AddAsync(image);
            await _context.SaveChangesAsync();

            var imageToReturn = _mapper.Map<GetImageDto>(image);

            return imageToReturn;
        }

        public async Task DeleteImage(int imageId)
        {
            var imageToDelete = await _context.ImagesEntities.FirstOrDefaultAsync(x => x.ImageId == imageId);

            var deleteParams = new DeletionParams(imageToDelete.PublicId);

            var result = _cloudinary.Destroy(deleteParams);

            if (result.Result == "ok")
            {
                _context.ImagesEntities.Remove(imageToDelete);
            }

            await _context.SaveChangesAsync();
        }

    }
}
