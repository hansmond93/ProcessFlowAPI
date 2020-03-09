using ProcessFlowSProj.API.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class StaffEntity : StaffBaseEntity
    {
        [Key]
        public int StaffId { get; set; }

        [ForeignKey("StaffLoginEntity")]
        public int? StaffLoginEntityId { get; set; }

        public StaffLoginEntity StaffLoginEntity { get; set; }

        [ForeignKey("StaffRoleEntity")]
        public int RoleId { get; set; }

        public StaffRoleEntity StaffRoleEntity { get; set; }
        
    }
}
