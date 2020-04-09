using ProcessFlowSProj.API.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class StaffEntity
    {
        [Key]
        public int StaffId { get; set; }

        [ForeignKey("StaffLoginEntity")]
        public int? StaffLoginEntityId { get; set; }

        public StaffLoginEntity StaffLoginEntity { get; set; }

        [ForeignKey("StaffRoleEntity")]
        public int RoleId { get; set; }

        public StaffRoleEntity StaffRoleEntity { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string Username { get; set; }


        [Required]
        public string Gender { get; set; }

        public string StaffCode { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }



    }
}
