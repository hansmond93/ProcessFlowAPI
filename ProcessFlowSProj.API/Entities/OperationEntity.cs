using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities
{
    public class OperationEntity
    {
        [Key]
        public int OperationId { get; set; }

        public string OperationName { get; set; }

        public string OperationCode { get; set; }
        

    }
}
