using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace VeloceCRM.Client2.Internals
{
    public class FormsettingsClass
    {
        private Window _window;
        private string folder = "";
        public FormsettingsClass(Window window) 
        { 
            folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Emdesoft Developments\Veloce\CRM";
            _window = window;
            DirectoryInfo dir = new DirectoryInfo(folder);
            if (!dir.Exists) dir.Create();
        }

        public void Load()
        {
            if (_window.Tag != null)
            {
                FileInfo file = new FileInfo(folder + @"\" + _window.Tag.ToString() + ".json");
                if (file.Exists)
                {
                    string content = "";
                    using (StreamReader sr = new StreamReader(file.FullName))
                    {                     
                        content = sr.ReadToEnd();
                        sr.Close();
                    }
                    WindowDataClass? data = Newtonsoft.Json.JsonConvert.DeserializeObject<WindowDataClass>(content);
                    if (data != null)
                    {
                        if (data.IsMaxed)
                        {
                            _window.WindowState = WindowState.Maximized;
                        }
                        else
                        {
                            _window.WindowState = WindowState.Normal;
                            _window.Top = data.Top;
                            _window.Left = data.Left;
                            _window.Width = data.Width;
                            _window.Height = data.Height;
                        }
                    }
                }
            }
        }
        public void Save()
        {
            WindowDataClass data = new WindowDataClass();
            data.Top = _window.Top;
            data.Left = _window.Left;
            data.Height = _window.Height;
            data.Width = _window.Width;
            data.IsMaxed = _window.WindowState == WindowState.Maximized;
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            using (StreamWriter sw = new StreamWriter(folder + @"\" + _window.Tag.ToString() + ".json" ))
            {
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }
        }
    }
    public class WindowDataClass
    {
        public double Top {  get; set; }
        public double Left {  get; set; }
        public double Width {  get; set; }
        public double Height {  get; set; }
        public bool IsMaxed {  get; set; }
    }
}
