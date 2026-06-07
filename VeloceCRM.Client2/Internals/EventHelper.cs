using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client2.Internals
{
    public class EventHelper
    {
        public delegate void DefaultHandler(object sender, EventArgs e);
        public event DefaultHandler? UserAuthenticated;
        public event DefaultHandler? CountryCollectionChanged;
        public event DefaultHandler? PostalzoneCollectionChanged;
        public event DefaultHandler? LocationCollectionChanged;
        public event DefaultHandler? UserCollectionChanged;
        public event DefaultHandler? CompanyCollectionChanged;
        public event DefaultHandler? PersonCollectionChanged;

        public void RaisePersonCollectionChangedEvent()
        {
            PersonCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCompanyCollectionChangedEvent()
        {
            CompanyCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseUserCollectionChangedEvent()
        {
            UserCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseLocationCollectionChangedEvent()
        {
            LocationCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaisePostalzoneCollectionChangedEvent()
        {
            PostalzoneCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCountryCollectionChangedEvent()
        {
            CountryCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseUserAuthenticatedEvent()
        {
            UserAuthenticated?.Invoke(this, EventArgs.Empty);
        }
    }
}
