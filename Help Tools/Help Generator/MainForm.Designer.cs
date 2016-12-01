namespace Help_Generator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.fileCloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileCloseAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.fileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.fileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTableOfContentsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewIndexMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewImagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateCompileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateFormatMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.generateRefreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.generateHelpsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsRegisterFileAssociationsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpSyntaxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsViewTopicsInHelpsFormatMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.viewMenuItem,
            this.generateMenuItem,
            this.settingsMenuItem,
            this.toolsMenuItem,
            this.helpMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1176, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNewMenuItem,
            this.toolStripSeparator5,
            this.fileCloseMenuItem,
            this.fileCloseAllMenuItem,
            this.toolStripSeparator4,
            this.fileSaveMenuItem,
            this.fileSaveAllMenuItem,
            this.toolStripSeparator2,
            this.fileExitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "&File";
            this.fileMenuItem.DropDownOpening += new System.EventHandler(this.fileMenuItem_DropDownOpening);
            // 
            // fileNewMenuItem
            // 
            this.fileNewMenuItem.Name = "fileNewMenuItem";
            this.fileNewMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.fileNewMenuItem.Size = new System.Drawing.Size(197, 22);
            this.fileNewMenuItem.Text = "&New Topic";
            this.fileNewMenuItem.Click += new System.EventHandler(this.fileNewMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(194, 6);
            // 
            // fileCloseMenuItem
            // 
            this.fileCloseMenuItem.Name = "fileCloseMenuItem";
            this.fileCloseMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.fileCloseMenuItem.Size = new System.Drawing.Size(197, 22);
            this.fileCloseMenuItem.Text = "&Close";
            this.fileCloseMenuItem.Click += new System.EventHandler(this.fileCloseMenuItem_Click);
            // 
            // fileCloseAllMenuItem
            // 
            this.fileCloseAllMenuItem.Name = "fileCloseAllMenuItem";
            this.fileCloseAllMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
            this.fileCloseAllMenuItem.Size = new System.Drawing.Size(197, 22);
            this.fileCloseAllMenuItem.Text = "Close All";
            this.fileCloseAllMenuItem.Click += new System.EventHandler(this.fileCloseAllMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(194, 6);
            // 
            // fileSaveMenuItem
            // 
            this.fileSaveMenuItem.Name = "fileSaveMenuItem";
            this.fileSaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.fileSaveMenuItem.Size = new System.Drawing.Size(197, 22);
            this.fileSaveMenuItem.Text = "&Save";
            this.fileSaveMenuItem.Click += new System.EventHandler(this.fileSaveMenuItem_Click);
            // 
            // fileSaveAllMenuItem
            // 
            this.fileSaveAllMenuItem.Name = "fileSaveAllMenuItem";
            this.fileSaveAllMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.fileSaveAllMenuItem.Size = new System.Drawing.Size(197, 22);
            this.fileSaveAllMenuItem.Text = "Save A&ll";
            this.fileSaveAllMenuItem.Click += new System.EventHandler(this.fileSaveAllMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(194, 6);
            // 
            // fileExitMenuItem
            // 
            this.fileExitMenuItem.Name = "fileExitMenuItem";
            this.fileExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.fileExitMenuItem.Size = new System.Drawing.Size(197, 22);
            this.fileExitMenuItem.Text = "E&xit";
            this.fileExitMenuItem.Click += new System.EventHandler(this.fileExitMenuItem_Click);
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewSettingsMenuItem,
            this.viewTableOfContentsMenuItem,
            this.viewIndexMenuItem,
            this.viewTopicsMenuItem,
            this.viewImagesMenuItem});
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewMenuItem.Text = "&View";
            // 
            // viewSettingsMenuItem
            // 
            this.viewSettingsMenuItem.Name = "viewSettingsMenuItem";
            this.viewSettingsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.viewSettingsMenuItem.Size = new System.Drawing.Size(207, 22);
            this.viewSettingsMenuItem.Text = "Project &Settings";
            this.viewSettingsMenuItem.Click += new System.EventHandler(this.viewSettingsMenuItem_Click);
            // 
            // viewTableOfContentsMenuItem
            // 
            this.viewTableOfContentsMenuItem.Name = "viewTableOfContentsMenuItem";
            this.viewTableOfContentsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.viewTableOfContentsMenuItem.Size = new System.Drawing.Size(207, 22);
            this.viewTableOfContentsMenuItem.Text = "Table of &Contents";
            this.viewTableOfContentsMenuItem.Click += new System.EventHandler(this.viewTableOfContentsMenuItem_Click);
            // 
            // viewIndexMenuItem
            // 
            this.viewIndexMenuItem.Name = "viewIndexMenuItem";
            this.viewIndexMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.viewIndexMenuItem.Size = new System.Drawing.Size(207, 22);
            this.viewIndexMenuItem.Text = "&Index";
            this.viewIndexMenuItem.Click += new System.EventHandler(this.viewIndexMenuItem_Click);
            // 
            // viewTopicsMenuItem
            // 
            this.viewTopicsMenuItem.Name = "viewTopicsMenuItem";
            this.viewTopicsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.viewTopicsMenuItem.Size = new System.Drawing.Size(207, 22);
            this.viewTopicsMenuItem.Text = "&Topics";
            this.viewTopicsMenuItem.Click += new System.EventHandler(this.viewTopicsMenuItem_Click);
            // 
            // viewImagesMenuItem
            // 
            this.viewImagesMenuItem.Name = "viewImagesMenuItem";
            this.viewImagesMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.viewImagesMenuItem.Size = new System.Drawing.Size(207, 22);
            this.viewImagesMenuItem.Text = "I&mages";
            this.viewImagesMenuItem.Click += new System.EventHandler(this.viewImagesMenuItem_Click);
            // 
            // generateMenuItem
            // 
            this.generateMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateCompileMenuItem,
            this.generateFormatMenuItem,
            this.toolStripSeparator1,
            this.generateRefreshMenuItem,
            this.toolStripSeparator3,
            this.generateHelpsMenuItem});
            this.generateMenuItem.Name = "generateMenuItem";
            this.generateMenuItem.Size = new System.Drawing.Size(66, 20);
            this.generateMenuItem.Text = "&Generate";
            this.generateMenuItem.DropDownOpening += new System.EventHandler(this.generateMenuItem_DropDownOpening);
            // 
            // generateCompileMenuItem
            // 
            this.generateCompileMenuItem.Name = "generateCompileMenuItem";
            this.generateCompileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.generateCompileMenuItem.Size = new System.Drawing.Size(181, 22);
            this.generateCompileMenuItem.Text = "&Compile File";
            this.generateCompileMenuItem.Click += new System.EventHandler(this.generateCompileMenuItem_Click);
            // 
            // generateFormatMenuItem
            // 
            this.generateFormatMenuItem.Name = "generateFormatMenuItem";
            this.generateFormatMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.generateFormatMenuItem.Size = new System.Drawing.Size(181, 22);
            this.generateFormatMenuItem.Text = "&Format File";
            this.generateFormatMenuItem.Click += new System.EventHandler(this.generateFormatMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // generateRefreshMenuItem
            // 
            this.generateRefreshMenuItem.Name = "generateRefreshMenuItem";
            this.generateRefreshMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.generateRefreshMenuItem.Size = new System.Drawing.Size(181, 22);
            this.generateRefreshMenuItem.Text = "&Refresh Sources";
            this.generateRefreshMenuItem.Click += new System.EventHandler(this.generateRefreshMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(178, 6);
            // 
            // generateHelpsMenuItem
            // 
            this.generateHelpsMenuItem.Name = "generateHelpsMenuItem";
            this.generateHelpsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.generateHelpsMenuItem.Size = new System.Drawing.Size(181, 22);
            this.generateHelpsMenuItem.Text = "&Helps";
            this.generateHelpsMenuItem.Click += new System.EventHandler(this.generateHelpsMenuItem_Click);
            // 
            // toolsMenuItem
            // 
            this.toolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsRegisterFileAssociationsMenuItem});
            this.toolsMenuItem.Name = "toolsMenuItem";
            this.toolsMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsMenuItem.Text = "&Tools";
            // 
            // toolsRegisterFileAssociationsMenuItem
            // 
            this.toolsRegisterFileAssociationsMenuItem.Name = "toolsRegisterFileAssociationsMenuItem";
            this.toolsRegisterFileAssociationsMenuItem.Size = new System.Drawing.Size(206, 22);
            this.toolsRegisterFileAssociationsMenuItem.Text = "&Register File Associations";
            this.toolsRegisterFileAssociationsMenuItem.Click += new System.EventHandler(this.toolsRegisterFileAssociationsMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpContextMenuItem,
            this.helpSyntaxMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "&Help";
            // 
            // helpContextMenuItem
            // 
            this.helpContextMenuItem.Name = "helpContextMenuItem";
            this.helpContextMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpContextMenuItem.Size = new System.Drawing.Size(140, 22);
            this.helpContextMenuItem.Text = "&Help";
            this.helpContextMenuItem.Click += new System.EventHandler(this.helpContextMenuItem_Click);
            // 
            // helpSyntaxMenuItem
            // 
            this.helpSyntaxMenuItem.Name = "helpSyntaxMenuItem";
            this.helpSyntaxMenuItem.Size = new System.Drawing.Size(140, 22);
            this.helpSyntaxMenuItem.Text = "Topic &Syntax";
            this.helpSyntaxMenuItem.Click += new System.EventHandler(this.helpSyntaxMenuItem_Click);
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsViewTopicsInHelpsFormatMenuItem});
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsMenuItem.Text = "&Settings";
            // 
            // settingsViewTopicsInHelpsFormatMenuItem
            // 
            this.settingsViewTopicsInHelpsFormatMenuItem.Checked = true;
            this.settingsViewTopicsInHelpsFormatMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settingsViewTopicsInHelpsFormatMenuItem.Name = "settingsViewTopicsInHelpsFormatMenuItem";
            this.settingsViewTopicsInHelpsFormatMenuItem.Size = new System.Drawing.Size(223, 22);
            this.settingsViewTopicsInHelpsFormatMenuItem.Text = "&View Topics in Helps Format";
            this.settingsViewTopicsInHelpsFormatMenuItem.Click += new System.EventHandler(this.settingsViewTopicsInHelpsFormatMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 577);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSPro Help Generator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileNewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileCloseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileSaveMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem fileExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewSettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewTableOfContentsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewIndexMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewTopicsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateRefreshMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpSyntaxMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateHelpsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateCompileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem fileSaveAllMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem fileCloseAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateFormatMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewImagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsRegisterFileAssociationsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsViewTopicsInHelpsFormatMenuItem;
    }
}

