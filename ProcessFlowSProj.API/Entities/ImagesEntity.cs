using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class ImagesEntity
    {
        [Key]
        public int ImageId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string PublicId { get; set; }
        public DateTime DateTimeAdded { get; set; }

        [ForeignKey("ProjectEntity")]
        public int ProjectId { get; set; }
        public ProjectEntity ProjectEntity { get; set; }
    }
}
