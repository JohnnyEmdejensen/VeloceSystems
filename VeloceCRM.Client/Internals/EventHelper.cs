using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    
    public class EventHelper
    {
        public delegate void DefaultHandler(object sender, EventArgs e);
        public event DefaultHandler? CountryCollectionChanged;
        public event DefaultHandler? PostalzoneCollectionChanged;
        public event DefaultHandler? LocationCollectionChanged;
        public event DefaultHandler? UserCollectionChanged;
        public event DefaultHandler? CompanyCollectionChanged;
        public event DefaultHandler? PersonCollectionChanged;
        public event DefaultHandler? ActiveCompanyChanged;
        public event DefaultHandler? CompanyChanged;
        public event DefaultHandler? CountryChanged;
        public event DefaultHandler? PostalzoneChanged;
        public event DefaultHandler? LocationChanged;
        public event DefaultHandler? ActivePersonChanged;
        public event DefaultHandler? PersonChanged;

        public void RaisePersonChangedEvent()
        {
            PersonChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseActivePersonChangedEvent()
        {
            ActivePersonChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseLocationChangedEvent()
        {
            LocationChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaisePostalzoneChangedEvent()
        {
            PostalzoneChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCountryChangedEvent()
        {
            CountryChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCompanyChangedEvent()
        {
            CompanyChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseActiveCompanyChangedEvent()
        {
            ActiveCompanyChanged?.Invoke(this, EventArgs.Empty);
        }
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
    }
}
