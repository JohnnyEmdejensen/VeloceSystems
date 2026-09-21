using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class DocumentRepository : BaseRepository
    {
        public DocumentRepository(string licenseKey, ApiContext apiContext) 
        {
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.Document? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Documents.FirstOrDefault(x => x.Id == Id && !x.IsDeleted && x.LicenseKey == LicenseKey);
        }
        public List<Entity.Document>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Documents.Where(x => !x.IsDeleted && x.LicenseKey == LicenseKey).ToList();
        }
        public List<Entity.Document>? GetByCompany(long CompanyId)
        {
            if (ApiContext == null) return null;
            return ApiContext.Documents.Where(x => !x.IsDeleted && x.LicenseKey == LicenseKey && x.CompanyId == CompanyId).ToList();
        }
        public Entity.Document? Create(Entity.Document? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.Documents.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.Document? Update(Entity.Document? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.CompanyId = Source.CompanyId;
            record.CreatedDate = Source.CreatedDate;
            record.Description = Source.Description;
            record.FilePath = Source.FilePath;
            record.FileSize = Source.FileSize;
            record.FileType = Source.FileType;
            record.ModifiedDate = Source.ModifiedDate;
            record.Name = Source.Name;
            record.PersonId = Source.PersonId;
            record.SalespersonId = Source.SalespersonId;
            record.Subject = Source.Subject;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.Document? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}
