using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VeloceCRM.Entity
{
    public class CanvasCaseParticipantRole : BaseEntity
    {
        [MaxLength(32)]
        public string Key { get; set; } = "";
        [MaxLength(128)]
        public string Text { get; set; } = "";
    }
}
