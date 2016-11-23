namespace Help_Generator
{
    partial class TextEditForm
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.linkLabelFilename = new System.Windows.Forms.LinkLabel();
            this.textBoxResults = new System.Windows.Forms.TextBox();
            this.editControl = new Colorizer.EditControl();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(13, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(32, 13);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Title";
            // 
            // linkLabelFilename
            // 
            this.linkLabelFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelFilename.LinkColor = System.Drawing.Color.Olive;
            this.linkLabelFilename.Location = new System.Drawing.Point(307, 10);
            this.linkLabelFilename.Name = "linkLabelFilename";
            this.linkLabelFilename.Size = new System.Drawing.Size(624, 13);
            this.linkLabelFilename.TabIndex = 2;
            this.linkLabelFilename.TabStop = true;
            this.linkLabelFilename.Text = "filename";
            this.linkLabelFilename.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkLabelFilename.VisitedLinkColor = System.Drawing.Color.White;
            this.linkLabelFilename.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFilename_LinkClicked);
            // 
            // textBoxResults
            // 
            this.textBoxResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResults.Location = new System.Drawing.Point(12, 437);
            this.textBoxResults.Multiline = true;
            this.textBoxResults.Name = "textBoxResults";
            this.textBoxResults.ReadOnly = true;
            this.textBoxResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxResults.Size = new System.Drawing.Size(919, 96);
            this.textBoxResults.TabIndex = 3;
            // 
            // editControl
            // 
            this.editControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editControl.Lexer = ScintillaNET.Lexer.Null;
            this.editControl.Location = new System.Drawing.Point(12, 30);
            this.editControl.Name = "editControl";
            this.editControl.Size = new System.Drawing.Size(919, 388);
            this.editControl.TabIndex = 0;
            this.editControl.UseTabs = true;
            // 
            // TextEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 545);
            this.Controls.Add(this.textBoxResults);
            this.Controls.Add(this.linkLabelFilename);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.editControl);
            this.Name = "TextEditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Text Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextEditForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Colorizer.EditControl editControl;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.LinkLabel linkLabelFilename;
        private System.Windows.Forms.TextBox textBoxResults;
    }
}