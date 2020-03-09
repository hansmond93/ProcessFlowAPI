using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class StaffLoginEntity
    {
        [Key]
        public int StaffLoginId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [ForeignKey("StaffEntity")]
        public int StaffId { get; set; }

        public StaffEntity StaffEntity { get; set; }


    }
}
