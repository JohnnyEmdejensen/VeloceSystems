using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VeloceCRM.Entity
{
    public class Company : BaseEntity
    {
        [MaxLength(16)]
        public string Number { get; set; }= string.Empty;
        [MaxLength(16)]
        public string Taxnumber { get; set; }= string.Empty;
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(64)]
        public string? Nickname { get; set; } 
        public long LocationId { get; set; }
        [MaxLength(16)]
        public string? Phone { get; set; }
        [MaxLength(16)]
        public string? Phone2 { get; set; }
        [MaxLength(256)]
        public string? Email { get; set; }
        [MaxLength(512)]
        public string? Website { get; set; }

    }
}
