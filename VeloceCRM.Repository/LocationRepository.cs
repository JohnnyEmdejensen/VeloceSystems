using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class LocationRepository : BaseRepository
    {
        public LocationRepository(string licenseKey, ApiContext apiContext) 
        { 
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.Location? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Locations.FirstOrDefault(x => x.Id == Id && !x.IsDeleted && x.LicenseKey == LicenseKey);
        }
        public Entity.Location? GetByAddress(string Street, string House, string Floor, string Door, long PostalzoneId)
        {
            if (ApiContext == null) return null;
            var items = ApiContext.Locations.Where(x => x.LicenseKey == LicenseKey && !x.IsDeleted && x.PostalzoneId == PostalzoneId && x.Street == Street && x.House == House);
            if (!string.IsNullOrEmpty(Floor))
            {
                items = items.Where(x => x.Floor == Floor);
            }
            if ( !string.IsNullOrEmpty(Door))
            {
                items = items.Where(x => x.Door == Door);
            }
            return items.FirstOrDefault();
        }
        public List<Entity.Location>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Locations.Where(x => !x.IsDeleted && x.LicenseKey == LicenseKey).ToList();
        }
        public Entity.Location? Create(Entity.Location? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.Locations.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.Location? Update(Entity.Location? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Door = Source.Door;
            record.Floor = Source.Floor;
            record.House = Source.House;
            record.PostalzoneId = Source.PostalzoneId;
            record.Street = Source.Street;
            record.Latitude = Source.Latitude;
            record.Longitude = Source.Longitude;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.Location? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}
