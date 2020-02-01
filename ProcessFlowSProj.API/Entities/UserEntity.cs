using ProcessFlowSProj.API.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class UserEntity : UserBaseEntity
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public int UserLoginEntityId { get; set; }

    }
}
