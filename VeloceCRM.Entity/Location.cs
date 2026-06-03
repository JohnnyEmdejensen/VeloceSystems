using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VeloceCRM.Entity
{
    public class Location : BaseEntity
    {
        [MaxLength(128)]
        public string Street { get; set; } = string.Empty;
        [MaxLength(128)] 
        public string City { get; set; }= string.Empty;
        [MaxLength(16)]
        public string House { get; set; } = string.Empty;
        [MaxLength(16)]
        public string? Floor { get; set; }
        [MaxLength(16)]
        public string? Door { get; set; }
        public long PostalzoneId { get; set; }
        [NotMapped]
        public string? Address { get; set; }
    }
}
