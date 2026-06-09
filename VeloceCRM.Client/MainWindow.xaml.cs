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
            dgCompanies.Columns[2].Width = width - 80 - 140 - 64 - 120 - 120 - 80 - 140 - 180 - 80 - 2;
        }
        private void DgRelationPersons_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgRelationPersons.ActualWidth;
            dgRelationPersons.Columns[1].Width = width - 140 - 64 - 120 - 120 - 80 - 80 - 140 - 140 - 2;
        }
    }
}