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
            _settings.Load();
        }

        private void Authenticate()
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
    }
}