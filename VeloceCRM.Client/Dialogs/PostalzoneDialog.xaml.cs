using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
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
    /// Interaction logic for PostalzoneDialog.xaml
    /// </summary>
    public partial class PostalzoneDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Postalzone? _postalzone;
        public PostalzoneDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += PostalzoneDialog_ContentRendered;
            Closing += PostalzoneDialog_Closing;
            KeyDown += PostalzoneDialog_KeyDown;
            App.Globals.EventHelper.PostalzoneCollectionChanged += EventHelper_PostalzoneCollectionChanged;
            _settings.Load();
            FillConrols();
            dgPostalzones.SizeChanged += DgPostalzones_SizeChanged;
        }


        private void DoAdd()
        {
            _postalzone = new Postalzone();
            DataContext = _postalzone;
            SetGui();
            txtName.Focus();
        }

        private void DoSave(bool CloseAfter)
        {
            _postalzone = DataContext as Entity.Postalzone;
            if (_postalzone != null)
            {
                if (_postalzone.Id == 0)
                {
                    _postalzone = App.Globals.AppShare.Repositories.PostalzoneRepository.Create(_postalzone);
                }
                else
                {
                    _postalzone = App.Globals.AppShare.Repositories.PostalzoneRepository.Update(_postalzone);
                }
                DataContext = _postalzone;
                SetGui();
                App.Globals.EventHelper.RaisePostalzoneSavedEvent();
            }
            if (CloseAfter)
            {
                Close();
            }
        }
        private void DoDelete()
        {
            _postalzone = DataContext as Entity.Postalzone;
            if (_postalzone == null) return;

            var message = Application.Current.Resources["postalzone.question.deletepostalzone"];
            var title = Application.Current.Resources["postalzone.question.deletepostalzone.title"];
            if (MessageBox.Show(message.ToString(), title.ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _postalzone = App.Globals.AppShare.Repositories.PostalzoneRepository.Delete(_postalzone.Id);
                _postalzone = null;
                DataContext = _postalzone;
                SetGui();
                App.Globals.EventHelper.RaisePostalzoneDeletedEvent();
            }

        }
        private void FillConrols()
        {
            using (new WorkerHandler("FillConrols"))
            {
                FillCountries();
                FillPostalzones();
            }
        }
        private void FillPostalzones()
        {
            using (new WorkerHandler("FillPostalzones"))
            {
                List<PostalzoneView> items = new List<PostalzoneView>();
                if (App.Globals.DataShare.PostalzoneCollection != null && App.Globals.DataShare.CountryCollection != null)
                {
                    foreach (var pz in App.Globals.DataShare.PostalzoneCollection)
                    {
                        PostalzoneView view = new PostalzoneView();
                        view.Id = pz.Id;
                        view.Zipcode = pz.Zipcode;
                        view.City = pz.City;
                        view.State = pz.State;
                        var ctry = App.Globals.DataShare.CountryCollection.FirstOrDefault(x => x.Id == pz.CountryId);
                        if (ctry != null) view.Country = ctry.Name;
                        items.Add(view);
                    }
                }
                dgPostalzones.BeginInit();
                dgPostalzones.ItemsSource = items;
                dgPostalzones.EndInit();
            }
        }
        private void FillCountries()
        {
            using (new WorkerHandler("FillCountries"))
            {
                cboCountry.BeginInit();
                cboCountry.ItemsSource = App.Globals.DataShare.CountryCollection;
                cboCountry.DisplayMemberPath = "Name";
                cboCountry.SelectedValuePath = "Id";
                cboCountry.EndInit();
            }
        }
        private void SetGui()
        {
            using (new WorkerHandler("SetGui"))
            {
                _postalzone = DataContext as Entity.Postalzone;
                cmdAdd.IsEnabled = false;
                cmdSave.IsEnabled = false;
                cmdSaveClose.IsEnabled = false;
                cmdDelete.IsEnabled = false;
                if (_postalzone != null)
                {
                    if (_postalzone.Id == 0)
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

        private void PostalzoneDialog_KeyDown(object sender, KeyEventArgs e)
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

        private void PostalzoneDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void PostalzoneDialog_ContentRendered(object? sender, EventArgs e)
        {
            _postalzone = DataContext as Entity.Postalzone;
            SetGui();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        private void dgPostalzones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgPostalzones.SelectedItem as PostalzoneView;
            if (item != null && App.Globals.DataShare.PostalzoneCollection != null)
            {
                _postalzone = App.Globals.DataShare.PostalzoneCollection.FirstOrDefault(x => x.Id == item.Id);
                DataContext = _postalzone;
                SetGui();
                txtName.Focus();
                txtName.SelectAll();
            }
        }
        private void DgPostalzones_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgPostalzones.ActualWidth;
            dgPostalzones.Columns[2].Width = width - 54 - 64 - 64 - 4;
        }
        private void EventHelper_PostalzoneCollectionChanged(object sender, EventArgs e)
        {
            FillPostalzones();
        }
    }
    public class PostalzoneView
    {
        public long Id { get; set; }
        public string Zipcode { get; set; } = "";
        public string City { get; set; } = "";
        public string? State { get; set; }
        public string Country { get; set; } = "";
    }
}
