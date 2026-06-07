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
            _settings.Load();
            lblDate.Content = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
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
    }
}