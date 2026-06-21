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
using VeloceCRM.Entity;

namespace VeloceCRM.Client.Dialogs
{
    /// <summary>
    /// Interaction logic for FollowuptypeDialog.xaml
    /// </summary>
    public partial class FollowuptypeDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Followuptype? _followuptype;
        public FollowuptypeDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += FollowuptypeDialog_ContentRendered;
            Closing += FollowuptypeDialog_Closing;
            KeyDown += FollowuptypeDialog_KeyDown;
            dgFollowuptypes.SizeChanged += DgFollowuptypes_SizeChanged;
            App.EventHelper.FollowuptypeCollectionChanged += EventHelper_FollowuptypeCollectionChanged;
            _settings.Load();
            FillControls();
        }


        private void FillControls()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            FillFollowuptypeControl();
            Mouse.OverrideCursor = c;
        }

        private void FillFollowuptypeControl()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            dgFollowuptypes.BeginInit();
            dgFollowuptypes.ItemsSource = App.DataShare.FollowuptypeCollection;
            dgFollowuptypes.EndInit();
            Mouse.OverrideCursor = c;
        }
        private void SetGui()
        {
            _followuptype = DataContext as Entity.Followuptype;
            cmdAdd.IsEnabled = false;
            cmdSave.IsEnabled = false;
            cmdSaveClose.IsEnabled = false;
            cmdDelete.IsEnabled = false;
            if (_followuptype != null)
            {
                if (_followuptype.Id == 0)
                {
                    cmdSave.IsEnabled = true;
                    cmdSaveClose.IsEnabled = true;
                    txtKey.Focus();
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

        private void DoAdd()
        {
            _followuptype = new Followuptype();
            DataContext = _followuptype;
            txtKey.Focus();
        }
        private void DoDelete()
        {
            _followuptype = DataContext as Entity.Followuptype;
            if (_followuptype != null)
            {
                var message = Application.Current.Resources["followuptype.question.delete"].ToString();
                var title = Application.Current.Resources["followuptype.question.delete.title"].ToString();
                if (MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _followuptype = App.AppShare.Repositories.FollowuptypeRepository.Delete(_followuptype.Id);
                    _followuptype = null;
                    DataContext = _followuptype;
                    SetGui();
                }
            }

        }
        private void DoSave(bool CloseAfter)
        {
            _followuptype = DataContext as Entity.Followuptype;
            if (_followuptype != null)
            {
                if (_followuptype.Id == 0)
                {
                    _followuptype = App.AppShare.Repositories.FollowuptypeRepository.Create(_followuptype);
                }
                else
                {
                    _followuptype = App.AppShare.Repositories.FollowuptypeRepository.Update(_followuptype);
                }
                App.EventHelper.RaiseFollowuptypeChangedEvent();
                DataContext = _followuptype;
                SetGui();
            }
            if (CloseAfter)
            {
                Close();
            }
        }
        private void imgCls_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void FollowuptypeDialog_KeyDown(object sender, KeyEventArgs e)
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
                    DoAdd();
                }
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                if (e.Key == Key.S)
                {
                    DoSave(true);
                }
            }
        }

        private void FollowuptypeDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void FollowuptypeDialog_ContentRendered(object? sender, EventArgs e)
        {
            _followuptype = DataContext as Entity.Followuptype;
            SetGui();
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

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void dgFollowuptypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _followuptype = dgFollowuptypes.SelectedItem as Entity.Followuptype;
            DataContext = _followuptype;
            SetGui();
            txtKey.Focus();
        }
        private void DgFollowuptypes_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgFollowuptypes.ActualWidth;
            dgFollowuptypes.Columns[2].Width = width - 48 - 48 - 24 - 4;
        }
        private void EventHelper_FollowuptypeCollectionChanged(object sender, EventArgs e)
        {
            FillFollowuptypeControl();
        }
    }
}
