using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace VeloceCRM.Client.Internals
{
    public class AppShare
    {
        public Repository.Repositories Repositories { get; set; } = new Repository.Repositories(Guid.Empty.ToString());
        public Entity.License? ActiveLicense { get; set; }
        public Entity.User? ActiveUser { get; set; }
        public Entity.Company? ActiveCompany { get; set; }
        public Entity.Person? ActivePerson { get; set; }

        public AppShare() 
        { 
            
        }

        public bool Authenticated()
        {
            bool result = false;
            if (WindowsAuthenticated())
            {
                if (ActiveUser != null)
                {
                    result = true;
                }
            }
            else
            {
                App.DialogHelper.ShowLoginDialog();
                if (ActiveUser != null)
                {
                    result = true;
                }
            }
            return result;
        }

        private bool WindowsAuthenticated()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            bool result = false;
            var user = Repositories.UserRepository.GetByAccount(Environment.UserDomainName + "/" + Environment.UserName);
            if (user != null)
            {
                var license = Repositories.LicenseRepository.GetByKey(user.LicenseKey);
                if (license != null)
                {
                    ActiveLicense = license;
                    ActiveUser = user;
                    result = true;
                }
            }
            Mouse.OverrideCursor = c;
            return result;
        }
    }
}
