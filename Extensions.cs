using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVTF
{
    internal static class Extensions
    {
        public static string[] WatcherAllowedExtensionsFilter = { "*.png", "*.bmp", "*.tga", "*.jpg", "*.jpeg", "*.psd", "*.vtf" };
        public static string[] ImageExtensions = { ".png", ".bmp", ".tga", ".jpg", ".jpeg", ".psd" };
        public static string Vmt = ".vmt";
        public static string Vtf = ".vtf";
        public static string Psd = ".psd";
        public static string Xcf = ".xcf";
        public static string PsdExportTemp = ".psd_export_temp";
        public static string VtfExportTempTga = ".tga";
    }
}
