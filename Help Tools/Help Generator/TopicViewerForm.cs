using System;
using System.Windows.Forms;

namespace Help_Generator
{
    partial class TopicViewerForm : Form
    {
        private string _baseWindowTitle;

        public TopicViewerForm()
        {
            InitializeComponent();

            _baseWindowTitle = this.Text;
        }

        public void UpdateContents(string title,string html)
        {
            this.Text = String.Format("{0} - {1}",title,_baseWindowTitle);
            webBrowser.DocumentText = html;
        }
    }
}
