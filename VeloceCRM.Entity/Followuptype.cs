using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Entity
{
    public class Followuptype : BaseEntity
    {
        public string Key { get; set; } = "";
        public string Text { get; set; } = "";
        public bool IsAutoGenerate { get; set; }
        public int GenerateInDays { get; set; }
    }
}
