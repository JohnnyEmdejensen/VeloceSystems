using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace VeloceCRM.Client.Internals
{
    public class DataShare
    {

        public List<Entity.Country>? CountryCollection {  get; set; }
        public List<Entity.Postalzone>? PostalzoneCollection { get; set; }
        public List<Entity.Location>? LocationCollection { get; set; }
        public List<Entity.User>? UserCollection { get; set; }
        public List<Entity.Company>? CompanyCollection { get; set; }
        public List<Entity.Person>? PersonCollection { get; set; }

        public DataShare() 
        { 

        }

        public void ResumeEvents()
        {
            App.EventHelper.CompanyChanged += EventHelper_CompanyChanged;
            App.EventHelper.CountryChanged += EventHelper_CountryChanged;
            App.EventHelper.PostalzoneChanged += EventHelper_PostalzoneChanged;
            App.EventHelper.LocationChanged += EventHelper_LocationChanged;
            App.EventHelper.PersonChanged += EventHelper_PersonChanged;
        }


        public void GetData()
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

        private void GetPersons()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            PersonCollection = App.AppShare.Repositories.PersonRepository.GetAll();
            if (PersonCollection != null) 
            {
                foreach (var person in PersonCollection)
                {
                    person.SetFullName();
                }
                PersonCollection = PersonCollection.OrderBy(x => x.Fullname).ToList();
                App.EventHelper.RaisePersonCollectionChangedEvent();
            }
            Mouse.OverrideCursor = c;
        }
        public void GetCompanies()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            CompanyCollection = App.AppShare.Repositories.CompanyRepository.GetAll();
            if (CompanyCollection != null)
                CompanyCollection = CompanyCollection.OrderBy(x => x.Number).ToList();
            App.EventHelper.RaiseCompanyCollectionChangedEvent();
            Mouse.OverrideCursor = c;
        }
        public void GetUsers()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            UserCollection = App.AppShare.Repositories.UserRepository.GetAll();
            if (UserCollection != null)
            {
                foreach (var user in UserCollection)
                {
                    user.SetFullName();
                }
                UserCollection = UserCollection.OrderBy(x => x.Fullname).ToList();
                App.EventHelper.RaiseUserCollectionChangedEvent();
            }

            Mouse.OverrideCursor = c;
        }
        public void GetLocations()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            LocationCollection = App.AppShare.Repositories.LocationRepository.GetAll();
            if (LocationCollection != null)
            {
                foreach(var location in LocationCollection)
                {
                    location.SetAddress();
                }
            }
            App.EventHelper.RaiseLocationCollectionChangedEvent();
            Mouse.OverrideCursor = c;
        }
        public void GetPostalzones()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            PostalzoneCollection = App.AppShare.Repositories.PostalzoneRepository.GetAll();
            App.EventHelper.RaisePostalzoneCollectionChangedEvent();
            Mouse.OverrideCursor = c;
        }
        public void GetCountries()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            CountryCollection = App.AppShare.Repositories.CountryRepository.GetAll();
            if (CountryCollection != null )
                CountryCollection = CountryCollection.OrderBy(x => x.Name).ToList();
            App.EventHelper.RaiseCountryCollectionChangedEvent();
            Mouse.OverrideCursor = c;
        }
        private void EventHelper_CompanyChanged(object sender, EventArgs e)
        {
            GetCompanies();
        }
        private void EventHelper_LocationChanged(object sender, EventArgs e)
        {
            GetLocations();
        }
        private void EventHelper_PersonChanged(object sender, EventArgs e)
        {
            GetPersons();
        }

        private void EventHelper_PostalzoneChanged(object sender, EventArgs e)
        {
            GetPostalzones();
        }

        private void EventHelper_CountryChanged(object sender, EventArgs e)
        {
            GetCountries();
        }
    }
}
