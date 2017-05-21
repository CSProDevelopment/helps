namespace Help_Generator
{
	partial class FilteredTopicsViewerForm
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
			this.textBoxFilter = new System.Windows.Forms.TextBox();
			this.listViewTopics = new System.Windows.Forms.ListView();
			this.webBrowser = new System.Windows.Forms.WebBrowser();
			this.buttonEditTopic = new System.Windows.Forms.Button();
			this.buttonFindReferences = new System.Windows.Forms.Button();
			this.labelTopicFilename = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Search Filter";
			// 
			// textBoxFilter
			// 
			this.textBoxFilter.AcceptsReturn = true;
			this.textBoxFilter.Location = new System.Drawing.Point(99, 11);
			this.textBoxFilter.Name = "textBoxFilter";
			this.textBoxFilter.Size = new System.Drawing.Size(149, 20);
			this.textBoxFilter.TabIndex = 1;
			this.textBoxFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxFilter_KeyDown);
			// 
			// listViewTopics
			// 
			this.listViewTopics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listViewTopics.FullRowSelect = true;
			this.listViewTopics.HideSelection = false;
			this.listViewTopics.Location = new System.Drawing.Point(15, 38);
			this.listViewTopics.MultiSelect = false;
			this.listViewTopics.Name = "listViewTopics";
			this.listViewTopics.Size = new System.Drawing.Size(233, 527);
			this.listViewTopics.TabIndex = 2;
			this.listViewTopics.UseCompatibleStateImageBehavior = false;
			this.listViewTopics.View = System.Windows.Forms.View.List;
			this.listViewTopics.SelectedIndexChanged += new System.EventHandler(this.listViewTopics_SelectedIndexChanged);
			// 
			// webBrowser
			// 
			this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowser.Location = new System.Drawing.Point(265, 11);
			this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser.Name = "webBrowser";
			this.webBrowser.Size = new System.Drawing.Size(507, 516);
			this.webBrowser.TabIndex = 3;
			this.webBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser_Navigating);
			// 
			// buttonEditTopic
			// 
			this.buttonEditTopic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditTopic.Enabled = false;
			this.buttonEditTopic.Location = new System.Drawing.Point(663, 542);
			this.buttonEditTopic.Name = "buttonEditTopic";
			this.buttonEditTopic.Size = new System.Drawing.Size(109, 23);
			this.buttonEditTopic.TabIndex = 4;
			this.buttonEditTopic.Text = "Edit Topic";
			this.buttonEditTopic.UseVisualStyleBackColor = true;
			this.buttonEditTopic.Click += new System.EventHandler(this.buttonEditTopic_Click);
			// 
			// buttonFindReferences
			// 
			this.buttonFindReferences.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFindReferences.Enabled = false;
			this.buttonFindReferences.Location = new System.Drawing.Point(529, 542);
			this.buttonFindReferences.Name = "buttonFindReferences";
			this.buttonFindReferences.Size = new System.Drawing.Size(109, 23);
			this.buttonFindReferences.TabIndex = 5;
			this.buttonFindReferences.Text = "Find References";
			this.buttonFindReferences.UseVisualStyleBackColor = true;
			this.buttonFindReferences.Click += new System.EventHandler(this.buttonFindReferences_Click);
			// 
			// labelTopicFilename
			// 
			this.labelTopicFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelTopicFilename.AutoSize = true;
			this.labelTopicFilename.Location = new System.Drawing.Point(262, 547);
			this.labelTopicFilename.Name = "labelTopicFilename";
			this.labelTopicFilename.Size = new System.Drawing.Size(16, 13);
			this.labelTopicFilename.TabIndex = 6;
			this.labelTopicFilename.Text = "...";
			this.labelTopicFilename.Visible = false;
			// 
			// FilteredTopicsViewerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 577);
			this.Controls.Add(this.labelTopicFilename);
			this.Controls.Add(this.buttonFindReferences);
			this.Controls.Add(this.buttonEditTopic);
			this.Controls.Add(this.webBrowser);
			this.Controls.Add(this.listViewTopics);
			this.Controls.Add(this.textBoxFilter);
			this.Controls.Add(this.label1);
			this.Name = "FilteredTopicsViewerForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Filtered Topics Viewer";
			this.Shown += new System.EventHandler(this.FilteredTopicsViewerForm_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxFilter;
		private System.Windows.Forms.ListView listViewTopics;
		private System.Windows.Forms.WebBrowser webBrowser;
		private System.Windows.Forms.Button buttonEditTopic;
		private System.Windows.Forms.Button buttonFindReferences;
		private System.Windows.Forms.Label labelTopicFilename;
	}
}