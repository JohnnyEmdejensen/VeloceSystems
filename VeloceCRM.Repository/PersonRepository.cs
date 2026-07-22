using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class PersonRepository : BaseRepository
    {
        public PersonRepository(string licenseKey, ApiContext apiContext)
        {
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.Person? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Persons.FirstOrDefault(x => x.Id == Id && !x.IsDeleted && x.LicenseKey == LicenseKey);
        }
        public List<Entity.Person>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Persons.Where(x => !x.IsDeleted && x.LicenseKey == LicenseKey).ToList();
        }
        public Entity.Person? Create(Entity.Person? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.Persons.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.Person? Update(Entity.Person? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.CompanyId = Source.CompanyId;
            record.Email = Source.Email;
            record.Firstname = Source.Firstname;
            record.LocationId = Source.LocationId;
            record.Middlename = Source.Middlename;
            record.Mobile = Source.Mobile;
            record.Phone    = Source.Phone;
            record.Surname = Source.Surname;
            record.TitleId = Source.TitleId;
            record.Initials = Source.Initials;
            record.Image = Source.Image;    
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.Person? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}
