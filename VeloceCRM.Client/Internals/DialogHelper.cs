using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class DialogHelper
    {
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
