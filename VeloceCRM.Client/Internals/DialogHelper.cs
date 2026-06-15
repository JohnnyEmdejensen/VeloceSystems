using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class DialogHelper
    {
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
