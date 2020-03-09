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

        private static readonly byte Initiation = 0;

        private static readonly byte Ongoing = 1;

        private static readonly byte Concluded = 1;


    }
}
