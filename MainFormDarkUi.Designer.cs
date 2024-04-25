using DarkUI.Controls;
using DarkUI.Docking;

namespace AutoVTF
{
    partial class MainFormDarkUi
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormDarkUi));
            WatchFolderTextbox = new DarkTextBox();
            StartWatchingButton = new DarkButton();
            BrowseButton = new DarkButton();
            folderBrowserDialog = new FolderBrowserDialog();
            hintlabel = new DarkLabel();
            VtfDragPanel = new Panel();
            VtfDragPanelLabelSimpleVmt = new DarkLabel();
            VtfDragPanelLabelXCF = new DarkLabel();
            VtfDragPanelLabelPSD = new DarkLabel();
            VtfDragPanelLabelTGA = new DarkLabel();
            VtfDragPanelLabelPNG = new DarkLabel();
            ImageDragPanel = new Panel();
            ImageDragPanelLabelAdvanced = new DarkLabel();
            ImageDragPanelLabelCompressed = new DarkLabel();
            ImageDragPanelLabelLossless = new DarkLabel();
            dragPanelsTimer = new System.Windows.Forms.Timer(components);
            StopWatchingButton = new DarkButton();
            AdvancedImportPanel = new Panel();
            panel2 = new Panel();
            groupBox3 = new DarkGroupBox();
            AdvancedImportPanel_F_Anisotropic = new DarkCheckBox();
            AdvancedImportPanel_F_NoLod = new DarkCheckBox();
            AdvancedImportPanel_F_PointSample = new DarkCheckBox();
            AdvancedImportPanel_F_ClampT = new DarkCheckBox();
            AdvancedImportPanel_F_ClampS = new DarkCheckBox();
            AdvancedImportPanel_VtfVersion = new DarkComboBox();
            AdvancedImportPanel_ImageFormat = new DarkComboBox();
            groupBox2 = new DarkGroupBox();
            label2 = new DarkLabel();
            label3 = new DarkLabel();
            label5 = new DarkLabel();
            AdvancedImportPanel_GenMipmaps = new DarkCheckBox();
            AdvancedImportPanel_MipmapFilter = new DarkComboBox();
            groupBox1 = new DarkGroupBox();
            label4 = new DarkLabel();
            AdvancedImportPanel_Cancel = new DarkButton();
            AdvancedImportPanel_Ok = new DarkButton();
            toolTip = new ToolTip(components);
            groupBox4 = new DarkGroupBox();
            GotoButton = new DarkButton();
            panel1 = new Panel();
            petPanel = new Panel();
            VtfDragPanel.SuspendLayout();
            ImageDragPanel.SuspendLayout();
            AdvancedImportPanel.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox4.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // WatchFolderTextbox
            // 
            WatchFolderTextbox.AllowDrop = true;
            WatchFolderTextbox.BackColor = Color.FromArgb(69, 73, 74);
            WatchFolderTextbox.BorderStyle = BorderStyle.FixedSingle;
            WatchFolderTextbox.Font = new Font("Segoe UI", 9F);
            WatchFolderTextbox.ForeColor = Color.FromArgb(220, 220, 220);
            WatchFolderTextbox.Location = new Point(7, 21);
            WatchFolderTextbox.MaximumSize = new Size(500, 23);
            WatchFolderTextbox.MinimumSize = new Size(0, 23);
            WatchFolderTextbox.Name = "WatchFolderTextbox";
            WatchFolderTextbox.Size = new Size(166, 23);
            WatchFolderTextbox.TabIndex = 4;
            WatchFolderTextbox.TextChanged += WatchFolderTextbox_TextChanged;
            WatchFolderTextbox.DragDrop += WatchFolderTextbox_DragDrop;
            WatchFolderTextbox.DragEnter += WatchFolderTextbox_DragEnter;
            // 
            // StartWatchingButton
            // 
            StartWatchingButton.Font = new Font("Lucida Console", 9F);
            StartWatchingButton.Location = new Point(5, 52);
            StartWatchingButton.Name = "StartWatchingButton";
            StartWatchingButton.Padding = new Padding(5);
            StartWatchingButton.Size = new Size(167, 43);
            StartWatchingButton.TabIndex = 8;
            StartWatchingButton.Text = "Start";
            StartWatchingButton.Click += StartWatchingButton_Click;
            // 
            // BrowseButton
            // 
            BrowseButton.Font = new Font("Segoe UI", 9F);
            BrowseButton.Location = new Point(178, 21);
            BrowseButton.Name = "BrowseButton";
            BrowseButton.Padding = new Padding(5);
            BrowseButton.Size = new Size(72, 23);
            BrowseButton.TabIndex = 1;
            BrowseButton.Text = "Browse...";
            BrowseButton.Click += BrowseButton_Click;
            // 
            // hintlabel
            // 
            hintlabel.AutoSize = true;
            hintlabel.BackColor = Color.Transparent;
            hintlabel.ForeColor = Color.FromArgb(220, 220, 220);
            hintlabel.Location = new Point(-1, 211);
            hintlabel.Name = "hintlabel";
            hintlabel.Size = new Size(0, 15);
            hintlabel.TabIndex = 9;
            // 
            // VtfDragPanel
            // 
            VtfDragPanel.Controls.Add(VtfDragPanelLabelSimpleVmt);
            VtfDragPanel.Controls.Add(VtfDragPanelLabelXCF);
            VtfDragPanel.Controls.Add(VtfDragPanelLabelPSD);
            VtfDragPanel.Controls.Add(VtfDragPanelLabelTGA);
            VtfDragPanel.Controls.Add(VtfDragPanelLabelPNG);
            VtfDragPanel.Location = new Point(330, 106);
            VtfDragPanel.Name = "VtfDragPanel";
            VtfDragPanel.Size = new Size(41, 33);
            VtfDragPanel.TabIndex = 10;
            VtfDragPanel.Visible = false;
            // 
            // VtfDragPanelLabelSimpleVmt
            // 
            VtfDragPanelLabelSimpleVmt.AllowDrop = true;
            VtfDragPanelLabelSimpleVmt.BackColor = SystemColors.Control;
            VtfDragPanelLabelSimpleVmt.BorderStyle = BorderStyle.FixedSingle;
            VtfDragPanelLabelSimpleVmt.Font = new Font("Segoe UI", 14F);
            VtfDragPanelLabelSimpleVmt.ForeColor = Color.FromArgb(220, 220, 220);
            VtfDragPanelLabelSimpleVmt.Location = new Point(98, 12);
            VtfDragPanelLabelSimpleVmt.Name = "VtfDragPanelLabelSimpleVmt";
            VtfDragPanelLabelSimpleVmt.Size = new Size(228, 50);
            VtfDragPanelLabelSimpleVmt.TabIndex = 3;
            VtfDragPanelLabelSimpleVmt.Text = "Simple VMT";
            VtfDragPanelLabelSimpleVmt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VtfDragPanelLabelXCF
            // 
            VtfDragPanelLabelXCF.AllowDrop = true;
            VtfDragPanelLabelXCF.BackColor = SystemColors.Control;
            VtfDragPanelLabelXCF.BorderStyle = BorderStyle.FixedSingle;
            VtfDragPanelLabelXCF.Enabled = false;
            VtfDragPanelLabelXCF.Font = new Font("Segoe UI", 18F);
            VtfDragPanelLabelXCF.ForeColor = Color.FromArgb(220, 220, 220);
            VtfDragPanelLabelXCF.Location = new Point(98, 12);
            VtfDragPanelLabelXCF.Name = "VtfDragPanelLabelXCF";
            VtfDragPanelLabelXCF.Size = new Size(89, 50);
            VtfDragPanelLabelXCF.TabIndex = 2;
            VtfDragPanelLabelXCF.Text = "XCF";
            VtfDragPanelLabelXCF.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VtfDragPanelLabelPSD
            // 
            VtfDragPanelLabelPSD.AllowDrop = true;
            VtfDragPanelLabelPSD.BackColor = SystemColors.Control;
            VtfDragPanelLabelPSD.BorderStyle = BorderStyle.FixedSingle;
            VtfDragPanelLabelPSD.Font = new Font("Segoe UI", 18F);
            VtfDragPanelLabelPSD.ForeColor = Color.FromArgb(220, 220, 220);
            VtfDragPanelLabelPSD.Location = new Point(10, 12);
            VtfDragPanelLabelPSD.Name = "VtfDragPanelLabelPSD";
            VtfDragPanelLabelPSD.Size = new Size(89, 50);
            VtfDragPanelLabelPSD.TabIndex = 1;
            VtfDragPanelLabelPSD.Text = "PSD";
            VtfDragPanelLabelPSD.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VtfDragPanelLabelTGA
            // 
            VtfDragPanelLabelTGA.AllowDrop = true;
            VtfDragPanelLabelTGA.BackColor = SystemColors.Control;
            VtfDragPanelLabelTGA.BorderStyle = BorderStyle.FixedSingle;
            VtfDragPanelLabelTGA.Font = new Font("Segoe UI", 18F);
            VtfDragPanelLabelTGA.ForeColor = Color.FromArgb(220, 220, 220);
            VtfDragPanelLabelTGA.Location = new Point(10, 164);
            VtfDragPanelLabelTGA.Name = "VtfDragPanelLabelTGA";
            VtfDragPanelLabelTGA.Size = new Size(89, 50);
            VtfDragPanelLabelTGA.TabIndex = 0;
            VtfDragPanelLabelTGA.Text = "TGA";
            VtfDragPanelLabelTGA.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VtfDragPanelLabelPNG
            // 
            VtfDragPanelLabelPNG.AllowDrop = true;
            VtfDragPanelLabelPNG.BackColor = SystemColors.Control;
            VtfDragPanelLabelPNG.BorderStyle = BorderStyle.FixedSingle;
            VtfDragPanelLabelPNG.Font = new Font("Segoe UI", 32F);
            VtfDragPanelLabelPNG.ForeColor = Color.FromArgb(220, 220, 220);
            VtfDragPanelLabelPNG.Location = new Point(10, 61);
            VtfDragPanelLabelPNG.Name = "VtfDragPanelLabelPNG";
            VtfDragPanelLabelPNG.Size = new Size(316, 153);
            VtfDragPanelLabelPNG.TabIndex = 0;
            VtfDragPanelLabelPNG.Text = "PNG";
            VtfDragPanelLabelPNG.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ImageDragPanel
            // 
            ImageDragPanel.Controls.Add(ImageDragPanelLabelAdvanced);
            ImageDragPanel.Controls.Add(ImageDragPanelLabelCompressed);
            ImageDragPanel.Controls.Add(ImageDragPanelLabelLossless);
            ImageDragPanel.Location = new Point(330, 70);
            ImageDragPanel.Name = "ImageDragPanel";
            ImageDragPanel.Size = new Size(44, 30);
            ImageDragPanel.TabIndex = 11;
            ImageDragPanel.Visible = false;
            // 
            // ImageDragPanelLabelAdvanced
            // 
            ImageDragPanelLabelAdvanced.AllowDrop = true;
            ImageDragPanelLabelAdvanced.BackColor = SystemColors.Control;
            ImageDragPanelLabelAdvanced.BorderStyle = BorderStyle.FixedSingle;
            ImageDragPanelLabelAdvanced.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ImageDragPanelLabelAdvanced.ForeColor = Color.FromArgb(220, 220, 220);
            ImageDragPanelLabelAdvanced.Location = new Point(12, 100);
            ImageDragPanelLabelAdvanced.Name = "ImageDragPanelLabelAdvanced";
            ImageDragPanelLabelAdvanced.Size = new Size(74, 27);
            ImageDragPanelLabelAdvanced.TabIndex = 4;
            ImageDragPanelLabelAdvanced.Text = "Advanced";
            ImageDragPanelLabelAdvanced.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ImageDragPanelLabelCompressed
            // 
            ImageDragPanelLabelCompressed.AllowDrop = true;
            ImageDragPanelLabelCompressed.BackColor = SystemColors.Control;
            ImageDragPanelLabelCompressed.BorderStyle = BorderStyle.FixedSingle;
            ImageDragPanelLabelCompressed.Font = new Font("Segoe UI", 25F);
            ImageDragPanelLabelCompressed.ForeColor = Color.FromArgb(220, 220, 220);
            ImageDragPanelLabelCompressed.Location = new Point(12, 113);
            ImageDragPanelLabelCompressed.Name = "ImageDragPanelLabelCompressed";
            ImageDragPanelLabelCompressed.Size = new Size(312, 101);
            ImageDragPanelLabelCompressed.TabIndex = 3;
            ImageDragPanelLabelCompressed.Text = "Compressed";
            ImageDragPanelLabelCompressed.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ImageDragPanelLabelLossless
            // 
            ImageDragPanelLabelLossless.AllowDrop = true;
            ImageDragPanelLabelLossless.BackColor = SystemColors.Control;
            ImageDragPanelLabelLossless.BorderStyle = BorderStyle.FixedSingle;
            ImageDragPanelLabelLossless.Font = new Font("Segoe UI", 25F);
            ImageDragPanelLabelLossless.ForeColor = Color.FromArgb(220, 220, 220);
            ImageDragPanelLabelLossless.Location = new Point(12, 13);
            ImageDragPanelLabelLossless.Name = "ImageDragPanelLabelLossless";
            ImageDragPanelLabelLossless.Size = new Size(312, 101);
            ImageDragPanelLabelLossless.TabIndex = 1;
            ImageDragPanelLabelLossless.Text = "Lossless";
            ImageDragPanelLabelLossless.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dragPanelsTimer
            // 
            dragPanelsTimer.Interval = 20;
            dragPanelsTimer.Tick += dragPanelsTimer_Tick;
            // 
            // StopWatchingButton
            // 
            StopWatchingButton.Enabled = false;
            StopWatchingButton.Font = new Font("Lucida Console", 9F);
            StopWatchingButton.Location = new Point(178, 51);
            StopWatchingButton.Name = "StopWatchingButton";
            StopWatchingButton.Padding = new Padding(5);
            StopWatchingButton.Size = new Size(128, 43);
            StopWatchingButton.TabIndex = 13;
            StopWatchingButton.Text = "Stop";
            StopWatchingButton.Click += StopWatchingButton_Click;
            // 
            // AdvancedImportPanel
            // 
            AdvancedImportPanel.Controls.Add(panel2);
            AdvancedImportPanel.Controls.Add(groupBox3);
            AdvancedImportPanel.Controls.Add(AdvancedImportPanel_VtfVersion);
            AdvancedImportPanel.Controls.Add(AdvancedImportPanel_ImageFormat);
            AdvancedImportPanel.Controls.Add(groupBox2);
            AdvancedImportPanel.Controls.Add(AdvancedImportPanel_GenMipmaps);
            AdvancedImportPanel.Controls.Add(AdvancedImportPanel_MipmapFilter);
            AdvancedImportPanel.Controls.Add(groupBox1);
            AdvancedImportPanel.Controls.Add(AdvancedImportPanel_Cancel);
            AdvancedImportPanel.Controls.Add(AdvancedImportPanel_Ok);
            AdvancedImportPanel.Location = new Point(330, 145);
            AdvancedImportPanel.Name = "AdvancedImportPanel";
            AdvancedImportPanel.Size = new Size(30, 82);
            AdvancedImportPanel.TabIndex = 14;
            AdvancedImportPanel.Visible = false;
            // 
            // panel2
            // 
            panel2.Location = new Point(104, 133);
            panel2.Name = "panel2";
            panel2.Size = new Size(77, 20);
            panel2.TabIndex = 10;
            // 
            // groupBox3
            // 
            groupBox3.BorderColor = Color.FromArgb(81, 81, 81);
            groupBox3.Controls.Add(AdvancedImportPanel_F_Anisotropic);
            groupBox3.Controls.Add(AdvancedImportPanel_F_NoLod);
            groupBox3.Controls.Add(AdvancedImportPanel_F_PointSample);
            groupBox3.Controls.Add(AdvancedImportPanel_F_ClampT);
            groupBox3.Controls.Add(AdvancedImportPanel_F_ClampS);
            groupBox3.Location = new Point(219, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(106, 158);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Misc";
            // 
            // AdvancedImportPanel_F_Anisotropic
            // 
            AdvancedImportPanel_F_Anisotropic.AutoSize = true;
            AdvancedImportPanel_F_Anisotropic.Location = new Point(6, 121);
            AdvancedImportPanel_F_Anisotropic.Name = "AdvancedImportPanel_F_Anisotropic";
            AdvancedImportPanel_F_Anisotropic.Size = new Size(87, 19);
            AdvancedImportPanel_F_Anisotropic.TabIndex = 12;
            AdvancedImportPanel_F_Anisotropic.Text = "Anisotropic";
            toolTip.SetToolTip(AdvancedImportPanel_F_Anisotropic, "Forces the texture to be rendered with anisotropic filtering,\r\neven if anisotropic filtering is disabled in the game settings.");
            // 
            // AdvancedImportPanel_F_NoLod
            // 
            AdvancedImportPanel_F_NoLod.AutoSize = true;
            AdvancedImportPanel_F_NoLod.Location = new Point(6, 96);
            AdvancedImportPanel_F_NoLod.Name = "AdvancedImportPanel_F_NoLod";
            AdvancedImportPanel_F_NoLod.Size = new Size(68, 19);
            AdvancedImportPanel_F_NoLod.TabIndex = 11;
            AdvancedImportPanel_F_NoLod.Text = "No LOD";
            toolTip.SetToolTip(AdvancedImportPanel_F_NoLod, "Prevents the texture from getting downscaled in low texture quality settings.");
            // 
            // AdvancedImportPanel_F_PointSample
            // 
            AdvancedImportPanel_F_PointSample.AutoSize = true;
            AdvancedImportPanel_F_PointSample.Location = new Point(6, 71);
            AdvancedImportPanel_F_PointSample.Name = "AdvancedImportPanel_F_PointSample";
            AdvancedImportPanel_F_PointSample.Size = new Size(96, 19);
            AdvancedImportPanel_F_PointSample.TabIndex = 10;
            AdvancedImportPanel_F_PointSample.Text = "Point Sample";
            toolTip.SetToolTip(AdvancedImportPanel_F_PointSample, "Makes the texture pixelated.");
            // 
            // AdvancedImportPanel_F_ClampT
            // 
            AdvancedImportPanel_F_ClampT.AutoSize = true;
            AdvancedImportPanel_F_ClampT.Location = new Point(6, 46);
            AdvancedImportPanel_F_ClampT.Name = "AdvancedImportPanel_F_ClampT";
            AdvancedImportPanel_F_ClampT.Size = new Size(70, 19);
            AdvancedImportPanel_F_ClampT.TabIndex = 9;
            AdvancedImportPanel_F_ClampT.Text = "Clamp T";
            toolTip.SetToolTip(AdvancedImportPanel_F_ClampT, "Doesn't let the texture repeat vertically.\r\nThe uppermost and bottommost pixels carry on infinitely.");
            // 
            // AdvancedImportPanel_F_ClampS
            // 
            AdvancedImportPanel_F_ClampS.AutoSize = true;
            AdvancedImportPanel_F_ClampS.Location = new Point(6, 21);
            AdvancedImportPanel_F_ClampS.Name = "AdvancedImportPanel_F_ClampS";
            AdvancedImportPanel_F_ClampS.Size = new Size(70, 19);
            AdvancedImportPanel_F_ClampS.TabIndex = 8;
            AdvancedImportPanel_F_ClampS.Text = "Clamp S";
            toolTip.SetToolTip(AdvancedImportPanel_F_ClampS, "Doesn't let the texture repeat horizontally.\r\nThe leftmost and rightmost pixels carry on infinitely.");
            // 
            // AdvancedImportPanel_VtfVersion
            // 
            AdvancedImportPanel_VtfVersion.DrawMode = DrawMode.OwnerDrawFixed;
            AdvancedImportPanel_VtfVersion.FormattingEnabled = true;
            AdvancedImportPanel_VtfVersion.Location = new Point(102, 62);
            AdvancedImportPanel_VtfVersion.Name = "AdvancedImportPanel_VtfVersion";
            AdvancedImportPanel_VtfVersion.Size = new Size(96, 24);
            AdvancedImportPanel_VtfVersion.TabIndex = 7;
            // 
            // AdvancedImportPanel_ImageFormat
            // 
            AdvancedImportPanel_ImageFormat.DrawMode = DrawMode.OwnerDrawFixed;
            AdvancedImportPanel_ImageFormat.FormattingEnabled = true;
            AdvancedImportPanel_ImageFormat.Location = new Point(102, 33);
            AdvancedImportPanel_ImageFormat.Name = "AdvancedImportPanel_ImageFormat";
            AdvancedImportPanel_ImageFormat.Size = new Size(96, 24);
            AdvancedImportPanel_ImageFormat.TabIndex = 2;
            // 
            // groupBox2
            // 
            groupBox2.BorderColor = Color.FromArgb(81, 81, 81);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label5);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(201, 87);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "General Options";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(5, 25);
            label2.Name = "label2";
            label2.Size = new Size(84, 15);
            label2.TabIndex = 5;
            label2.Text = "Image Format:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(220, 220, 220);
            label3.Location = new Point(6, 53);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 6;
            label3.Text = "VTF Version:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.FromArgb(220, 220, 220);
            label5.Location = new Point(6, 27);
            label5.Name = "label5";
            label5.Size = new Size(0, 15);
            label5.TabIndex = 9;
            // 
            // AdvancedImportPanel_GenMipmaps
            // 
            AdvancedImportPanel_GenMipmaps.AutoSize = true;
            AdvancedImportPanel_GenMipmaps.Checked = true;
            AdvancedImportPanel_GenMipmaps.CheckState = CheckState.Checked;
            AdvancedImportPanel_GenMipmaps.Location = new Point(22, 106);
            AdvancedImportPanel_GenMipmaps.Name = "AdvancedImportPanel_GenMipmaps";
            AdvancedImportPanel_GenMipmaps.Size = new Size(126, 19);
            AdvancedImportPanel_GenMipmaps.TabIndex = 3;
            AdvancedImportPanel_GenMipmaps.Text = "Generate Mipmaps";
            AdvancedImportPanel_GenMipmaps.CheckedChanged += AdvancedImportPanel_GenMipmaps_CheckedChanged;
            // 
            // AdvancedImportPanel_MipmapFilter
            // 
            AdvancedImportPanel_MipmapFilter.DrawMode = DrawMode.OwnerDrawFixed;
            AdvancedImportPanel_MipmapFilter.FormattingEnabled = true;
            AdvancedImportPanel_MipmapFilter.Location = new Point(102, 131);
            AdvancedImportPanel_MipmapFilter.Name = "AdvancedImportPanel_MipmapFilter";
            AdvancedImportPanel_MipmapFilter.Size = new Size(96, 24);
            AdvancedImportPanel_MipmapFilter.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.BorderColor = Color.FromArgb(81, 81, 81);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(12, 106);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(201, 64);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.FromArgb(220, 220, 220);
            label4.Location = new Point(6, 29);
            label4.Name = "label4";
            label4.Size = new Size(84, 15);
            label4.TabIndex = 9;
            label4.Text = "Mipmap Filter:";
            // 
            // AdvancedImportPanel_Cancel
            // 
            AdvancedImportPanel_Cancel.Location = new Point(12, 187);
            AdvancedImportPanel_Cancel.Name = "AdvancedImportPanel_Cancel";
            AdvancedImportPanel_Cancel.Padding = new Padding(5);
            AdvancedImportPanel_Cancel.Size = new Size(65, 27);
            AdvancedImportPanel_Cancel.TabIndex = 0;
            AdvancedImportPanel_Cancel.Text = "Cancel";
            AdvancedImportPanel_Cancel.Click += AdvancedImportPanel_Cancel_Click;
            // 
            // AdvancedImportPanel_Ok
            // 
            AdvancedImportPanel_Ok.Location = new Point(83, 187);
            AdvancedImportPanel_Ok.Name = "AdvancedImportPanel_Ok";
            AdvancedImportPanel_Ok.Padding = new Padding(5);
            AdvancedImportPanel_Ok.Size = new Size(242, 27);
            AdvancedImportPanel_Ok.TabIndex = 1;
            AdvancedImportPanel_Ok.Text = "OK";
            AdvancedImportPanel_Ok.Click += AdvancedImportPanel_Ok_Click;
            // 
            // groupBox4
            // 
            groupBox4.BorderColor = Color.FromArgb(81, 81, 81);
            groupBox4.Controls.Add(GotoButton);
            groupBox4.Controls.Add(WatchFolderTextbox);
            groupBox4.Controls.Add(BrowseButton);
            groupBox4.Controls.Add(StartWatchingButton);
            groupBox4.Controls.Add(StopWatchingButton);
            groupBox4.Font = new Font("Segoe UI", 9F);
            groupBox4.Location = new Point(12, 12);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(312, 101);
            groupBox4.TabIndex = 15;
            groupBox4.TabStop = false;
            groupBox4.Text = "Materials folder to watch:";
            // 
            // GotoButton
            // 
            GotoButton.Font = new Font("Segoe UI", 9F);
            GotoButton.Location = new Point(256, 21);
            GotoButton.Name = "GotoButton";
            GotoButton.Padding = new Padding(5);
            GotoButton.Size = new Size(50, 23);
            GotoButton.TabIndex = 5;
            GotoButton.Text = "Goto";
            GotoButton.Click += GotoButton_Click;
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.splash;
            panel1.BackgroundImageLayout = ImageLayout.Center;
            panel1.Controls.Add(petPanel);
            panel1.Location = new Point(12, 119);
            panel1.Name = "panel1";
            panel1.Size = new Size(312, 97);
            panel1.TabIndex = 2;
            // 
            // petPanel
            // 
            petPanel.BackColor = Color.Transparent;
            petPanel.Location = new Point(179, 12);
            petPanel.Name = "petPanel";
            petPanel.Size = new Size(53, 47);
            petPanel.TabIndex = 0;
            petPanel.MouseDown += petPanel_MouseDown;
            petPanel.MouseUp += petPanel_MouseUp;
            // 
            // MainFormDarkUi
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(336, 226);
            Controls.Add(VtfDragPanel);
            Controls.Add(ImageDragPanel);
            Controls.Add(AdvancedImportPanel);
            Controls.Add(panel1);
            Controls.Add(groupBox4);
            Controls.Add(hintlabel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainFormDarkUi";
            Text = "Titled";
            Load += MainFormDarkUi_Load;
            Shown += MainForm_Shown;
            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;
            DragLeave += MainForm_DragLeave;
            Paint += MainFormDarkUi_Paint;
            VtfDragPanel.ResumeLayout(false);
            ImageDragPanel.ResumeLayout(false);
            AdvancedImportPanel.ResumeLayout(false);
            AdvancedImportPanel.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private FolderBrowserDialog folderBrowserDialog;
        private Panel VtfDragPanel;
        private Panel ImageDragPanel;
        private System.Windows.Forms.Timer dragPanelsTimer;
        private Panel AdvancedImportPanel;
        private ToolTip toolTip;
        private Panel panel1;
        private Panel petPanel;
        private DarkTextBox WatchFolderTextbox;
        private DarkButton StartWatchingButton;
        private DarkButton BrowseButton;
        private DarkLabel hintlabel;
        private DarkLabel VtfDragPanelLabelPNG;
        private DarkLabel VtfDragPanelLabelTGA;
        private DarkLabel ImageDragPanelLabelCompressed;
        private DarkLabel ImageDragPanelLabelLossless;
        private DarkLabel ImageDragPanelLabelAdvanced;
        private DarkButton StopWatchingButton;
        private DarkLabel VtfDragPanelLabelPSD;
        private DarkLabel VtfDragPanelLabelXCF;
        private DarkLabel VtfDragPanelLabelSimpleVmt;
        private DarkButton AdvancedImportPanel_Cancel;
        private DarkButton AdvancedImportPanel_Ok;
        public DarkComboBox AdvancedImportPanel_MipmapFilter;
        public DarkCheckBox AdvancedImportPanel_GenMipmaps;
        public DarkComboBox AdvancedImportPanel_ImageFormat;
        private DarkLabel label3;
        private DarkLabel label2;
        public DarkCheckBox AdvancedImportPanel_F_ClampS;
        public DarkComboBox AdvancedImportPanel_VtfVersion;
        private DarkGroupBox groupBox2;
        private DarkLabel label5;
        private DarkGroupBox groupBox1;
        private DarkLabel label4;
        private DarkGroupBox groupBox3;
        public DarkCheckBox AdvancedImportPanel_F_ClampT;
        public DarkCheckBox AdvancedImportPanel_F_Anisotropic;
        public DarkCheckBox AdvancedImportPanel_F_NoLod;
        public DarkCheckBox AdvancedImportPanel_F_PointSample;
        private DarkGroupBox groupBox4;
        private DarkButton GotoButton;
        private Panel panel2;
    }
}
