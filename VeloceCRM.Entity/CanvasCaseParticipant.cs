using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Entity
{
    public class CanvasCaseParticipant : BaseEntity
    {
        public long CanvasCaseId { get; set; }
        public long? PersonId { get; set; }
        public long? UserId { get; set; }
        public long RoleId { get; set; } 
    }
}
