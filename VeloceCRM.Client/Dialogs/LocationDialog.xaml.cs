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
using VeloceCRM.Entity;

namespace VeloceCRM.Client.Dialogs
{
    /// <summary>
    /// Interaction logic for LocationDialog.xaml
    /// </summary>
    public partial class LocationDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Location? _location;
        public LocationDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += LocationDialog_ContentRendered;
            Closing += LocationDialog_Closing;
            KeyDown += LocationDialog_KeyDown;
            App.Globals.EventHelper.LocationCollectionChanged += EventHelper_LocationCollectionChanged;
            _settings.Load();
            FillControls();
            dgLocation.SizeChanged += DgLocation_SizeChanged;
        }

        private void DoAdd()
        {
            _location = new Entity.Location();
            DataContext = _location;
            SetGui();
            txtName.Focus();
        }
        private void DoSave(bool CloseAfter)
        {
            _location = DataContext as Entity.Location;
            if (_location != null)
            {
                if (_location.Id == 0)
                {
                    _location = App.Globals.AppShare.Repositories.LocationRepository.Create(_location);
                }
                else
                {
                    _location = App.Globals.AppShare.Repositories.LocationRepository.Update(_location);
                }
                DataContext = _location;
                App.Globals.EventHelper.RaiseLocationSavedEvent();
                SetGui();
            }
            if (CloseAfter) 
            {
                Close();
            }
        }
        private void DoDelete() 
        {
            _location = DataContext as Entity.Location;
            if (_location == null) return;

            var message = Application.Current.Resources["postalzone.question.deletepostalzone"];
            var title = Application.Current.Resources["postalzone.question.deletepostalzone.title"];
            if (MessageBox.Show(message.ToString(), title.ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _location = App.Globals.AppShare.Repositories.LocationRepository.Delete(_location.Id);
                _location = null;
                DataContext = _location;
                SetGui();
                App.Globals.EventHelper.RaiseLocationDeletedEvent();
            }
        }

        private void FillControls()
        {
            FillPostalzones();
            FillLocations();
        }
        private void FillPostalzones()
        {
            cboPostalzones.BeginInit();
            cboPostalzones.ItemsSource = App.Globals.DataShare.PostalzoneCollection;
            cboPostalzones.DisplayMemberPath = "Zipcode";
            cboPostalzones.SelectedValuePath = "Id";
            cboPostalzones.EndInit();
        }

        private void FillLocations()
        {
            using (new WorkerHandler("FillLocations"))
            {
                List<LocationView> items = new List<LocationView>();
                if (App.Globals.DataShare.LocationCollection != null && App.Globals.DataShare.PostalzoneCollection != null)
                {
                    foreach (var item in App.Globals.DataShare.LocationCollection)
                    {
                        LocationView view = new LocationView();
                        view.Door = item.Door;
                        view.Floor = item.Floor;
                        view.House = item.House;
                        view.Id = item.Id;
                        view.Street = item.Street;
                        var pz = App.Globals.DataShare.PostalzoneCollection.FirstOrDefault(x => x.Id == item.PostalzoneId);
                        if (pz != null) view.Postzone = pz.Zipcode + " " + pz.City;
                        items.Add(view);
                    }
                    items = items.OrderBy(x => x.Postzone).ThenBy(x => x.Street).ToList();
                }
                dgLocation.BeginInit();
                dgLocation.ItemsSource = items;
                dgLocation.EndInit();
            }
        }
        private void SetGui()
        {
            using (new WorkerHandler("SetGui"))
            {
                _location = DataContext as Entity.Location;
                cmdAdd.IsEnabled = false;
                cmdSave.IsEnabled = false;
                cmdSaveClose.IsEnabled = false;
                cmdDelete.IsEnabled = false;
                if (_location != null)
                {
                    if (_location.Id == 0)
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

        }
        private void LocationDialog_KeyDown(object sender, KeyEventArgs e)
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

        private void LocationDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void LocationDialog_ContentRendered(object? sender, EventArgs e)
        {
            _location = DataContext as Entity.Location;
            SetGui();
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        private void dgLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgLocation.SelectedItem as LocationView;
            if (item != null && App.Globals.DataShare.LocationCollection != null)
            {
                _location = App.Globals.DataShare.LocationCollection.FirstOrDefault(x => x.Id == item.Id);
                DataContext = _location;
                SetGui();
                txtName.Focus();
                txtName.SelectAll();
            }
        }
        private void EventHelper_LocationCollectionChanged(object sender, EventArgs e)
        {
            FillLocations();
        }
        private void DgLocation_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgLocation.ActualWidth;
            dgLocation.Columns[1].Width = width - 48 - 32 - 32 - 140 - 4;
        }

    }
    public class LocationView
    {
        public long Id { get; set; }
        public string Street { get; set; } = "";
        public string House { get; set; } = "";
        public string? Door { get; set; }
        public string? Floor { get; set; }
        public string Postzone { get; set; } = "";
    }
}
