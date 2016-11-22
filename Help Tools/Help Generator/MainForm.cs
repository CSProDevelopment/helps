using System;
using System.IO;
using System.Windows.Forms;

namespace Help_Generator
{
    public partial class MainForm : Form
    {
        private string _hhcExe;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender,EventArgs e)
        {
            _hhcExe = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),@"HTML Help Workshop\hhc.exe");

            if( !File.Exists(_hhcExe) )
            {
                MessageBox.Show("Could not find the HTML Help Compiler here:\n\n" + _hhcExe);
                Close();
                return;
            }
        }

        private void fileNewMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: fileNewMenuItem_Click");

        }

        private void fileCloseMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: fileCloseMenuItem_Click");

        }

        private void fileSaveMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: fileSaveMenuItem_Click");

        }

        private void fileExitMenuItem_Click(object sender,EventArgs e)
        {
            // TODO: check if any files are not saved
            Close();
        }

        private void viewSettingsMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: viewSettingsMenuItem_Click");
        }

        private void viewTableOfContentsMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: viewTableOfContentsMenuItem_Click");
        }

        private void viewIndexMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: viewIndexMenuItem_Click");
        }

        private void viewTopicsMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: viewTopicsMenuItem_Click");
        }

        private void generateRefreshMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: generateRefreshMenuItem_Click");
        }

        private void generateHelpsMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: generateHelpsMenuItem_Click");
        }

        private void helpSyntaxMenuItem_Click(object sender,EventArgs e)
        {
            new SyntaxHelp().Show();
        }

    }
}
