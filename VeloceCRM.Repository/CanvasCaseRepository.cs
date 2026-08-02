using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class CanvasCaseRepository : BaseRepository
    {
        public CanvasCaseRepository(string licenseKey, ApiContext apiContext) 
        {
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.CanvasCase? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.CanvasCases.FirstOrDefault(x => x.Id == Id && x.LicenseKey == LicenseKey);
        }
        public List<Entity.CanvasCase>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.CanvasCases.Where(x => x.LicenseKey == LicenseKey).ToList();
        }

        public Entity.CanvasCase? Create(Entity.CanvasCase? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.CanvasCases.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.CanvasCase? Update(Entity.CanvasCase? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.ActivityType = Source.ActivityType;
            record.CaseMasterId = Source.CaseMasterId;
            record.Description = Source.Description;
            record.Ends = Source.Ends;
            record.Name = Source.Name;
            record.Starts = Source.Starts;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.CanvasCase? Delete(long Id)
        {
            var record = Get(Id);   
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}
