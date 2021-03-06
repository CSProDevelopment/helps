﻿namespace Help_Generator
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
            this.filteredTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editBoldMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editItalicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editFontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSubheaderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCenterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editLinkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editTableMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.editLogicColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSyntaxArgumentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.editPffColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.listBuilderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateCompileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateCompileAllFilesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateCompilationModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateViewTopicsInChmFormatMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.generateFormatMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.generateRefreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.generateHelpsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpSyntaxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.helpRegisterFileAssociationsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateChmHelpOnlyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.viewMenuItem,
            this.editMenuItem,
            this.generateMenuItem,
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
            this.viewImagesMenuItem,
            this.filteredTopicsMenuItem});
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
            // filteredTopicsMenuItem
            // 
            this.filteredTopicsMenuItem.Name = "filteredTopicsMenuItem";
            this.filteredTopicsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
            this.filteredTopicsMenuItem.Size = new System.Drawing.Size(207, 22);
            this.filteredTopicsMenuItem.Text = "&Filtered Topics";
            this.filteredTopicsMenuItem.Click += new System.EventHandler(this.filteredTopicsMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editBoldMenuItem,
            this.editItalicsMenuItem,
            this.editFontMenuItem,
            this.editSubheaderMenuItem,
            this.editCenterMenuItem,
            this.editLinkMenuItem,
            this.editListMenuItem,
            this.editTableMenuItem,
            this.toolStripSeparator6,
            this.editLogicColorMenuItem,
            this.editSyntaxArgumentMenuItem,
            this.toolStripSeparator7,
            this.editPffColorMenuItem,
            this.toolStripSeparator10,
            this.listBuilderToolStripMenuItem});
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editMenuItem.Text = "&Edit";
            this.editMenuItem.DropDownOpening += new System.EventHandler(this.editMenuItem_DropDownOpening);
            // 
            // editBoldMenuItem
            // 
            this.editBoldMenuItem.Name = "editBoldMenuItem";
            this.editBoldMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.editBoldMenuItem.Size = new System.Drawing.Size(239, 22);
            this.editBoldMenuItem.Tag = "bold";
            this.editBoldMenuItem.Text = "&Bold";
            this.editBoldMenuItem.Click += new System.EventHandler(this.editCommandMenuItem_Click);
            // 
            // editItalicsMenuItem
            // 
            this.editItalicsMenuItem.Name = "editItalicsMenuItem";
            this.editItalicsMenuItem.Size = new System.Drawing.Size(239, 22);
            this.editItalicsMenuItem.Tag = "italics";
            this.editItalicsMenuItem.Text = "&Italics";
            this.editItalicsMenuItem.Click += new System.EventHandler(this.editCommandMenuItem_Click);
            // 
            // editFontMenuItem
            // 
            this.editFontMenuItem.Name = "editFontMenuItem";
            this.editFontMenuItem.Size = new System.Drawing.Size(239, 22);
            this.editFontMenuItem.Tag = "font";
            this.editFontMenuItem.Text = "&Font";
            this.editFontMenuItem.Click += new System.EventHandler(this.editCommandMenuItem_Click);
            // 
            // editSubheaderMenuItem
            // 
            this.editSubheaderMenuItem.Name = "editSubheaderMenuItem";
            this.editSubheaderMenuItem.Size = new System.Drawing.Size(239, 22);
            this.editSubheaderMenuItem.Tag = "subheader";
            this.editSubheaderMenuItem.Text = "Sub&header";
            this.editSubheaderMenuItem.Click += new System.EventHandler(this.editCommandMenuItem_Click);
            // 
            // editCenterMenuItem
            // 
            this.editCenterMenuItem.Name = "editCenterMenuItem";
            this.editCenterMenuItem.Size = new System.Drawing.Size(239, 22);
            this.editCenterMenuItem.Tag = "center";
            this.editCenterMenuItem.Text = "&Center";
            this.editCenterMenuItem.Click += new System.EventHandler(this.editCommandMenuItem_Click);
            // 
            // editLinkMenuItem
            // 
            this.editLinkMenuItem.Name = "editLinkMenuItem";
            this.editLinkMenuItem.Size = new System.Drawing.Size(239, 22);
            this.editLinkMenuItem.Tag = "link";
            this.editLinkMenuItem.Text = "Lin&k";
            this.editLinkMenuItem.Click += new System.EventHandler(this.editCommandMenuItem_Click);
            // 
            // editListMenuItem
            // 
            this.editListMenuItem.Name = "editListMenuItem";
            this.editListMenuItem.Size = new System.Drawing.Size(239, 22);
            this.editListMenuItem.Tag = "list";
            this.editListMenuItem.Text = "&List";
            this.editListMenuItem.Click += new System.EventHandler(this.editCommandMenuItem_Click);
            // 
            // editTableMenuItem
            // 
            this.editTableMenuItem.Name = "editTableMenuItem";
            this.editTableMenuItem.Size = new System.Drawing.Size(239, 22);
            this.editTableMenuItem.Tag = "table";
            this.editTableMenuItem.Text = "&Table";
            this.editTableMenuItem.Click += new System.EventHandler(this.editCommandMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(236, 6);
            // 
            // editLogicColorMenuItem
            // 
            this.editLogicColorMenuItem.Name = "editLogicColorMenuItem";
            this.editLogicColorMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.editLogicColorMenuItem.Size = new System.Drawing.Size(239, 22);
            this.editLogicColorMenuItem.Tag = "logiccolor";
            this.editLogicColorMenuItem.Text = "Logic Color";
            this.editLogicColorMenuItem.Click += new System.EventHandler(this.editCommandMenuItem_Click);
            // 
            // editSyntaxArgumentMenuItem
            // 
            this.editSyntaxArgumentMenuItem.Name = "editSyntaxArgumentMenuItem";
            this.editSyntaxArgumentMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.editSyntaxArgumentMenuItem.Size = new System.Drawing.Size(239, 22);
            this.editSyntaxArgumentMenuItem.Tag = "syntaxargument";
            this.editSyntaxArgumentMenuItem.Text = "Syntax Argument";
            this.editSyntaxArgumentMenuItem.Click += new System.EventHandler(this.editCommandMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(236, 6);
            // 
            // editPffColorMenuItem
            // 
            this.editPffColorMenuItem.Name = "editPffColorMenuItem";
            this.editPffColorMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.editPffColorMenuItem.Size = new System.Drawing.Size(239, 22);
            this.editPffColorMenuItem.Tag = "pffcolor";
            this.editPffColorMenuItem.Text = "PFF Color";
            this.editPffColorMenuItem.Click += new System.EventHandler(this.editCommandMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(236, 6);
            // 
            // listBuilderToolStripMenuItem
            // 
            this.listBuilderToolStripMenuItem.Name = "listBuilderToolStripMenuItem";
            this.listBuilderToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.listBuilderToolStripMenuItem.Tag = "listbuilder";
            this.listBuilderToolStripMenuItem.Text = "List Builder";
            this.listBuilderToolStripMenuItem.Click += new System.EventHandler(this.editCommandMenuItem_Click);
            // 
            // generateMenuItem
            // 
            this.generateMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateCompileMenuItem,
            this.generateCompileAllFilesMenuItem,
            this.generateCompilationModeMenuItem,
            this.toolStripSeparator8,
            this.generateFormatMenuItem,
            this.toolStripSeparator1,
            this.generateRefreshMenuItem,
            this.toolStripSeparator3,
            this.generateChmHelpOnlyMenuItem,
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
            this.generateCompileMenuItem.Size = new System.Drawing.Size(236, 22);
            this.generateCompileMenuItem.Text = "&Compile File";
            this.generateCompileMenuItem.Click += new System.EventHandler(this.generateCompileMenuItem_Click);
            // 
            // generateCompileAllFilesMenuItem
            // 
            this.generateCompileAllFilesMenuItem.Name = "generateCompileAllFilesMenuItem";
            this.generateCompileAllFilesMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.K)));
            this.generateCompileAllFilesMenuItem.Size = new System.Drawing.Size(236, 22);
            this.generateCompileAllFilesMenuItem.Text = "Compile All Files";
            this.generateCompileAllFilesMenuItem.Click += new System.EventHandler(this.generateCompileAllFilesMenuItem_Click);
            // 
            // generateCompilationModeMenuItem
            // 
            this.generateCompilationModeMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateViewTopicsInChmFormatMenuItem});
            this.generateCompilationModeMenuItem.Name = "generateCompilationModeMenuItem";
            this.generateCompilationModeMenuItem.Size = new System.Drawing.Size(236, 22);
            this.generateCompilationModeMenuItem.Text = "Compilation Mode";
            // 
            // generateViewTopicsInChmFormatMenuItem
            // 
            this.generateViewTopicsInChmFormatMenuItem.Checked = true;
            this.generateViewTopicsInChmFormatMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.generateViewTopicsInChmFormatMenuItem.Name = "generateViewTopicsInChmFormatMenuItem";
            this.generateViewTopicsInChmFormatMenuItem.Size = new System.Drawing.Size(276, 22);
            this.generateViewTopicsInChmFormatMenuItem.Text = "&View Compiled Topics in CHM Format";
            this.generateViewTopicsInChmFormatMenuItem.Click += new System.EventHandler(this.generateViewTopicsInChmFormatMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(233, 6);
            // 
            // generateFormatMenuItem
            // 
            this.generateFormatMenuItem.Name = "generateFormatMenuItem";
            this.generateFormatMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.generateFormatMenuItem.Size = new System.Drawing.Size(236, 22);
            this.generateFormatMenuItem.Text = "&Format File";
            this.generateFormatMenuItem.Click += new System.EventHandler(this.generateFormatMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(233, 6);
            // 
            // generateRefreshMenuItem
            // 
            this.generateRefreshMenuItem.Name = "generateRefreshMenuItem";
            this.generateRefreshMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.generateRefreshMenuItem.Size = new System.Drawing.Size(236, 22);
            this.generateRefreshMenuItem.Text = "&Refresh Sources";
            this.generateRefreshMenuItem.Click += new System.EventHandler(this.generateRefreshMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(233, 6);
            // 
            // generateHelpsMenuItem
            // 
            this.generateHelpsMenuItem.Name = "generateHelpsMenuItem";
            this.generateHelpsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.generateHelpsMenuItem.Size = new System.Drawing.Size(236, 22);
            this.generateHelpsMenuItem.Text = "&Helps";
            this.generateHelpsMenuItem.Click += new System.EventHandler(this.generateHelpsMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpContextMenuItem,
            this.helpSyntaxMenuItem,
            this.toolStripSeparator9,
            this.helpRegisterFileAssociationsMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "&Help";
            // 
            // helpContextMenuItem
            // 
            this.helpContextMenuItem.Name = "helpContextMenuItem";
            this.helpContextMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpContextMenuItem.Size = new System.Drawing.Size(206, 22);
            this.helpContextMenuItem.Text = "&Help";
            this.helpContextMenuItem.Click += new System.EventHandler(this.helpContextMenuItem_Click);
            // 
            // helpSyntaxMenuItem
            // 
            this.helpSyntaxMenuItem.Name = "helpSyntaxMenuItem";
            this.helpSyntaxMenuItem.Size = new System.Drawing.Size(206, 22);
            this.helpSyntaxMenuItem.Text = "Topic &Syntax";
            this.helpSyntaxMenuItem.Click += new System.EventHandler(this.helpSyntaxMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(203, 6);
            // 
            // helpRegisterFileAssociationsMenuItem
            // 
            this.helpRegisterFileAssociationsMenuItem.Name = "helpRegisterFileAssociationsMenuItem";
            this.helpRegisterFileAssociationsMenuItem.Size = new System.Drawing.Size(206, 22);
            this.helpRegisterFileAssociationsMenuItem.Text = "&Register File Associations";
            this.helpRegisterFileAssociationsMenuItem.Click += new System.EventHandler(this.helpRegisterFileAssociationsMenuItem_Click);
            // 
            // generateChmHelpOnlyMenuItem
            // 
            this.generateChmHelpOnlyMenuItem.Name = "generateChmHelpOnlyMenuItem";
            this.generateChmHelpOnlyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F5)));
            this.generateChmHelpOnlyMenuItem.Size = new System.Drawing.Size(236, 22);
            this.generateChmHelpOnlyMenuItem.Text = "CHM Help Only";
            this.generateChmHelpOnlyMenuItem.Click += new System.EventHandler(this.generateChmHelpOnlyMenuItem_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
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
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
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
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editBoldMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editItalicsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editFontMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCenterMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editLinkMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editTableMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editLogicColorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPffColorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editListMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSubheaderMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem editSyntaxArgumentMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem generateCompilationModeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateViewTopicsInChmFormatMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem helpRegisterFileAssociationsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateCompileAllFilesMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripMenuItem listBuilderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filteredTopicsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateChmHelpOnlyMenuItem;
    }
}

