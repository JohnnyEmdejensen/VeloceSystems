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
    /// Interaction logic for AuthenticateDialog.xaml
    /// </summary>
    public partial class AuthenticateDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private AuthenticateDataClass? _dataClass;
        public AuthenticateDialog()
        {
            InitializeComponent();
            _dataClass = new AuthenticateDataClass();
            DataContext = _dataClass;
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += AuthenticateDialog_ContentRendered;
            Closing += AuthenticateDialog_Closing;
            KeyDown += AuthenticateDialog_KeyDown;
            _settings.Load();
        }



        private void AuthenticateDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
        }

        private void AuthenticateDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void AuthenticateDialog_ContentRendered(object? sender, EventArgs e)
        {
            txtLicense.Focus();
            txtLicense.SelectAll();
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            _dataClass = DataContext as AuthenticateDataClass;
            if (_dataClass != null)
            {
                if (!string.IsNullOrEmpty(_dataClass.License) && ! string.IsNullOrEmpty(_dataClass.Login) && !string.IsNullOrEmpty(_dataClass.Password))
                {
                    Repository.Repositories repositories = new Repository.Repositories(Guid.Empty.ToString());
                    Internals.WorkerHandler workerHandler = new Internals.WorkerHandler("Authentication");
                    var license = repositories.LicenseRepository.GetByPrefix(_dataClass.License);
                    if (license != null)
                    {
                        repositories = new Repository.Repositories(license.Key);
                        var user = repositories.UserRepository.Authenticate(_dataClass.Login, _dataClass.Password);
                        if (user != null)
                        {
                            user.SetFullName();
                            App.Globals.AppShare.ActiveUser = user;
                            App.Globals.AppShare.Repositories = new Repository.Repositories(license.Key);
                            App.Globals.EventHelper.RaiseAuthenticatedEvent();
                            workerHandler.Dispose();
                            Close();
                        }
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

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _dataClass = DataContext as AuthenticateDataClass;
            if (_dataClass != null)
            {
                _dataClass.Password = txtPassword.Password;
                DataContext = _dataClass;
            }
        }
    }
    public class AuthenticateDataClass
    {
        public string License { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool SaveLogin { get; set; } = false;
    }
}
