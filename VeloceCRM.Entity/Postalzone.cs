using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VeloceCRM.Entity
{
    public class Postalzone : BaseEntity
    {
        [MaxLength(16)]
        public string Zipcode { get; set; } = string.Empty;
        [MaxLength(128)]
        public string City { get; set; } = string.Empty;
        [MaxLength(128)]
        public string? State { get; set; }
        public long CountryId { get; set; }

        [NotMapped]
        public Entity.Country? Country { get; set; }
    }
}
