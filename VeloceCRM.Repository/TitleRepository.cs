using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class TitleRepository : BaseRepository
    {
        public TitleRepository(string licenseKey, ApiContext apiContext) 
        { 
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.Title? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Titles.FirstOrDefault(x => x.Id == Id && x.LicenseKey == LicenseKey && !x.IsDeleted);
        }
        public List<Entity.Title>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Titles.Where(x => x.LicenseKey == LicenseKey && !x.IsDeleted).ToList();
        }
        public Entity.Title? Create(Entity.Title? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.Titles.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.Title? Update(Entity.Title? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Key = Source.Key;
            record.Text = Source.Text;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.Title? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}
