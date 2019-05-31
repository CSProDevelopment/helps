namespace Code_Colorizer
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
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.notepadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editControl = new Colorizer.EditControl();
            this.buttonCopyHtml = new System.Windows.Forms.Button();
            this.buttonCopyUsersForum = new System.Windows.Forms.Button();
            this.buttonCopyUsersBlog = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.languageMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1184, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menu";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenuItem,
            this.toolStripSeparator1,
            this.notepadMenuItem,
            this.toolStripSeparator2,
            this.exitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "&File";
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMenuItem.Size = new System.Drawing.Size(236, 22);
            this.openMenuItem.Text = "&Open File";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(233, 6);
            // 
            // notepadMenuItem
            // 
            this.notepadMenuItem.Name = "notepadMenuItem";
            this.notepadMenuItem.Size = new System.Drawing.Size(236, 22);
            this.notepadMenuItem.Text = "Generate &Notepad++ Colorizer";
            this.notepadMenuItem.Click += new System.EventHandler(this.notepadMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(233, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitMenuItem.Size = new System.Drawing.Size(236, 22);
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // languageMenuItem
            // 
            this.languageMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logicMenuItem,
            this.pffMenuItem,
            this.messageMenuItem});
            this.languageMenuItem.Name = "languageMenuItem";
            this.languageMenuItem.Size = new System.Drawing.Size(71, 20);
            this.languageMenuItem.Text = "&Language";
            // 
            // logicMenuItem
            // 
            this.logicMenuItem.Checked = true;
            this.logicMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.logicMenuItem.Name = "logicMenuItem";
            this.logicMenuItem.Size = new System.Drawing.Size(120, 22);
            this.logicMenuItem.Tag = Code_Colorizer.Processor.BufferType.Logic;
            this.logicMenuItem.Text = "&Logic";
            this.logicMenuItem.Click += new System.EventHandler(this.languageMenuItem_Click);
            // 
            // pffMenuItem
            // 
            this.pffMenuItem.Name = "pffMenuItem";
            this.pffMenuItem.Size = new System.Drawing.Size(120, 22);
            this.pffMenuItem.Tag = Code_Colorizer.Processor.BufferType.Pff;
            this.pffMenuItem.Text = "&PFF";
            this.pffMenuItem.Click += new System.EventHandler(this.languageMenuItem_Click);
            // 
            // messageMenuItem
            // 
            this.messageMenuItem.Name = "messageMenuItem";
            this.messageMenuItem.Size = new System.Drawing.Size(120, 22);
            this.messageMenuItem.Tag = Code_Colorizer.Processor.BufferType.Message;
            this.messageMenuItem.Text = "&Message";
            this.messageMenuItem.Click += new System.EventHandler(this.languageMenuItem_Click);
            // 
            // editControl
            // 
            this.editControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editControl.Lexer = ScintillaNET.Lexer.Null;
            this.editControl.Location = new System.Drawing.Point(12, 35);
            this.editControl.Name = "editControl";
            this.editControl.Size = new System.Drawing.Size(1160, 581);
            this.editControl.TabIndex = 0;
            this.editControl.UseTabs = true;
            // 
            // buttonCopyHtml
            // 
            this.buttonCopyHtml.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCopyHtml.Location = new System.Drawing.Point(294, 626);
            this.buttonCopyHtml.Name = "buttonCopyHtml";
            this.buttonCopyHtml.Size = new System.Drawing.Size(160, 23);
            this.buttonCopyHtml.TabIndex = 1;
            this.buttonCopyHtml.Text = "Copy Formatted HTML";
            this.buttonCopyHtml.UseVisualStyleBackColor = true;
            this.buttonCopyHtml.Click += new System.EventHandler(this.buttonCopyHtml_Click);
            // 
            // buttonCopyUsersForum
            // 
            this.buttonCopyUsersForum.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCopyUsersForum.Location = new System.Drawing.Point(512, 626);
            this.buttonCopyUsersForum.Name = "buttonCopyUsersForum";
            this.buttonCopyUsersForum.Size = new System.Drawing.Size(160, 23);
            this.buttonCopyUsersForum.TabIndex = 2;
            this.buttonCopyUsersForum.Text = "Copy Users Forum Code";
            this.buttonCopyUsersForum.UseVisualStyleBackColor = true;
            this.buttonCopyUsersForum.Click += new System.EventHandler(this.buttonCopyUsersForum_Click);
            // 
            // buttonCopyUsersBlog
            // 
            this.buttonCopyUsersBlog.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCopyUsersBlog.Location = new System.Drawing.Point(730, 626);
            this.buttonCopyUsersBlog.Name = "buttonCopyUsersBlog";
            this.buttonCopyUsersBlog.Size = new System.Drawing.Size(160, 23);
            this.buttonCopyUsersBlog.TabIndex = 3;
            this.buttonCopyUsersBlog.Text = "Copy Users Blog Code";
            this.buttonCopyUsersBlog.UseVisualStyleBackColor = true;
            this.buttonCopyUsersBlog.Click += new System.EventHandler(this.buttonCopyUsersBlog_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.buttonCopyUsersBlog);
            this.Controls.Add(this.buttonCopyUsersForum);
            this.Controls.Add(this.buttonCopyHtml);
            this.Controls.Add(this.editControl);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSPro Code Colorizer";
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
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem notepadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pffMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Colorizer.EditControl editControl;
        private System.Windows.Forms.Button buttonCopyHtml;
        private System.Windows.Forms.Button buttonCopyUsersForum;
        private System.Windows.Forms.ToolStripMenuItem messageMenuItem;
        private System.Windows.Forms.Button buttonCopyUsersBlog;
    }
}

