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
    /// Interaction logic for PersonDialog.xaml
    /// </summary>
    public partial class PersonDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Person? _person;
        public PersonDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += PersonDialog_ContentRendered;
            Closing += PersonDialog_Closing;
            KeyDown += PersonDialog_KeyDown;
            FillControls();
            _settings.Load();
        }

        private void FillControls()
        {
            FillCountryControl();
            FillCompanyControl();
        }
        private void FillCountryControl()
        {
            cboCountry.BeginInit();
            cboCountry.ItemsSource = App.DataShare.CountryCollection;
            cboCountry.DisplayMemberPath = "Name";
            cboCountry.SelectedValuePath = "Id";
            cboCountry.EndInit();
        }
        private void FillCompanyControl()
        {
            cboCompany.BeginInit();
            cboCompany.ItemsSource = App.DataShare.CompanyCollection;
            cboCompany.DisplayMemberPath = "Name";
            cboCompany.SelectedValuePath = "Id";
            cboCompany.EndInit();
        }
        
        private void DoAdd()
        {
            _person = new Entity.Person();
            SetGui();
            ShowDetail();
        }
        private void DoDelete()
        {
            _person = DataContext as Entity.Person;
            if (_person != null)
            {
                var message = App.Current.Resources["person.question.delete"].ToString();
                var title = App.Current.Resources["person.question.delete.title"].ToString();
                if (MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _person = App.AppShare.Repositories.PersonRepository.Delete(_person.Id);
                    _person = null;
                    DataContext = _person;
                    SetGui();
                    ShowDetail();
                }
            }
        }
        private void DoSave(bool CloseAfter)
        {
            _person = DataContext as Entity.Person;
            if (_person != null)
            {
                if (_person.Id == 0)
                {
                    _person = App.AppShare.Repositories.PersonRepository.Create(_person);
                }
                else
                {
                    _person = App.AppShare.Repositories.PersonRepository.Update(_person);
                }
                DataContext = _person;
                SetGui();
                ShowDetail();
            }

            if (CloseAfter)
            {
                Close();
            }
        }
        private void SetGui()
        {
            _person = DataContext as Entity.Person;
            cmdAdd.IsEnabled = false;
            cmdSave.IsEnabled = false;
            cmdSaveClose.IsEnabled = false;
            cmdDelete.IsEnabled = false;
            if (_person != null)
            {
                if (_person.Id == 0)
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
        private void ShowDetail()
        {
            _person = DataContext as Entity.Person;
            txtStreet.Text = "";
            txtHouse.Text = "";
            txtFloor.Text = "";
            txtDoor.Text = "";
            txtZipcode.Text = "";
            txtCity.Text = "";
            cboCountry.SelectedValue = null;
            if (_person != null)
            {
                if (App.DataShare.LocationCollection != null)
                {
                    var location = App.DataShare.LocationCollection.FirstOrDefault(x => x.Id == _person.LocationId);
                    if (location != null)
                    {
                        location.SetAddress();
                        txtStreet.Text = location.Street;
                        txtHouse.Text = location.House;
                        txtFloor.Text = location.Floor;
                        txtDoor.Text = location.Door;
                        if (App.DataShare.PostalzoneCollection != null)
                        {
                            var pz = App.DataShare.PostalzoneCollection.FirstOrDefault(x => x.Id == location.PostalzoneId);
                            if (pz != null)
                            {
                                txtZipcode.Text = pz.Zipcode;
                                txtCity.Text = pz.City;
                                cboCountry.SelectedValue = pz.CountryId;
                            }
                        }
                    }
                }
            }
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
        private void PersonDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
                DoDelete();
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
        private void PersonDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void PersonDialog_ContentRendered(object? sender, EventArgs e)
        {
            _person = DataContext as Entity.Person;
            SetGui();
            ShowDetail();
        }
    }
}
