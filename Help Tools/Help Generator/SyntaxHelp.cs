using System.Windows.Forms;

namespace Help_Generator
{
    partial class SyntaxHelp : Form
    {
        public SyntaxHelp()
        {
            InitializeComponent();
            webBrowser.DocumentText = Properties.Resources.SyntaxHelp;
        }
    }
}
