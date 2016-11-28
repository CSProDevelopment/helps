using System.Windows.Forms;

namespace Help_Generator
{
    partial class TopicEditForm : Form
    {
        private HelpComponents _helpComponents;
        private Preprocessor.TopicPreprocessor _preprocessedTopic;

        public TopicEditForm(Preprocessor.TopicPreprocessor preprocessedTopic,HelpComponents helpComponents)
        {
            InitializeComponent();

            _helpComponents = helpComponents;
            _preprocessedTopic = preprocessedTopic;

            labelTemp.Text = _preprocessedTopic.Filename;
        }

        public Preprocessor.TopicPreprocessor PreprocessedTopic { get { return _preprocessedTopic; } }
    }
}
