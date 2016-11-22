using System.Windows.Forms;

namespace Help_Generator
{
    public partial class SyntaxHelp : Form
    {
        public SyntaxHelp()
        {
            InitializeComponent();
            webBrowser.DocumentText = Properties.Resources.SyntaxHelp;
        }
    }
}
