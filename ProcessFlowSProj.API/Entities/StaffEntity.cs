﻿using Microsoft.AspNetCore.Identity;
using ProcessFlowSProj.API.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class StaffEntity : IdentityUser<int>
    {
        [Key]
        public override int Id { get; set; }

        public int RoleEntityId { get; set; }
        public RoleEntity RoleEntity { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string Gender { get; set; }

        public string StaffCode { get; set; }

        [EmailAddress]
        public override string Email { get; set; }

        public ICollection<StaffUserRole> StaffUserRoles { get; set; }

        public WorkFlowTrailEntity WorkFlowTrailEntityApprovedBy { get; set; }
        public WorkFlowTrailEntity WorkFlowTrailEntityRequestStaff { get; set; }
        public WorkFlowTrailEntity WorkFlowTrailEntityFromStaff { get; set; }
        public WorkFlowTrailEntity WorkFlowTrailEntityToStaff { get; set; }



    }
}
