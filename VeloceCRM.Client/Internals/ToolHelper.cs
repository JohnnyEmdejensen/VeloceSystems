using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class ToolHelper
    {
        public Entity.Location? ParseLocation(string Street, string House, string Floor, string Door, string Zipcode, string City, string Country)
        {
            Entity.Location? result = null;
            var country = App.AppShare.Repositories.CountryRepository.GetByName(Country);
            if (country == null)
            {
                country = new Entity.Country
                {
                    Name = Country,
                    IsoCode = "",
                    Number = 0,
                };
                country = App.AppShare.Repositories.CountryRepository.Create(country);
                App.EventHelper.RaiseCountryChangedEvent();
            }
            if (country != null)
            {
                var postalzone = App.AppShare.Repositories.PostalzoneRepository.GetByZipCountry(Zipcode, country.Id);
                if (postalzone == null)
                {
                    postalzone = new Entity.Postalzone
                    {
                        CountryId = country.Id,
                        Zipcode = Zipcode,
                        City = City,
                    };
                    postalzone = App.AppShare.Repositories.PostalzoneRepository.Create(postalzone);
                    App.EventHelper.RaisePostalzoneChangedEvent();
                }
                if (postalzone != null)
                {
                    var location = App.AppShare.Repositories.LocationRepository.GetByAddress(Street, House, Floor, Door, postalzone.Id);
                    if (location == null)
                    {
                        location = new Entity.Location
                        {
                            Street = Street,
                            House = House,
                            Floor = Floor,
                            Door = Door,
                            PostalzoneId = postalzone.Id
                        };
                        location = App.AppShare.Repositories.LocationRepository.Create(location);
                        App.EventHelper.RaiseLocationChangedEvent();
                    }
                    if (location != null)
                    {
                        result = location;
                    }
                }
            }
            return result;
        }
    }
}
