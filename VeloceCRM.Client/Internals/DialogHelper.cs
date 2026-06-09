using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class DialogHelper
    {
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
