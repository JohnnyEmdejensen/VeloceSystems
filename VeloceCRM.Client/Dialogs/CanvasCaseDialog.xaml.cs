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
    /// Interaction logic for CanvasCaseDialog.xaml
    /// </summary>
    public partial class CanvasCaseDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.CanvasCase? _canvasCase;
        public CanvasCaseDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += CanvasCaseDialog_ContentRendered;
            Closing += CanvasCaseDialog_Closing;
            KeyDown += CanvasCaseDialog_KeyDown;
            _settings.Load();
        }

        private void DoAdd()
        {

        }

        private void DoSave(bool CloseAfter)
        {
            if (CloseAfter)
            {
                Close();
            }
        }

        private void DoDelete()
        {

        }

        private void SetGui()
        {
            _canvasCase = DataContext as Entity.CanvasCase;
            cmdAdd.IsEnabled = false;
            cmdSave.IsEnabled = false;
            cmdSaveClose.IsEnabled = false;
            cmdDelete.IsEnabled = false;
            if (_canvasCase != null)
            {
                if (_canvasCase.Id == 0)
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

        private void CanvasCaseDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void CanvasCaseDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void CanvasCaseDialog_ContentRendered(object? sender, EventArgs e)
        {
            _canvasCase = DataContext as Entity.CanvasCase;
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
