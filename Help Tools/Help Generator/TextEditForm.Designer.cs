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
            this.titleLabel = new System.Windows.Forms.Label();
            this.editControl = new Colorizer.EditControl();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(13, 10);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(29, 13);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "label";
            // 
            // editControl
            // 
            this.editControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editControl.Lexer = ScintillaNET.Lexer.Null;
            this.editControl.Location = new System.Drawing.Point(12, 30);
            this.editControl.Name = "editControl";
            this.editControl.Size = new System.Drawing.Size(830, 293);
            this.editControl.TabIndex = 0;
            this.editControl.UseTabs = true;
            // 
            // TextEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 335);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.editControl);
            this.Name = "TextEditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Text Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Colorizer.EditControl editControl;
        private System.Windows.Forms.Label titleLabel;
    }
}