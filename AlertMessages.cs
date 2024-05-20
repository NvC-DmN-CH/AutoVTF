namespace AutoVTF
{
    public static class AlertMessages
    {
        public const string WatchFolderInvalid = "The program thinks that this folder doesn't exist.\n(Check spelling or just drag and drop a folder in the text field)";
        public const string VmtNoMaterialsFolder = "Couldn't construct a correct path inside the VMT. It will be correct if the file is in a \"materials\" folder.\n";
        public const string VmtWriteFail = "Couldn't write VMT to the system";
        public const string ExportPsdFail = "Couldn't export PSD";
        public const string ConvertToPsdFail = "Couldn't convert to PSD";
        public const string ConvertToXcfFail = "Couldn't convert to XCF";
        public const string VtfHeaderReadFail = "Couldn't read header of VTF";
        public const string UnknownImageFormat = "Unknown image format";
        public const string InvalidFilesDragged = $"Dropped files aren't supported. You can drag multiple images or VTF files";
    }
}