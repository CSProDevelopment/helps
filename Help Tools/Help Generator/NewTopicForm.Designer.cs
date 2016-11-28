namespace Help_Generator
{
    partial class NewTopicForm
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
            this.textBoxTopicFilename = new System.Windows.Forms.TextBox();
            this.buttonNewTopic = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Topic Filename";
            // 
            // textBoxTopicFilename
            // 
            this.textBoxTopicFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTopicFilename.Location = new System.Drawing.Point(98, 10);
            this.textBoxTopicFilename.Name = "textBoxTopicFilename";
            this.textBoxTopicFilename.Size = new System.Drawing.Size(296, 20);
            this.textBoxTopicFilename.TabIndex = 0;
            // 
            // buttonNewTopic
            // 
            this.buttonNewTopic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewTopic.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonNewTopic.Location = new System.Drawing.Point(417, 8);
            this.buttonNewTopic.Name = "buttonNewTopic";
            this.buttonNewTopic.Size = new System.Drawing.Size(111, 23);
            this.buttonNewTopic.TabIndex = 1;
            this.buttonNewTopic.Text = "Create New Topic";
            this.buttonNewTopic.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(417, 47);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(111, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // NewTopicForm
            // 
            this.AcceptButton = this.buttonNewTopic;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(542, 85);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonNewTopic);
            this.Controls.Add(this.textBoxTopicFilename);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewTopicForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Topic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewTopicForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTopicFilename;
        private System.Windows.Forms.Button buttonNewTopic;
        private System.Windows.Forms.Button buttonCancel;
    }
}