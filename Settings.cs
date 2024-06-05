using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace AutoVTF
{
    internal class SettingsObject
    {
        public SettingsObject()
        {
        }

        public string WatchFolderPath { get; set; }

        public int AdvancedImportPanel_MipmapFilter { get; set; }
        public int AdvancedImportPanel_VtfVersion { get; set; }
        public int AdvancedImportPanel_ImageFormat { get; set; }
        public bool AdvancedImportPanel_GenMipmaps { get; set; }
        public bool AdvancedImportPanel_F_ClampS { get; set; }
        public bool AdvancedImportPanel_F_ClampT { get; set; }
        public bool AdvancedImportPanel_F_PointSample {  get; set; }
        public bool AdvancedImportPanel_F_NoLod {  get; set; }
        public bool AdvancedImportPanel_F_Anisotropic { get; set; }
    }

    internal class Settings
    {
        private static string filePath = "settings.txt";

        public static void Save()
        {
            SettingsObject s = new SettingsObject();
            s.WatchFolderPath                       = Program.MainFormInstance.GetWatchFolderTextboxValue();
            s.AdvancedImportPanel_MipmapFilter      = Program.MainFormInstance.AdvancedImportPanel_MipmapFilter.SelectedIndex;
            s.AdvancedImportPanel_VtfVersion        = Program.MainFormInstance.AdvancedImportPanel_VtfVersion.SelectedIndex;
            s.AdvancedImportPanel_ImageFormat       = Program.MainFormInstance.AdvancedImportPanel_ImageFormat.SelectedIndex;
            s.AdvancedImportPanel_GenMipmaps        = Program.MainFormInstance.AdvancedImportPanel_GenMipmaps.Checked;
            s.AdvancedImportPanel_F_ClampS          = Program.MainFormInstance.AdvancedImportPanel_F_ClampS.Checked;
            s.AdvancedImportPanel_F_ClampT          = Program.MainFormInstance.AdvancedImportPanel_F_ClampT.Checked;
            s.AdvancedImportPanel_F_PointSample     = Program.MainFormInstance.AdvancedImportPanel_F_PointSample.Checked;
            s.AdvancedImportPanel_F_NoLod           = Program.MainFormInstance.AdvancedImportPanel_F_NoLod.Checked;
            s.AdvancedImportPanel_F_Anisotropic     = Program.MainFormInstance.AdvancedImportPanel_F_Anisotropic.Checked;

            JsonSerializerOptions o = new JsonSerializerOptions();
            o.WriteIndented = true;
            string content = JsonSerializer.Serialize(s, o);

            File.WriteAllText(filePath, content);
        }

        public static void Load()
        {
            string content = null;
            SettingsObject new_object = null;
            try
            {
                content = File.ReadAllText(filePath);
                new_object = JsonSerializer.Deserialize<SettingsObject>(content);
            }
            catch (Exception e)
            {
                Program.MainFormInstance.AdvancedImportPanel_MipmapFilter.SelectedIndex = 10;
                Program.MainFormInstance.AdvancedImportPanel_VtfVersion.SelectedIndex = 1;
                Program.MainFormInstance.AdvancedImportPanel_ImageFormat.SelectedIndex = 2;
                Program.MainFormInstance.AdvancedImportPanel_GenMipmaps.Checked = true;
                Program.MainFormInstance.AdvancedImportPanel_F_ClampS.Checked = false;
                Program.MainFormInstance.AdvancedImportPanel_F_ClampT.Checked = false;
                Program.MainFormInstance.AdvancedImportPanel_F_PointSample.Checked = false;
                Program.MainFormInstance.AdvancedImportPanel_F_NoLod.Checked = false;
                Program.MainFormInstance.AdvancedImportPanel_F_Anisotropic.Checked = false;
                return;
            }

            Program.MainFormInstance.SetWatchFolderTextboxValue(new_object.WatchFolderPath + "."); // is this hacky?? We need to make sure that the value we set here, differs from the last value in order to trigger the TextboxChanged event. I dont like doing stuff like this... makes me feel stupid :(
            Program.MainFormInstance.SetWatchFolderTextboxValue(new_object.WatchFolderPath);
            Program.MainFormInstance.AdvancedImportPanel_MipmapFilter.SelectedIndex = new_object.AdvancedImportPanel_MipmapFilter;
            Program.MainFormInstance.AdvancedImportPanel_VtfVersion.SelectedIndex   = new_object.AdvancedImportPanel_VtfVersion;
            Program.MainFormInstance.AdvancedImportPanel_ImageFormat.SelectedIndex  = new_object.AdvancedImportPanel_ImageFormat;
            Program.MainFormInstance.AdvancedImportPanel_GenMipmaps.Checked         = new_object.AdvancedImportPanel_GenMipmaps;
            Program.MainFormInstance.AdvancedImportPanel_F_ClampS.Checked           = new_object.AdvancedImportPanel_F_ClampS;
            Program.MainFormInstance.AdvancedImportPanel_F_ClampT.Checked           = new_object.AdvancedImportPanel_F_ClampT;
            Program.MainFormInstance.AdvancedImportPanel_F_PointSample.Checked      = new_object.AdvancedImportPanel_F_PointSample;
            Program.MainFormInstance.AdvancedImportPanel_F_NoLod.Checked            = new_object.AdvancedImportPanel_F_NoLod;
            Program.MainFormInstance.AdvancedImportPanel_F_Anisotropic.Checked      = new_object.AdvancedImportPanel_F_Anisotropic;
        }
    }
}
