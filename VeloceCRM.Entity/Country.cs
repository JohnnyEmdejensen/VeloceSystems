using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VeloceCRM.Entity
{
    public class Country : BaseEntity
    {
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(8)]
        public string IsoCode { get; set; } = string.Empty;
        public int Number { get; set; }
    }
}
