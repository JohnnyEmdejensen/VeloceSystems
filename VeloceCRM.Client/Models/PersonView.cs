using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Models
{
    public class PersonView
    {
        public long Id { get; set; }
        public string Firstname { get; set; } = "";
        public string? Middlename { get; set; }
        public string Surname { get; set; } = "";
        public string? Fullname { get; set; }
        public string Initials { get; set; } = "";
        public string? Address { get; set; } 
        public string? Zipcode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Company { get; set; }
        public long CompanyId { get; set; }
    }
}
