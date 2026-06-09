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

namespace VeloceCRM.Client.Dialogs
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        Internals.FormSettingsClass _settings;
        public LoginDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += LoginDialog_ContentRendered;
            Closing += LoginDialog_Closing;
            KeyDown += LoginDialog_KeyDown;
            _settings.Load();
            txtLicense.Text = "TST";
            txtLogin.Text = "JEJ";
            txtPassword.Password = "1Lillegris";
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
            txtLicense.Focus();
        }

        private void imgCls_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string license = txtLicense.Text;
            string login = txtLogin.Text;
            string password = txtPassword.Password;
            bool save = false;
            if (chkSave.IsChecked.HasValue && chkSave.IsChecked.Value)
            {
                save = true;
            }
            var dbLicense = App.AppShare.Repositories.LicenseRepository.GetByPrefix(license);
            if (dbLicense != null)
            {
                App.AppShare.ActiveLicense = dbLicense;
                App.AppShare.Repositories = new Repository.Repositories(dbLicense.Key);
                var user = App.AppShare.Repositories.UserRepository.Authenticate(login, password);
                if (user != null)
                {
                    App.AppShare.ActiveUser = user;
                    Close();
                }
            }
        }
    }
}
