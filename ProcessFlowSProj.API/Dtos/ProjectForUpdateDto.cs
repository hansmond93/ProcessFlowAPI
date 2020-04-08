using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class ProjectForUpdateDto
    {
        [Required(ErrorMessage = "ProjectTitle is Required")]
        public string ProjectTitle { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "DurationInMonths is Required")]
        public int DurationInMonths { get; set; }

        [Required(ErrorMessage = "ProposedAmount is Required")]
        public decimal ProposedAmount { get; set; }

        public decimal? ApprovedAmount { get; set; }

        [Required(ErrorMessage = "Location is Required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Contact Person is Required")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Contact Number is Required")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Contact Address is Required")]
        public string ContactAddress { get; set; }

        [Required(ErrorMessage = "Company Name is Required")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Company Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CompanyEmail { get; set; }
    }
}
