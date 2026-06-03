using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace VeloceCRM.Client.Internals
{
    public class FormSettingsClass
    {
        private Window _window;
        public FormSettingsClass(Window window) 
        { 
            _window = window;
            DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Emdesoft Developments\VeloceCRM\");
            if (!dir.Exists) dir.Create();
        }

        public void Load()
        {
            if (_window.Tag != null) 
            {
                var file = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Emdesoft Developments\VeloceCRM\" + _window.Tag.ToString() + ".json");
                if (file.Exists)
                {
                    string content = "";
                    using (StreamReader sr = new StreamReader(file.FullName))
                    {
                        content = sr.ReadToEnd();
                        sr.Close();
                    }
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<FormSettingsDataClass>(content);
                    if (data != null)
                    {
                        if (data.IsMaxed)
                        {
                            _window.WindowState = WindowState.Maximized;
                        }
                        else
                        {
                            _window.Top = data.Top;
                            _window.Left = data.Left;
                            _window.Width = data.Width;
                            _window.Height = data.Height;
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Dialog has no Tag information.");
            }
        }
        public void Save()
        {
            if (_window.Tag != null)
            {
                var file = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Emdesoft Developments\VeloceCRM\" + _window.Tag.ToString() + ".json");
                FormSettingsDataClass data = new FormSettingsDataClass();
                data.Top = _window.Top;
                data.Left = _window.Left;
                data.Width = _window.Width;
                data.Height = _window.Height;
                data.IsMaxed = _window.WindowState == WindowState.Maximized;
                string content = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                using (StreamWriter sw = new StreamWriter(file.FullName))
                {
                    sw.WriteLine(content);
                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {
                throw new Exception("Dialog has no Tag information.");
            }

        }
    }    

    public class FormSettingsDataClass
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsMaxed { get; set; }
    }
}
