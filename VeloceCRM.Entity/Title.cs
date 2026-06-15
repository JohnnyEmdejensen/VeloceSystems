using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VeloceCRM.Entity
{
    public class Title : BaseEntity
    {
        [MaxLength(16)]
        public string Key { get; set; } = "";
        [MaxLength(64)]
        public string Text { get; set; } = "";
    }
}
