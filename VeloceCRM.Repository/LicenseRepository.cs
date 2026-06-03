using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class LicenseRepository : BaseRepository
    {
        public LicenseRepository(string licenseKey, ApiContext? apiContext)
        {
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.License? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Licenses.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public Entity.License? GetByKey(string key)
        {
            if (ApiContext == null) return null;
            return ApiContext.Licenses.FirstOrDefault(x => x.Key == key && !x.IsDeleted);
        }
        public Entity.License? GetByPrefix(string prefix)
        {
            if (ApiContext == null) return null;
            return ApiContext.Licenses.FirstOrDefault(x => x.Prefix == prefix && !x.IsDeleted);
        }
        public List<Entity.License>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Licenses.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.License? Create(Entity.License? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Licenses.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.License? Update(Entity.License? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.CompanyName = Source.CompanyName;
            record.Email = Source.Email;
            record.IsActive = Source.IsActive;
            record.IsLocked = Source.IsLocked;
            record.LocationId = Source.LocationId;
            record.Phone = Source.Phone;
            record.TaxNumber = Source.TaxNumber;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.License? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}
