using Pomelo.EntityFrameworkCore.MySql.Query.ExpressionTranslators.Internal;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xaml;

namespace VeloceCRM.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Internals.FormSettingsClass _settings;
        private System.Timers.Timer _timer;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new System.Timers.Timer();
            _timer.Interval = 5000;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
            _settings = new Internals.FormSettingsClass(this);
            _settings.DialogLoaded += _settings_DialogLoaded;
            ContentRendered += MainWindow_ContentRendered;
            Closing += MainWindow_Closing;
            KeyDown += MainWindow_KeyDown;
            dgCompanies.SizeChanged += DgCompanies_SizeChanged;
            dgRelationPersons.SizeChanged += DgRelationPersons_SizeChanged;
            App.EventHelper.CompanyCollectionChanged += EventHelper_CompanyCollectionChanged;
            App.EventHelper.PersonCollectionChanged += EventHelper_PersonCollectionChanged;
            App.EventHelper.ActivityCollectionChanged += EventHelper_ActivityCollectionChanged;
            App.EventHelper.ActiveCompanyChanged += EventHelper_ActiveCompanyChanged;
            App.EventHelper.ActivePersonChanged += EventHelper_ActivePersonChanged;
            dgRelationActivities.SizeChanged += DgRelationActivities_SizeChanged;
            _settings.Load();
            lblDate.Content = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            pageDashboard.IsSelected = true;
            ResumeSettingValues();
        }

        private void FillActivityControl()
        {
            List<Models.ActivityView> list = new List<Models.ActivityView>();
            if (App.DataShare.ActitiyCollection != null)
            {
                foreach (var activity in App.DataShare.ActitiyCollection)
                {
                    Models.ActivityView view = new Models.ActivityView
                    {
                        Id = activity.Id,
                        StartDate = App.ToolHelper.ConvertLongDateToString(activity.Starts),
                        StartTime = App.ToolHelper.ConvertLongTimeToString(activity.Starts),
                        EndDate = App.ToolHelper.ConvertLongDateToString(activity.Ends),
                        EndTime = App.ToolHelper.ConvertLongTimeToString(activity.Ends),
                        Subject = activity.Subject,
                        IsCompleted = activity.IsCompleted,
                        Reason = activity.Reason,
                        Conclution = activity.Conclution,
                        TookMinutes = activity.TookMinutes,
                        BilledMinutes = activity.BilledMinutes,
                        TypeStr = "T"
                    };
                    if (activity.ActivityType == 1) view.TypeStr = "P";
                    if (activity.ActivityType == 2) view.TypeStr = "M";
                    if (App.DataShare.CompanyCollection != null)
                    {
                        var company = App.DataShare.CompanyCollection.FirstOrDefault(x => x.Id == activity.CompanyId);
                        if (company != null)
                            view.Company = company.Name;
                    }
                    if (App.DataShare.PersonCollection != null)
                    {
                        var person = App.DataShare.PersonCollection.FirstOrDefault(x => x.Id == activity.PersonId);
                        if(person != null)
                        {
                            person.SetFullName();
                            view.Person = person.Fullname;
                        }
                    }
                    if (App.DataShare.UserCollection != null)
                    {
                        var salesperson = App.DataShare.UserCollection.FirstOrDefault(x => x.Id == activity.SalespersonId);
                        if (salesperson != null)
                        {
                            salesperson.SetFullName();
                            view.Salesperson = salesperson.Fullname ?? "";
                        }
                    }
                    if (App.DataShare.FollowuptypeCollection != null)
                    {
                        var followup = App.DataShare.FollowuptypeCollection.FirstOrDefault(x => x.Id == activity.FollowuptypeId);
                        if (followup != null)
                        {
                            view.Followuptype = followup.Text;
                        }
                    }
                    if (App.AppShare.ActiveCompany != null)
                    {
                        if (activity.CompanyId == App.AppShare.ActiveCompany.Id)
                        {
                            list.Add(view);
                        }
                    }
                    else
                    {
                        list.Add(view);
                    }
                }
                dgRelationActivities.BeginInit();
                dgRelationActivities.ItemsSource = list;
                dgRelationActivities.EndInit();
            }
        }
        private void FillPersonControl()
        {
            List<Models.PersonView> list = new List<Models.PersonView>();
            if (App.DataShare.PersonCollection != null)
            {
                foreach (var person in App.DataShare.PersonCollection)
                {
                    person.SetFullName();
                    Models.PersonView view = new Models.PersonView
                    {
                        Id = person.Id,
                        Firstname = person.Firstname,
                        Middlename = person.Middlename,
                        Surname = person.Surname,
                        Phone = person.Phone,
                        Mobile = person.Mobile,
                        Email = person.Email,
                        Fullname = person.Fullname,
                        CompanyId = person.CompanyId,
                    };
                    if (App.DataShare.CompanyCollection != null)
                    {
                        var company = App.DataShare.CompanyCollection.FirstOrDefault(x => x.Id == person.CompanyId);
                        if (company != null)
                        {
                            view.Company = company.Name;
                        }
                    }
                    if (App.DataShare.LocationCollection != null)
                    {
                        var location = App.DataShare.LocationCollection.FirstOrDefault(x => x.Id == person.LocationId);
                        if (location != null)
                        {
                            location.SetAddress();
                            view.Address = location.Address;
                            if (App.DataShare.PostalzoneCollection != null)
                            {
                                var pz = App.DataShare.PostalzoneCollection.FirstOrDefault(x => x.Id == location.PostalzoneId);
                                if (pz != null)
                                {
                                    view.Zipcode = pz.Zipcode;
                                    view.City = pz.City;
                                    if (App.DataShare.CountryCollection != null)
                                    {
                                        var ctry = App.DataShare.CountryCollection.FirstOrDefault(x => x.Id == pz.CountryId);
                                        if (ctry != null)
                                        {
                                            view.Country = ctry.Name;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (App.AppShare.ActiveCompany != null)
                    {
                        if (App.AppShare.ActiveCompany.Id == view.CompanyId)
                        {
                            list.Add(view);
                        }
                    }
                    else
                    {
                        list.Add(view);
                    }
                }
            }
            dgRelationPersons.BeginInit();
            dgRelationPersons.ItemsSource = list;
            dgRelationPersons.EndInit();
        }
        private void ResumeSettingValues()
        {
            var checkedDashboardItem = _settings.KeyValues.FirstOrDefault(x => x.Key == "CheckedDashboard");
            var checkedCompaniesItem = _settings.KeyValues.FirstOrDefault(x => x.Key == "CheckedCompanies");
            var checkedPersonsItem = _settings.KeyValues.FirstOrDefault(x => x.Key == "CheckedPersons");
            if (checkedDashboardItem != null)
            {
                if (Convert.ToBoolean(checkedDashboardItem.Value))
                {
                    cmdViewDashboard.IsChecked = true;
                    pageDashboard.Visibility = Visibility.Visible;
                }
                else
                {
                    cmdViewDashboard.IsChecked = false;
                    pageDashboard.Visibility = Visibility.Collapsed;
                }
            }
            if (checkedCompaniesItem != null)
            {
                if (Convert.ToBoolean(checkedCompaniesItem.Value))
                {
                    cmdViewCompanies.IsChecked = true;
                    pageCompanies.Visibility = Visibility.Visible;
                }
                else
                {
                    cmdViewCompanies.IsChecked = false;
                    pageCompanies.Visibility = Visibility.Collapsed;
                }
            }
            if (checkedPersonsItem != null)
            {
                if (Convert.ToBoolean(checkedPersonsItem.Value))
                {
                    cmdViewPersons.IsChecked = true;
                    pagePersons.Visibility = Visibility.Visible;
                }
                else
                {
                    cmdViewPersons.IsChecked = false;
                    pagePersons.Visibility = Visibility.Collapsed;
                }
            }
            pageDashboard.IsSelected = true;
            var selectedView = _settings.KeyValues.FirstOrDefault(x => x.Key == "SelectedView");
            if (selectedView != null)
            {
                if (selectedView.Value == "Companies")
                {
                    if (cmdViewCompanies.IsChecked.HasValue)
                    {
                        if (cmdViewCompanies.IsChecked.Value)
                        {
                            pageCompanies.IsSelected = true;
                        }
                    }
                }
                if (selectedView.Value == "Persons")
                {
                    if (cmdViewPersons.IsChecked.HasValue)
                    {
                        if (cmdViewPersons.IsChecked.Value)
                        {
                            pagePersons.IsSelected = true;
                        }
                    }
                }
            }
        }
        private void ShowView()
        {
            if (cmdViewCompanies.IsChecked.HasValue)
            {
                if (cmdViewCompanies.IsChecked.Value)
                {
                    pageCompanies.Visibility = Visibility.Visible;
                }
                else
                {
                    pageCompanies.Visibility = Visibility.Collapsed;
                }
            }
            if (cmdViewDashboard.IsChecked.HasValue)
            {
                if (cmdViewDashboard.IsChecked.Value)
                {
                    pageDashboard.Visibility = Visibility.Visible;
                }
                else
                {
                    pageDashboard.Visibility = Visibility.Collapsed;
                }
            }
            if (cmdViewPersons.IsChecked.HasValue)
            {
                if (cmdViewPersons.IsChecked.Value)
                {
                    pagePersons.Visibility = Visibility.Visible;
                }
                else
                {
                    pagePersons.Visibility = Visibility.Collapsed;
                }
            }
            if (pageCompanies.Visibility == Visibility.Collapsed && pageDashboard.Visibility == Visibility.Collapsed && pagePersons.Visibility == Visibility.Collapsed)
            {
                cmdViewDashboard.IsChecked = true;
                pageDashboard.Visibility = Visibility.Visible;
            }
        }
        private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Stop();
            Dispatcher.BeginInvoke(new Action(() => {
                lblDate.Content = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            }));
            _timer.Start();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            var selectedViewValue = _settings.KeyValues.FirstOrDefault(x => x.Key == "SelectedView");
            if (selectedViewValue == null)
            {
                selectedViewValue = new Internals.FormKeyValue();
                selectedViewValue.Key = "SelectedView";
                selectedViewValue.Value = "Dashboard";
                if (pageCompanies.IsSelected)
                    selectedViewValue.Value = "Companies";
                if (pagePersons.IsSelected)
                    selectedViewValue.Value = "Persons";
                _settings.KeyValues.Add(selectedViewValue);
            }
            else
            {
                selectedViewValue.Value = "Dashboard";
                if (pageCompanies.IsSelected)
                    selectedViewValue.Value = "Companies";
                if (pagePersons.IsSelected)
                    selectedViewValue.Value = "Persons";
            }
            var checkedDashboardItem = _settings.KeyValues.FirstOrDefault(x => x.Key == "CheckedDashboard");
            if (checkedDashboardItem == null)
            {
                checkedDashboardItem = new Internals.FormKeyValue();
                checkedDashboardItem.Key = "CheckedDashboard";
                if (cmdViewDashboard.IsChecked.HasValue)
                    checkedDashboardItem.Value = cmdViewDashboard.IsChecked.Value.ToString();
                _settings.KeyValues.Add(checkedDashboardItem);
            }
            else
            {
                if (cmdViewDashboard.IsChecked.HasValue)
                    checkedDashboardItem.Value = cmdViewDashboard.IsChecked.Value.ToString();
            }
            var checkedCompaniesItem = _settings.KeyValues.FirstOrDefault(x => x.Key == "CheckedCompanies");
            if (checkedCompaniesItem == null)
            {
                checkedCompaniesItem = new Internals.FormKeyValue();
                checkedCompaniesItem.Key = "CheckedCompanies";
                if (cmdViewCompanies.IsChecked.HasValue)
                    checkedCompaniesItem.Value = cmdViewCompanies.IsChecked.Value.ToString();
                _settings.KeyValues.Add(checkedCompaniesItem);
            }
            else
            {
                if (cmdViewCompanies.IsChecked.HasValue)
                    checkedCompaniesItem.Value = cmdViewCompanies.IsChecked.Value.ToString();
            }
            var checkedPersonsItem = _settings.KeyValues.FirstOrDefault(x => x.Key == "CheckedPersons");
            if (checkedPersonsItem == null)
            {
                checkedPersonsItem = new Internals.FormKeyValue();
                checkedPersonsItem.Key = "CheckedPersons";
                if (cmdViewPersons.IsChecked.HasValue)
                    checkedPersonsItem.Value = cmdViewPersons.IsChecked.Value.ToString();
                _settings.KeyValues.Add(checkedPersonsItem);
            }
            else
            {
                if (cmdViewPersons.IsChecked.HasValue)
                    checkedPersonsItem.Value = cmdViewPersons.IsChecked.Value.ToString();
            }
            _settings.Save();
        }

        private void MainWindow_ContentRendered(object? sender, EventArgs e)
        {
            if (App.AppShare.Authenticated())
            {
                if (App.AppShare.ActiveUser != null)
                {
                    App.AppShare.ActiveUser.SetFullName();
                    lblUser.Content = App.AppShare.ActiveUser.Fullname;
                    App.DataShare.ResumeEvents();
                    App.DataShare.GetData();
                }
            }
            else
            {

            }
        }

        private void imgMin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void imgMax_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Maximized;
            imgRes.Visibility = Visibility.Visible;
            imgMax.Visibility = Visibility.Collapsed;
        }

        private void imgRes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Normal;
            imgRes.Visibility = Visibility.Collapsed;
            imgMax.Visibility = Visibility.Visible;
        }

        private void imgCls_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                WindowState = WindowState.Maximized;
                imgMax.Visibility = Visibility.Collapsed;
                imgRes.Visibility = Visibility.Visible;
            }
            else
            {
                this.DragMove();
            }
        }
        private void _settings_DialogLoaded(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                imgMax.Visibility = Visibility.Collapsed;
                imgRes.Visibility = Visibility.Visible;
            }
        }

        private void cmdViewDashboard_Click(object sender, RoutedEventArgs e)
        {
            ShowView();
        }

        private void cmdViewCompanies_Click(object sender, RoutedEventArgs e)
        {
            ShowView();
        }

        private void cmdViewPersons_Click(object sender, RoutedEventArgs e)
        {
            ShowView();
        }
        private void DgCompanies_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgCompanies.ActualWidth;
            dgCompanies.Columns[2].Width = width - 80 - 140 - 64 - 120 - 120 - 80 - 140 - 180 - 80 - 48 - 48 - 2;
        }
        private void DgRelationPersons_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgRelationPersons.ActualWidth;
            dgRelationPersons.Columns[1].Width = width - 140 - 64 - 120 - 120 - 80 - 80 - 140 - 140 - 20;
        }
        private void EventHelper_CompanyCollectionChanged(object sender, EventArgs e)
        {
            List<Models.CompanyView> list = new List<Models.CompanyView>();
            if (App.DataShare.CompanyCollection != null)
            {
                foreach (var company in App.DataShare.CompanyCollection)
                {
                    Models.CompanyView view = new Models.CompanyView
                    {
                        Id = company.Id,
                        Number = company.Number,
                        Taxnumber = company.Taxnumber,
                        Name = company.Name,
                        Nickname = company.Nickname,
                        Phone = company.Phone,
                        Email = company.Email,
                        Website = company.Website,
                        Employees = company.Employees,
                        FoundedYear = company.FoundedYear,
                    };
                    if (App.DataShare.LocationCollection != null)
                    {
                        company.Location = App.DataShare.LocationCollection.FirstOrDefault(x => x.Id == company.LocationId);
                        if (company.Location != null)
                        {
                            view.Address = company.Location.Address ?? "";
                            if (App.DataShare.PostalzoneCollection != null)
                            {
                                company.Location.Postalzone = App.DataShare.PostalzoneCollection.FirstOrDefault(x => x.Id == company.Location.PostalzoneId);
                                if (company.Location.Postalzone != null)
                                {
                                    view.Zipcode = company.Location.Postalzone.Zipcode;
                                    view.City = company.Location.Postalzone.City;
                                    if (App.DataShare.CountryCollection != null)
                                    {
                                        company.Location.Postalzone.Country = App.DataShare.CountryCollection.FirstOrDefault(x => x.Id == company.Location.Postalzone.CountryId);
                                        if (company.Location.Postalzone.Country != null)
                                        {
                                            view.Country = company.Location.Postalzone.Country.Name;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    list.Add(view);
                }
            }
            dgCompanies.BeginInit();
            dgCompanies.ItemsSource = list;
            dgCompanies.EndInit();
        }

        private void EventHelper_ActivityCollectionChanged(object sender, EventArgs e)
        {
            FillActivityControl();
        }

        private void EventHelper_PersonCollectionChanged(object sender, EventArgs e)
        {
            FillPersonControl();
        }

        private void dgCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgCompanies.SelectedItem as Models.CompanyView;
            if (item != null && App.DataShare.CompanyCollection != null)
            {
                App.AppShare.ActiveCompany =App.DataShare.CompanyCollection.FirstOrDefault(x => x.Id == item.Id);
                App.EventHelper.RaiseActiveCompanyChangedEvent();
                if (App.AppShare.ActiveCompany != null)
                {
                    cmdActionWebsite.IsEnabled = false;
                    cmdActionLocation.IsEnabled = false;
                    cmdActionProff.IsEnabled = false;
                    if (!string.IsNullOrEmpty( App.AppShare.ActiveCompany.Website))
                    {
                        cmdActionWebsite.IsEnabled = true;
                    }
                    if (App.DataShare.LocationCollection != null)
                    {
                        var location = App.DataShare.LocationCollection.FirstOrDefault(x => x.Id == App.AppShare.ActiveCompany.LocationId);
                        if (location != null)
                        {
                            cmdActionLocation.IsEnabled = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(App.AppShare.ActiveCompany.Taxnumber))
                    {
                        cmdActionProff.IsEnabled = true;
                    }
                }
            }
        }

        private void dgCompanies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = dgCompanies.SelectedItem as Models.CompanyView;
            if (item != null && App.DataShare.CompanyCollection != null)
            {
                App.DialogHelper.ShowCompanyDialog(App.DataShare.CompanyCollection.FirstOrDefault(x => x.Id == item.Id));
            }

        }
        private void EventHelper_ActiveCompanyChanged(object sender, EventArgs e)
        {
            if (App.AppShare.ActiveCompany != null)
            {
                var c = Mouse.OverrideCursor;
                Mouse.OverrideCursor = Cursors.Wait;
                lblCompany.Content = App.AppShare.ActiveCompany.Name;
                FillPersonControl();
                FillActivityControl();
                Mouse.OverrideCursor = c;
            }
        }
        private void EventHelper_ActivePersonChanged(object sender, EventArgs e)
        {
            if (App.AppShare.ActivePerson != null)
            {
                lblPerson.Content = App.AppShare.ActivePerson.Fullname;
            }
        }

        private void cmdNewCompany_Click(object sender, RoutedEventArgs e)
        {
            App.DialogHelper.ShowCompanyDialog(new Entity.Company());
        }

        private void cmdDataLocations_Click(object sender, RoutedEventArgs e)
        {
            App.DialogHelper.ShowLocationsDialog();
        }

        private void mnuNewPerson_Click(object sender, RoutedEventArgs e)
        {
            App.DialogHelper.ShowPersonDialog(new Entity.Person());
        }

        private void dgRelationPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgRelationPersons.SelectedItem as Models.PersonView;
            if (item != null && App.DataShare.PersonCollection != null)
            {
                App.AppShare.ActivePerson = App.DataShare.PersonCollection.FirstOrDefault(x => x.Id == item.Id);                
                App.EventHelper.RaiseActivePersonChangedEvent();
            }
        }

        private void dgRelationPersons_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = dgRelationPersons.SelectedItem as Models.PersonView;
            if (item != null && App.DataShare.PersonCollection != null)
            {
                App.DialogHelper.ShowPersonDialog(App.DataShare.PersonCollection.FirstOrDefault(x => x.Id == item.Id));
            }
        }

        private void cmdActionLocation_Click(object sender, RoutedEventArgs e)
        {
            var item = dgCompanies.SelectedItem as Models.CompanyView;
            if (item == null || App.DataShare.CompanyCollection == null || App.DataShare.LocationCollection == null) return;
            var company = App.DataShare.CompanyCollection.FirstOrDefault(x => x.Id == item.Id);
            if (company == null) return;
            var location = App.DataShare.LocationCollection.FirstOrDefault(x => x.Id == company.LocationId);
            if (location == null || App.DataShare.PostalzoneCollection == null) return;
            var postzone = App.DataShare.PostalzoneCollection.FirstOrDefault(x => x.Id == location.PostalzoneId);
            if (postzone == null || App.DataShare.CountryCollection == null) return;
            var country = App.DataShare.CountryCollection.FirstOrDefault(x => x.Id == postzone.CountryId);
            if (country == null) return;
            // https://www.google.com/maps/place/Stenhusvej+53,+4300+Holb%C3%A6k/@55.7016788,11.6700185,17z/data=!3m1!4b1!4m6!3m5!1s0x46527e8e5e91e3fd:0x76935299063f229d!8m2!3d55.7016788!4d11.6725934!16s%2Fg%2F11crttdcrm?entry=ttu&g_ep=EgoyMDI2MDcxOS4wIKXMDSoASAFQAw%3D%3D
            var url = "https://www.google.com/maps/place/"+ location.Street + "+" + location.House + "+" + postzone.Zipcode + "+" + postzone.City;
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true,
            });
        }

        private void cmdActionWebsite_Click(object sender, RoutedEventArgs e)
        {
            //https://www.demai.tech/
            if (App.AppShare.ActiveCompany == null) return;
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = App.AppShare.ActiveCompany.Website,
                UseShellExecute = true
            });

        }

        private void cmdActionProff_Click(object sender, RoutedEventArgs e)
        {
            //https://www.proff.dk/branches%C3%B8g?q=42514241
            if (App.AppShare.ActiveCompany == null) return;
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.proff.dk/branches%C3%B8g?q=" + App.AppShare.ActiveCompany.Taxnumber,
                UseShellExecute = true
            });
        }

        private void mnuNewTask_Click(object sender, RoutedEventArgs e)
        {
            Entity.Actitiy activity = new Entity.Actitiy();
            activity.ActivityType = 0;
            if (App.AppShare.ActiveCompany != null)
                activity.CompanyId = App.AppShare.ActiveCompany.Id;
            if (App.AppShare.ActivePerson != null)
                activity.PersonId = App.AppShare.ActivePerson.Id;
            if (App.AppShare.ActiveUser != null)
                activity.SalespersonId = App.AppShare.ActiveUser.Id;
            activity.Starts = App.ToolHelper.ConvertDateTimeToLong(DateTime.Now);
            activity.Ends = App.ToolHelper.ConvertDateTimeToLong(DateTime.Now.AddHours(1));
            App.DialogHelper.ShowActivityDialog(activity);
        }

        private void mnuNewPhone_Click(object sender, RoutedEventArgs e)
        {
            Entity.Actitiy activity = new Entity.Actitiy();
            activity.ActivityType = 1;
            if (App.AppShare.ActiveCompany != null)
                activity.CompanyId = App.AppShare.ActiveCompany.Id;
            if (App.AppShare.ActivePerson != null)
                activity.PersonId = App.AppShare.ActivePerson.Id;
            if (App.AppShare.ActiveUser != null)
                activity.SalespersonId = App.AppShare.ActiveUser.Id;
            activity.Starts = App.ToolHelper.ConvertDateTimeToLong(DateTime.Now);
            activity.Ends = App.ToolHelper.ConvertDateTimeToLong(DateTime.Now.AddMinutes(15));
            App.DialogHelper.ShowActivityDialog(activity);
        }

        private void mnuNewMeeting_Click(object sender, RoutedEventArgs e)
        {
            Entity.Actitiy activity = new Entity.Actitiy();
            activity.ActivityType = 2;
            if (App.AppShare.ActiveCompany != null)
                activity.CompanyId = App.AppShare.ActiveCompany.Id;
            if (App.AppShare.ActivePerson != null)
                activity.PersonId = App.AppShare.ActivePerson.Id;
            if (App.AppShare.ActiveUser != null)
                activity.SalespersonId = App.AppShare.ActiveUser.Id;
            activity.Starts = App.ToolHelper.ConvertDateTimeToLong(DateTime.Now);
            activity.Ends = App.ToolHelper.ConvertDateTimeToLong(DateTime.Now.AddHours(2));
            App.DialogHelper.ShowActivityDialog(activity);
        }

        private void dgRelationActivities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void DgRelationActivities_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgRelationActivities.ActualWidth;
            dgRelationActivities.Columns[7].Width = width - 16 - 16 - 60 - 48 - 60 - 48 - 160 - 160 - 160 - 160-16;
        }
    }
}