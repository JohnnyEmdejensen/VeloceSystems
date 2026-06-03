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
using VeloceCRM.Client.Internals;

namespace VeloceCRM.Client.Dialogs
{
    /// <summary>
    /// Interaction logic for CountryDialog.xaml
    /// </summary>
    public partial class CountryDialog : Window
    {
        private Internals.FormSettingsClass _setttings;
        private Entity.Country? _country;

        public CountryDialog()
        {
            InitializeComponent();
            _setttings = new Internals.FormSettingsClass(this);
            ContentRendered += CountryDialog_ContentRendered;
            Closing += CountryDialog_Closing;
            KeyDown += CountryDialog_KeyDown;
            _setttings.Load();
            dgCountries.SizeChanged += DgCountries_SizeChanged;
            FillControls();
            App.Globals.EventHelper.CountryCollectionChanged += EventHelper_CountryCollectionChanged;
        }

        private void DoAdd()
        {
            _country = new Entity.Country();
            DataContext = _country;
            SetGui();
            txtName.Focus();
        }

        private void DoSave(bool CloseAfter)
        {
            _country = DataContext as Entity.Country;
            if (_country != null)
            {
                if (_country.Id == 0)
                {
                    _country = App.Globals.AppShare.Repositories.CountryRepository.Create(_country);
                }
                else
                {
                    _country = App.Globals.AppShare.Repositories.CountryRepository.Update(_country);
                }
                App.Globals.EventHelper.RaiseCountrySavedEvent();
                DataContext = _country;
                SetGui();
            }

            if (CloseAfter)
            {
                Close();
            }
        }

        private void DoDelete()
        {
            _country = DataContext as Entity.Country;
            if (_country == null) return;

            var message = Application.Current.Resources["country.question.deletecountry"];
            var title = Application.Current.Resources["country.question.deletecountry.title"];
            if (MessageBox.Show(message.ToString(), title.ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _country = App.Globals.AppShare.Repositories.CountryRepository.Delete(_country.Id);
                App.Globals.EventHelper.RaiseCountryDeletedEvent();
            }
        }

        private void FillControls()
        {
            using (new WorkerHandler("FillControls"))
            {
                FillCountries();
            }
        }
        private void FillCountries()
        {
            using (new WorkerHandler("FillCountries"))
            {
                dgCountries.BeginInit();
                dgCountries.ItemsSource = App.Globals.DataShare.CountryCollection;
                dgCountries.EndInit();
            }
        }
        private void SetDetails()
        {
            _country = DataContext as Entity.Country;

            txtName.Focus();
            txtName.SelectAll();
        }
        private void SetGui()
        {
            _country = DataContext as Entity.Country;
            cmdAdd.IsEnabled = false;
            cmdSave.IsEnabled = false;
            cmdSaveClose.IsEnabled = false;
            cmdDelete.IsEnabled = false;
            if (_country != null)
            {
                if (_country.Id == 0)
                {
                    cmdSave.IsEnabled = true;
                    cmdSaveClose.IsEnabled = true;
                }
                else
                {
                    cmdSave.IsEnabled = true;
                    cmdSaveClose.IsEnabled = true;
                    cmdAdd.IsEnabled = true;
                    cmdDelete.IsEnabled = true;
                }
            }
            else
            {
                cmdAdd.IsEnabled = true;
            }
        }

        private void CountryDialog_KeyDown(object sender, KeyEventArgs e)
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
                if (e.Key == Key.D)
                {
                    e.Handled = true;
                    DoDelete();
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

        private void CountryDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _setttings.Save();
        }

        private void CountryDialog_ContentRendered(object? sender, EventArgs e)
        {
            SetGui();
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
        private void DgCountries_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgCountries.ActualWidth;
            dgCountries.Columns[1].Width = width - 32-32-4;
        }

        private void dgCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _country = dgCountries.SelectedItem as Entity.Country;
            DataContext = _country;
            SetDetails();
            SetGui();
        }
        private void EventHelper_CountryCollectionChanged(object sender, EventArgs e)
        {
            FillCountries();
        }
    }
}
