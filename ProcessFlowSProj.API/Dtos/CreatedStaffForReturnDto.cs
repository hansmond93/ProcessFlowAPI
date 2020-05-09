using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class CreatedStaffForReturnDto
    {
        public int Id { get; set; }

        public int RoleEntityId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Gender { get; set; }

        public string StaffCode { get; set; }

        public string Email { get; set; }
    }
}
