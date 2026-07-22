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
        public DateTime ConvertLongDateToDateTime(long Value)
        {
            DateTime result = DateTime.Now;
            var tmp = Value.ToString();
            var year = tmp.Substring(0, 4);
            var month = tmp.Substring(4, 2);
            var day = tmp.Substring(6, 2);
            var hour = tmp.Substring(8, 2);
            var minute = tmp.Substring(10, 2);

            return result;
        }
        public void SendAppointmentRequest(long Starts, long Ends, string Subject, string Reason, Entity.Company Company, Entity.Person? Person, Entity.User Salesperson)
        {
            string organizerEmail = "organizer@yourdomain.com";
            string organizerName = "Your Name";
            string attendeeEmail = Company.Email;
            string subject = Subject;
            string location = "Teams/Zoom Meeting";
            string description = "Let's review the timeline and deliverables for Q3.";
            DateTime startTime = ConvertLongDateToDateTime(Starts);
            DateTime endTime = ConvertLongDateToDateTime(Ends);
            if (!string.IsNullOrEmpty(Salesperson.Email))
            {
                Salesperson.SetFullName();
                organizerEmail = Salesperson.Email;
                organizerName = Salesperson.Fullname ?? (Salesperson.Firstname + " " + Salesperson.Surname);
            }
            if (Person != null)
            {
                
            }
            string icsContent = GenerateIcsString(organizerEmail, organizerName, "", subject, "", Reason, startTime, endTime);
        }

        private string GenerateIcsString(string orgEmail, string orgName, string attendeeEmail, string subject, string location, string description, DateTime start, DateTime end)
        {
            var sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("PRODID:-//MyCompany//AppointmentRequest//EN");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("METHOD:REQUEST");
            sb.AppendLine("BEGIN:VEVENT");
            sb.AppendLine($"DTSTART:{start:yyyyMMddTHHmmssZ}");
            sb.AppendLine($"DTEND:{end:yyyyMMddTHHmmssZ}");
            sb.AppendLine($"DTSTAMP:{DateTime.UtcNow:yyyyMMddTHHmmssZ}");
            sb.AppendLine($"ORGANIZER;CN=\"{orgName}\":MAILTO:{orgEmail}");
            sb.AppendLine($"ATTENDEE;RSVP=TRUE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION:MAILTO:{attendeeEmail}");
            sb.AppendLine($"UID:{Guid.NewGuid()}");
            sb.AppendLine($"SUMMARY:{subject}");
            sb.AppendLine($"LOCATION:{location}");
            sb.AppendLine($"DESCRIPTION:{description}");
            sb.AppendLine("BEGIN:VALARM");
            sb.AppendLine("TRIGGER:-PT15M");
            sb.AppendLine("ACTION:DISPLAY");
            sb.AppendLine("DESCRIPTION:Reminder");
            sb.AppendLine("END:VALARM");
            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");

            return sb.ToString();
        }
    }
}
