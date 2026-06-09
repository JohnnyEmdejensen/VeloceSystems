using System.Configuration;
using System.Data;
using System.Windows;

namespace VeloceCRM.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Internals.EventHelper EventHelper = new Internals.EventHelper();
        public static Internals.AppShare AppShare = new Internals.AppShare();
        public static Internals.DataShare DataShare = new Internals.DataShare();
        public static Internals.DialogHelper DialogHelper = new Internals.DialogHelper();
        public static Internals.ToolHelper ToolHelper = new Internals.ToolHelper();
    }

}
