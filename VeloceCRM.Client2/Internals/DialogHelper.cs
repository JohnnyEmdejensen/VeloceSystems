using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client2.Internals
{
    public class DialogHelper
    {
        public void Authenticate()
        {
            Dialogs.LoginDialog frm = new Dialogs.LoginDialog
            {
                DataContext = new Model.AuthenticateClass
                {
                    License = "TST",
                    Login = "JEJ",
                    Password = "1Lillegris"
                }
            };
            frm.ShowDialog();
        }
    }
}
