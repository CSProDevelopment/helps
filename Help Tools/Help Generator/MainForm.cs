using System;
using System.IO;
using System.Windows.Forms;

namespace Help_Generator
{
    partial class MainForm : Form
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

        private void MainForm_Activated(object sender,EventArgs e)
        {
            foreach( Form form in MdiChildren )
            {
                if( form is TextEditForm )
                    ((TextEditForm)form).CheckExternalFileEdit();
            }
        }

        private void fileMenuItem_DropDownOpening(object sender,EventArgs e)
        {
            bool formOpened = false;
            bool currentFileSaveable = false;
            bool saveableFileOpened = false;

            foreach( Form form in MdiChildren )
            {
                formOpened = true;

                if( form is TextEditForm )
                {
                    saveableFileOpened = true;

                    if( ActiveMdiChild == form )
                        currentFileSaveable = true;
                }
            }

            fileCloseMenuItem.Enabled = formOpened;
            fileCloseAllMenuItem.Enabled = formOpened;
            fileSaveMenuItem.Enabled = currentFileSaveable;
            fileSaveAllMenuItem.Enabled = saveableFileOpened;
        }

        private void fileNewMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: fileNewMenuItem_Click");
        }

        private void fileCloseMenuItem_Click(object sender,EventArgs e)
        {
            if( ActiveMdiChild != null )
                ActiveMdiChild.Close();
        }

        private void fileCloseAllMenuItem_Click(object sender,EventArgs e)
        {
            for( int i = MdiChildren.Length - 1; i >= 0; i-- )
                MdiChildren[i].Close();
        }

        private void fileSaveMenuItem_Click(object sender,EventArgs e)
        {
            if( ActiveMdiChild != null )
            {
                if( ActiveMdiChild is TextEditForm )
                    ((TextEditForm)ActiveMdiChild).SaveFile();
            }
        }

        private void fileSaveAllMenuItem_Click(object sender,EventArgs e)
        {
            foreach( Form form in MdiChildren )
            {
                if( form is TextEditForm )
                    ((TextEditForm)ActiveMdiChild).SaveFile();
            }
        }

        private void fileExitMenuItem_Click(object sender,EventArgs e)
        {
            Close();
        }

        private void viewSettingsMenuItem_Click(object sender,EventArgs e)
        {
            ShowOrCreateForm(_settings);
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

        private void generateMenuItem_DropDownOpening(object sender,EventArgs e)
        {
            generateCompileMenuItem.Enabled = ( this.ActiveMdiChild != null && this.ActiveMdiChild is TextEditForm );
        }

        private void generateCompileMenuItem_Click(object sender,EventArgs e)
        {
            if( this.ActiveMdiChild != null && this.ActiveMdiChild is TextEditForm )
                ((TextEditForm)this.ActiveMdiChild).Compile();
        }

        private void generateRefreshMenuItem_Click(object sender,EventArgs e)
        {
            try
            {
                _preprocessor.Refresh();
            }

            catch( Exception exception )
            {
                MessageBox.Show(exception.Message);
            }                
        }

        private void generateHelpsMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: generateHelpsMenuItem_Click");
        }

        private void helpContextMenuItem_Click(object sender,EventArgs e)
        {
            if( this.ActiveMdiChild != null && this.ActiveMdiChild is TextEditForm )
                ((TextEditForm)this.ActiveMdiChild).ShowHelp();

            else
                ShowOrCreateForm(typeof(SyntaxHelp));
        }

        private void helpSyntaxMenuItem_Click(object sender,EventArgs e)
        {
            ShowOrCreateForm(typeof(SyntaxHelp));
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
                Directory.SetCurrentDirectory(_projectPath);

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

        private void ShowOrCreateForm(Object formObject)
        {
            bool isSyntaxHelp = ( formObject is Type && ((Type)formObject) == typeof(SyntaxHelp) );
            bool isTextEditableInterface = ( formObject is TextEditableInterface );

            foreach( Form form in MdiChildren )
            {
                if( ( isSyntaxHelp && form is SyntaxHelp ) ||
                    ( isTextEditableInterface && form is TextEditForm && ((TextEditForm)form).IsOfType(((TextEditableInterface)formObject)) ) )
                {
                    form.Activate();
                    return;
                }
            }

            // open a new form
            Form newForm = isSyntaxHelp ? (Form)(new SyntaxHelp()) : (Form)(new TextEditForm((TextEditableInterface)formObject,_preprocessor));
            newForm.MdiParent = this;
            newForm.Show();
        }

    }
}
