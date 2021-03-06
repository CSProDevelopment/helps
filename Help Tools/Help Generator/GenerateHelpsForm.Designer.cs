﻿namespace Help_Generator
{
    partial class GenerateHelpsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateHelpsForm));
            this.pictureBoxCheckmarkIndex = new System.Windows.Forms.PictureBox();
            this.buttonOpenChm = new System.Windows.Forms.Button();
            this.buttonCancelClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBarTopics = new System.Windows.Forms.ProgressBar();
            this.pictureBoxCheckmarkSettings = new System.Windows.Forms.PictureBox();
            this.pictureBoxCheckmarkTableOfContents = new System.Windows.Forms.PictureBox();
            this.pictureBoxCheckmarkTopics = new System.Windows.Forms.PictureBox();
            this.pictureBoxCheckmarkChm = new System.Windows.Forms.PictureBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.checkBoxDeleteTemporaryFiles = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBoxCheckmarkWebsite = new System.Windows.Forms.PictureBox();
            this.buttonOpenWebsite = new System.Windows.Forms.Button();
            this.pictureBoxCheckmarkPdf = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonOpenPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkTableOfContents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkTopics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkChm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkWebsite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkPdf)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxCheckmarkIndex
            // 
            this.pictureBoxCheckmarkIndex.Image = global::Help_Generator.Properties.Resources.Checkmark;
            this.pictureBoxCheckmarkIndex.Location = new System.Drawing.Point(199, 74);
            this.pictureBoxCheckmarkIndex.Name = "pictureBoxCheckmarkIndex";
            this.pictureBoxCheckmarkIndex.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxCheckmarkIndex.TabIndex = 0;
            this.pictureBoxCheckmarkIndex.TabStop = false;
            this.pictureBoxCheckmarkIndex.Visible = false;
            // 
            // buttonOpenChm
            // 
            this.buttonOpenChm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenChm.Enabled = false;
            this.buttonOpenChm.Location = new System.Drawing.Point(870, 229);
            this.buttonOpenChm.Name = "buttonOpenChm";
            this.buttonOpenChm.Size = new System.Drawing.Size(102, 23);
            this.buttonOpenChm.TabIndex = 3;
            this.buttonOpenChm.Text = "Open CHM";
            this.buttonOpenChm.UseVisualStyleBackColor = true;
            this.buttonOpenChm.Click += new System.EventHandler(this.buttonOpenChm_Click);
            // 
            // buttonCancelClose
            // 
            this.buttonCancelClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancelClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancelClose.Location = new System.Drawing.Point(870, 9);
            this.buttonCancelClose.Name = "buttonCancelClose";
            this.buttonCancelClose.Size = new System.Drawing.Size(102, 23);
            this.buttonCancelClose.TabIndex = 0;
            this.buttonCancelClose.Text = "Cancel";
            this.buttonCancelClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Table of Contents";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label3.Location = new System.Drawing.Point(12, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Index";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label4.Location = new System.Drawing.Point(12, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Topics";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label5.Location = new System.Drawing.Point(12, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "CHM File";
            // 
            // progressBarTopics
            // 
            this.progressBarTopics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarTopics.Location = new System.Drawing.Point(199, 112);
            this.progressBarTopics.Name = "progressBarTopics";
            this.progressBarTopics.Size = new System.Drawing.Size(773, 30);
            this.progressBarTopics.Step = 1;
            this.progressBarTopics.TabIndex = 8;
            this.progressBarTopics.Visible = false;
            // 
            // pictureBoxCheckmarkSettings
            // 
            this.pictureBoxCheckmarkSettings.Image = global::Help_Generator.Properties.Resources.Checkmark;
            this.pictureBoxCheckmarkSettings.Location = new System.Drawing.Point(199, 2);
            this.pictureBoxCheckmarkSettings.Name = "pictureBoxCheckmarkSettings";
            this.pictureBoxCheckmarkSettings.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxCheckmarkSettings.TabIndex = 9;
            this.pictureBoxCheckmarkSettings.TabStop = false;
            this.pictureBoxCheckmarkSettings.Visible = false;
            // 
            // pictureBoxCheckmarkTableOfContents
            // 
            this.pictureBoxCheckmarkTableOfContents.Image = global::Help_Generator.Properties.Resources.Checkmark;
            this.pictureBoxCheckmarkTableOfContents.Location = new System.Drawing.Point(199, 38);
            this.pictureBoxCheckmarkTableOfContents.Name = "pictureBoxCheckmarkTableOfContents";
            this.pictureBoxCheckmarkTableOfContents.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxCheckmarkTableOfContents.TabIndex = 10;
            this.pictureBoxCheckmarkTableOfContents.TabStop = false;
            this.pictureBoxCheckmarkTableOfContents.Visible = false;
            // 
            // pictureBoxCheckmarkTopics
            // 
            this.pictureBoxCheckmarkTopics.Image = global::Help_Generator.Properties.Resources.Checkmark;
            this.pictureBoxCheckmarkTopics.Location = new System.Drawing.Point(199, 110);
            this.pictureBoxCheckmarkTopics.Name = "pictureBoxCheckmarkTopics";
            this.pictureBoxCheckmarkTopics.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxCheckmarkTopics.TabIndex = 11;
            this.pictureBoxCheckmarkTopics.TabStop = false;
            this.pictureBoxCheckmarkTopics.Visible = false;
            // 
            // pictureBoxCheckmarkChm
            // 
            this.pictureBoxCheckmarkChm.Image = global::Help_Generator.Properties.Resources.Checkmark;
            this.pictureBoxCheckmarkChm.Location = new System.Drawing.Point(199, 218);
            this.pictureBoxCheckmarkChm.Name = "pictureBoxCheckmarkChm";
            this.pictureBoxCheckmarkChm.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxCheckmarkChm.TabIndex = 12;
            this.pictureBoxCheckmarkChm.TabStop = false;
            this.pictureBoxCheckmarkChm.Visible = false;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.Location = new System.Drawing.Point(17, 259);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(955, 340);
            this.textBoxLog.TabIndex = 3;
            // 
            // checkBoxDeleteTemporaryFiles
            // 
            this.checkBoxDeleteTemporaryFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDeleteTemporaryFiles.AutoSize = true;
            this.checkBoxDeleteTemporaryFiles.Checked = true;
            this.checkBoxDeleteTemporaryFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDeleteTemporaryFiles.Location = new System.Drawing.Point(648, 13);
            this.checkBoxDeleteTemporaryFiles.Name = "checkBoxDeleteTemporaryFiles";
            this.checkBoxDeleteTemporaryFiles.Size = new System.Drawing.Size(200, 17);
            this.checkBoxDeleteTemporaryFiles.TabIndex = 4;
            this.checkBoxDeleteTemporaryFiles.Text = "Delete Temporary Files Upon Closing";
            this.checkBoxDeleteTemporaryFiles.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label6.Location = new System.Drawing.Point(12, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 25);
            this.label6.TabIndex = 13;
            this.label6.Text = "Website";
            // 
            // pictureBoxCheckmarkWebsite
            // 
            this.pictureBoxCheckmarkWebsite.Image = global::Help_Generator.Properties.Resources.Checkmark;
            this.pictureBoxCheckmarkWebsite.Location = new System.Drawing.Point(199, 146);
            this.pictureBoxCheckmarkWebsite.Name = "pictureBoxCheckmarkWebsite";
            this.pictureBoxCheckmarkWebsite.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxCheckmarkWebsite.TabIndex = 14;
            this.pictureBoxCheckmarkWebsite.TabStop = false;
            this.pictureBoxCheckmarkWebsite.Visible = false;
            // 
            // buttonOpenWebsite
            // 
            this.buttonOpenWebsite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenWebsite.Enabled = false;
            this.buttonOpenWebsite.Location = new System.Drawing.Point(870, 157);
            this.buttonOpenWebsite.Name = "buttonOpenWebsite";
            this.buttonOpenWebsite.Size = new System.Drawing.Size(102, 23);
            this.buttonOpenWebsite.TabIndex = 1;
            this.buttonOpenWebsite.Text = "Open Website";
            this.buttonOpenWebsite.UseVisualStyleBackColor = true;
            this.buttonOpenWebsite.Click += new System.EventHandler(this.buttonOpenWebsite_Click);
            // 
            // pictureBoxCheckmarkPdf
            // 
            this.pictureBoxCheckmarkPdf.Image = global::Help_Generator.Properties.Resources.Checkmark;
            this.pictureBoxCheckmarkPdf.Location = new System.Drawing.Point(199, 182);
            this.pictureBoxCheckmarkPdf.Name = "pictureBoxCheckmarkPdf";
            this.pictureBoxCheckmarkPdf.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxCheckmarkPdf.TabIndex = 16;
            this.pictureBoxCheckmarkPdf.TabStop = false;
            this.pictureBoxCheckmarkPdf.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label7.Location = new System.Drawing.Point(12, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 25);
            this.label7.TabIndex = 15;
            this.label7.Text = "PDF";
            // 
            // buttonOpenPdf
            // 
            this.buttonOpenPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenPdf.Enabled = false;
            this.buttonOpenPdf.Location = new System.Drawing.Point(870, 193);
            this.buttonOpenPdf.Name = "buttonOpenPdf";
            this.buttonOpenPdf.Size = new System.Drawing.Size(102, 23);
            this.buttonOpenPdf.TabIndex = 2;
            this.buttonOpenPdf.Text = "Open PDF";
            this.buttonOpenPdf.UseVisualStyleBackColor = true;
            this.buttonOpenPdf.Click += new System.EventHandler(this.buttonOpenPdf_Click);
            // 
            // GenerateHelpsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancelClose;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.buttonOpenPdf);
            this.Controls.Add(this.pictureBoxCheckmarkPdf);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonOpenWebsite);
            this.Controls.Add(this.pictureBoxCheckmarkWebsite);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkBoxDeleteTemporaryFiles);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.pictureBoxCheckmarkChm);
            this.Controls.Add(this.pictureBoxCheckmarkTopics);
            this.Controls.Add(this.pictureBoxCheckmarkTableOfContents);
            this.Controls.Add(this.pictureBoxCheckmarkSettings);
            this.Controls.Add(this.progressBarTopics);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancelClose);
            this.Controls.Add(this.buttonOpenChm);
            this.Controls.Add(this.pictureBoxCheckmarkIndex);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GenerateHelpsForm";
			this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generating Helps";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GenerateHelpsForm_FormClosing);
            this.Load += new System.EventHandler(this.GenerateHelpsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkTableOfContents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkTopics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkChm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkWebsite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckmarkPdf)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxCheckmarkIndex;
        private System.Windows.Forms.Button buttonOpenChm;
        private System.Windows.Forms.Button buttonCancelClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar progressBarTopics;
        private System.Windows.Forms.PictureBox pictureBoxCheckmarkSettings;
        private System.Windows.Forms.PictureBox pictureBoxCheckmarkTableOfContents;
        private System.Windows.Forms.PictureBox pictureBoxCheckmarkTopics;
        private System.Windows.Forms.PictureBox pictureBoxCheckmarkChm;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.CheckBox checkBoxDeleteTemporaryFiles;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBoxCheckmarkWebsite;
        private System.Windows.Forms.Button buttonOpenWebsite;
        private System.Windows.Forms.PictureBox pictureBoxCheckmarkPdf;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonOpenPdf;
    }
}