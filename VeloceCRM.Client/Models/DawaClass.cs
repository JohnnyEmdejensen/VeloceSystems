using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Models
{
    public class DawaClass
    {
        public List<Models.DawaItemClass> Items { get; set; }
        public DawaClass()
        {
            Items = new List<Models.DawaItemClass>();
        }
    }
}
