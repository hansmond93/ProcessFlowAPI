using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class GetProjectDto
    {
        public int ProjectId { get; set; }

        public string ProjectTitle { get; set; }

        public string Description { get; set; }

        public int DurationInMonths { get; set; }

        public decimal ProposedAmount { get; set; }

        public decimal? ApprovedAmount { get; set; }

        public string Location { get; set; }

        public string ContactPerson { get; set; }

        public string ContactNumber { get; set; }

        public string ContactAddress { get; set; }

        public string CompanyName { get; set; }

        public string CompanyEmail { get; set; }

        public int ApprovalStatusId { get; set; }
    }
}
