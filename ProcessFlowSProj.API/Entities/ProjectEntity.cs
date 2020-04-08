using ProcessFlowSProj.API.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class ProjectEntity : AuditedEntity
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        public string ProjectTitle { get; set; }
        [Required]
        public string Description { get; set; }

        public int DurationInMonths { get; set; }
        public decimal ProposedAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string ContactPerson { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public string ContactAddress { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        [EmailAddress]
        public string CompanyEmail { get; set; }

        public ICollection<ImagesEntity> ImagesEntities { get; set; }


    }
}
