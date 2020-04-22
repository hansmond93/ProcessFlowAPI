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

        public WorkFlowTrailEntity WorkFlowTrailEntity { get; set; }

        public static readonly int Initiation = 0;

        public static readonly int Ongoing = 1;

        public static readonly int Completed = 2;


    }
}
