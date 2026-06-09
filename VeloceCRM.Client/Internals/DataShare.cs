using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class DataShare
    {

        public List<Entity.Country>? CountryCollection {  get; set; }
        public List<Entity.Postalzone>? PostalzoneCollection { get; set; }
        public List<Entity.Location>? LocationCollection { get; set; }
        public List<Entity.User>? UserCollection { get; set; }
        public List<Entity.Company>? CompanyCollection { get; set; }
        public List<Entity.Person>? PersonCollection { get; set; }

        public DataShare() 
        { 

        }
    }
}
