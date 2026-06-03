using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class DialogHelperClass
    {
        public void ShowCountryDialog(Entity.Country? Country)
        {
            Dialogs.CountryDialog frm = new Dialogs.CountryDialog()
            {
                DataContext = Country,
            };
            frm.Show();
        }
    }
}
