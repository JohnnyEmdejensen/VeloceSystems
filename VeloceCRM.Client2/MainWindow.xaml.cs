using System.Drawing.Imaging.Effects;
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

namespace VeloceCRM.Client2
{
    public partial class MainWindow : Window
    {
        private Internals.FormsettingsClass _settings;
        private System.Timers.Timer _timer;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new System.Timers.Timer();
            _timer.Interval = 1000 * 5;
            _timer.Enabled = true;
            _timer.Elapsed += _timer_Elapsed;
            _settings = new Internals.FormsettingsClass(this);
            ContentRendered += MainWindow_ContentRendered;
            Closing += MainWindow_Closing;
            KeyDown += MainWindow_KeyDown;
            App.EventHelper.UserAuthenticated += EventHelper_UserAuthenticated;
            App.EventHelper.CompanyCollectionChanged += EventHelper_CompanyCollectionChanged;
            _settings.Load();
            lblDate.Content = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            FillControls();
        }

        private void FillControls()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            FillCompaniesControl();
            Mouse.OverrideCursor = c;
        }
        private void FillCompaniesControl()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            List<Model.CompanyView> list = new List<Model.CompanyView>();
            if (App.DataShare.CompanyCollection != null)
            {
                foreach (var company in App.DataShare.CompanyCollection)
                {
                    Model.CompanyView view = new Model.CompanyView
                    {
                        Email = company.Email ?? "",
                        Name = company.Name,
                        Nickname = company.Nickname ?? "",
                        Number = company.Number,
                        Phone = company.Phone ?? "",
                        Taxnumber = company.Taxnumber,
                        Website = company.Website ?? "",
                    };
                    if (company.Location != null)
                    {
                        view.Address = company.Location.Address ?? "";
                        if (company.Location.Postalzone != null)
                        {
                            view.Zipcode = company.Location.Postalzone.Zipcode;
                            view.City = company.Location.Postalzone.City;
                            if (company.Location.Postalzone.Country != null)
                            {
                                view.Country = company.Location.Postalzone.Country.Name;
                            }
                        }
                    }
                    list.Add(view);
                }
                dgCompanies.BeginInit();
                dgCompanies.ItemsSource = list;
                dgCompanies.EndInit();
            }
            Mouse.OverrideCursor = c;
        }
        private void SetViews()
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
                pageDashboard.Visibility = Visibility.Visible;
                cmdViewDashboard.IsChecked = true;
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
            if (App.AppShare.Authenticated())
            {
                App.EventHelper.RaiseUserAuthenticatedEvent();
            }
            else
            {

            }
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void appCls_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void appMax_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            appMax.Visibility = Visibility.Collapsed;
            appRes.Visibility = Visibility.Visible;
            this.WindowState = WindowState.Maximized;
        }

        private void appRes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            appMax.Visibility = Visibility.Visible;
            appRes.Visibility = Visibility.Collapsed;
            this.WindowState = WindowState.Normal;

        }

        private void appMin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Stop();
            this.Dispatcher.BeginInvoke(new Action(() => {
                lblDate.Content = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            }));
            _timer.Start();
        }
        private void EventHelper_UserAuthenticated(object sender, EventArgs e)
        {
            if (App.AppShare.ActiveUser != null)
            {
                App.AppShare.ActiveUser.SetFullName();
                lblUser.Content = App.AppShare.ActiveUser.Fullname;
            }
        }

        private void cmdViewDashboard_Click(object sender, RoutedEventArgs e)
        {
            SetViews();
        }

        private void cmdViewCompanies_Click(object sender, RoutedEventArgs e)
        {
            SetViews();

        }

        private void cmdViewPersons_Click(object sender, RoutedEventArgs e)
        {
            SetViews();

        }
        private void EventHelper_CompanyCollectionChanged(object sender, EventArgs e)
        {
            FillCompaniesControl();
        }
    }
}