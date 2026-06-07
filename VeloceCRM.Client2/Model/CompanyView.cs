using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client2.Model
{
    public class CompanyView
    {
        public string Number { get; set; } = "";
        public string Taxnumber { get; set; } = "";
        public string Name { get; set; } = "";
        public string Nickname { get; set; } = "";
        public string Address { get; set; } = "";
        public string Zipcode { get; set; } = "";
        public string City { get; set; } = "";
        public string Country { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Website { get; set; } = "";
    }
}
