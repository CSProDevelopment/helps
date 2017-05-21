using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Help_Generator
{
	partial class FilteredTopicsViewerForm : Form
	{
		private HelpComponents _helpComponents;
		private string _initialWindowTitle;
		private List<TopicListParser.TopicListNode> _orderedTopics;
		private TextEditFormTopicCompilerSettings _topicCompilerSettings;
		private Dictionary<TopicListParser.TopicListNode,string> _loadedText;
		private Preprocessor.TopicPreprocessor _lastDisplayedPreprocessedTopic;

		public FilteredTopicsViewerForm(HelpComponents helpComponents)
		{
			InitializeComponent();

			_helpComponents = helpComponents;
			_initialWindowTitle = this.Text;
			_topicCompilerSettings = new TextEditFormTopicCompilerSettings();
		}

		private void FilteredTopicsViewerForm_Shown(object sender,EventArgs e)
		{
			try
			{
				_helpComponents.tableOfContents.Compile(_helpComponents.preprocessor);
				_orderedTopics = _helpComponents.tableOfContents.GetOrderedTopicsForPdf();
			}

			catch( Exception )
			{
				MessageBox.Show("You must have a valid table of contents before you can use this feature");
				Close();
			}
		}

		private void EnableTopicButtons(bool enabled)
		{
			buttonEditTopic.Enabled = enabled;
			buttonFindReferences.Enabled = enabled;
			labelTopicFilename.Visible = enabled;
		}

		private void textBoxFilter_KeyDown(object sender,KeyEventArgs e)
		{
			if( e.KeyCode == Keys.Enter )
			{
				FilterTopics(textBoxFilter.Text);
				e.SuppressKeyPress = true;
			}
		}

		private void FilterTopics(string filter)
		{
			bool mustLoadText = ( _loadedText == null );

			if( mustLoadText )
				_loadedText = new Dictionary<TopicListParser.TopicListNode,string>();

			try
			{
				listViewTopics.Items.Clear();

				foreach( var node in _orderedTopics )
				{
					if( node.Topic == null ) // ignore chapters
						continue;

					if( mustLoadText )
						_loadedText.Add(node,File.ReadAllText(node.Topic.Filename));

					if( ( node.Topic.Filename.IndexOf(filter,StringComparison.InvariantCultureIgnoreCase) >= 0 ) ||
						( _loadedText[node].IndexOf(filter,StringComparison.InvariantCultureIgnoreCase) >= 0 ) )
					{
						ListViewItem lvi = listViewTopics.Items.Add(node.Title);
						lvi.Tag = node;
					}
				}

				// select the first topic
				if( listViewTopics.Items.Count > 0 )
					listViewTopics.Items[0].Selected = true;

				else
				{
					DisplayTextInBrowser("No results");
					EnableTopicButtons(false);
				}
			}

            catch( Exception exception )
            {
                MessageBox.Show(exception.Message);
            }

			this.Text = String.Format("{0}{1}{2}",filter,String.IsNullOrWhiteSpace(filter) ? "" : " - ",_initialWindowTitle);
		}

		private void listViewTopics_SelectedIndexChanged(object sender,EventArgs e)
		{
			bool enableTopicButtons = false;

			if( listViewTopics.SelectedItems.Count > 0 )
			{
				try
				{
					ListViewItem lvi = listViewTopics.SelectedItems[0];
					TopicListParser.TopicListNode node = (TopicListParser.TopicListNode)lvi.Tag;
					DisplayTopic(node.Topic);
					enableTopicButtons = true;
				}

				catch( Exception exception )
				{
					DisplayTextInBrowser(exception.Message);
				}

				EnableTopicButtons(enableTopicButtons);
			}
		}

		private void DisplayTopic(Preprocessor.TopicPreprocessor preprocessedTopic)
		{
			TopicCompiler topicCompiler = new TopicCompiler(_helpComponents,preprocessedTopic,_topicCompilerSettings);
			string[] topicLines = File.ReadAllLines(preprocessedTopic.Filename);
			string html = topicCompiler.CompileForHtml(topicLines);
			webBrowser.DocumentText = html;

			_lastDisplayedPreprocessedTopic = preprocessedTopic;
			labelTopicFilename.Text = Path.GetFileName(_lastDisplayedPreprocessedTopic.Filename);
		}

		private void DisplayTextInBrowser(string text)
		{
			webBrowser.DocumentText = "<html><body><b>" + text + "</b></body></html>";
		}

		private void webBrowser_Navigating(object sender,WebBrowserNavigatingEventArgs e)
		{
            if( e.Url.OriginalString.IndexOf(TopicViewerForm.TopicUrlPrefix,StringComparison.InvariantCultureIgnoreCase) == 0 )
            {
                e.Cancel = true;
                
                try
                {
                    string filename = e.Url.OriginalString.Substring(TopicViewerForm.TopicUrlPrefix.Length);
					DisplayTopic(_helpComponents.preprocessor.GetTopic(filename));
                }

                catch( Exception )
                {
                }
            }
		}

		private void buttonEditTopic_Click(object sender,EventArgs e)
		{
			((MainForm)this.ParentForm).ShowOrCreateForm(_lastDisplayedPreprocessedTopic);
		}

		private void buttonFindReferences_Click(object sender,EventArgs e)
		{
			string filename = Path.GetFileName(_lastDisplayedPreprocessedTopic.Filename);
			textBoxFilter.Text = filename;
			FilterTopics(filename);
		}
	}
}
