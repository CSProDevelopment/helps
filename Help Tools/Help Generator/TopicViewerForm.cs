using System;
using System.Windows.Forms;

namespace Help_Generator
{
    partial class TopicViewerForm : Form
    {
        private Preprocessor _preprocessor;
        private string _baseWindowTitle;

        public TopicViewerForm(Preprocessor preprocessor)
        {
            InitializeComponent();

            _preprocessor = preprocessor;
            _baseWindowTitle = this.Text;
        }

        public void UpdateContents(string title,string html)
        {
            this.Text = String.Format("{0} - {1}",title,_baseWindowTitle);
            webBrowser.DocumentText = html;
        }

        private void webBrowser_Navigating(object sender,WebBrowserNavigatingEventArgs e)
        {
            // open topics in the topic editor
            if( e.Url.OriginalString.IndexOf(TopicUrlPrefix,StringComparison.InvariantCultureIgnoreCase) == 0 )
            {
                e.Cancel = true;
                
                try
                {
                    string filename = e.Url.OriginalString.Substring(TopicUrlPrefix.Length);
                    ((MainForm)this.ParentForm).ShowOrCreateForm(_preprocessor.GetTopic(filename));
                }

                catch( Exception )
                {
                }
            }
        }

        public const string TopicUrlPrefix = "http://TopicViewerForm/";
    }
}
