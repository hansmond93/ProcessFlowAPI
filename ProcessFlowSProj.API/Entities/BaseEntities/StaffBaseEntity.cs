using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities.BaseEntities
{
    public abstract class StaffBaseEntity
    {
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
    }
}
