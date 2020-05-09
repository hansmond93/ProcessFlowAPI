using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class RoleEntity 
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleCode { get; set; }

        public ICollection<StaffEntity> StaffEntities{ get; set; }

    }
}
