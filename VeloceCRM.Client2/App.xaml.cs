using System.Configuration;
using System.Data;
using System.Windows;

namespace VeloceCRM.Client2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Internals.EventHelper EventHelper { get; set; } = new Internals.EventHelper();
        public static Internals.AppShare AppShare { get; set; } = new Internals.AppShare();
        public static Internals.DataShare DataShare { get; set; } = new Internals.DataShare();
        public static Internals.DialogHelper DialogHelper { get; set; } = new Internals.DialogHelper();

        public App()
        {

        }
    }

}
