using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class CompanyRepository : BaseRepository
    {
        public CompanyRepository(string licenseKey, ApiContext apiContext)
        {
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.Company? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Companies.FirstOrDefault(x => x.Id == Id && !x.IsDeleted && x.LicenseKey == LicenseKey);
        }
        public List<Entity.Company>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Companies.Where(x => !x.IsDeleted && x.LicenseKey == LicenseKey).ToList();
        }
        public Entity.Company? Create(Entity.Company? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.Companies.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.Company? Update(Entity.Company? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Email = Source.Email;
            record.LocationId = Source.LocationId;
            record.Name = Source.Name;
            record.Nickname = Source.Nickname;
            record.Number = Source.Number;
            record.Phone = Source.Phone;
            record.Phone2 = Source.Phone2;
            record.Taxnumber = Source.Taxnumber;
            record.Website = Source.Website;
            record.FoundedYear = Source.FoundedYear;
            record.Employees = Source.Employees;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.Company? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}
