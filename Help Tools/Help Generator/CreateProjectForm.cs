using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Help_Generator
{
    partial class CreateProjectForm : Form
    {
        private string  _projectPath;
        public string ProjectPath { get { return _projectPath; } }

        public CreateProjectForm()
        {
            InitializeComponent();
        }

        private void CreateProjectForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            if( DialogResult == DialogResult.OK )
            {
                try
                {
                    _projectPath = textBoxPath.Text;

                    if( Directory.Exists(_projectPath) )
                    {
                        if( Directory.EnumerateFileSystemEntries(_projectPath).Any() )
                            throw new Exception("You must specify a directory that does not exist or that is empty.");
                    }

                    else
                        Directory.CreateDirectory(_projectPath);

                    Directory.CreateDirectory(Path.Combine(_projectPath,Constants.TopicsDirectoryName));
                    Directory.CreateDirectory(Path.Combine(_projectPath,Constants.ImagesDirectoryName));
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
