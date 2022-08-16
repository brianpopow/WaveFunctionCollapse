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
            this.openImagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OutputPanel = new System.Windows.Forms.Panel();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.OutputPicture = new System.Windows.Forms.PictureBox();
            this.ImageMenu.SuspendLayout();
            this.OutputPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutputPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // InputImagesPanel
            // 
            this.InputImagesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InputImagesPanel.Location = new System.Drawing.Point(12, 44);
            this.InputImagesPanel.Name = "InputImagesPanel";
            this.InputImagesPanel.Size = new System.Drawing.Size(400, 609);
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
            this.openImagesMenuItem});
            this.openFilesToolStripMenuItem.Name = "openFilesToolStripMenuItem";
            this.openFilesToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.openFilesToolStripMenuItem.Text = "Images";
            // 
            // openImagesMenuItem
            // 
            this.openImagesMenuItem.Name = "openImagesMenuItem";
            this.openImagesMenuItem.Size = new System.Drawing.Size(129, 22);
            this.openImagesMenuItem.Text = "Open Files";
            this.openImagesMenuItem.Click += new System.EventHandler(this.OpenImagesMenuItem_Click);
            // 
            // OutputPanel
            // 
            this.OutputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutputPanel.Controls.Add(this.OutputPicture);
            this.OutputPanel.Location = new System.Drawing.Point(418, 44);
            this.OutputPanel.Name = "OutputPanel";
            this.OutputPanel.Size = new System.Drawing.Size(877, 609);
            this.OutputPanel.TabIndex = 2;
            // 
            // GenerateButton
            // 
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
            // OutputPicture
            // 
            this.OutputPicture.Location = new System.Drawing.Point(13, 16);
            this.OutputPicture.Name = "OutputPicture";
            this.OutputPicture.Size = new System.Drawing.Size(847, 576);
            this.OutputPicture.TabIndex = 0;
            this.OutputPicture.TabStop = false;
            // 
            // InteractiveTestUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1307, 709);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel InputImagesPanel;
        private MenuStrip ImageMenu;
        private ToolStripMenuItem openFilesToolStripMenuItem;
        private ToolStripMenuItem openImagesMenuItem;
        private Panel OutputPanel;
        private Button GenerateButton;
        private PictureBox OutputPicture;
    }
}