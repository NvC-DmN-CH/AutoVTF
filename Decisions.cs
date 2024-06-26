﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using static System.Net.Mime.MediaTypeNames;
using System.Web;
using System.Drawing.Interop;
using System.Threading.Channels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Buffers;
using ImageMagick.Formats;

namespace AutoVTF
{
    internal class Decisions
    {
        private const int VTFCMD_TIMEOUT_MS = 3000 * 10; // 30 seconds
        private static List<string> ignoredExtensions = [ Extensions.PsdExportTemp, Extensions.Vtf]; // i kinda dont like the whole thing with the ignored extensions here but its ok

        public static void OnFileUpdated(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();

            if (ignoredExtensions.Contains(ext))
            {
                return;
            }
            VtfImportOptionsObject importOptions = VtfImportOptionsObject.GenPrefabLossless();

            // if vtf exists then we're gonna use its settings
            string vtfPath = Path.ChangeExtension(filePath, Extensions.Vtf);
            if (File.Exists(vtfPath))
            {
                importOptions.SetFromVtf(vtfPath);
            }
            importOptions.SetImageFormatHasAlphaFromFile(filePath);
            MakeAsset(filePath, importOptions);
        }

        //public static void OnFileDeleted(string file_path)
        //{
        //    return;
        //    string ext = Path.GetExtension(file_path).ToLower();
        //    if ( ignoredExtensions.Contains(ext) )
        //    {
        //        return;
        //    }

        //    DeleteAsset(file_path);
        //}

        //public static void OnFileRenamed(string file_path_old, string file_path_new)
        //{
        //    return;
        //    string ext = Path.GetExtension(file_path_old).ToLower();
        //    if (ignoredExtensions.Contains(ext))
        //    {
        //        return;
        //    }

        //    RenameAsset(file_path_old, file_path_new);
        //}

        public static void MakeAsset(string filePath, VtfImportOptionsObject importOptions)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            if (ext == Extensions.Psd)
            {
                // we disable this for now:
                if (false)
                {
                    MakeAssetFromPsd(filePath, importOptions);
                    return;
                }
                // reason why:
                // 
                // we let VTFCmd deal with converting PSD to VTF. Its PSD reading capabilities seem to:
                // 1. avoid premultiplying alpha when exporting PSD, something which Magick.NET does and i'm not sure if it can be disabled
                // 2. always read the Alpha Channel, regardless of whether the PSD has more than 1 layer or not. Magick.NET reads it only if there is 1 layer when it's the default locked "Background" layer
                // 
                // still Magick.NET is preferred because it is actively supported, VTFCmd uses DevIL which was last updated in 2017
            }

            bool pause = false;
            bool is_silent = true;
            string fileName = "vtfcmd/vtfcmd.exe";
            string arguments = @"-file " + "\"" + filePath + "\" " + importOptions.ToArgumentsString() + (is_silent ? " -silent" : "") + (pause ? " -pause" : "") + " -srgb";

            ProcessStartInfo startInfo = new ProcessStartInfo(fileName, arguments);
            startInfo.CreateNoWindow = true;

            try
            {
                using (Process p = Process.Start(startInfo))
                {
                    p.WaitForExit(VTFCMD_TIMEOUT_MS);
                    TryDeleteTempImageFromVtf(filePath);
                }
            }
            catch
            {
                TryDeleteTempImageFromVtf(filePath);
            }
        }

        public static void ExportAsset(string filePath, string exportOptions)
        {
            bool pause = false;
            bool is_silent = true;
            string fileName = "vtfcmd/vtfcmd.exe";
            string arguments = @"-file " + "\"" + filePath + "\" " + exportOptions + (is_silent ? " -silent" : "") + (pause ? " -pause" : "");

            ProcessStartInfo startInfo = new ProcessStartInfo(fileName, arguments);
            startInfo.CreateNoWindow = true;

            try
            {
                using (Process p = Process.Start(startInfo))
                {
                    p.WaitForExit(VTFCMD_TIMEOUT_MS);
                    TryDeleteTempImageFromVtf(filePath);
                }
            }
            catch
            {
                TryDeleteTempImageFromVtf(filePath);
            }
        }

        public static void ExportAssetToPsd(string file_path)
        {
            bool pause = false;
            bool is_silent = true;
            string fileName = "vtfcmd/vtfcmd.exe";
            string arguments = @"-file " + "\"" + file_path + "\" " + "-output \"" + Program.VtfExportTempDirectory + "\" " + VTFExportOptions.TGA + (is_silent ? " -silent" : "") + (pause ? " -pause" : "");

            ProcessStartInfo startInfo = new ProcessStartInfo(fileName, arguments);
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;

            try
            {
                using (Process p = Process.Start(startInfo))
                {
                    p.WaitForExit(VTFCMD_TIMEOUT_MS);
                    string tga_filename = ChangeExtension(Path.GetFileName(file_path), Extensions.VtfExportTempTga);
                    string tga_path = Path.Combine(Program.VtfExportTempDirectory, tga_filename);
                    MakePsdFromTga(tga_path);
                    string psd_path = ChangeExtension(tga_path, Extensions.Psd);
                    string psd_path_new = Path.Combine(Path.GetDirectoryName(file_path), Path.GetFileName(psd_path));
                    File.Delete(tga_path);
                    File.Move(psd_path, psd_path_new, true);
                }
            }
            catch (Exception e)
            {
                Program.Alert(AlertMessages.ConvertToPsdFail);
            }
        }

        public static void ExportAssetToXcf(string file_path)
        {
            bool pause = false;
            bool is_silent = true;
            string fileName = "vtfcmd/vtfcmd.exe";
            string arguments = @"-file " + "\"" + file_path + "\" " + "-output \"" + Program.VtfExportTempDirectory + "\" " + VTFExportOptions.TGA + (is_silent ? " -silent" : "") + (pause ? " -pause" : "");

            ProcessStartInfo startInfo = new ProcessStartInfo(fileName, arguments);
            startInfo.CreateNoWindow = true;

            try
            {
                using (Process p = Process.Start(startInfo))
                {
                    p.WaitForExit(VTFCMD_TIMEOUT_MS);
                    string tga_filename = ChangeExtension(Path.GetFileName(file_path), Extensions.VtfExportTempTga);
                    string tga_path = Path.Combine(Program.VtfExportTempDirectory, tga_filename);
                    MakeXcfFromTga(tga_path);
                    string xcf_path = ChangeExtension(tga_path, Extensions.Xcf);
                    string xcf_path_new = Path.Combine(Path.GetDirectoryName(file_path), Path.GetFileName(xcf_path));
                    File.Delete(tga_path);
                    File.Move(xcf_path, xcf_path_new, true);
                }
            }
            catch (Exception e)
            {
                Program.Alert(AlertMessages.ConvertToXcfFail);
            }
        }

        public static void DeleteAsset(string file_path_srcimage)
        {
            string directory = Path.GetDirectoryName(file_path_srcimage);
            string file_path_asset = Path.Combine(directory, Path.GetFileNameWithoutExtension(file_path_srcimage) + ".vtf");

            try
            {
                File.Delete(file_path_asset);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void RenameAsset(string file_path_srcimage_old, string file_path_srcimage_new)
        {
            string directory = Path.GetDirectoryName(file_path_srcimage_old);
            string file_path_asset_old = Path.Combine(directory, Path.GetFileNameWithoutExtension(file_path_srcimage_old) + ".vtf" );
            string file_path_asset_new = Path.Combine(directory, Path.GetFileNameWithoutExtension(file_path_srcimage_new) + ".vtf");
            
            try
            {
                File.Move(file_path_asset_old, file_path_asset_new, true);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static void MakeAssetFromPsd(string filePathPsd, VtfImportOptionsObject importOptions)
        {
            string filePathTemp = ChangeExtension(filePathPsd, Extensions.PsdExportTemp);
            try
            {
                /*
                // read psd layers
                using (MagickImageCollection collection = new MagickImageCollection(filePathPsd, MagickFormat.Psd))
                {
                    string print = "";
                    for (int i = 0; i < collection.; i++)
                    {
                        print += "l: " + collection[i].Label + "\n";
                    }
                    MessageBox.Show("" + print);
                }
                */


                using (MagickImage psd = new MagickImage(filePathPsd, MagickFormat.Psd))
                {
                    //IEnumerable<PixelChannel> channels = psd.Channels;
                    //MessageBox.Show("channels:" + channels.Count());

                    psd.Write(filePathTemp, MagickFormat.Tga);
                    MakeAsset(filePathTemp, importOptions);
                }
            }
            catch (MagickException e)
            {
                Program.Alert(AlertMessages.ExportPsdFail + "\n" + e.Message);
            }
        }

        public static void MakePsdFromTga(string file_path_tga)
        {
            string file_path_psd = ChangeExtension(file_path_tga, Extensions.Psd);
            try
            {
                using (MagickImage tga = new MagickImage(file_path_tga))
                {
                    // default first layer
                    MagickImage defaultFirstLayer = (MagickImage)tga.Clone();

                    // alpha
                    MagickImage alphaChannel = (MagickImage)tga.Separate(Channels.Alpha).First();
                    alphaChannel.Compose = CompositeOperator.No;

                    // color
                    MagickImage colorChannel = (MagickImage)tga.Clone();
                    colorChannel.Compose = CompositeOperator.No;

                    // color with alpha
                    MagickImage colorWithAlpha = (MagickImage)tga.Clone();
                    colorWithAlpha.Composite(alphaChannel, CompositeOperator.CopyAlpha);

                    // make psd
                    MagickImageCollection collection = new MagickImageCollection();

                    // add layers
                    collection.Add(defaultFirstLayer);
                    collection[0].Label = "Default First Layer";

                    collection.Add(alphaChannel);
                    collection[1].Label = "Alpha";

                    collection.Add(colorChannel);
                    collection[2].Label = "Color";

                    collection.Add(colorWithAlpha);
                    collection[3].Label = "Color + Alpha";

                    // save psd
                    collection.Write(file_path_psd, MagickFormat.Psd);
                }
            }
            catch (MagickException e)
            {
                throw e;
            }
        }
        public static void MakeXcfFromTga(string file_path_tga)
        {
            string file_path_xcf = ChangeExtension(file_path_tga, Extensions.Xcf);
            try
            {
                using (MagickImage image = new MagickImage(file_path_tga))
                {
                    image.Write(file_path_xcf, MagickFormat.Xcf);
                }
            }
            catch (MagickException e)
            {
                throw e;
            }
        }

        public static void TryDeleteTempImageFromVtf(string file_path_vtf)
        {
            string temp_file_path = ChangeExtension(file_path_vtf, Extensions.PsdExportTemp);
            /*
            bool temp_file_exists = File.Exists(temp_file_path);

            if (!temp_file_exists)
                return;
            */

            try
            {
                File.Delete(temp_file_path);
            }
            catch (Exception e)
            {
                
            }
        }

        public static void MakeSimpleVmt(string file_path_vtf)
        {
            string[] contents = {
                "LightmappedGeneric" /*+ " // change to VertexLitGeneric if applied to model"*/,
                "{",
                "\t" + "\"$basetexture\"" + " " + $"\"{FilepathToVmtImageString(file_path_vtf)}\"",
                "}",
//                "// Generated by " + Program.MainFormInstance.title,
            };

            string vmtPath = ChangeExtension(file_path_vtf, Extensions.Vmt);
            
            try
            {
                File.WriteAllLines(vmtPath, contents);
            }
            catch (Exception)
            {
                Program.Alert(AlertMessages.VmtWriteFail);
                throw;
            }
        }

        public static string ChangeExtension(string file_path, string extension)
        {
            string directory = Path.GetDirectoryName(file_path);
            string result = Path.Combine(directory, Path.GetFileNameWithoutExtension(file_path) + extension);
            return result;
        }

        public static string FilepathToVmtImageString(string file_path)
        {
            string MATERIALS_FOLDER_NAME = "materials";
            //string watchFolderName = new DirectoryInfo(Program.MainFormInstance.GetWatchFolderTextboxValue()).Name;
            string root;
            
            // see if we can find a materials folder up the chain
            int index = file_path.LastIndexOf(MATERIALS_FOLDER_NAME);

            if (index != -1)
            {
                root = file_path.Substring(0, index + MATERIALS_FOLDER_NAME.Length);
            }
            else
            {
                Program.Alert(AlertMessages.VmtNoMaterialsFolder);
                root = Path.GetDirectoryName(file_path);
            }


            return Path.ChangeExtension(Path.GetRelativePath(root, file_path), null).Replace("\\", "/");
        }
    }
}
