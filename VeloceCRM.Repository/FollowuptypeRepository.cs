using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class FollowuptypeRepository : BaseRepository
    {
        public FollowuptypeRepository(string licenseKey, ApiContext apiContext)
        {
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.Followuptype? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Followuptypess.FirstOrDefault(x => x.Id == Id && x.LicenseKey == LicenseKey && !x.IsDeleted);
        }
        public List<Entity.Followuptype>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Followuptypess.Where(x => x.LicenseKey == LicenseKey && !x.IsDeleted).ToList();
        }
        public Entity.Followuptype? Create(Entity.Followuptype? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.Followuptypess.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.Followuptype? Update(Entity.Followuptype? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.IsAutoGenerate = Source.IsAutoGenerate;
            record.Key = Source.Key;
            record.Text = Source.Text;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.Followuptype? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}
