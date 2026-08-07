using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class CanvasCaseCompanyLinkRepository : BaseRepository
    {
        public CanvasCaseCompanyLinkRepository(string licenseKey, ApiContext apiContext) 
        {
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.CanvasCaseCompanyLink? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.CanvasCaseCompanyLinks.FirstOrDefault(x => x.Id == Id && x.LicenseKey == LicenseKey && !x.IsDeleted);
        }
        public List<Entity.CanvasCaseCompanyLink>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.CanvasCaseCompanyLinks.Where(x =>x.LicenseKey == LicenseKey && !x.IsDeleted).ToList();
        }
        public Entity.CanvasCaseCompanyLink? Create(Entity.CanvasCaseCompanyLink? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.CanvasCaseCompanyLinks.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.CanvasCaseCompanyLink? Update(Entity.CanvasCaseCompanyLink? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.CanvasCaseId = Source.CanvasCaseId;
            record.CompanyId = Source.CompanyId;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.CanvasCaseCompanyLink? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}
