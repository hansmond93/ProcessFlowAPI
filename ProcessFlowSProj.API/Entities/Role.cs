using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<StaffUserRole> StaffUserRoles { get; set; }
    }
}
