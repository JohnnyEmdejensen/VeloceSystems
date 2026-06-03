using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class PostalzoneRepository : BaseRepository
    {
        public PostalzoneRepository(string licenseKey, ApiContext apiContext)
        {
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.Postalzone? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Postalzones.FirstOrDefault(x => x.Id == Id && !x.IsDeleted && x.LicenseKey == LicenseKey);
        }
        public List<Entity.Postalzone>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Postalzones.Where(x => !x.IsDeleted && x.LicenseKey == LicenseKey).ToList();
        }
        public Entity.Postalzone? Create(Entity.Postalzone? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.Postalzones.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.Postalzone? Update(Entity.Postalzone? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.City = Source.City;
            record.CountryId = Source.CountryId;
            record.State = Source.State;
            record.Zipcode = Source.Zipcode;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.Postalzone? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}
