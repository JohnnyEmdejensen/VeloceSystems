using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VeloceCRM.Entity;

namespace VeloceCRM.Client.Dialogs
{
    /// <summary>
    /// Interaction logic for ActivityDialog.xaml
    /// </summary>
    public partial class ActivityDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Actitiy? _actitiy;
        public ActivityDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += ActivityDialog_ContentRendered;
            Closing += ActivityDialog_Closing;
            KeyDown += ActivityDialog_KeyDown;
            
            Fillcontrols();
            _settings.Load();
        }

        private void DoAdd()
        {
            _actitiy = new Entity.Actitiy();
            if (App.AppShare.ActiveCompany != null) _actitiy.CompanyId = App.AppShare.ActiveCompany.Id;
            if (App.AppShare.ActivePerson != null) _actitiy.PersonId = App.AppShare.ActivePerson.Id;
            if (App.AppShare.ActiveUser != null) _actitiy.SalespersonId = App.AppShare.ActiveUser.Id;

        }

        private void DoDelete()
        {
        }
        public void DoSave(bool CloseAfter)
        {
            _actitiy = DataContext as Entity.Actitiy;
            if (_actitiy != null)
            {
                if (!DataValidated())
                {
                    string? message = Application.Current.Resources["activity.information.data.invalid"].ToString();
                    string? title = Application.Current.Resources["activity.information.data.invalid.title"].ToString();
                    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _actitiy.Starts = App.ToolHelper.ConvertDateTimeToLong(Convert.ToDateTime(txtStartsDate.Text + " " + txtStartsTime.Text + ":00"));
                    _actitiy.Ends = App.ToolHelper.ConvertDateTimeToLong(Convert.ToDateTime(txtEndsDate.Text + " " + txtEndsTime.Text + ":00"));
                    if (_actitiy.Id == 0)
                    {
                        _actitiy = App.AppShare.Repositories.ActivityRepository.Create(_actitiy);
                    }
                    else
                    {
                        _actitiy = App.AppShare.Repositories.ActivityRepository.Update(_actitiy);
                    }
                    DataContext = _actitiy;
                    SetGui();
                    ShowDetails();
                }
            }
            if (CloseAfter)
            {
                Close();
            }
        }

        private void Fillcontrols()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            FillCompanyControl();
            FillPersonControl();
            FillSalespersonControl();
            FillFollowuptypeControl();
            Mouse.OverrideCursor = c;
        }

        private void FillFollowuptypeControl()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            cboFollowuptype.BeginInit();
            cboFollowuptype.ItemsSource = App.DataShare.FollowuptypeCollection;
            cboFollowuptype.DisplayMemberPath = "Text";
            cboFollowuptype.SelectedValuePath = "Id";
            cboFollowuptype.EndInit();
            Mouse.OverrideCursor = c;
        }
        private void FillCompanyControl()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            cboCompany.BeginInit();
            cboCompany.ItemsSource = App.DataShare.CompanyCollection;
            cboCompany.DisplayMemberPath = "Name";
            cboCompany.SelectedValuePath = "Id";
            cboCompany.EndInit();
            Mouse.OverrideCursor = c;
        }
        private void FillPersonControl()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            cboPerson.BeginInit();
            if (App.AppShare.ActiveCompany != null)
            {
                if (App.DataShare.PersonCollection != null)
                    cboPerson.ItemsSource = App.DataShare.PersonCollection.Where(x => x.CompanyId == App.AppShare.ActiveCompany.Id).ToList();
            }
            else
            {
                cboPerson.ItemsSource = App.DataShare.PersonCollection;
            }
            cboPerson.DisplayMemberPath = "Fullname";
            cboPerson.SelectedValuePath = "Id";
            cboPerson.EndInit();
            Mouse.OverrideCursor = c;
        }
        private void FillSalespersonControl()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            cboSalesperson.BeginInit();
            cboSalesperson.ItemsSource = App.DataShare.UserCollection;
            cboSalesperson.DisplayMemberPath = "Fullname";
            cboSalesperson.SelectedValuePath = "Id";
            cboSalesperson.EndInit();
            Mouse.OverrideCursor = c;
        }
        private void ShowDetails()
        {
            _actitiy = DataContext as Entity.Actitiy;
            if (_actitiy == null) return;

            radTask.IsChecked = false;
            radPhone.IsChecked = false;
            radMeeting.IsChecked = false;
            if (_actitiy != null)
            {
                if (_actitiy.ActivityType == 0) radTask.IsChecked = true;
                if (_actitiy.ActivityType == 1) radPhone.IsChecked = true;
                if (_actitiy.ActivityType == 2) radMeeting.IsChecked = true;
            }
            txtStartsDate.Text = App.ToolHelper.ConvertLongDateToString(_actitiy.Starts);
            txtStartsTime.Text = App.ToolHelper.ConvertLongTimeToString(_actitiy.Starts);
            txtEndsDate.Text = App.ToolHelper.ConvertLongDateToString(_actitiy.Ends);
            txtEndsTime.Text = App.ToolHelper.ConvertLongTimeToString(_actitiy.Ends);
        }
        private bool DataValidated()
        {
            bool result = true;
            _actitiy = DataContext as Entity.Actitiy;
            if (_actitiy != null)
            {
                if (_actitiy.Reason == "") result = false;
                if (_actitiy.Subject == "") result = false;
                if (_actitiy.Starts == 0) result = false;
                if (_actitiy.Ends == 0) result = false;
            }
            return result;
        }

        private void SetGui()
        {
            _actitiy = DataContext as Entity.Actitiy;
            cmdAdd.IsEnabled = false;
            cmdSave.IsEnabled = false;
            cmdSaveClose.IsEnabled = false;
            cmdDelete.IsEnabled = false;
            if (_actitiy != null)
            {
                if (_actitiy.Id == 0)
                {
                    cmdSave.IsEnabled = true;
                    cmdSaveClose.IsEnabled = true;
                }
                else
                {
                    cmdSave.IsEnabled = true;
                    cmdSaveClose.IsEnabled = true;
                    cmdDelete.IsEnabled = true;
                    cmdAdd.IsEnabled = true;
                }
            }
            else
            {
                cmdAdd.IsEnabled = true;
            }
        }
        private void ActivityDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
        }

        private void ActivityDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void ActivityDialog_ContentRendered(object? sender, EventArgs e)
        {
            _actitiy = DataContext as Entity.Actitiy;
            SetGui();
            ShowDetails();
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            DoAdd();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave(false);
        }

        private void cmdSaveClose_Click(object sender, RoutedEventArgs e)
        {
            DoSave(true);
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            DoDelete();
        }

        private void imgCls_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void txtStartsDate_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtStartsTime_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtEndsDate_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtEndsTime_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void lblFollowoupType_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.DialogHelper.ShowFollowuptypeDialog(new());
        }
    }
}
