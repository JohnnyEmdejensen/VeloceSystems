using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VeloceCRM.Client.Internals;

namespace VeloceCRM.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Internals.FormSettingsClass _settings;
        private Dialogs.AuthenticateDialog? _dialogAuthenticate;
        private System.Timers.Timer _timer;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new System.Timers.Timer();
            _timer.Interval = 1000 * 10;
            _timer.Elapsed += _timer_Elapsed;
            lblDatetime.Content = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += MainWindow_ContentRendered;
            Closing += MainWindow_Closing;
            KeyDown += MainWindow_KeyDown;
            App.Globals.EventHelper.Authenticated += EventHelper_Authenticated;
            App.Globals.EventHelper.CountryCollectionChanged += EventHelper_CountryCollectionChanged;
            App.Globals.EventHelper.PostalzoneCollectionChanged += EventHelper_PostalzoneCollectionChanged;
            App.Globals.EventHelper.LocationCollectionChanged += EventHelper_LocationCollectionChanged;
            _settings.Load();
        }

        private void Authenticate()
        {
            Repository.Repositories repositories = new Repository.Repositories(Guid.Empty.ToString());
            var wa = Environment.UserDomainName + "/" + Environment.UserName;
            var user = repositories.UserRepository.GetByAccount(wa);
            if (user == null)
            {
                _dialogAuthenticate = new Dialogs.AuthenticateDialog();
                _dialogAuthenticate.Topmost = true;
                _dialogAuthenticate.ShowDialog();
                if (App.Globals.AppShare.ActiveUser == null)
                {
                    Close();
                }
                else
                {

                }
            }
            else
            {
                user.SetFullName();
                App.Globals.AppShare.ActiveUser = user;
                App.Globals.AppShare.Repositories = new Repository.Repositories(user.LicenseKey);
                App.Globals.EventHelper.RaiseAuthenticatedEvent();
            }
        }
        private void ModelLocations()
        {
            if (App.Globals.DataShare.CountryCollection == null || App.Globals.DataShare.PostalzoneCollection == null || App.Globals.DataShare.LocationCollection == null) return;
            using (new WorkerHandler("ModelLocations"))
            {
                foreach (var item in App.Globals.DataShare.LocationCollection)
                {
                    item.Postalzone = App.Globals.DataShare.PostalzoneCollection.FirstOrDefault(x => x.Id == item.PostalzoneId);
                    if (item.Postalzone != null)
                    {
                        item.Postalzone.Country = App.Globals.DataShare.CountryCollection.FirstOrDefault(x => x.Id == item.Postalzone.CountryId);                        
                    }
                }
            }
        }
        private void SetGui()
        {
            using (new WorkerHandler("SetGui"))
            {

            }
        }
        private void SetView() 
        {
            using (new WorkerHandler("SetView"))
            {
                if (cmdViewDashboard.IsChecked.HasValue)
                {
                    if (cmdViewDashboard.IsChecked.Value)
                    {
                        pageDashboard.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        pageDashboard.Visibility = Visibility.Collapsed;
                    }                   
                }
                if (cmdViewCompanies.IsChecked.HasValue)
                {
                    if (cmdViewCompanies.IsChecked.Value)
                    {
                        pageCompanies.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        pageCompanies.Visibility = Visibility.Collapsed;
                    }
                }
                if (cmdViewPersons.IsChecked.HasValue)
                {
                    if (cmdViewPersons.IsChecked.Value)
                    {
                        pagePersons.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        pagePersons.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void MainWindow_ContentRendered(object? sender, EventArgs e)
        {
            SetGui();
            Authenticate();
        }

        private void cmdViewDashboard_Click(object sender, RoutedEventArgs e)
        {
            SetView();
        }

        private void cmdViewCompanies_Click(object sender, RoutedEventArgs e)
        {
            SetView();

        }

        private void cmdViewPersons_Click(object sender, RoutedEventArgs e)
        {
            SetView();

        }
        private void EventHelper_Authenticated(object sender, EventArgs e)
        {
            _timer.Enabled = true;
            App.Globals.DataShare.ResumeEvents();
            App.Globals.DataShare.GetInitialData();
            if (App.Globals.AppShare.ActiveUser != null)
            {
                lblUser.Content = App.Globals.AppShare.ActiveUser.Fullname;
            }
        }
        private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Stop();
            Dispatcher?.BeginInvoke(new Action(() => {
                lblDatetime.Content = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            }));
            _timer.Start();
        }
        private void EventHelper_LocationCollectionChanged(object sender, EventArgs e)
        {
            ModelLocations();
        }

        private void EventHelper_PostalzoneCollectionChanged(object sender, EventArgs e)
        {
            ModelLocations();
        }

        private void EventHelper_CountryCollectionChanged(object sender, EventArgs e)
        {
            ModelLocations();
        }

        private void cmdDataCountry_Click(object sender, RoutedEventArgs e)
        {
            App.Globals.DialogHelper.ShowCountryDialog(null);
        }

        private void cmdDataPostalzone_Click(object sender, RoutedEventArgs e)
        {
            App.Globals.DialogHelper.ShowPostalzoneDialog(null);
        }

        private void cmdDatalocation_Click(object sender, RoutedEventArgs e)
        {
            App.Globals.DialogHelper.ShowLocationDialog(null);
        }
    }
}