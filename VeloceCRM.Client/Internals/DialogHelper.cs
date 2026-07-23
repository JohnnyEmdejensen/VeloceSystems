using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class DialogHelper
    {
        public void ShowAppSettingsDialog()
        {
            Dialogs.AppSettingsDialog frm = new Dialogs.AppSettingsDialog
            {
            };
            frm.Show();
        }
        public void ShowFollowuptypeDialog(Entity.Followuptype? Followuptype)
        {
            Dialogs.FollowuptypeDialog frm = new Dialogs.FollowuptypeDialog
            {
                DataContext = Followuptype,
            };
            frm.Show();
        }
        public void ShowActivityDialog(Entity.Actitiy? Activity)
        {
            Dialogs.ActivityDialog frm = new Dialogs.ActivityDialog
            {
                DataContext = Activity,
            };
            frm.Show();
        }
        public void ShowTitleDialog(Entity.Title? Title)
        {
            Dialogs.TitleDialog frm = new Dialogs.TitleDialog
            {
                DataContext = Title
            };
            frm.Show();
        }
        public void ShowPersonDialog(Entity.Person? Person)
        {
            if (Person != null && Person.Id == 0)
            {
                if (App.AppShare.ActiveCompany != null)
                {
                    Person.CompanyId = App.AppShare.ActiveCompany.Id;
                }
            }
            Dialogs.PersonDialog frm = new Dialogs.PersonDialog
            {
                DataContext = Person,
            };
            frm.Show();
        }
        public void ShowLocationsDialog()
        {
            Dialogs.LocationsDialog frm = new Dialogs.LocationsDialog
            {

            };
            frm.Show();
        }
        public void ShowCompanyDialog(Entity.Company? Company)
        {
            Dialogs.CompanyDialog frm = new Dialogs.CompanyDialog
            {
                DataContext = Company,
            };
            frm.Show();
        }
        public void ShowLoginDialog()
        {
            Dialogs.LoginDialog frm = new Dialogs.LoginDialog
            {
                Topmost = true,
            };
            frm.ShowDialog();
        }
    }
}
