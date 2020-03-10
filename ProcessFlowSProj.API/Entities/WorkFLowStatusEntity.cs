using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class WorkFLowStatusEntity
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        public string StatusName { get; set; }

        public static readonly byte Initiation = 0;

        public static readonly byte Ongoing = 1;

        public static readonly byte Completed = 2;


    }
}
