namespace Help_Generator
{
    partial class ItemListForm
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
            this.radioButtonTopics = new System.Windows.Forms.RadioButton();
            this.radioButtonImages = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.buttonCopyFilename = new System.Windows.Forms.Button();
            this.buttonView = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.listViewItems = new System.Windows.Forms.ListView();
            this.buttonCopyForTopicSource = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Viewing:";
            // 
            // radioButtonTopics
            // 
            this.radioButtonTopics.AutoSize = true;
            this.radioButtonTopics.Checked = true;
            this.radioButtonTopics.Location = new System.Drawing.Point(72, 11);
            this.radioButtonTopics.Name = "radioButtonTopics";
            this.radioButtonTopics.Size = new System.Drawing.Size(57, 17);
            this.radioButtonTopics.TabIndex = 5;
            this.radioButtonTopics.TabStop = true;
            this.radioButtonTopics.Text = "Topics";
            this.radioButtonTopics.UseVisualStyleBackColor = true;
            this.radioButtonTopics.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButtonImages
            // 
            this.radioButtonImages.AutoSize = true;
            this.radioButtonImages.Location = new System.Drawing.Point(135, 11);
            this.radioButtonImages.Name = "radioButtonImages";
            this.radioButtonImages.Size = new System.Drawing.Size(59, 17);
            this.radioButtonImages.TabIndex = 6;
            this.radioButtonImages.Text = "Images";
            this.radioButtonImages.UseVisualStyleBackColor = true;
            this.radioButtonImages.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Filter:";
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilter.Location = new System.Drawing.Point(72, 40);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(306, 20);
            this.textBoxFilter.TabIndex = 7;
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // buttonCopyFilename
            // 
            this.buttonCopyFilename.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCopyFilename.Enabled = false;
            this.buttonCopyFilename.Location = new System.Drawing.Point(213, 507);
            this.buttonCopyFilename.Name = "buttonCopyFilename";
            this.buttonCopyFilename.Size = new System.Drawing.Size(165, 23);
            this.buttonCopyFilename.TabIndex = 3;
            this.buttonCopyFilename.Text = "Copy Filename";
            this.buttonCopyFilename.UseVisualStyleBackColor = true;
            this.buttonCopyFilename.Click += new System.EventHandler(this.buttonCopyFilename_Click);
            // 
            // buttonView
            // 
            this.buttonView.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonView.Enabled = false;
            this.buttonView.Location = new System.Drawing.Point(16, 507);
            this.buttonView.Name = "buttonView";
            this.buttonView.Size = new System.Drawing.Size(165, 23);
            this.buttonView.TabIndex = 1;
            this.buttonView.Text = "View (Enter)";
            this.buttonView.UseVisualStyleBackColor = true;
            this.buttonView.Click += new System.EventHandler(this.buttonView_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(16, 542);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(165, 23);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Delete (Del)";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // listViewItems
            // 
            this.listViewItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewItems.FullRowSelect = true;
            this.listViewItems.HideSelection = false;
            this.listViewItems.Location = new System.Drawing.Point(16, 73);
            this.listViewItems.MultiSelect = false;
            this.listViewItems.Name = "listViewItems";
            this.listViewItems.Size = new System.Drawing.Size(362, 428);
            this.listViewItems.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewItems.TabIndex = 0;
            this.listViewItems.UseCompatibleStateImageBehavior = false;
            this.listViewItems.View = System.Windows.Forms.View.List;
            this.listViewItems.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewItems_ItemSelectionChanged);
            this.listViewItems.DoubleClick += new System.EventHandler(this.buttonView_Click);
            this.listViewItems.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listViewItems_KeyUp);
            // 
            // buttonCopyForTopicSource
            // 
            this.buttonCopyForTopicSource.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCopyForTopicSource.Enabled = false;
            this.buttonCopyForTopicSource.Location = new System.Drawing.Point(213, 542);
            this.buttonCopyForTopicSource.Name = "buttonCopyForTopicSource";
            this.buttonCopyForTopicSource.Size = new System.Drawing.Size(165, 23);
            this.buttonCopyForTopicSource.TabIndex = 4;
            this.buttonCopyForTopicSource.Text = "Copy for Topic Source (Space)";
            this.buttonCopyForTopicSource.UseVisualStyleBackColor = true;
            this.buttonCopyForTopicSource.Click += new System.EventHandler(this.buttonCopyForTopicSource_Click);
            // 
            // ItemListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 577);
            this.Controls.Add(this.buttonCopyForTopicSource);
            this.Controls.Add(this.listViewItems);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonView);
            this.Controls.Add(this.buttonCopyFilename);
            this.Controls.Add(this.textBoxFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButtonImages);
            this.Controls.Add(this.radioButtonTopics);
            this.Controls.Add(this.label1);
            this.Name = "ItemListForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Item List";
            this.Load += new System.EventHandler(this.TopicListForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonTopics;
        private System.Windows.Forms.RadioButton radioButtonImages;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.Button buttonCopyFilename;
        private System.Windows.Forms.Button buttonView;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ListView listViewItems;
        private System.Windows.Forms.Button buttonCopyForTopicSource;
    }
}