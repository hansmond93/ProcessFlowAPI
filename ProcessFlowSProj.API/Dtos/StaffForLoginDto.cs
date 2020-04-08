using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class StaffForLoginDto
    {
        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 16 characters")]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }
        
        
    }
}
