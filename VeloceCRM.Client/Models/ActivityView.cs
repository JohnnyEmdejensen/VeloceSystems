using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VeloceCRM.Client.Models
{
    public class ActivityView
    {
        public long Id { get; set; }
        public int ActivityType { get; set; }
        public string TypeStr { get; set; } = "";
        public long CompanyId { get; set; }
        public long? PersonId { get; set; }
        public long SalespersonId { get; set; }
        public long Starts { get; set; }
        public long Ends { get; set; }
        public string Subject { get; set; } = "";
        public string Reason { get; set; } = "";
        public string? Conclution { get; set; }
        public bool IsCompleted { get; set; }
        public long TookMinutes { get; set; }
        public long BilledMinutes { get; set; }
        public long LastBilled { get; set; }
        public long FollowuptypeId { get; set; }
        public string Company { get; set; } = "";
        public string? Person { get; set; }
        public string Salesperson { get; set; } = "";
        public string Followuptype { get; set; } = "";
        public string StartDate { get; set; } = "";
        public string StartTime { get; set; } = "";
        public string EndDate { get; set; } = "";
        public string EndTime { get; set; } = "";

    }
}
