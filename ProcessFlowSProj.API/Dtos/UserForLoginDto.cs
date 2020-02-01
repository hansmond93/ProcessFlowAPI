using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Dtos
{
    public class UserForLoginDto
    {
        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 16 characters")]
        public string Username { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
