using Accessibility;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VeloceCRM.Entity;

namespace VeloceCRM.Client.Dialogs
{
    /// <summary>
    /// Interaction logic for TitleDialog.xaml
    /// </summary>
    public partial class TitleDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Title? _title;
        public TitleDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += TitleDialog_ContentRendered;
            Closing += TitleDialog_Closing;
            KeyDown += TitleDialog_KeyDown;
            App.EventHelper.TitleCollectionChanged += EventHelper_TitleCollectionChanged;
            FillControls();
            _settings.Load();
        }

        private void EventHelper_TitleCollectionChanged(object sender, EventArgs e)
        {
            FillTitlesControl();
        }

        private void FillControls()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            FillTitlesControl();
            Mouse.OverrideCursor = c;
        }
        private void FillTitlesControl()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            dgTitles.BeginInit();
            dgTitles.ItemsSource = App.DataShare.TitleCollection;
            dgTitles.EndInit();
            Mouse.OverrideCursor = c;
        }

        private void SetGui()
        {
            _title = DataContext as Entity.Title;
            cmdAdd.IsEnabled = false;
            cmdSave.IsEnabled = false;
            cmdSaveClose.IsEnabled = false;
            cmdDelete.IsEnabled = false;
            if (_title != null)
            {
                if (_title.Id == 0)
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

        private void TitleDialog_KeyDown(object sender, KeyEventArgs e)
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
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift) )
            {
                if (e.Key == Key.S)
                {
                    e.Handled = true;
                    DoSave(true);
                }
            }
        }

        private void TitleDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void TitleDialog_ContentRendered(object? sender, EventArgs e)
        {
            _title = DataContext as Entity.Title;
            SetGui();
        }

        private void DoAdd()
        {
            _title = new Title();
            SetGui();
        }
        private void DoDelete()
        {

        }
        private void DoSave(bool CloseAfter)
        {
            _title = DataContext as Entity.Title;
            if (_title != null)
            {
                if (_title.Id == 0)
                {
                    _title = App.AppShare.Repositories.TitleRepository.Create(_title);
                }
                else
                {
                    _title = App.AppShare.Repositories.TitleRepository.Update(_title);
                }
                App.EventHelper.RaiseTitleChangedEvent();
                DataContext = _title;
                SetGui();
            }
            if (CloseAfter)
            {
                Close();
            }
        }
        private void dgTitles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgTitles.SelectedItem as Entity.Title;
            if (item != null)
            {
                _title = item;
                DataContext = _title; 
                SetGui();
                txtKey.Focus();
                txtKey.SelectAll();
            }
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void imgCls_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
    }
}
