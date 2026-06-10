using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Models
{
    public class DawaVejpunkt
    {
        public Guid id { get; set; }
        public string kilde { get; set; } = "";
        public string nøjagtighed { get; set; } = "";
        public string tekniskstandard { get; set; } = "";
        public double[] koordinater { get; set; }

    }
}
