using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class CountryRepository : BaseRepository
    {
        public CountryRepository(string licenseKey, ApiContext apiContext) 
        { 
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.Country? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Countries.FirstOrDefault(x => x.Id == Id && !x.IsDeleted && x.LicenseKey == LicenseKey);
        }
        public Entity.Country? GetByName(string Name)
        {
            if (ApiContext == null) return null;
            return ApiContext.Countries.FirstOrDefault(x => x.Name == Name && !x.IsDeleted && x.LicenseKey == LicenseKey);
        }
        public List<Entity.Country>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Countries.Where(x => !x.IsDeleted && x.LicenseKey == LicenseKey).ToList();
        }
        public Entity.Country? Create(Entity.Country? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.Countries.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.Country? Update(Entity.Country? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.IsoCode = Source.IsoCode;
            record.Name = Source.Name;
            record.Number = Source.Number;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.Country? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }

    }
}
