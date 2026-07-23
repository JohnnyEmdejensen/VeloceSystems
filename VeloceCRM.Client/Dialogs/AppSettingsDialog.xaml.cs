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
    /// Interaction logic for AppSettingsDialog.xaml
    /// </summary>
    public partial class AppSettingsDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Internals.AppSettingsData? _data = App.AppSettings.Settings;
        public AppSettingsDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += AppSettingsDialog_ContentRendered;
            Closing += AppSettingsDialog_Closing;
            KeyDown += AppSettingsDialog_KeyDown;
            _settings.Load();
            DataContext = _data;
        }

        private void DoSave()
        {
            App.AppSettings.Save();
        }

        private void SetGui()
        {
            txtPrivateFolder.Focus();
            txtPrivateFolder.SelectAll();
        }
        private void AppSettingsDialog_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void AppSettingsDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void AppSettingsDialog_ContentRendered(object? sender, EventArgs e)
        {
            SetGui();
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();    
        }

        private void imgCls_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DoSave();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dirPrivate_Click(object sender, RoutedEventArgs e)
        {
            if (_data != null)
            {
                _data.PrivateDocumentFolder = App.ToolHelper.PromptFolder("Select folder", "");
                txtPrivateFolder.Text = _data.PrivateDocumentFolder;
                DataContext = _data;
            }
        }

        private void dirPublic_Click(object sender, RoutedEventArgs e)
        {
            if (_data != null)
            {
                _data.PublicDocumentFolder  = App.ToolHelper.PromptFolder("Select folder", "");
                txtPublicFolder.Text = _data.PublicDocumentFolder;
                DataContext = _data;
            }

        }

        private void chkTask_Checked(object sender, RoutedEventArgs e)
        {
            if (_data != null)
            {
                _data.SendAppointmentRequestOnTasks = true;
                DataContext = _data;
            }
        }

        private void chkTask_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_data != null)
            {
                _data.SendAppointmentRequestOnTasks = false;
                DataContext = _data;
            }

        }

        private void chkPhone_Checked(object sender, RoutedEventArgs e)
        {
            if (_data != null)
            {
                _data.SendAppointmentRequestOnPhone = true;
                DataContext = _data;
            }

        }

        private void chkPhone_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_data != null)
            {
                _data.SendAppointmentRequestOnPhone = false;
                DataContext = _data;
            }

        }

        private void chkMeeting_Checked(object sender, RoutedEventArgs e)
        {
            if (_data != null)
            {
                _data.SendAppointmentRequestOnMeeting = true;
                DataContext = _data;
            }

        }

        private void chkMeeting_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_data != null)
            {
                _data.SendAppointmentRequestOnMeeting = false;
                DataContext = _data;
            }

        }
    }
}
