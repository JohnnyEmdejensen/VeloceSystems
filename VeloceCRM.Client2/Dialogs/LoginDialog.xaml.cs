using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VeloceCRM.Client2.Dialogs
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        private Internals.FormsettingsClass _settings;
        private Model.AuthenticateClass? _logindata;
        public LoginDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormsettingsClass(this);
            ContentRendered += LoginDialog_ContentRendered;
            Closing += LoginDialog_Closing;
            KeyDown += LoginDialog_KeyDown;
            _settings.Load();
        }

        private void LoginDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
        }

        private void LoginDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void LoginDialog_ContentRendered(object? sender, EventArgs e)
        {
            _logindata = DataContext as Model.AuthenticateClass;
            if (_logindata != null)
            {
                txtLicense.Text = _logindata.License;
                txtLogin.Text = _logindata.Login;
                txtPasswored.Password = _logindata.Password;
            }
            txtLicense.Focus();
        }

        private void appCls_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void appMax_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            appMax.Visibility = Visibility.Collapsed;
            appRes.Visibility = Visibility.Visible;
        }

        private void appRes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            appMax.Visibility = Visibility.Visible;
            appRes.Visibility = Visibility.Collapsed;
        }

        private void appMin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void txtLicense_TextChanged(object sender, TextChangedEventArgs e)
        {
            _logindata = DataContext as Model.AuthenticateClass;
            if (_logindata != null)
            {
                _logindata.License = txtLicense.Text;
            }
            DataContext = _logindata;
        }

        private void txtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            _logindata = DataContext as Model.AuthenticateClass;
            if (_logindata != null)
            {
                _logindata.Login = txtLogin.Text;
            }
            DataContext = _logindata;
        }

        private void txtPasswored_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _logindata = DataContext as Model.AuthenticateClass;
            if (_logindata != null)
            {
                _logindata.Password = txtPasswored.Password;
            }
            DataContext = _logindata;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var license = App.AppShare.Repositories.LicenseRepository.GetByPrefix(txtLicense.Text);
            if (license != null)
            {
                App.AppShare.ActiveLicense = license;
                App.AppShare.Repositories = new Repository.Repositories(license.Key);
                var user = App.AppShare.Repositories.UserRepository.Authenticate(txtLogin.Text, txtPasswored.Password);
                if (user != null)
                {
                    App.AppShare.ActiveUser = user;
                    Close();
                }
                else
                {

                }
            }
            else
            {

            }
        }
    }
}
