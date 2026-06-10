using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Models
{
    public class DawaAdgangsadresse
    {
        public string href { get; set; } = "";
        public Guid id { get; set; } 
        public string adressebetegnelse { get; set; } = "";
        public string husnr { get; set; } = "";
        public DawaPostnummer postnummer { get; set; } = new DawaPostnummer();
        public DawaKommune kommune { get; set; } = new DawaKommune();
        public DawaEjerlav ejerlav { get; set; }= new DawaEjerlav();
        public string esrejendomsnr { get; set; } = "";
        public string matrikelnr { get; set; } = "";
        public DawaVejpunkt vejpunkt { get; set; }=new DawaVejpunkt();
        public DawaSogn sogn { get; set; } = new DawaSogn();
        public DawaRegion region { get; set; } = new DawaRegion();
        public DawaRetskreds retskreds { get; set; } = new DawaRetskreds();
        public DawaPolitikreds politikreds { get; set; } = new DawaPolitikreds();
        public bool brofast { get; set; }
    }
}
