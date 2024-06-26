using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace AutoVTF
{
    public partial class MainFormDarkUi : DarkForm
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(Keys Key);

        public string title = Program.title + " " + Program.version;
        public RecentsManager RecentsManager;

        private bool isMouseOverForm = false;
        private string titleImageDragPanel = "Convert To VTF:";
        private string titleVtfDragPanel = "Export VTF To:";
        private List<Label> DragPanelLabels = new List<Label>();
        private bool watchFolderTextboxHasText = true;


        public MainFormDarkUi()
        {
            InitializeComponent();
            this.Text = title;
            VtfDragPanel.Dock = DockStyle.Fill;
            ImageDragPanel.Dock = DockStyle.Fill;
            DragPanelLabels = [ImageDragPanelLabelLossless, ImageDragPanelLabelCompressed, ImageDragPanelLabelAdvanced, VtfDragPanelLabelPNG, VtfDragPanelLabelTGA, VtfDragPanelLabelPSD, VtfDragPanelLabelXCF, VtfDragPanelLabelSimpleVmt];

            for (int i = 0; i < DragPanelLabels.Count; i++)
            {
                DragPanelLabels[i].DragEnter += DragPanelLabel_DragEnter;
                DragPanelLabels[i].DragLeave += DragPanelLabel_DragLeave;
                DragPanelLabels[i].DragDrop += DragPanelLabel_DragDrop;
            }

            RecentsManager = new RecentsManager(RecentsMenuStrip);
            MakeButtonsUnfocusable(this);
            SetChildrenControlsDoubleBuffered(this);
            InitAdvancedImportPanel();
            petPanelInit();
        }

        public string GetWatchFolderTextboxValue()
        {
            return WatchFolderTextbox.Text.Trim();
        }

        public void SetWatchFolderTextboxValue(string value)
        {
            string result = value.Replace("\\", "/").Trim();
            WatchFolderTextbox.Text = result;
            WatchFolderTextbox.Select(result.Length, 0);

            WatchFolderTextbox_TextChanged(null, null);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Settings.Load();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = folderBrowserDialog.SelectedPath;
                SetWatchFolderTextboxValue(path);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Settings.Save();
            base.OnFormClosing(e);
        }

        private void WatchFolderTextbox_DragDrop(object sender, DragEventArgs e)
        {
            if (!IsDraggingOneFolder(sender, e))
                return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null)
                return;

            string folder = files[0];

            SetWatchFolderTextboxValue(folder);
        }

        private void WatchFolderTextbox_DragEnter(object sender, DragEventArgs e)
        {
            if (!IsDraggingOneFolder(sender, e))
                return;

            e.Effect = DragDropEffects.All;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (AdvancedImportPanel.Visible)
            {
                return;
            }

            string[] files = GetDraggingFilePaths(sender, e);
            if (files == null)
                return;

            bool is_images = true;
            bool is_vtf = true;

            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                string ext = Path.GetExtension(file).ToLower();

                if (!Extensions.ImageExtensions.Contains(ext))
                {
                    is_images = false;
                }

                if (ext != Extensions.Vtf)
                {
                    is_vtf = false;
                }
            }

            if (is_images)
            {
                ShowImageDragPanel();
                return;
            }

            if (is_vtf)
            {
                ShowVtfDragPanel();
                return;
            }

            // is something else or combination of other unsupported extensions
        }

        private void MainForm_DragLeave(object sender, EventArgs e)
        {

        }

        private bool IsDraggingOneFolder(object sender, DragEventArgs e)
        {
            string[] files = GetDraggingFilePaths(sender, e);

            if (files == null)
                return false;

            string file = files[0];
            bool is_folder = File.GetAttributes(file).HasFlag(FileAttributes.Directory);

            if (!is_folder)
                return false;

            return true;
        }

        private string[] GetDraggingFilePaths(object sender, DragEventArgs e)
        {
            if (e.Data == null)
                return null;

            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return null;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null)
                return null;

            return files;
        }

        private void HideDragPanels()
        {
            if (ImageDragPanel.Visible)
            {
                HideImageDragPanel();
            }

            if (VtfDragPanel.Visible)
            {
                HideVTFDragPanel();
            }
        }

        private void ShowImageDragPanel()
        {
            DragPanelsTimerStart();
            ImageDragPanel.Visible = true;

            ResetDragPanelLabels();
            this.Refresh();
            ImageDragPanel.Refresh();
            this.Text = titleImageDragPanel;
        }

        private void HideImageDragPanel()
        {
            ImageDragPanel.Visible = false;
            this.Refresh();
            ImageDragPanel.Refresh();
            this.Text = title;
        }

        private void ShowVtfDragPanel()
        {
            DragPanelsTimerStart();
            VtfDragPanel.Visible = true;

            ResetDragPanelLabels();
            this.Refresh();
            VtfDragPanel.Refresh();
            this.Text = titleVtfDragPanel;
        }

        private void HideVTFDragPanel()
        {
            VtfDragPanel.Visible = false;
            this.Refresh();
            VtfDragPanel.Refresh();
            this.Text = title;
        }

        private void ResetDragPanelLabels()
        {
            for (int i = 0; i < DragPanelLabels.Count; i++)
            {
                UnhighlightLabel(DragPanelLabels[i]);
            }
        }

        private void DragPanelLabel_DragEnter(object sender, DragEventArgs e)
        {
            HighlightLabel((Label)sender);

            e.Effect = DragDropEffects.Copy;
        }

        private void DragPanelLabel_DragLeave(object sender, EventArgs e)
        {
            UnhighlightLabel((Label)sender);
        }

        private void DragPanelLabel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = GetDraggingFilePaths(sender, e);
            VtfImportOptionsObject importOptions = null;
            Label label = (Label)sender;


            if (label == ImageDragPanelLabelLossless)
            {
                importOptions = VtfImportOptionsObject.GenPrefabLossless();
            }
            else if (label == ImageDragPanelLabelCompressed)
            {
                importOptions = VtfImportOptionsObject.GenPrefabCompressed();
            }
            else if (label == ImageDragPanelLabelAdvanced)
            {
                ShowAdvancedImportPanel();
                AdvancedImportFilesList.Clear();
                AdvancedImportFilesList.AddRange(files);
                HideDragPanels();
                return;
            }

            HideDragPanels();
            for (int i = 0; i < files.Length; i++)
            {
                string filePath = files[i];

                if (importOptions == null)
                {
                    // exporting
                    if (label == VtfDragPanelLabelPSD)
                    {
                        Task.Run(() => { Decisions.ExportAssetToPsd(filePath); });
                        continue;
                    }
                    else if (label == VtfDragPanelLabelXCF)
                    {
                        Task.Run(() => { Decisions.ExportAssetToXcf(filePath); });
                        continue;
                    }
                    else if (label == VtfDragPanelLabelSimpleVmt)
                    {
                        Task.Run(() => { Decisions.MakeSimpleVmt(filePath); });
                        continue;
                    }

                    string exportOptions = GetExportOptions(label);

                    Task.Run(() => { Decisions.ExportAsset(filePath, exportOptions); });
                    continue;
                }

                VtfImageFormat imageFormat = importOptions.GetImageFormat();

                // load vtf options and override image format with ours
                string vtfPath = Path.ChangeExtension(filePath, Extensions.Vtf);
                bool overwritingExistingVtf = File.Exists(vtfPath);

                if (overwritingExistingVtf)
                {
                    importOptions.SetFromVtf(vtfPath);
                }

                importOptions.SetImageFormat(imageFormat);

                if (overwritingExistingVtf)
                {
                    importOptions.SetImageFormatHasAlphaFromFile(filePath);
                }

                Task.Run(() => { Decisions.MakeAsset(filePath, importOptions); });
            }
        }

        private string GetExportOptions(Label label)
        {

            string value = "";
            if (label == VtfDragPanelLabelPNG)
            {
                value = VTFExportOptions.PNG;
            }
            else if (label == VtfDragPanelLabelTGA)
            {
                value = VTFExportOptions.TGA;
            }

            return value;
        }

        private void HighlightLabel(Label label)
        {
            label.ForeColor = DarkUI.Config.Colors.DarkBackground;
            label.BackColor = DarkUI.Config.Colors.BlueHighlight;
        }

        private void UnhighlightLabel(Label label)
        {
            label.ForeColor = DarkUI.Config.Colors.LightText;
            label.BackColor = DarkUI.Config.Colors.DarkBackground;
        }

        private int dragTimerDelayedExitTicksDuration = 8;
        private int dragTimerTicksWithoutMouseOver = 0;


        private void DragPanelsTimerStart()
        {
            dragPanelsTimer.Start();
        }

        private void DragPanelsTimerStop()
        {
            dragPanelsTimer.Stop();
            HideDragPanels();
        }

        private void dragPanelsTimer_Tick(object sender, EventArgs e)
        {
            HandleMouseState();
            HandleKayboardState();

            if (!isMouseOverForm)
            {
                dragTimerTicksWithoutMouseOver++;
            }
            else
            {
                dragTimerTicksWithoutMouseOver = 0;
            }

            if (dragTimerTicksWithoutMouseOver > dragTimerDelayedExitTicksDuration)
            {
                DragPanelsTimerStop();
                return;
            }
        }

        private void HandleKayboardState()
        {
            if (GetAsyncKeyState(Keys.Escape))
            {
                DragPanelsTimerStop();
            }
        }

        private void HandleMouseState()
        {
            var mp = new Point(); // mouse pointer
            GetCursorPos(ref mp);

            if (mp.X > this.Left && mp.X < this.Right && mp.Y > this.Top && mp.Y < this.Bottom)
            {
                if (isMouseOverForm == false)
                {
                    OnMouseEnterForm();
                }
                isMouseOverForm = true;
                return;
            }

            if (isMouseOverForm == false)
            {
                OnMouseLeaveForm();
            }
            isMouseOverForm = false;
        }

        private void OnMouseEnterForm()
        {

        }

        private void OnMouseLeaveForm()
        {

        }

        private void StartWatchingButton_Click(object sender, EventArgs e)
        {
            string watchFolderText = GetWatchFolderTextboxValue();

            if (watchFolderText.Trim() == "")
            {
                HighlightWatchFolderTextbox();
                Program.Alert(AlertMessages.WatchFolderEmpty);
                return;
            }

            if (!Directory.Exists(watchFolderText))
            {
                HighlightWatchFolderTextbox();
                Program.Alert(AlertMessages.WatchFolderInvalid);
                return;
            }

            OnStartWatchingSuccess();
        }
        private void UnhighlightWatchFolderTextbox()
        {
            WatchFolderTextbox.BackColor = DarkUI.Config.Colors.GreyBackground;
            WatchFolderTextbox.ForeColor = DarkUI.Config.Colors.LightText;
        }
        private void HighlightWatchFolderTextbox()
        {
            WatchFolderTextbox.BackColor = DarkUI.Config.Colors.LightText;
            WatchFolderTextbox.ForeColor = DarkUI.Config.Colors.GreyBackground;
        }

        private void StopWatchingButton_Click(object sender, EventArgs e)
        {
            OnStopWatching();
        }

        private void OnStopWatching()
        {
            // ui
            StartWatchingButton.Enabled = true;
            StopWatchingButton.Enabled = false;

            BrowseButton.Enabled = true;
            GotoButton.Enabled = true;
            WatchFolderTextbox.Enabled = true;
            RecentsButton.Enabled = true;

            // other
            FileWatcher.StopWatcher();
        }

        private void OnStartWatchingSuccess()
        {
            // ui
            StartWatchingButton.Enabled = false;
            StopWatchingButton.Enabled = true;

            BrowseButton.Enabled = false;
            GotoButton.Enabled = false;
            WatchFolderTextbox.Enabled = false;
            RecentsButton.Enabled = false;
            
            // other
            FileWatcher.StartWatcher();
            RecentsManager.Register(WatchFolderTextbox.Text);
        }

        private void WatchFolderTextbox_TextChanged(object sender, EventArgs e)
        {
            sender = null;
            e = null;


            // disable goto button if empty (why? it was suggested in an issue, i thought it was a good idea then, but now idk. but anyway its a nice feature ig)
            bool textBefore = watchFolderTextboxHasText;
            bool textNow = WatchFolderTextbox.Text.Length > 0;

            if (textBefore == false && textNow == true)
            {
                GotoButton.Enabled = true;
            }

            if (textBefore == true && textNow == false)
            {
                GotoButton.Enabled = false;
            }

            watchFolderTextboxHasText = textNow;

            UnhighlightWatchFolderTextbox();
        }

        private void AdvancedImportPanel_GenMipmaps_CheckedChanged(object sender, EventArgs e)
        {
            AdvancedImportPanel_MipmapFilter.Enabled = AdvancedImportPanel_GenMipmaps.Checked;
            panel2.Visible = !AdvancedImportPanel_GenMipmaps.Checked;
            label4.ForeColor = AdvancedImportPanel_GenMipmaps.Checked ? DarkUI.Config.Colors.LightText : DarkUI.Config.Colors.DisabledText;
        }

        private void AdvancedImportPanel_Cancel_Click(object sender, EventArgs e)
        {
            HideAdvancedImportPanel();
        }

        private void AdvancedImportPanel_Ok_Click(object sender, EventArgs e)
        {
            VtfImageFormat imageFormat = (VtfImageFormat)AdvancedImportPanel_ImageFormat.SelectedItem;
            string[] versionStringArr = AdvancedImportPanel_VtfVersion.SelectedItem.ToString().Split('.');
            VtfImportOptionsObject o = new VtfImportOptionsObject(imageFormat);
            o.SetVersion(uint.Parse(versionStringArr[0]), uint.Parse(versionStringArr[1]));
            o.SetMipEnabled(AdvancedImportPanel_GenMipmaps.Checked);
            o.SetMipFilters((string)AdvancedImportPanel_MipmapFilter.SelectedItem, VtfImportOptionsObject.DEFAULT_MIP_FILTER_SHARPEN);
            uint flags = VtfImportOptionsObject.DEFAULT_FLAGS;
            flags |= AdvancedImportPanel_F_ClampS.Checked ? (uint)VtfImageFlag.CLAMPS : 0;
            flags |= AdvancedImportPanel_F_ClampT.Checked ? (uint)VtfImageFlag.CLAMPT : 0;
            flags |= AdvancedImportPanel_F_PointSample.Checked ? (uint)VtfImageFlag.POINTSAMPLE : 0;
            flags |= AdvancedImportPanel_F_NoLod.Checked ? (uint)VtfImageFlag.NOLOD : 0;
            flags |= AdvancedImportPanel_F_Anisotropic.Checked ? (uint)VtfImageFlag.ANISOTROPIC : 0;
            o.SetFlags(flags);
            foreach (string file in AdvancedImportFilesList)
            {
                Decisions.MakeAsset(file, o);
            }
            HideAdvancedImportPanel();
        }

        private void ShowAdvancedImportPanel()
        {
            AdvancedImportPanel.Visible = true;
            AdvancedImportPanel_ImageFormat.Refresh();
            AdvancedImportPanel_VtfVersion.Refresh();
            AdvancedImportPanel_MipmapFilter.Refresh();
            this.Refresh();
            AdvancedImportPanel.Refresh();
        }

        private void HideAdvancedImportPanel()
        {
            AdvancedImportPanel.Visible = false;
            this.Refresh();
            AdvancedImportPanel.Refresh();
        }

        private void InitAdvancedImportPanel()
        {
            AdvancedImportPanel.Dock = DockStyle.Fill;
            AdvancedImportPanel.Visible = false;

            AdvancedImportPanel_ImageFormat.Items.Add(VtfImageFormat.RGB565);
            AdvancedImportPanel_ImageFormat.Items.Add(VtfImageFormat.RGB888);
            AdvancedImportPanel_ImageFormat.Items.Add(VtfImageFormat.BGRA8888);
            AdvancedImportPanel_ImageFormat.Items.Add(VtfImageFormat.I8);
            AdvancedImportPanel_ImageFormat.Items.Add(VtfImageFormat.IA88);
            AdvancedImportPanel_ImageFormat.Items.Add(VtfImageFormat.DXT1);
            AdvancedImportPanel_ImageFormat.Items.Add(VtfImageFormat.DXT5);

            AdvancedImportPanel_MipmapFilter.Items.Add("Point");
            AdvancedImportPanel_MipmapFilter.Items.Add("Box");
            AdvancedImportPanel_MipmapFilter.Items.Add("Triangle");
            AdvancedImportPanel_MipmapFilter.Items.Add("Quadratic");
            AdvancedImportPanel_MipmapFilter.Items.Add("Cubic");
            AdvancedImportPanel_MipmapFilter.Items.Add("Catrom");
            AdvancedImportPanel_MipmapFilter.Items.Add("Mitchell");
            AdvancedImportPanel_MipmapFilter.Items.Add("Gaussian");
            AdvancedImportPanel_MipmapFilter.Items.Add("Sinc");
            AdvancedImportPanel_MipmapFilter.Items.Add("Bessel");
            AdvancedImportPanel_MipmapFilter.Items.Add("Hanning");
            AdvancedImportPanel_MipmapFilter.Items.Add("Hamming");
            AdvancedImportPanel_MipmapFilter.Items.Add("Blackman");
            AdvancedImportPanel_MipmapFilter.Items.Add("Kaiser");

            AdvancedImportPanel_VtfVersion.Items.Add("7.0");
            AdvancedImportPanel_VtfVersion.Items.Add("7.1");
            AdvancedImportPanel_VtfVersion.Items.Add("7.2");
            AdvancedImportPanel_VtfVersion.Items.Add("7.3");
            AdvancedImportPanel_VtfVersion.Items.Add("7.4");

            AdvancedImportPanel_GenMipmaps_CheckedChanged(null, null);
        }

        private List<string> AdvancedImportFilesList = new List<string>();

        private void GotoButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(GetWatchFolderTextboxValue()))
            {
                HighlightWatchFolderTextbox();
                Program.Alert(AlertMessages.WatchFolderInvalid);
                return;
            }

            try
            {
                Process.Start("explorer.exe", Path.GetFullPath(GetWatchFolderTextboxValue()));
            }
            catch
            {
                HighlightWatchFolderTextbox();
                Program.Alert(AlertMessages.WatchFolderInvalid);
            }
        }

        // :3
        Cursor petCursor1 = new Cursor(Properties.Resources.Cursor1.Handle);
        Cursor petCursor2 = new Cursor(Properties.Resources.Cursor2.Handle);
        private byte petCount = 0;

        private void petPanelInit()
        {
            petPanel.Cursor = petCursor1;
        }

        private void petPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                petPanel.Cursor = petCursor2;
                petCount++;
            }

            if (petCount == 255)
            {
                this.Text = "meow!";
            }
        }

        private void petPanel_MouseUp(object sender, MouseEventArgs e)
        {
            petPanel.Cursor = petCursor1;
        }

        private void SetChildrenControlsDoubleBuffered(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                ClassExtensions.SetControlDoubleBuffered(c, true);
                SetChildrenControlsDoubleBuffered(c);
            }
        }

        private void MakeButtonsUnfocusable(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is DarkButton)
                {
                    c.GotFocus += RedirectFocusAwayFromSelf;
                }
                MakeButtonsUnfocusable(c);
            }
        }

        private void RedirectFocusAwayFromSelf(object? sender, EventArgs e)
        {
            this.Focus();
        }

        // double buffering flickering issue fix??? i guess??
        BufferedGraphics g;
        BufferedGraphicsContext gc;

        private void MainFormDarkUi_Paint(object sender, PaintEventArgs e)
        {
            g.Graphics.Clear(this.BackColor);
            g.Render();
        }

        private void MainFormDarkUi_Load(object sender, EventArgs e)
        {
            gc = BufferedGraphicsManager.Current;
            g = gc.Allocate(this.CreateGraphics(), this.ClientRectangle);
        }
        // fix ends here

        private void WatchFolderRecentsButton_Click(object sender, EventArgs e)
        {
            Point showPosition = RecentsButton.PointToScreen(new Point(0, 0));
            showPosition.Y += WatchFolderTextbox.Height;
            showPosition.Y -= 1;

            RecentsManager.Show(showPosition);
        }
    }

    public static class ClassExtensions
    {
        public static void SetControlDoubleBuffered(this Control control, bool enabled)
        {
            var controlProperty1 = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            controlProperty1.SetValue(control, true, null);
        }
    }
}