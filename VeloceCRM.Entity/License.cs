using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VeloceCRM.Entity
{
    public class License
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(64)]
        public string Key { get; set; } = string.Empty;
        [MaxLength(64)]
        public string CompanyName { get; set; } = string.Empty;
        public long LocationId { get; set; }
        [MaxLength(16)]
        public string Phone { get; set; } = string.Empty;
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(16)]
        public string TaxNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; } 
        public bool IsLocked { get; set; }
        public bool IsDeleted { get; set; }
        public long Created { get; set; }
        public long? Updated { get; set; }
    }
}
