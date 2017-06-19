namespace Code_Colorizer
{
	partial class DesignerCopierForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.buttonCopyUsersForum = new System.Windows.Forms.Button();
			this.buttonCopyHtml = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(495, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "In one second, the text will be copied as HTML and this dialog will close. You ca" +
    "n also select an option:";
			// 
			// buttonCopyUsersForum
			// 
			this.buttonCopyUsersForum.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonCopyUsersForum.Location = new System.Drawing.Point(279, 33);
			this.buttonCopyUsersForum.Name = "buttonCopyUsersForum";
			this.buttonCopyUsersForum.Size = new System.Drawing.Size(160, 23);
			this.buttonCopyUsersForum.TabIndex = 1;
			this.buttonCopyUsersForum.Text = "Copy Users Forum Code";
			this.buttonCopyUsersForum.UseVisualStyleBackColor = true;
			this.buttonCopyUsersForum.Click += new System.EventHandler(this.buttonCopyUsersForum_Click);
			// 
			// buttonCopyHtml
			// 
			this.buttonCopyHtml.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonCopyHtml.Location = new System.Drawing.Point(64, 33);
			this.buttonCopyHtml.Name = "buttonCopyHtml";
			this.buttonCopyHtml.Size = new System.Drawing.Size(160, 23);
			this.buttonCopyHtml.TabIndex = 0;
			this.buttonCopyHtml.Text = "Copy Formatted HTML";
			this.buttonCopyHtml.UseVisualStyleBackColor = true;
			this.buttonCopyHtml.Click += new System.EventHandler(this.buttonCopyHtml_Click);
			// 
			// DesignerCopierForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(503, 68);
			this.Controls.Add(this.buttonCopyUsersForum);
			this.Controls.Add(this.buttonCopyHtml);
			this.Controls.Add(this.label1);
			this.Name = "DesignerCopierForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CSPro Code Colorizer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DesignerCopierForm_FormClosing);
			this.Shown += new System.EventHandler(this.DesignerCopierForm_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonCopyUsersForum;
		private System.Windows.Forms.Button buttonCopyHtml;
	}
}