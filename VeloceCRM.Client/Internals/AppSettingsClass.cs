using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class AppSettingsClass
    {
        private string _folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Emdesoft Developments\Veloce\CRM\Settings";
        private AppSettingsData _settings;
        public AppSettingsData Settings { get { return _settings; } }
        public AppSettingsClass()
        {
            DirectoryInfo dir = new DirectoryInfo(_folder);
            if (!dir.Exists) dir.Create();
            _settings = new AppSettingsData();
        }

        public void Load()
        {
            var file = new FileInfo(_folder + @"\Veloce.json");
            if (file.Exists)
            {
                string content = "";
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    content = sr.ReadToEnd();
                    sr.Close();
                }
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<AppSettingsData>(content);
                if (data != null)
                    _settings = data;
            }
        }

        public void Save()
        {
            var file = new FileInfo(_folder + @"\Veloce.json");
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(_settings);
            using (StreamWriter sw = new StreamWriter(file.FullName))
            {
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }
        }
    }

    public class AppSettingsData
    {
        public string PrivateDocumentFolder { get; set; } = "";
        public string PublicDocumentFolder { get; set; } = "";
        public bool SendAppointmentRequestOnTasks { get; set; } = false;
        public bool SendAppointmentRequestOnPhone { get; set; } = false;
        public bool SendAppointmentRequestOnMeeting { get; set; } = false;
    }
}
