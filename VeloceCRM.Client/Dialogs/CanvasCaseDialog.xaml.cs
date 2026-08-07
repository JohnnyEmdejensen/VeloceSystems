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
        private List<Entity.CanvasCaseParticipant> _participants = new List<Entity.CanvasCaseParticipant>();
        private List<Entity.CanvasCaseCompanyLink> _companyLinks = new List<Entity.CanvasCaseCompanyLink>();
        private List<Models.CompanyView> _companies = new List<Models.CompanyView>();   
        public CanvasCaseDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += CanvasCaseDialog_ContentRendered;
            Closing += CanvasCaseDialog_Closing;
            KeyDown += CanvasCaseDialog_KeyDown;
            dgParticipants.SizeChanged += DgParticipants_SizeChanged;
            dgCompanies.SizeChanged += DgCompanies_SizeChanged;

            _settings.Load();
            FillControls();
        }


        private void FillControls()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            FillMasterControl();
            Mouse.OverrideCursor = c;
        }

        private void FillMasterControl()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            cboMaster.BeginInit();
            cboMaster.ItemsSource = App.DataShare.UserCollection;
            cboMaster.DisplayMemberPath = "Fullname";
            cboMaster.SelectedValuePath = "Id";
            cboMaster.EndInit();
            Mouse.OverrideCursor = c;
        }
        private void DoAdd()
        {
            _canvasCase = new Entity.CanvasCase();
            DataContext = _canvasCase;
            SetGui();
        }

        private void DoSave(bool CloseAfter)
        {
            _canvasCase = DataContext as Entity.CanvasCase;
            if (_canvasCase != null)
            {
                if (_canvasCase.Id == 0)
                {
                    _canvasCase = App.AppShare.Repositories.CanvasCaseRepository.Create(_canvasCase);
                }
                else
                {
                    _canvasCase = App.AppShare.Repositories.CanvasCaseRepository.Update(_canvasCase);
                }
                DataContext = _canvasCase;
                SetGui();
            }
            if (CloseAfter)
            {
                Close();
            }
        }

        private void DoDelete()
        {

        }
        private void FillCompanies()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            dgCompanies.BeginInit();
            dgCompanies.ItemsSource = _companies;
            dgCompanies.EndInit();
            Mouse.OverrideCursor = c;
        }
        private void FillParticipants()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            dgParticipants.BeginInit();
            dgParticipants.ItemsSource = _participants;
            dgParticipants.EndInit();
            Mouse.OverrideCursor = c;
        }
        private void SetDetails()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            FillParticipants();
            FillCompanies();
            Mouse.OverrideCursor = c;
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
            if (_canvasCase != null)
            {
                txtStarts.Text =App.ToolHelper.ConvertLongDateToString(_canvasCase.Starts);
                txtEnds.Text =App.ToolHelper.ConvertLongDateToString(_canvasCase.Ends??0);
                if (App.DataShare.CanvasCaseCompanyLinkCollection != null)
                {
                    _companyLinks = App.DataShare.CanvasCaseCompanyLinkCollection.Where(x => x.CanvasCaseId == _canvasCase.Id).ToList();
                    if (_companyLinks != null && _companyLinks.Count > 0 && App.DataShare.CompanyCollection != null)
                    {
                        foreach (var item in _companyLinks)
                        {
                            var company = App.DataShare.CompanyCollection.FirstOrDefault(x => x.Id == item.CompanyId);
                            if (company != null)
                            {
                                var view = new Models.CompanyView();
                                view.Email = company.Email;
                                view.Employees = company.Employees;
                                view.FoundedYear = company.FoundedYear;
                                view.Id = company.Id;
                                view.Name = company.Name;
                                view.Nickname = company.Nickname;
                                view.Number = company.Number;
                                view.Phone = company.Phone;
                                view.Taxnumber = company.Taxnumber;
                                view.Website = company.Website;
                                if (App.DataShare.LocationCollection != null && App.DataShare.PostalzoneCollection != null && App.DataShare.CountryCollection != null)
                                {
                                    var loc = App.DataShare.LocationCollection.FirstOrDefault(X =>X .Id == company.LocationId);
                                    if (loc != null)
                                    {
                                        loc.SetAddress();
                                        view.Address = loc.Address ?? "";
                                        var postal = App.DataShare.PostalzoneCollection.FirstOrDefault(x => x.Id == loc.PostalzoneId);
                                        if (postal != null)
                                        {
                                            view.City = postal.City;
                                            view.Zipcode = postal.Zipcode;
                                            var country = App.DataShare.CountryCollection.FirstOrDefault(x => x.Id == postal.CountryId);
                                            if (country != null)
                                            {
                                                view.Country = country.Name;
                                            }
                                        }
                                    }
                                }
                                _companies.Add(view);
                            }

                        }
                    }
                }
            }
            SetDetails();
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
        private void DgParticipants_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgParticipants.ActualWidth;
            dgParticipants.Columns[2].Width = width - 120 - 16 - 8;
        }
        private void DgCompanies_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgCompanies.ActualWidth;
            dgCompanies.Columns[1].Width = width - 60 - 140 - 8;
        }

        private void cmdDeleteParticipant_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdSaveCloseParticipant_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdSaveParticipant_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdAddParticipant_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
