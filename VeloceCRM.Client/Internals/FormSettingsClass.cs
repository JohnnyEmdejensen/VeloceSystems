using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace VeloceCRM.Client.Internals
{
    public class FormSettingsClass
    {
        public delegate void DialogHandler(object sender, EventArgs e);
        public event DialogHandler? DialogLoaded;

        private string _folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Emdesoft Developments\Veloce\CRM\Settings";
        private Window _parent;
        public List<FormKeyValue> KeyValues { get; set; }
        public FormSettingsClass(Window Parent) 
        {
            DirectoryInfo dir = new DirectoryInfo(_folder);
            if (!dir.Exists) dir.Create();
            _parent = Parent;
            KeyValues = new List<FormKeyValue>();
        }

        public void Load()
        {
            var file = new FileInfo(_folder + @"\" + _parent.Tag.ToString() + ".json");
            if (file.Exists)
            {
                string content = "";
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    content = sr.ReadToEnd();
                    sr.Close();
                }
                if (!string.IsNullOrEmpty(content))
                {
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<FormDataClass>(content);
                    if (data != null)
                    {
                        if (data.IsMaxed)
                        {
                            _parent.WindowState = WindowState.Maximized;
                        }
                        else
                        {
                            _parent.Top = data.Top;
                            _parent.Left = data.Left;
                            _parent.Width = data.Width;
                            _parent.Height = data.Height;
                            this.KeyValues = data.KeyValues;
                        }
                        DialogLoaded?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }
        public void Save()
        {
            var data = new FormDataClass
            {
                Top = _parent.Top,
                Left = _parent.Left,
                Width = _parent.Width,
                Height = _parent.Height,
                IsMaxed = _parent.WindowState == WindowState.Maximized,
                KeyValues = this.KeyValues,
            };
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            using (StreamWriter sw = new StreamWriter(_folder + @"\" + _parent.Tag.ToString() + ".json"))
            {
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }
        }
    }
    public class FormDataClass
    {
        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsMaxed { get; set; }
        public List<FormKeyValue> KeyValues { get; set; }= new List<FormKeyValue>();
    }

    public class FormKeyValue
    {
        public string Key { get; set; } = "";
        public string Value { get; set; } = "";
    }
}
