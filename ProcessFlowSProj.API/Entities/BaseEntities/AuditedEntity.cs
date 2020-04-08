using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Entities.BaseEntities
{
    public class AuditedEntity
    {
        public int CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public DateTime? DateTimeDeleted { get; set; }



    }
}
