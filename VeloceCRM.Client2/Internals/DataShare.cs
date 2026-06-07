using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VeloceCRM.Entity;

namespace VeloceCRM.Client2.Internals
{
    public class DataShare
    {
        public List<Entity.Country>? CountryCollection { get; set; }
        public List<Entity.Postalzone>? PostalzoneCollection { get; set; }
        public List<Entity.Location>? LocationCollection { get; set; }
        public List<Entity.User>? UserCollection { get; set; }
        public List<Entity.Company>? CompanyCollection { get; set; }
        public List<Entity.Person>? PersonCollection { get; set; }

        public DataShare() 
        {
            App.EventHelper.UserAuthenticated += EventHelper_UserAuthenticated;
            App.EventHelper.CountryCollectionChanged += EventHelper_CountryCollectionChanged;
            App.EventHelper.PostalzoneCollectionChanged += EventHelper_PostalzoneCollectionChanged;
            App.EventHelper.LocationCollectionChanged += EventHelper_LocationCollectionChanged;
        }

        private void ModelPoints()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            if (CountryCollection != null && PostalzoneCollection != null && LocationCollection != null)
            {
                foreach (var location in LocationCollection)
                {
                    location.SetAddress();
                    location.Postalzone = PostalzoneCollection.FirstOrDefault(x => x.Id == location.PostalzoneId);
                    if (location.Postalzone != null)
                    {
                        location.Postalzone.Country = CountryCollection.FirstOrDefault(x => x.Id == location.Postalzone.CountryId);
                    }
                }
            }
            Mouse.OverrideCursor = c;
        }

        private void InitializeData()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            GetCountries();
            GetPostalzones();
            GetLocations();
            GetUsers();
            GetCompanies();
            GetPersons();
            Mouse.OverrideCursor = c;
        }

        public void GetPersons()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            var data = App.AppShare.Repositories.PersonRepository.GetAll();
            if (data != null)
            {
                foreach (var item in data)
                {
                    item.SetFullName();
                    if (LocationCollection != null)
                        item.Location = LocationCollection.FirstOrDefault(x => x.Id == item.LocationId);
                }
                PersonCollection = data.OrderBy(x => x.Fullname).ToList();
                App.EventHelper.RaisePersonCollectionChangedEvent();
            }
            Mouse.OverrideCursor = c;
        }
        public void GetCompanies()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            var data = App.AppShare.Repositories.CompanyRepository.GetAll();
            if (data != null)
            {
                foreach (var item in data)
                {
                    if (LocationCollection != null)
                        item.Location = LocationCollection.FirstOrDefault(x => x.Id == item.LocationId);
                }
                CompanyCollection = data.OrderBy(x => x.Number).ToList();
                App.EventHelper.RaiseCompanyCollectionChangedEvent();
            }
            Mouse.OverrideCursor = c;
        }
        public void GetUsers()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            var data = App.AppShare.Repositories.UserRepository.GetAll();
            if (data != null)
            {
                foreach (var item in data)
                {
                    item.SetFullName();
                    if (LocationCollection != null)
                        item.Location = LocationCollection.FirstOrDefault(x => x.Id == item.LocationId);
                }
                UserCollection = data.OrderBy(x => x.Fullname).ToList();
                App.EventHelper.RaiseUserCollectionChangedEvent();
            }
            Mouse.OverrideCursor = c;
        }
        public void GetLocations()
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
        public void GetPostalzones()
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
        private void EventHelper_LocationCollectionChanged(object sender, EventArgs e)
        {
            ModelPoints();
        }

        private void EventHelper_PostalzoneCollectionChanged(object sender, EventArgs e)
        {
            ModelPoints();
        }

        private void EventHelper_CountryCollectionChanged(object sender, EventArgs e)
        {
            ModelPoints();
        }
    }
}
