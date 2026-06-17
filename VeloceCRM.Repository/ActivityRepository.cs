using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class ActivityRepository : BaseRepository
    {
        public ActivityRepository(string licenseKey, ApiContext apiContext)
        {
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.Actitiy? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Activities.FirstOrDefault(x => x.Id == Id && x.LicenseKey == LicenseKey && !x.IsDeleted);
        }
        public List<Entity.Actitiy>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Activities.Where(x => x.LicenseKey == LicenseKey && !x.IsDeleted).ToList();
        }
        public List<Entity.Actitiy>? GetByCompany(long CompanyId)
        {
            if (ApiContext == null) return null;
            return ApiContext.Activities.Where(x => x.LicenseKey == LicenseKey && !x.IsDeleted && x.CompanyId == CompanyId).ToList();
        }
        public Entity.Actitiy? Create(Entity.Actitiy? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.Activities.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.Actitiy? Update(Entity.Actitiy? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.ActivityType = Source.ActivityType;
            record.BilledMinutes = Source.BilledMinutes;
            record.CompanyId = Source.CompanyId;
            record.Conclution = Source.Conclution;
            record.Ends = Source.Ends;
            record.FollowuptypeId = Source.FollowuptypeId;
            record.IsCompleted = Source.IsCompleted;
            record.LastBilled = Source.LastBilled;
            record.PersonId = Source.PersonId;
            record.Reason = Source.Reason;
            record.SalespersonId = Source.SalespersonId;
            record.Starts = Source.Starts;
            record.Subject = Source.Subject;
            record.TookMinutes = Source.TookMinutes;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.Actitiy? Delete(long Id)
        {
            var record = Get(Id);            
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}
