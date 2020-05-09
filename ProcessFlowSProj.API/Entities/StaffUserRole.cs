using Microsoft.AspNetCore.Identity;

namespace ProcessFlowSProj.API.Entities
{
    public class StaffUserRole : IdentityUserRole<int>
    {
        public StaffEntity StaffEntity { get; set; }
        public Role Role { get; set; }
    }
}
