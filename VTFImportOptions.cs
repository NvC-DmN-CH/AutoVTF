using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ImageMagick;

namespace AutoVTF
{
    internal class VtfImportOptionsObject
    {
        public static uint DEFAULT_FLAGS = 0;
        public static uint DEFAULT_VER1 = 7;
        public static uint DEFAULT_VER2 = 1;
        public static bool DEFAULT_MIP_ENABLED = true;
        public static string DEFAULT_MIP_FILTER_SAMPLING = "HANNING";
        public static string DEFAULT_MIP_FILTER_SHARPEN = "NONE";
        public static bool DEFAULT_RESIZE_ENABLED = true;
        public static string DEFAULT_RESIZE_METHOD = "NEAREST";
        public static string DEFAULT_RESIZE_FILTER = "HANNING";


        public static VtfImportOptionsObject GenPrefabLossless()
        {
            VtfImportOptionsObject o = new VtfImportOptionsObject(VtfImageFormat.BGRA8888);
            return o;
        }

        public static VtfImportOptionsObject GenPrefabCompressed()
        {
            VtfImportOptionsObject o = new VtfImportOptionsObject(VtfImageFormat.DXT5);
            return o;
        }

        private VtfImageFormat imageFormat;
        private uint flags;
        private uint versionDigitTens;
        private uint versionDigitOnes;
        private bool mipEnabled;
        private string mipFilterSampling;
        private string mipFilterSharpen;
        private bool resizeEnabled;
        private string resizeMethod;
        private string resizeFilter;

        public VtfImportOptionsObject(VtfImageFormat imageFormat)
        {
            SetImageFormat(imageFormat);
            SetFlags(DEFAULT_FLAGS);
            SetVersion(DEFAULT_VER1, DEFAULT_VER2);

            SetMipEnabled(DEFAULT_MIP_ENABLED);
            SetMipFilters(DEFAULT_MIP_FILTER_SAMPLING, DEFAULT_MIP_FILTER_SHARPEN);

            SetResizeEnabled(DEFAULT_RESIZE_ENABLED);
            SetResizeSettings(DEFAULT_RESIZE_METHOD, DEFAULT_RESIZE_FILTER);
        }

        public void SetFromVtf(string vtfPath)
        {
            VtfHeader h = VtfReading.ReadHeader(vtfPath);

            if (h == null)
            {
                return;
            }

            SetImageFormat((VtfImageFormat)h.highResImageFormat);
            SetVersion(h.versionDigitTens, h.versionDigitOnes);
            SetFlags(h.flags);
            SetMipEnabled(h.HasMipmaps());
        }

        public VtfImageFormat GetImageFormat()
        {
            return imageFormat;
        }

        public void SetImageFormat(VtfImageFormat value)
        {
            imageFormat = value;
        }

        public void SetImageFormatHasAlpha(bool hasAlpha)
        {
            if (hasAlpha)
            {
                SetImageFormat(VtfReading.GetTransparentVariantOfImageFormat(imageFormat));
            }
            else
            {
                SetImageFormat(VtfReading.GetOpaqueVariantOfImageFormat(imageFormat));
            }
        }

        public void SetImageFormatHasAlphaFromFile(string filePath)
        {
            bool isOpaque = true;

            // omg this is SO ugly i hate this i hate it i hate it i hate it
            // but at least we will be able to read the PSD alpha channel
            // I blame imagemagick for not being able to do this simple operation. Actually blame adobe
            
            if (Path.GetExtension(filePath) == Extensions.Psd)
            {
                bool isFileWatcherSupposedToWatch = FileWatcher.IsWatching();
                if (isFileWatcherSupposedToWatch)
                {
                    FileWatcher.StopWatcher();
                }

                string tempVtfPath = Path.ChangeExtension(filePath, ".vtf");
                string tempTgaPath = Path.ChangeExtension(filePath, ".tga");

                Decisions.MakeAsset(filePath, VtfImportOptionsObject.GenPrefabLossless());
                Decisions.ExportAsset(tempVtfPath, VTFExportOptions.TGA);
                
                using (MagickImage tga = new MagickImage(tempTgaPath))
                {
                    MagickImage alphaChannel = (MagickImage)tga.Separate(Channels.Alpha).First();
                    string meanStr = alphaChannel.FormatExpression("%[fx:mean]");
                    if (meanStr != null)
                    {
                        double meanDouble = Double.Parse(meanStr);
                        isOpaque = (meanDouble == 1);
                        //Program.Alert("" + meanDouble); // debug
                    }
                }

                File.Delete(tempTgaPath);
                File.Delete(tempVtfPath);

                if (isFileWatcherSupposedToWatch)
                {
                    FileWatcher.StartWatcher();
                }
            }
            else
            {
                MagickImage i = new MagickImage(filePath);
                isOpaque = i.IsOpaque;
            }

            if (isOpaque)
            {
                SetImageFormatHasAlpha(false);
            }
            else
            {
                SetImageFormatHasAlpha(true);
            }

            //File.Delete();
        }

        public void SetFlags(uint value)
        {
            flags = value;
        }

        public void SetVersion(uint tens, uint ones)
        {
            versionDigitTens = tens;
            versionDigitOnes = ones;
        }

        public void SetMipEnabled(bool value)
        {
            mipEnabled = value;
        }

        public void SetMipFilters(string samplingFilter, string sharpenFilter)
        {
            mipFilterSampling = samplingFilter;
            mipFilterSharpen = sharpenFilter;
        }

        public void SetResizeEnabled(bool value)
        {
            resizeEnabled = value;
        }

        public void SetResizeSettings(string method, string filter)
        {
            resizeMethod = method;
            resizeFilter = filter;
        }

        public string ToArgumentsString()
        {
            List<string> settingsList = new List<string>();
            settingsList.Add($"-format {VtfReading.ImageFormatToString(imageFormat)}");
            settingsList.Add($"-alphaformat {VtfReading.ImageFormatToString(imageFormat)}");
            settingsList.Add($"-version {versionDigitTens}.{versionDigitOnes}");

            if (resizeEnabled)
            {
                settingsList.Add($"-resize");
                settingsList.Add($"-rmethod {resizeMethod}");
                settingsList.Add($"-rfilter {resizeFilter}");
            }

            if(mipEnabled)
            {
                settingsList.Add($"-mfilter {mipFilterSampling}");
                //settingsList.Add($"-msharpen {mipFilterSharpen}"); // the updated vtfcmd from https://github.com/Sky-rym/VTFEdit-Reloaded doesn't support msharpen
            }
            else
            {
                settingsList.Add($"-nomipmaps");
                // and make sure to have these flags disabled
                flags &= ~(uint)(VtfImageFlag.NOMIP);
                flags &= ~(uint)(VtfImageFlag.NOLOD);
            }


            string settingsString = string.Join(" ", settingsList.ToArray());
            string[] flagsArr = VtfReading.FlagsToStringArr(flags);
            string flagsString = flagsArr.Length > 0 ? (" -flag " + string.Join(" -flag ", flagsArr)) : "";

            return settingsString + flagsString;
        }
    }
}
