using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class DialogHelperClass
    {
        public void ShowLocationDialog(Entity.Location? Location)
        {
            Dialogs.LocationDialog frm = new Dialogs.LocationDialog
            {
                DataContext = Location,
            };
            frm.Show();
        }
        public void ShowPostalzoneDialog(Entity.Postalzone? Postalzone)
        {
            Dialogs.PostalzoneDialog frm = new Dialogs.PostalzoneDialog
            {
                DataContext = Postalzone,
            };
            frm.Show();
        }
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
