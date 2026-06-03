using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class DataShareClass
    {
        public List<Internals.WorkerItem> WorkerItems { get; set; } = new List<WorkerItem>();
        public List<Entity.Country>? CountryCollection { get; set; }
        public List<Entity.Postalzone>? PostalzoneCollection { get; set; }
        public List<Entity.Location>? LocationCollection { get; set; }

        public void ResumeEvents()
        {
            App.Globals.EventHelper.CountryDeleted += EventHelper_CountryDeleted;
            App.Globals.EventHelper.CountrySaved += EventHelper_CountrySaved;
        }


        public void GetInitialData()
        {
            using (new WorkerHandler("GetInitialData"))
            {
                GetCountries();
                GetPostalzones();
                GetLocations();
            }
        }
        public void GetLocations()
        {
            using (new WorkerHandler("GetLocations"))
            {
                var data = App.Globals.AppShare.Repositories.LocationRepository.GetAll();
                if (data != null)
                {
                    data = data.OrderBy(x => x.Street).ToList();
                    LocationCollection = data;
                    App.Globals.EventHelper.RaiseLocationCollectionChangedEvent();
                }
            }
        }
        public void GetPostalzones()
        {
            using (new WorkerHandler("GetPostalzones"))
            {
                var data = App.Globals.AppShare.Repositories.PostalzoneRepository.GetAll();
                if (data != null)
                {
                    data = data.OrderBy(x => x.Zipcode).ToList();
                    PostalzoneCollection = data;
                    App.Globals.EventHelper.RaisePostalzoneCollectionChangedEvent();
                }
            }
        }
        public void GetCountries()
        {
            using (new WorkerHandler("GetCountries"))
            {
                var data = App.Globals.AppShare.Repositories.CountryRepository.GetAll();
                if (data != null)
                {
                    data = data.OrderBy(x => x.Name).ToList();
                    CountryCollection = data;
                    App.Globals.EventHelper.RaiseCountryCollectionChangedEvent();
                }
            }
        }
        private void EventHelper_CountryDeleted(object sender, EventArgs e)
        {
            GetCountries();
        }
        private void EventHelper_CountrySaved(object sender, EventArgs e)
        {
            GetCountries();
        }
    }
}
