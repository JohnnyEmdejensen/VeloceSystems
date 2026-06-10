using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Models
{
    public class DawaItemClass
    {
        public Guid id { get; set; }
        public int status { get; set; }
        public int darstatus {  get; set; }
        public string? etage { get; set; }
        public string? dør { get; set; }
        public DawaAdgangsadresse adgangsadresse { get; set; } = new DawaAdgangsadresse();
    }
}
