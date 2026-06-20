using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Media.Imaging;

namespace VeloceCRM.Client.Internals
{
    public class ToolHelper
    {
        public long ConvertDateTimeToLong(DateTime date)
        {
            long result = 0;
            result = Convert.ToInt64(string.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}", date.Year, date.Month, date.Day, date.Hour, date.Minute));
            return result;
        }
        public string ConvertLongDateToString(long date)
        {
            string result = "";
            result = date.ToString().Substring(6, 2) + "-" + date.ToString().Substring(4, 2) + "-" + date.ToString().Substring(0, 4);
            return result;
        }
        public string ConvertLongTimeToString(long date)
        {
            string result = "";
            result = date.ToString().Substring(8, 2) + ":" + date.ToString().Substring(10, 2);
            return result;
        }
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
        public byte[] ConvertImageToByteArray(BitmapImage? Image)
        {
            MemoryStream memoryStream = new MemoryStream();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(Image));
            encoder.Save(memoryStream);
            return memoryStream.ToArray();
        }
        public BitmapImage ConvertByteArrayToBitmapImage(Byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }
    }
}
