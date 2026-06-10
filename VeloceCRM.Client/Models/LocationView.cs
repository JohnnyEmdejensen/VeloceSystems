using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Models
{
    public class LocationView
    {
        public long Id { get; set; }
        public string Street { get; set; } = "";
        public string House { get; set; } = "";
        public string? Floor { get; set; }
        public string? Door { get; set; }
        public long PostalzoneId { get; set; }
        public string Zipcode { get; set; } = "";
        public string City { get; set; } = "";
        public string Country { get; set; } = "";
        public long CountryId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsVerified { get; set; }
    }
}
