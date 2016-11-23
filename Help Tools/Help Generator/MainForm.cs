using System;
using System.IO;
using System.Windows.Forms;

namespace Help_Generator
{
    public partial class MainForm : Form
    {
        private string _hhcExe;
        private string _projectPath;

        private Settings _settings;
        private Preprocessor _preprocessor;

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

            Array commandArgs = Environment.GetCommandLineArgs();

            if( commandArgs.Length == 1 )
            {
                if( !CreateProject() )
                {
                    Close();
                    return;
                }
            }

            else
            {
                // TODO: add way to build the helps from the command line
                _projectPath = Path.GetDirectoryName((string)commandArgs.GetValue(1));
            }

            LoadProject();
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
            try
            {
                _preprocessor.Refresh();
            }

            catch( Exception exception )
            {
                // TODO: make sure everything is saved before closing
                MessageBox.Show(exception.Message);
                Close();
            }                
        }

        private void generateHelpsMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: generateHelpsMenuItem_Click");
        }

        private void helpSyntaxMenuItem_Click(object sender,EventArgs e)
        {
            new SyntaxHelp().Show();
        }


        private bool CreateProject()
        {
            CreateProjectForm dlg = new CreateProjectForm();
            
            if( dlg.ShowDialog() == DialogResult.OK )
            {
                _projectPath = dlg.ProjectPath;
                return true;
            }

            else
                return false;
        }

        private void LoadProject()
        {
            try
            {
                _settings = new Settings(_projectPath);

                _preprocessor = Preprocessor.Create(_projectPath);
                _preprocessor.Refresh();
            }

            catch( Exception exception )
            {
                MessageBox.Show(exception.Message);
                Close();
            }
        }

    }
}
