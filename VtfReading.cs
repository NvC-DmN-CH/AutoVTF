using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.DataFormats;

namespace AutoVTF
{
    enum VtfImageFlag : uint
    {
        POINTSAMPLE = 0x00000001,
        TRILINEAR = 0x00000002,
        CLAMPS = 0x00000004,
        CLAMPT = 0x00000008,
        ANISOTROPIC = 0x00000010,
        HINT_DXT5 = 0x00000020,
        SRGB = 0x00000040,
        DEPRECATED_NOCOMPRESS = 0x00000040,
        NORMAL = 0x00000080,
        NOMIP = 0x00000100,
        NOLOD = 0x00000200,
        MINMIP = 0x00000400,
        PROCEDURAL = 0x00000800,
        ONEBITALPHA = 0x00001000,
        EIGHTBITALPHA = 0x00002000,
        ENVMAP = 0x00004000,
        RENDERTARGET = 0x00008000,
        DEPTHRENDERTARGET = 0x00010000,
        NODEBUGOVERRIDE = 0x00020000,
        SINGLECOPY = 0x00040000,
        UNUSED0 = 0x00080000,
        DEPRECATED_ONEOVERMIPLEVELINALPHA = 0x00080000,
        UNUSED1 = 0x00100000,
        DEPRECATED_PREMULTCOLORBYONEOVERMIPLEVEL = 0x00100000,
        UNUSED2 = 0x00200000,
        DEPRECATED_NORMALTODUDV = 0x00200000,
        UNUSED3 = 0x00400000,
        DEPRECATED_ALPHATESTMIPGENERATION = 0x00400000,
        NODEPTHBUFFER = 0x00800000,
        UNUSED4 = 0x01000000,
        DEPRECATED_NICEFILTERED = 0x01000000,
        CLAMPU = 0x02000000,
        VERTEXTEXTURE = 0x04000000,
        SSBUMP = 0x08000000,
        UNUSED5 = 0x10000000,
        DEPRECATED_UNFILTERABLE_OK = 0x10000000,
        BORDER = 0x20000000,
        DEPRECATED_SPECVAR_RED = 0x40000000,
//      DEPRECATED_SPECVAR_ALPHA = 0x80000000,
        LAST = 0x20000000,
        COUNT = 30
    }

    enum VtfImageFormat
    {
        RGB888 = 2,
        RGB565 = 4,
        I8 = 5,
        IA88 = 6,
        BGRA8888 = 12,
        DXT1 = 13,
        DXT5 = 15
    }

    internal class VtfHeader
    {
        public string signature;
        public uint versionDigitTens;
        public uint versionDigitOnes;
        public uint headerSize;
        public ushort width;
        public ushort height;
        public uint flags;
        public ushort frames;
        public ushort firstFrame;
        public float[] reflectivityVec;
        public float bumpmapScale;
        public int highResImageFormat;
        public byte mipmapCount; // 1 + mipmaps, because the image is the largest mipmap

        public bool HasMipmaps()
        {
            return mipmapCount > 1;
        }

        public override string ToString()
        {
            string[] contents = {
            $"signature: {signature[0]}{signature[1]}{signature[2]}",
            $"version: {versionDigitTens}.{versionDigitOnes}",
            $"header size: {headerSize}",
            $"width: {width}",
            $"height: {height}",
            $"flags: {flags}",
            $"frames: {frames}",
            $"firstFrame: {firstFrame}",
            $"reflectivity: [{reflectivityVec[0]}, {reflectivityVec[1]}, {reflectivityVec[2]}]",
            $"bumpmap scale: {bumpmapScale}",
            $"highResImageFormat: {highResImageFormat}",
            $"mipmapCount: {mipmapCount}",
            };

            return String.Join("\n", contents);
        }
    }

    internal class VtfReading
    {
        public static VtfHeader ReadHeader(string filePath)
        {
            try
            {
                using (FileStream fs = File.OpenRead(filePath))
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        char[] signature = reader.ReadChars(4);
                        uint versionNumberTens = reader.ReadUInt32();
                        uint versionNumberOnes = reader.ReadUInt32();
                        uint headerSize = reader.ReadUInt32();
                        ushort width = reader.ReadUInt16();
                        ushort height = reader.ReadUInt16();
                        uint flags = reader.ReadUInt32();
                        ushort frames = reader.ReadUInt16();
                        ushort firstFrame = reader.ReadUInt16();
                        reader.ReadChars(4); // padding
                        float reflectivityVecX = reader.ReadSingle();
                        float reflectivityVecY = reader.ReadSingle();
                        float reflectivityVecZ = reader.ReadSingle();
                        reader.ReadChars(4); // padding
                        float bumpmapScale = reader.ReadSingle();
                        int highResImageFormat = reader.ReadInt32();
                        byte mipmapCount = reader.ReadByte();

                        VtfHeader h = new VtfHeader();
                        h.signature = $"{signature[0]}{signature[1]}{signature[2]}";
                        h.versionDigitTens = versionNumberTens;
                        h.versionDigitOnes = versionNumberOnes;
                        h.headerSize = headerSize;
                        h.width = width;
                        h.height = height;
                        h.flags = flags;
                        h.frames = frames;
                        h.firstFrame = firstFrame;
                        h.reflectivityVec = [reflectivityVecX, reflectivityVecY, reflectivityVecZ];
                        h.bumpmapScale = bumpmapScale;
                        h.highResImageFormat = highResImageFormat;
                        h.mipmapCount = mipmapCount;
                        return h;
                    }
                }
            }
            catch
            {
                Program.Alert(AlertMessages.VtfHeaderReadFail + " \"{filePath}\"");
            }
            return null;
        }

        public static string[] FlagsToStringArr(uint flags)
        {
            List<string> flagsStringList = new List<string>();

            uint mask = 0x00000001;
            for (int i = 0; i < 32; i++)
            {
                VtfImageFlag flag = (VtfImageFlag)(flags & mask);
                string flagString = FlagToString(flag);
                if (flagString != null)
                {
                    flagsStringList.Add(flagString);
                }
                mask <<= 1;
            }
            return flagsStringList.ToArray();
        }

        private static string FlagToString(VtfImageFlag flag)
        {
            switch (flag)
            {
                case VtfImageFlag.POINTSAMPLE: return "POINTSAMPLE";
                case VtfImageFlag.TRILINEAR: return "TRILINEAR";
                case VtfImageFlag.CLAMPS: return "CLAMPS";
                case VtfImageFlag.CLAMPT: return "CLAMPT";
                case VtfImageFlag.ANISOTROPIC: return "ANISOTROPIC";
                case VtfImageFlag.HINT_DXT5: return "HINT_DXT5";
                case VtfImageFlag.NORMAL: return "NORMAL";
                case VtfImageFlag.NOMIP: return "NOMIP";
                case VtfImageFlag.NOLOD: return "NOLOD";
                case VtfImageFlag.MINMIP: return "MINMIP";
                case VtfImageFlag.PROCEDURAL: return "PROCEDURAL";
                case VtfImageFlag.RENDERTARGET: return "RENDERTARGET";
                case VtfImageFlag.DEPTHRENDERTARGET: return "DEPTHRENDERTARGET";
                case VtfImageFlag.NODEBUGOVERRIDE: return "NODEBUGOVERRIDE";
                case VtfImageFlag.SINGLECOPY: return "SINGLECOPY";
                case VtfImageFlag.NODEPTHBUFFER: return "NODEPTHBUFFER";
                case VtfImageFlag.CLAMPU: return "CLAMPU";
                case VtfImageFlag.VERTEXTEXTURE: return "VERTEXTEXTURE";
                case VtfImageFlag.SSBUMP: return "SSBUMP";
                case VtfImageFlag.BORDER: return "BORDER";
            }

            return null;
        }

        public static string ImageFormatToString(VtfImageFormat value)
        {
            switch (value)
            {
                case VtfImageFormat.RGB888: return "RGB888";
                case VtfImageFormat.RGB565: return "RGB565";
                case VtfImageFormat.I8: return "I8";
                case VtfImageFormat.IA88: return "IA88";
                case VtfImageFormat.BGRA8888: return "BGRA8888";
                case VtfImageFormat.DXT1: return "DXT1";
                case VtfImageFormat.DXT5: return "DXT5";
            }
            Program.Alert(AlertMessages.UnknownImageFormat + ": " + value);
            return null;
        }

        public static VtfImageFormat GetOpaqueVariantOfImageFormat(VtfImageFormat value)
        {
            switch (value)
            {
                case VtfImageFormat.IA88: return VtfImageFormat.I8;
                case VtfImageFormat.DXT5: return VtfImageFormat.DXT1;
                case VtfImageFormat.BGRA8888: return VtfImageFormat.RGB888;
            }
            return value;
        }

        public static VtfImageFormat GetTransparentVariantOfImageFormat(VtfImageFormat value)
        {
            switch (value)
            {
                case VtfImageFormat.I8: return VtfImageFormat.IA88;
                case VtfImageFormat.DXT1: return VtfImageFormat.DXT5;
                case VtfImageFormat.RGB888: return VtfImageFormat.BGRA8888;
                //case VtfImageFormat.RGB565: return VtfImageFormat.BGRA8888; // choosing BGRA888 because there isn't an alpha counterpart of RGB565
            }
            return value;
        }
    }
}
