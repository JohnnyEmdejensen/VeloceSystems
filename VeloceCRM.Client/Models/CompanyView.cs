using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Models
{
    public class CompanyView
    {
        public long Id { get; set; }

        public string Number { get; set; } = "";
        public string Taxnumber { get; set; } = "";
        public string Name { get; set; } = "";
        public string? Nickname { get; set; }
        public string Address { get; set; } = "";
        public string Zipcode { get; set; } = "";
        public string City { get; set; } = "";
        public string Country { get; set; } = "";
        public string? Phone { get; set; } 
        public string? Email { get; set; } 
        public string? Website { get; set; }
        public int FoundedYear { get; set; }
        public int Employees { get; set; }
    }
}
