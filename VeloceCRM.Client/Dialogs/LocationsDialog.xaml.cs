using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VeloceCRM.Entity;

namespace VeloceCRM.Client.Dialogs
{
    /// <summary>
    /// Interaction logic for LocationsDialog.xaml
    /// </summary>
    public partial class LocationsDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Location? _location;
        public LocationsDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += LocationsDialog_ContentRendered;
            Closing += LocationsDialog_Closing;
            KeyDown += LocationsDialog_KeyDown;
            dgLocations.SizeChanged += DgLocations_SizeChanged;
            App.EventHelper.LocationCollectionChanged += EventHelper_LocationCollectionChanged;
            _settings.Load();
            FillControls();
        }

        private void SetDetails()
        {
            _location = DataContext as Entity.Location;
            if (_location != null)
            {
                txtLatitude.Text = _location.Latitude.ToString();
                txtLongitude.Text = _location.Longitude.ToString();
                chkVerified.IsChecked = _location.IsVerified;
                if (App.DataShare.PostalzoneCollection != null)
                {
                    var pz = App.DataShare.PostalzoneCollection.FirstOrDefault(x => x.Id == _location.PostalzoneId);
                    if (pz != null)
                    {
                        txtCity.Text = pz.City;
                        txtZipcode.Text = pz.Zipcode;
                        cboCountry.SelectedValue = pz.CountryId;
                    }
                }
            }
        }
        private void SetGui()
        {
            _location = DataContext as Entity.Location;
            cmdAdd.IsEnabled = false;
            cmdSave.IsEnabled = false;
            cmdSaveClose.IsEnabled = false;
            cmdDelete.IsEnabled = false;
            cmdCheck.IsEnabled = false;
            if (_location != null)
            {
                if (_location.Id == 0)
                {
                    cmdSave.IsEnabled = true;
                    cmdSaveClose.IsEnabled = true;
                }
                else
                {
                    cmdSave.IsEnabled = true;
                    cmdSaveClose.IsEnabled = true;
                    cmdDelete.IsEnabled = true;
                    cmdAdd.IsEnabled = true;
                    cmdCheck.IsEnabled= true;
                }
            }
            else
            {
                cmdAdd.IsEnabled = true;
            }
        }
        private void FillControls()
        {
            FillCountryControl();
            FillLocationsControl();
        }

        private void FillLocationsControl()
        {
            List<Models.LocationView> list = new List<Models.LocationView>();
            if (App.DataShare.LocationCollection != null)
            {
                foreach (var location in App.DataShare.LocationCollection)
                {
                    Models.LocationView view = new Models.LocationView
                    {
                        Id = location.Id,
                        Street = location.Street,
                        House = location.House,
                        Floor = location.Floor,
                        Door = location.Door,
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        PostalzoneId = location.PostalzoneId,
                        IsVerified = location.IsVerified,
                    };
                    if (App.DataShare.PostalzoneCollection != null)
                    {
                        var pz = App.DataShare.PostalzoneCollection.FirstOrDefault(x => x.Id == location.PostalzoneId);
                        if (pz != null)
                        {
                            view.Zipcode = pz.Zipcode;
                            view.City = pz.City;
                            view.CountryId = pz.CountryId;
                            if (App.DataShare.CountryCollection != null)
                            {
                                var ctry = App.DataShare.CountryCollection.FirstOrDefault(x => x.Id == pz.CountryId);
                                if (ctry != null)
                                {
                                    view.Country = ctry.Name;
                                }
                            }
                        }
                    }
                    list.Add(view);
                }
            }
            dgLocations.BeginInit();
            dgLocations.ItemsSource = list;
            dgLocations.EndInit();
        }
        private void FillCountryControl()
        {
            cboCountry.BeginInit();
            cboCountry.ItemsSource = App.DataShare.CountryCollection;
            cboCountry.DisplayMemberPath = "Name";
            cboCountry.SelectedValuePath = "Id";
            cboCountry.EndInit();
        }

        private void LocationsDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
        }

        private void LocationsDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void LocationsDialog_ContentRendered(object? sender, EventArgs e)
        {

        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdSaveClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DgLocations_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgLocations.ActualWidth;
            dgLocations.Columns[1].Width = width - 40 - 24 - 24 - 54 - 100 - 100 - 16 - 24;
        }

        private void imgCls_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void dgLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgLocations.SelectedItem as Models.LocationView;
            if (item != null && App.DataShare.LocationCollection != null)
            {
                _location = App.DataShare.LocationCollection.FirstOrDefault(x => x.Id == item.Id);
                DataContext = _location;
                SetGui();
                SetDetails();
            }
        }

        private void cmdCheck_Click(object sender, RoutedEventArgs e)
        {
            _location = DataContext as Entity.Location;
            if (_location != null)
            {
                Entity.Postalzone? pz = null;
                string zipTag = "";
                if (App.DataShare.PostalzoneCollection != null)
                {
                    pz = App.DataShare.PostalzoneCollection.FirstOrDefault(x => x.Id == _location.PostalzoneId);
                    if (pz != null)
                    {
                        zipTag = "&postnr=" + pz.Zipcode;
                    }
                }
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.dataforsyningen.dk/adresser");
                    client.Timeout = new TimeSpan(0, 5, 0);

                    var response = client.GetAsync("?vejnavn=" + _location.Street + "&husnr=" + _location.House + zipTag);
                    var result = response.GetAwaiter().GetResult();
                    var data = result.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(data))
                    {
                        _location.IsVerified = false;
                        List<Models.DawaItemClass> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.DawaItemClass>>(data);
                        if (obj != null && obj.Count > 0)
                        {
                            if (obj.First().adgangsadresse != null && obj.First().adgangsadresse.vejpunkt != null)
                            {
                                var vp = obj.First().adgangsadresse.vejpunkt;
                                if (vp.koordinater != null && vp.koordinater.Length == 2)
                                {
                                    var lo = vp.koordinater[0];
                                    var la = vp.koordinater[1];
                                    if (lo > 0 && la > 0)
                                    {
                                        _location.Latitude = la;
                                        _location.Longitude = lo;
                                        _location.IsVerified = true;
                                        App.AppShare.Repositories.LocationRepository.Update(_location);
                                        App.EventHelper.RaiseLocationChangedEvent();
                                        DataContext = _location;
                                        SetDetails();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void EventHelper_LocationCollectionChanged(object sender, EventArgs e)
        {
            FillLocationsControl();
        }
    }
}
