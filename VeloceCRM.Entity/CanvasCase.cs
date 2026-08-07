using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VeloceCRM.Entity
{
    public class CanvasCase : BaseEntity
    {
        [MaxLength(128)]
        public string Name { get; set; } = "";
        [MaxLength(1024)]
        public string Description { get; set; } = "";
        public long Starts { get; set; }
        public long? Ends { get; set; }
        public long CaseMasterId { get; set; }
        public int ActivityType { get; set; }
    }
}
