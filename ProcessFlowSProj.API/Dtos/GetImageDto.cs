﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class GetImageDto
    {
        public int ImageId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

        public string PublicId { get; set; }
    }
}
