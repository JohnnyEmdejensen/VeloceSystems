using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace VeloceCRM.Client2.Internals
{
    public class DataShare
    {
        public List<Entity.Country>? CountryCollection { get; set; }
        public List<Entity.Postalzone>? PostalzoneCollection { get; set; }
        public List<Entity.Location>? LocationCollection { get; set; }
        public DataShare() 
        {
            App.EventHelper.UserAuthenticated += EventHelper_UserAuthenticated;
        }

        private void InitializeData()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            GetCountries();
            GetPostalzones();
            GetLocations();
            Mouse.OverrideCursor = c;
        }

        private void GetLocations()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            var data = App.AppShare.Repositories.LocationRepository.GetAll();
            if (data != null)
            {
                LocationCollection = data.OrderBy(x => x.Street).ToList();
                App.EventHelper.RaiseLocationCollectionChangedEvent();
            }
            Mouse.OverrideCursor = c;
        }
        private void GetPostalzones()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            var data = App.AppShare.Repositories.PostalzoneRepository.GetAll();
            if (data != null)
            {
                PostalzoneCollection = data.OrderBy(x => x.Zipcode).ToList();
                App.EventHelper.RaisePostalzoneCollectionChangedEvent();
            }
            Mouse.OverrideCursor = c;
        }
        public void GetCountries()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            var data = App.AppShare.Repositories.CountryRepository.GetAll();
            if (data != null)
            {
                CountryCollection = data.OrderBy(x => x.Name).ToList();
                App.EventHelper.RaiseCountryCollectionChangedEvent();
            }
            Mouse.OverrideCursor = c;
        }

        private void EventHelper_UserAuthenticated(object sender, EventArgs e)
        {
            InitializeData();
        }
    }
}
