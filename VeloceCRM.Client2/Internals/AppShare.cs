using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client2.Internals
{
    public class AppShare
    {
        public Repository.Repositories Repositories { get; set; }
        private Entity.User? _activeUser;

        public Entity.User? ActiveUser
        {
            get { return _activeUser; }
            set 
            { 
                _activeUser = value; 
            }
        }
        private Entity.License? _activeLicense;

        public Entity.License? ActiveLicense
        {
            get { return _activeLicense; }
            set { _activeLicense = value; }
        }

        public AppShare() 
        {
            Repositories = new Repository.Repositories(Guid.Empty.ToString());
        }
        /// <summary>
        /// Authenticate into the application.
        /// </summary>
        /// <returns></returns>
        public bool Authenticated()
        {
            bool result = false;
            App.DialogHelper.Authenticate();
            if (_activeUser == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}
