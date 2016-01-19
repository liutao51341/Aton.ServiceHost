using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Aton.ServiceHost.NativeHelper
{
    class FileTracerHelper
    {
        static string uri = System.Configuration.ConfigurationManager.AppSettings.Get("TraceFileUri");

        public static void Trace(string str)
        {
            FileStream fs = new FileStream(uri, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(DateTime.Now);
            sw.WriteLine(str);
            sw.WriteLine("");
            sw.Close();
            fs.Close();
        }
        public static void Trace(Exception ex)
        {
            FileStream fs = new FileStream(uri, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(DateTime.Now);
            sw.WriteLine(ex.Message);
            sw.WriteLine(ex.StackTrace);
            sw.WriteLine("");
            sw.Close();
            fs.Close();
        }
    }
}
