using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for DocumentDialog.xaml
    /// </summary>
    public partial class DocumentDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Document? _document;
        public DocumentDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += DocumentDialog_ContentRendered;
            Closing += DocumentDialog_Closing;
            KeyDown += DocumentDialog_KeyDown;
            FillControls();
            _settings.Load();
        }

        private void DoAdd()
        {

        }

        private void DoDelete()
        {

        }
        private void DoSave(bool CloseAfter)
        {
            _document = DataContext as Entity.Document;
            if (_document != null)
            {
                if (_document.Id == 0)
                {
                    _document = App.AppShare.Repositories.DocumentRepository.Create(_document);
                }
                else
                {
                    _document = App.AppShare.Repositories.DocumentRepository.Update(_document);
                }
                DataContext = _document;
                SetGui();
                App.EventHelper.RaiseDocumentChangedEvent();
            }
            if (CloseAfter)
            {
                Close();
            }
        }
        private void FillControls()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            FillCompanyControl();
            FillPersonControl();
            FillSalespersonControl();
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
            cboPerson.ItemsSource = App.DataShare.PersonCollection;
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
        private void SetGui()
        {
            _document = DataContext as Entity.Document;
            cmdAdd.IsEnabled = false;
            cmdSave.IsEnabled = false;
            cmdSaveClose.IsEnabled = false;
            cmdDelete.IsEnabled = false;
            if (_document != null)
            {
                if (_document.Id == 0)
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

        private void DocumentDialog_KeyDown(object sender, KeyEventArgs e)
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

        private void DocumentDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void DocumentDialog_ContentRendered(object? sender, EventArgs e)
        {
            SetGui();
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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

        private void btnFilepath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files|*.*;";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileNames != null)
            {
                if (openFileDialog.FileNames.Length == 1)
                {
                    _document = DataContext as Entity.Document;
                    if (_document != null)
                    {
                        _document.FilePath = openFileDialog.FileName;
                        FileInfo file = new FileInfo(openFileDialog.FileName);
                        if (file.Exists)
                        {
                            lblFiletype.Text = file.Extension;
                            lblFilesize.Text = file.Length.ToString();
                            _document.CreatedDate = App.ToolHelper.ConvertDateTimeToLong(file.CreationTime);
                            _document.ModifiedDate = App.ToolHelper.ConvertDateTimeToLong(file.LastWriteTime);
                            var created = App.ToolHelper.ConvertDateTimeToLong(file.CreationTime);
                            var modified = App.ToolHelper.ConvertDateTimeToLong(file.LastWriteTime);
                            txtCreated.Text = App.ToolHelper.ConvertLongDateToString(created);
                            txtUpdated.Text = App.ToolHelper.ConvertLongDateToString(modified);
                            _document.Name = file.Name;
                        }
                        DataContext = null;
                        DataContext = _document;
                        SetGui();
                    }
                }
                else
                {

                }
            }
        }
    }
}
