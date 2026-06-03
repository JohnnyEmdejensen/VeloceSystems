using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class EventHelperClass
    {
        public delegate void DefaultHandler(object sender, EventArgs e);
        public event DefaultHandler? Authenticated;
        public event DefaultHandler? CountryCollectionChanged;
        public event DefaultHandler? PostalzoneCollectionChanged;
        public event DefaultHandler? LocationCollectionChanged;
        public event DefaultHandler? CountryDeleted;
        public event DefaultHandler? CountrySaved;

        public void RaiseCountrySavedEvent()
        {
            CountrySaved?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCountryDeletedEvent()
        {
            CountryDeleted?.Invoke(this, EventArgs.Empty);
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
        public void RaiseAuthenticatedEvent()
        {
            Authenticated?.Invoke(this, EventArgs.Empty);
        }
    }
}
