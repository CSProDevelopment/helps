using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code_Colorizer
{
	partial class DesignerCopierForm : Form
	{
		private bool _copyHtml;

		public DesignerCopierForm()
		{
			InitializeComponent();

			_copyHtml = true;
		}

		private void DesignerCopierForm_Shown(object sender,EventArgs e)
		{
			// close the form after a second
			Task.Delay(TimeSpan.FromSeconds(1))
				.ContinueWith((t) => this.Close(),TaskScheduler.FromCurrentSynchronizationContext());
		}

		private void DesignerCopierForm_FormClosing(object sender,FormClosingEventArgs e)
		{
			try
			{
				Processor processor = new Processor();
				string text = Clipboard.GetText();

				if( _copyHtml )
					processor.HtmlProcessor(text);

				else
					processor.UsersForumProcessor(text);
			}

			catch( Exception exception )
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void buttonCopyHtml_Click(object sender,EventArgs e)
		{
			Close();
		}

		private void buttonCopyUsersForum_Click(object sender,EventArgs e)
		{
			_copyHtml = false;
			Close();
		}
	}
}
