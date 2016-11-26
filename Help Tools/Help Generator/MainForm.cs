using System;
using System.IO;
using System.Windows.Forms;

namespace Help_Generator
{
    partial class MainForm : Form
    {
        private HelpComponents _helpComponents;

        public MainForm()
        {
            InitializeComponent();

            _helpComponents = new HelpComponents();
        }

        private void MainForm_Load(object sender,EventArgs e)
        {
            _helpComponents.htmlHelpCompilerExecutable = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),@"HTML Help Workshop\hhc.exe");

            if( !File.Exists(_helpComponents.htmlHelpCompilerExecutable) )
            {
                MessageBox.Show("Could not find the HTML Help Compiler here:\n\n" + _helpComponents.htmlHelpCompilerExecutable);
                Close();
                return;
            }

            Array commandArgs = Environment.GetCommandLineArgs();
            bool generateAndClose = false;

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
                if( commandArgs.Length > 2 && ((string)commandArgs.GetValue(1)).Equals("/generate",StringComparison.InvariantCultureIgnoreCase) )
                    generateAndClose = true;

                _helpComponents.projectPath = Path.GetDirectoryName((string)commandArgs.GetValue(generateAndClose ? 2 : 1));
            }

            if( LoadProject() )
            {
                if( generateAndClose )
                {
                    GenerateHelps(true);
                    Close();
                }
            }
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
            ShowOrCreateForm(_helpComponents.settings);
        }

        private void viewTableOfContentsMenuItem_Click(object sender,EventArgs e)
        {
            ShowOrCreateForm(_helpComponents.tableOfContents);
        }

        private void viewIndexMenuItem_Click(object sender,EventArgs e)
        {
            ShowOrCreateForm(_helpComponents.index);
        }

        private void viewTopicsMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: viewTopicsMenuItem_Click");
        }

        private void generateMenuItem_DropDownOpening(object sender,EventArgs e)
        {
            bool isTextEditForm = ( this.ActiveMdiChild != null && this.ActiveMdiChild is TextEditForm );
            generateCompileMenuItem.Enabled = isTextEditForm;
            generateFormatMenuItem.Enabled = isTextEditForm;
        }

        private void generateCompileMenuItem_Click(object sender,EventArgs e)
        {
            if( this.ActiveMdiChild != null && this.ActiveMdiChild is TextEditForm )
                ((TextEditForm)this.ActiveMdiChild).Compile();
        }

        private void generateFormatMenuItem_Click(object sender,EventArgs e)
        {
            if( this.ActiveMdiChild != null && this.ActiveMdiChild is TextEditForm )
                ((TextEditForm)this.ActiveMdiChild).Format();
        }

        private void generateRefreshMenuItem_Click(object sender,EventArgs e)
        {
            try
            {
                _helpComponents.preprocessor.Refresh();
            }

            catch( Exception exception )
            {
                MessageBox.Show(exception.Message);
            }                
        }

        private void generateHelpsMenuItem_Click(object sender,EventArgs e)
        {
            // make sure that all of the files are saved
            foreach( Form form in MdiChildren )
            {
                if( form is TextEditForm )
                {
                    if( !((TextEditForm)ActiveMdiChild).IsFileReady() )
                        return;
                }
            }

            try
            {
                _helpComponents.preprocessor.Refresh();
                GenerateHelps(false);
            }

            catch( Exception exception )
            {
                MessageBox.Show(exception.Message);
            }                
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
                _helpComponents.projectPath = dlg.ProjectPath;
                return true;
            }

            else
                return false;
        }

        private bool LoadProject()
        {
            try
            {
                Directory.SetCurrentDirectory(_helpComponents.projectPath);

                _helpComponents.settings = new Settings(_helpComponents.projectPath);
                _helpComponents.tableOfContents = new TableOfContents(_helpComponents.projectPath);                
                _helpComponents.index = new Index(_helpComponents.projectPath);

                _helpComponents.preprocessor = Preprocessor.Create(_helpComponents.projectPath);
                _helpComponents.preprocessor.Refresh();

                this.Text = String.Format("{0} - {1}",Path.GetFileName(_helpComponents.projectPath),this.Text);
            }

            catch( Exception exception )
            {
                MessageBox.Show(exception.Message);
                Close();
                return false;
            }

            return true;
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
            Form newForm = isSyntaxHelp ? (Form)(new SyntaxHelp()) : (Form)(new TextEditForm((TextEditableInterface)formObject,_helpComponents.preprocessor));
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void GenerateHelps(bool generateAndClose)
        {
            GenerateHelpsForm dlg = new GenerateHelpsForm(_helpComponents,generateAndClose);
            dlg.ShowDialog();
        }
    }
}
