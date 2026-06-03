using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(string licenseKey, ApiContext apiContext) 
        { 
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.User? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Users.FirstOrDefault(x => x.Id == Id && !x.IsDeleted && x.LicenseKey == LicenseKey);
        }
        public Entity.User? GetByAccount(string Account)
        {
            if (ApiContext == null) return null;
            return ApiContext.Users.FirstOrDefault(x => x.WindowsAccount != null && x.WindowsAccount.ToLower().Contains(Account.ToLower()) && !x.IsDeleted);
        }
        public Entity.User? Authenticate(string login, string password  )
        {
            if (ApiContext == null) return null;
            var result = ApiContext.Users.FirstOrDefault(x => x.Login == login && !x.IsDeleted);
            if (result != null)
            {
                if (!result.Password.Equals(password))
                {
                   result.Authenticationstatus = "Invalid password.";
                }
            }
            else
            {
                result = new Entity.User();
                result.Authenticationstatus = "User not found.";
            }
            return result;
        }
        public List<Entity.User>? GetAll()
        {
            // TODO: leave out login and password for security reasons.
            if (ApiContext == null) return null;
            return ApiContext.Users.Where(x => !x.IsDeleted).Select(x => new Entity.User { Id = x.Id,
                Fullname = x.Fullname,
                Firstname = x.Firstname, 
                Surname = x.Surname, 
                Middlename = x.Middlename, 
                LocationId = x.LocationId, 
                Email = x.Email, 
                ForceNewPassword = x.ForceNewPassword, 
                Mobile = x.Mobile, 
                Phone = x.Phone, 
                IsActive = x.IsActive, 
                LicenseKey = x.LicenseKey, 
                Created = x.Created, 
                Updated = x.Updated }).ToList();
        }
        public Entity.User? Create(Entity.User? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.Users.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.User? Update(Entity.User? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Email = Source.Email;
            record.Firstname = Source.Firstname;
            record.ForceNewPassword = Source.ForceNewPassword;
            record.Initialis = Source.Initialis;
            record.IsActive = Source.IsActive;
            record.LocationId = Source.LocationId;
            record.Login = Source.Login;
            record.Middlename = Source.Middlename;
            record.Mobile = Source.Mobile;
            record.Password = Source.Password;
            record.Phone = Source.Phone;
            record.Surname = Source.Surname;
            record.WindowsAccount = Source.WindowsAccount;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.User? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }

    }
}
