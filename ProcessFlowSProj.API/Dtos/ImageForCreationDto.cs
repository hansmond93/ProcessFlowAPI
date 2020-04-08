using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class ImageForCreationDto
    {
        public IFormFile File { get; set; }
        public string Description { get; set; }
    }

}
