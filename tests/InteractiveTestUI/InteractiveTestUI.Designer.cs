namespace InteractiveTestUI
{
    partial class InteractiveTestUI
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
            this.InputImagesPanel = new System.Windows.Forms.Panel();
            this.ImageMenu = new System.Windows.Forms.MenuStrip();
            this.openFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenOverlappingImage = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenTiledSettingFile = new System.Windows.Forms.ToolStripMenuItem();
            this.OutputPanel = new System.Windows.Forms.Panel();
            this.OutputPicture = new System.Windows.Forms.PictureBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.Label();
            this.InputLabel = new System.Windows.Forms.Label();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.Options = new System.Windows.Forms.GroupBox();
            this.SubsetsLabel = new System.Windows.Forms.Label();
            this.SubsetsComboBox = new System.Windows.Forms.ComboBox();
            this.NComboBox = new System.Windows.Forms.ComboBox();
            this.NLabel = new System.Windows.Forms.Label();
            this.HeuristicLabel = new System.Windows.Forms.Label();
            this.HeuristicComboBox = new System.Windows.Forms.ComboBox();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.SizeTrackBar = new System.Windows.Forms.TrackBar();
            this.PeriodicInput = new System.Windows.Forms.CheckBox();
            this.Ground = new System.Windows.Forms.CheckBox();
            this.Periodic = new System.Windows.Forms.CheckBox();
            this.ImageMenu.SuspendLayout();
            this.OutputPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutputPicture)).BeginInit();
            this.Options.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SizeTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // InputImagesPanel
            // 
            this.InputImagesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.InputImagesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InputImagesPanel.Location = new System.Drawing.Point(12, 78);
            this.InputImagesPanel.Name = "InputImagesPanel";
            this.InputImagesPanel.Size = new System.Drawing.Size(400, 276);
            this.InputImagesPanel.TabIndex = 0;
            // 
            // ImageMenu
            // 
            this.ImageMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFilesToolStripMenuItem});
            this.ImageMenu.Location = new System.Drawing.Point(0, 0);
            this.ImageMenu.Name = "ImageMenu";
            this.ImageMenu.Size = new System.Drawing.Size(1307, 24);
            this.ImageMenu.TabIndex = 1;
            this.ImageMenu.Text = "ImageMenu";
            // 
            // openFilesToolStripMenuItem
            // 
            this.openFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenOverlappingImage,
            this.OpenTiledSettingFile});
            this.openFilesToolStripMenuItem.Name = "openFilesToolStripMenuItem";
            this.openFilesToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.openFilesToolStripMenuItem.Text = "Images";
            // 
            // OpenOverlappingImage
            // 
            this.OpenOverlappingImage.Name = "OpenOverlappingImage";
            this.OpenOverlappingImage.Size = new System.Drawing.Size(207, 22);
            this.OpenOverlappingImage.Text = "Open Overlapping Image";
            this.OpenOverlappingImage.Click += new System.EventHandler(this.OpenOverlappingImage_Click);
            // 
            // OpenTiledSettingFile
            // 
            this.OpenTiledSettingFile.Name = "OpenTiledSettingFile";
            this.OpenTiledSettingFile.Size = new System.Drawing.Size(207, 22);
            this.OpenTiledSettingFile.Text = "Open Tiled Setting File";
            this.OpenTiledSettingFile.Click += new System.EventHandler(this.OpenTiledSettingFile_Click);
            // 
            // OutputPanel
            // 
            this.OutputPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutputPanel.Controls.Add(this.OutputPicture);
            this.OutputPanel.Location = new System.Drawing.Point(418, 78);
            this.OutputPanel.Name = "OutputPanel";
            this.OutputPanel.Size = new System.Drawing.Size(877, 575);
            this.OutputPanel.TabIndex = 2;
            // 
            // OutputPicture
            // 
            this.OutputPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputPicture.Location = new System.Drawing.Point(13, 16);
            this.OutputPicture.Name = "OutputPicture";
            this.OutputPicture.Size = new System.Drawing.Size(847, 545);
            this.OutputPicture.TabIndex = 0;
            this.OutputPicture.TabStop = false;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.GenerateButton.ForeColor = System.Drawing.SystemColors.MenuText;
            this.GenerateButton.Location = new System.Drawing.Point(1158, 661);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(137, 36);
            this.GenerateButton.TabIndex = 3;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = false;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // Status
            // 
            this.Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Status.AutoSize = true;
            this.Status.BackColor = System.Drawing.Color.White;
            this.Status.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Status.Location = new System.Drawing.Point(1022, 672);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(39, 15);
            this.Status.TabIndex = 4;
            this.Status.Text = "Status";
            // 
            // InputLabel
            // 
            this.InputLabel.AutoSize = true;
            this.InputLabel.BackColor = System.Drawing.Color.White;
            this.InputLabel.ForeColor = System.Drawing.SystemColors.MenuText;
            this.InputLabel.Location = new System.Drawing.Point(15, 48);
            this.InputLabel.Name = "InputLabel";
            this.InputLabel.Size = new System.Drawing.Size(76, 15);
            this.InputLabel.TabIndex = 5;
            this.InputLabel.Text = "Input Images";
            // 
            // OutputLabel
            // 
            this.OutputLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.BackColor = System.Drawing.Color.White;
            this.OutputLabel.ForeColor = System.Drawing.SystemColors.MenuText;
            this.OutputLabel.Location = new System.Drawing.Point(418, 48);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(97, 15);
            this.OutputLabel.TabIndex = 6;
            this.OutputLabel.Text = "Generated Image";
            // 
            // Options
            // 
            this.Options.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Options.Controls.Add(this.SubsetsLabel);
            this.Options.Controls.Add(this.SubsetsComboBox);
            this.Options.Controls.Add(this.NComboBox);
            this.Options.Controls.Add(this.NLabel);
            this.Options.Controls.Add(this.HeuristicLabel);
            this.Options.Controls.Add(this.HeuristicComboBox);
            this.Options.Controls.Add(this.SizeLabel);
            this.Options.Controls.Add(this.SizeTrackBar);
            this.Options.Controls.Add(this.PeriodicInput);
            this.Options.Controls.Add(this.Ground);
            this.Options.Controls.Add(this.Periodic);
            this.Options.Location = new System.Drawing.Point(13, 378);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(389, 275);
            this.Options.TabIndex = 8;
            this.Options.TabStop = false;
            this.Options.Text = "Options";
            // 
            // SubsetsLabel
            // 
            this.SubsetsLabel.AutoSize = true;
            this.SubsetsLabel.ForeColor = System.Drawing.SystemColors.MenuText;
            this.SubsetsLabel.Location = new System.Drawing.Point(160, 101);
            this.SubsetsLabel.Name = "SubsetsLabel";
            this.SubsetsLabel.Size = new System.Drawing.Size(45, 15);
            this.SubsetsLabel.TabIndex = 12;
            this.SubsetsLabel.Text = "Subset:";
            // 
            // SubsetsComboBox
            // 
            this.SubsetsComboBox.Enabled = false;
            this.SubsetsComboBox.FormattingEnabled = true;
            this.SubsetsComboBox.Location = new System.Drawing.Point(211, 98);
            this.SubsetsComboBox.Name = "SubsetsComboBox";
            this.SubsetsComboBox.Size = new System.Drawing.Size(121, 23);
            this.SubsetsComboBox.TabIndex = 11;
            this.SubsetsComboBox.SelectedIndexChanged += new System.EventHandler(this.SubsetsCoboBox_SelectedIndexChanged);
            // 
            // NComboBox
            // 
            this.NComboBox.FormattingEnabled = true;
            this.NComboBox.Items.AddRange(new object[] {
            "2",
            "3"});
            this.NComboBox.Location = new System.Drawing.Point(212, 64);
            this.NComboBox.Name = "NComboBox";
            this.NComboBox.Size = new System.Drawing.Size(121, 23);
            this.NComboBox.TabIndex = 10;
            this.NComboBox.SelectedIndexChanged += new System.EventHandler(this.NComboBox_SelectedIndexChanged);
            // 
            // NLabel
            // 
            this.NLabel.AutoSize = true;
            this.NLabel.ForeColor = System.Drawing.SystemColors.MenuText;
            this.NLabel.Location = new System.Drawing.Point(187, 67);
            this.NLabel.Name = "NLabel";
            this.NLabel.Size = new System.Drawing.Size(19, 15);
            this.NLabel.TabIndex = 9;
            this.NLabel.Text = "N:";
            // 
            // HeuristicLabel
            // 
            this.HeuristicLabel.AutoSize = true;
            this.HeuristicLabel.ForeColor = System.Drawing.SystemColors.MenuText;
            this.HeuristicLabel.Location = new System.Drawing.Point(149, 35);
            this.HeuristicLabel.Name = "HeuristicLabel";
            this.HeuristicLabel.Size = new System.Drawing.Size(57, 15);
            this.HeuristicLabel.TabIndex = 8;
            this.HeuristicLabel.Text = "Heuristic:";
            // 
            // HeuristicComboBox
            // 
            this.HeuristicComboBox.FormattingEnabled = true;
            this.HeuristicComboBox.Items.AddRange(new object[] {
            "Scanline",
            "Entropy",
            "MRV"});
            this.HeuristicComboBox.Location = new System.Drawing.Point(212, 33);
            this.HeuristicComboBox.Name = "HeuristicComboBox";
            this.HeuristicComboBox.Size = new System.Drawing.Size(121, 23);
            this.HeuristicComboBox.TabIndex = 7;
            this.HeuristicComboBox.SelectedIndexChanged += new System.EventHandler(this.HeuristicComboBox_SelectedIndexChanged);
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.ForeColor = System.Drawing.SystemColors.MenuText;
            this.SizeLabel.Location = new System.Drawing.Point(312, 143);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(45, 15);
            this.SizeLabel.TabIndex = 6;
            this.SizeLabel.Text = "Size: 48";
            // 
            // SizeTrackBar
            // 
            this.SizeTrackBar.Location = new System.Drawing.Point(16, 133);
            this.SizeTrackBar.Maximum = 500;
            this.SizeTrackBar.Minimum = 10;
            this.SizeTrackBar.Name = "SizeTrackBar";
            this.SizeTrackBar.Size = new System.Drawing.Size(290, 45);
            this.SizeTrackBar.TabIndex = 5;
            this.SizeTrackBar.Value = 24;
            this.SizeTrackBar.ValueChanged += new System.EventHandler(this.SizeTrackBar_ValueChanged);
            // 
            // PeriodicInput
            // 
            this.PeriodicInput.AutoSize = true;
            this.PeriodicInput.ForeColor = System.Drawing.SystemColors.MenuText;
            this.PeriodicInput.Location = new System.Drawing.Point(18, 60);
            this.PeriodicInput.Name = "PeriodicInput";
            this.PeriodicInput.Size = new System.Drawing.Size(100, 19);
            this.PeriodicInput.TabIndex = 4;
            this.PeriodicInput.Text = "periodic input";
            this.PeriodicInput.UseVisualStyleBackColor = true;
            this.PeriodicInput.CheckedChanged += new System.EventHandler(this.PeriodicInput_CheckedChanged);
            // 
            // Ground
            // 
            this.Ground.AutoSize = true;
            this.Ground.ForeColor = System.Drawing.SystemColors.MenuText;
            this.Ground.Location = new System.Drawing.Point(18, 88);
            this.Ground.Name = "Ground";
            this.Ground.Size = new System.Drawing.Size(65, 19);
            this.Ground.TabIndex = 1;
            this.Ground.Text = "ground";
            this.Ground.UseVisualStyleBackColor = true;
            this.Ground.CheckedChanged += new System.EventHandler(this.Ground_CheckedChanged);
            // 
            // Periodic
            // 
            this.Periodic.AutoSize = true;
            this.Periodic.Checked = true;
            this.Periodic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Periodic.ForeColor = System.Drawing.SystemColors.MenuText;
            this.Periodic.Location = new System.Drawing.Point(18, 35);
            this.Periodic.Name = "Periodic";
            this.Periodic.Size = new System.Drawing.Size(69, 19);
            this.Periodic.TabIndex = 0;
            this.Periodic.Text = "periodic";
            this.Periodic.UseVisualStyleBackColor = true;
            this.Periodic.CheckedChanged += new System.EventHandler(this.Periodic_CheckedChanged);
            // 
            // InteractiveTestUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1307, 709);
            this.Controls.Add(this.Options);
            this.Controls.Add(this.OutputLabel);
            this.Controls.Add(this.InputLabel);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.OutputPanel);
            this.Controls.Add(this.InputImagesPanel);
            this.Controls.Add(this.ImageMenu);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.MainMenuStrip = this.ImageMenu;
            this.Name = "InteractiveTestUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InteractiveTest";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ImageMenu.ResumeLayout(false);
            this.ImageMenu.PerformLayout();
            this.OutputPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OutputPicture)).EndInit();
            this.Options.ResumeLayout(false);
            this.Options.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SizeTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel InputImagesPanel;
        private MenuStrip ImageMenu;
        private ToolStripMenuItem openFilesToolStripMenuItem;
        private ToolStripMenuItem OpenOverlappingImage;
        private Panel OutputPanel;
        private Button GenerateButton;
        private PictureBox OutputPicture;
        private Label Status;
        private Label InputLabel;
        private Label OutputLabel;
        private GroupBox Options;
        private CheckBox Periodic;
        private CheckBox Ground;
        private CheckBox PeriodicInput;
        private Label SizeLabel;
        private TrackBar SizeTrackBar;
        private ComboBox HeuristicComboBox;
        private Label HeuristicLabel;
        private ComboBox NComboBox;
        private Label NLabel;
        private ToolStripMenuItem OpenTiledSettingFile;
        private Label SubsetsLabel;
        private ComboBox SubsetsComboBox;
    }
}