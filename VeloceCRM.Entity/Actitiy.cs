using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VeloceCRM.Entity
{
    public class Actitiy : BaseEntity
    {
        public int ActivityType { get; set; }
        public long CompanyId { get; set; }
        public long? PersonId { get; set; }
        public long SalespersonId { get; set; }
        public long Starts {  get; set; }
        public long Ends {  get; set; }
        [MaxLength(128)]
        public string Subject { get; set; } = "";
        public string Reason { get; set; } = "";
        public string? Conclution { get; set; }
        public bool IsCompleted { get; set; }
        public long TookMinutes { get; set; }
        public long BilledMinutes { get; set; }
        public long LastBilled { get; set; }
        public long FollowuptypeId { get; set; }

    }
}
