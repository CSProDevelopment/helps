using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Help_Generator
{
    partial class NewTopicForm : Form
    {
        private HelpComponents _helpComponents;
        private Preprocessor.TopicPreprocessor _preprocessedTopic;

        public Preprocessor.TopicPreprocessor PreprocessedTopic { get { return _preprocessedTopic; } }

        public NewTopicForm(HelpComponents helpComponents)
        {
            InitializeComponent();

            _helpComponents = helpComponents;
            _preprocessedTopic = null;
        }

        private void NewTopicForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            if( DialogResult == DialogResult.OK )
            {
                try
                {
                    if( String.IsNullOrWhiteSpace(textBoxTopicFilename.Text) )
                        throw new Exception("Enter a topic filename.");

                    string topicsPath = Path.Combine(_helpComponents.projectPath,Constants.TopicsDirectoryName);
                    string topicFilename = Path.Combine(topicsPath,textBoxTopicFilename.Text.Trim() + Constants.TopicExtension);

                    if( File.Exists(topicFilename) )
                        throw new Exception("A topic with the filename already exists: " + topicFilename);

                    // create a blank topic file
                    using( TextWriter tw = new StreamWriter(topicFilename,false,Encoding.UTF8) )
                        tw.WriteLine("<title>{0}</title>",textBoxTopicFilename.Text);

                    _helpComponents.preprocessor.Refresh();
                    _preprocessedTopic = _helpComponents.preprocessor.GetTopic(Path.GetFileName(topicFilename));                    
                }

                catch( Exception exception )
                {
                    MessageBox.Show(exception.Message);
                    e.Cancel = true;
                }
            }
        }
    }
}
