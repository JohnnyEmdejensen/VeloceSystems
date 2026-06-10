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
using System.Xaml;

namespace VeloceCRM.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Internals.FormSettingsClass _settings;
        private System.Timers.Timer _timer;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new System.Timers.Timer();
            _timer.Interval = 5000;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
            _settings = new Internals.FormSettingsClass(this);
            _settings.DialogLoaded += _settings_DialogLoaded;
            ContentRendered += MainWindow_ContentRendered;
            Closing += MainWindow_Closing;
            KeyDown += MainWindow_KeyDown;
            dgCompanies.SizeChanged += DgCompanies_SizeChanged;
            dgRelationPersons.SizeChanged += DgRelationPersons_SizeChanged;
            App.EventHelper.CompanyCollectionChanged += EventHelper_CompanyCollectionChanged;
            App.EventHelper.ActiveCompanyChanged += EventHelper_ActiveCompanyChanged;
            _settings.Load();
            lblDate.Content = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        }

        private void ShowView()
        {
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
            if (pageCompanies.Visibility == Visibility.Collapsed && pageDashboard.Visibility == Visibility.Collapsed && pagePersons.Visibility == Visibility.Collapsed)
            {
                cmdViewDashboard.IsChecked = true;
                pageDashboard.Visibility = Visibility.Visible;
            }
        }
        private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Stop();
            Dispatcher.BeginInvoke(new Action(() => {
                lblDate.Content = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            }));
            _timer.Start();
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
            if (App.AppShare.Authenticated())
            {
                if (App.AppShare.ActiveUser != null)
                {
                    App.AppShare.ActiveUser.SetFullName();
                    lblUser.Content = App.AppShare.ActiveUser.Fullname;
                    App.DataShare.ResumeEvents();
                    App.DataShare.GetData();
                }
            }
            else
            {

            }
        }

        private void imgMin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void imgMax_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Maximized;
            imgRes.Visibility = Visibility.Visible;
            imgMax.Visibility = Visibility.Collapsed;
        }

        private void imgRes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Normal;
            imgRes.Visibility = Visibility.Collapsed;
            imgMax.Visibility = Visibility.Visible;
        }

        private void imgCls_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                WindowState = WindowState.Maximized;
                imgMax.Visibility = Visibility.Collapsed;
                imgRes.Visibility = Visibility.Visible;
            }
            else
            {
                this.DragMove();
            }
        }
        private void _settings_DialogLoaded(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                imgMax.Visibility = Visibility.Collapsed;
                imgRes.Visibility = Visibility.Visible;
            }
        }

        private void cmdViewDashboard_Click(object sender, RoutedEventArgs e)
        {
            ShowView();
        }

        private void cmdViewCompanies_Click(object sender, RoutedEventArgs e)
        {
            ShowView();
        }

        private void cmdViewPersons_Click(object sender, RoutedEventArgs e)
        {
            ShowView();
        }
        private void DgCompanies_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgCompanies.ActualWidth;
            dgCompanies.Columns[2].Width = width - 80 - 140 - 64 - 120 - 120 - 80 - 140 - 180 - 80 - 48 - 48 - 2;
        }
        private void DgRelationPersons_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgRelationPersons.ActualWidth;
            dgRelationPersons.Columns[1].Width = width - 140 - 64 - 120 - 120 - 80 - 80 - 140 - 140 - 2;
        }
        private void EventHelper_CompanyCollectionChanged(object sender, EventArgs e)
        {
            List<Models.CompanyView> list = new List<Models.CompanyView>();
            if (App.DataShare.CompanyCollection != null)
            {
                foreach (var company in App.DataShare.CompanyCollection)
                {
                    Models.CompanyView view = new Models.CompanyView
                    {
                        Id = company.Id,
                        Number = company.Number,
                        Taxnumber = company.Taxnumber,
                        Name = company.Name,
                        Nickname = company.Nickname,
                        Phone = company.Phone,
                        Email = company.Email,
                        Website = company.Website,
                        Employees = company.Employees,
                        FoundedYear = company.FoundedYear,
                    };
                    if (App.DataShare.LocationCollection != null)
                    {
                        company.Location = App.DataShare.LocationCollection.FirstOrDefault(x => x.Id == company.LocationId);
                        if (company.Location != null)
                        {
                            view.Address = company.Location.Address ?? "";
                            if (App.DataShare.PostalzoneCollection != null)
                            {
                                company.Location.Postalzone = App.DataShare.PostalzoneCollection.FirstOrDefault(x => x.Id == company.Location.PostalzoneId);
                                if (company.Location.Postalzone != null)
                                {
                                    view.Zipcode = company.Location.Postalzone.Zipcode;
                                    view.City = company.Location.Postalzone.City;
                                    if (App.DataShare.CountryCollection != null)
                                    {
                                        company.Location.Postalzone.Country = App.DataShare.CountryCollection.FirstOrDefault(x => x.Id == company.Location.Postalzone.CountryId);
                                        if (company.Location.Postalzone.Country != null)
                                        {
                                            view.Country = company.Location.Postalzone.Country.Name;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    list.Add(view);
                }
            }
            dgCompanies.BeginInit();
            dgCompanies.ItemsSource = list;
            dgCompanies.EndInit();
        }

        private void dgCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgCompanies.SelectedItem as Models.CompanyView;
            if (item != null && App.DataShare.CompanyCollection != null)
            {
                App.AppShare.ActiveCompany =App.DataShare.CompanyCollection.FirstOrDefault(x => x.Id == item.Id);
                App.EventHelper.RaiseActiveCompanyChangedEvent();
            }
        }

        private void dgCompanies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = dgCompanies.SelectedItem as Models.CompanyView;
            if (item != null && App.DataShare.CompanyCollection != null)
            {
                App.DialogHelper.ShowCompanyDialog(App.DataShare.CompanyCollection.FirstOrDefault(x => x.Id == item.Id));
            }

        }
        private void EventHelper_ActiveCompanyChanged(object sender, EventArgs e)
        {
            if (App.AppShare.ActiveCompany != null)
            {
                lblCompany.Content = App.AppShare.ActiveCompany.Name;
            }
        }

        private void cmdNewCompany_Click(object sender, RoutedEventArgs e)
        {
            App.DialogHelper.ShowCompanyDialog(new Entity.Company());
        }

        private void cmdDataLocations_Click(object sender, RoutedEventArgs e)
        {
            App.DialogHelper.ShowLocationsDialog();
        }
    }
}