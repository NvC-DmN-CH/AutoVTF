using DarkUI.Forms;

using System.Media;
using Microsoft.Win32;


namespace AutoVTF
{
    internal static class Program
    {
        public const string title = "AutoVTF";
        public const string version = "1.2.1";
        public static MainFormDarkUi MainFormInstance;
        public static string VtfExportTempDirectory;
        [STAThread]
        static void Main()
        {
            try
            {
                //if (!Directory.Exists("vtfexporttemp"))
                {
                    Directory.CreateDirectory("vtfexporttemp");
                }

                VtfExportTempDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "vtfexporttemp/");

                ApplicationConfiguration.Initialize();
                MainFormInstance = new MainFormDarkUi();
                Application.Run(MainFormInstance);
            }
            catch (Exception e)
            {
                Alert("Exception caught in Main()\n" + e.Message + "\n" + e.StackTrace);
            }
        }

        public static void Alert(string message)
        {
            SystemSounds.Exclamation.Play();
            DarkMessageBox.ShowWarning(message, "Alert");
        }
    }
}