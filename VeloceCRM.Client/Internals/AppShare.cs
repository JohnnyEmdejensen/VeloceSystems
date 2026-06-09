using System;
using System.Collections.Generic;
using System.Text;

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
            App.DialogHelper.ShowLoginDialog();
            if (ActiveUser != null)
            {
                result = true;
            }
            return result;
        }
    }
}
