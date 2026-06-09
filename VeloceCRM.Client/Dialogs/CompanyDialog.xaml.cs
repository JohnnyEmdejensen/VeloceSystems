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
    /// Interaction logic for CompanyDialog.xaml
    /// </summary>
    public partial class CompanyDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Company? _company;
        public CompanyDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += CompanyDialog_ContentRendered;
            Closing += CompanyDialog_Closing;
            KeyDown += CompanyDialog_KeyDown;
            FillControls();
            _settings.Load();
        }
        private void DoAdd()
        {
            _company = new Entity.Company();
            DataContext = _company;
            txtStreet.Text = "";
            txtHouse.Text = "";
            txtFloor.Text = "";
            txtDoor.Text = "";
            txtCity.Text = "";
            txtZicode.Text = "";
            cboCountry.SelectedItem = null;
            SetGui();
            txtNumber.Focus();
        }
        private void DoDelete()
        {
            _company = DataContext as Entity.Company;
            if (_company != null)
            {
                var message = Application.Current.Resources["company.question.delete"].ToString();
                var title = Application.Current.Resources["company.question.delete.title"].ToString();
                if (MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _company = App.AppShare.Repositories.CompanyRepository.Delete(_company.Id);
                    _company = null;
                    DataContext = _company;
                    txtStreet.Text = "";
                    txtHouse.Text = "";
                    txtFloor.Text = "";
                    txtDoor.Text = "";
                    txtCity.Text = "";
                    txtZicode.Text = "";
                    cboCountry.SelectedItem = null;
                    App.EventHelper.RaiseCompanyChangedEvent();
                    SetGui();
                    txtNumber.Focus();
                }
            }
        }
        private void DoSave(bool CloseAfter)
        {
            _company = DataContext as Entity.Company;
            if (_company != null)
            {
                if (_company.LocationId == 0)
                {
                    var location = App.ToolHelper.ParseLocation(txtStreet.Text, txtHouse.Text, txtFloor.Text, txtDoor.Text, txtZicode.Text, txtCity.Text, cboCountry.Text);
                    if (location != null)
                    {
                        _company.LocationId = location.Id;
                    }
                }
                if (_company.Id == 0)
                {
                    _company = App.AppShare.Repositories.CompanyRepository.Create(_company);
                }
                else
                {
                    _company = App.AppShare.Repositories.CompanyRepository.Update(_company);
                }
                DataContext = _company;
                App.EventHelper.RaiseCompanyChangedEvent();
                SetGui();
            }
            if (CloseAfter)
            {
                Close();
            }
        }
        private void FillControls()
        {
            cboCountry.BeginInit();
            cboCountry.ItemsSource = App.DataShare.CountryCollection;
            cboCountry.DisplayMemberPath = "Name";
            cboCountry.SelectedValuePath = "Id";
            cboCountry.EndInit();
        }
        private void SetGui()
        {
            _company = DataContext as Entity.Company;
            cmdAdd.IsEnabled = false;
            cmdSave.IsEnabled = false;
            cmdSaveClose.IsEnabled = false;
            cmdDelete.IsEnabled = false;
            if (_company != null)
            {
                if (_company.Id == 0)
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
        private void CompanyDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                if (e.Key == Key.S)
                {
                    e.Handled = true;
                    DoSave(false);
                }
                if (e.Key == Key.N)
                {
                    e.Handled = true;   
                    DoAdd();
                }
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                if (e.Key == Key.S)
                {
                    e.Handled = true;
                    DoSave(true);
                }
            }
        }

        private void CompanyDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void CompanyDialog_ContentRendered(object? sender, EventArgs e)
        {
            _company = DataContext as Entity.Company;
            if (_company != null)
            {
                txtNumber.Focus();
                if (App.DataShare.LocationCollection != null)
                {
                    var location = App.DataShare.LocationCollection.FirstOrDefault(x => x.Id == _company.LocationId);
                    if (location != null)
                    {
                        txtStreet.Text = location.Street;
                        txtHouse.Text = location.House;
                        txtFloor.Text = location.Floor;
                        txtDoor.Text = location.Door;
                        if (App.DataShare.PostalzoneCollection != null)
                        {
                            var pz = App.DataShare.PostalzoneCollection.FirstOrDefault(x => x.Id == location.PostalzoneId);
                            if (pz != null)
                            {
                                txtZicode.Text = pz.Zipcode;
                                txtCity.Text = pz.City;
                                cboCountry.SelectedValue = pz.CountryId;
                            }
                        }
                    }
                }
            }
            SetGui();
        }

        private void imgCls_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            DoDelete();
        }

        private void cmdSaveClose_Click(object sender, RoutedEventArgs e)
        {
            DoSave(true);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave(false);
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            DoAdd();
        }
    }
}
