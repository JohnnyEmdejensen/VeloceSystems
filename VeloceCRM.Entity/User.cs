using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VeloceCRM.Entity
{
    public class User : BaseEntity
    {
        [MaxLength(64)]
        public string Firstname { get; set; } = string.Empty;
        [MaxLength(64)]
        public string? Middlename { get; set; }
        [MaxLength(64)]
        public string Surname { get; set; } = string.Empty;
        [MaxLength(8)]
        public string Initialis { get; set; } = string.Empty;
        [MaxLength(16)]
        public string? Phone { get; set; }
        [MaxLength(16)]
        public string? Mobile { get; set; }
        [MaxLength(256)]
        public string? Email { get; set; }
        public long LocationId { get; set; }
        [MaxLength(64)]
        public string Login { get; set; } = string.Empty;
        [MaxLength(256)]
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool ForceNewPassword { get; set; }

    }
}
