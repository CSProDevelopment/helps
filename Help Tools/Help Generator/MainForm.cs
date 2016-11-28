using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

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
            string initialTopicFilename = null;

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

                string fileArgument = (string)commandArgs.GetValue(generateAndClose ? 2 : 1);

                if( File.Exists(fileArgument) )
                {
                    // loading the settings file
                    if( Path.GetExtension(fileArgument).Equals(Constants.SettingsFileExtension,StringComparison.InvariantCultureIgnoreCase) )
                        _helpComponents.projectPath = Path.GetDirectoryName(fileArgument);

                    // loading a topic
                    else if( Path.GetExtension(fileArgument).Equals(Constants.TopicExtension,StringComparison.InvariantCultureIgnoreCase) )
                    {
                        initialTopicFilename = Path.GetFileName(fileArgument);

                        // find the settings file by looking at parent folders
                        string directory = Path.GetDirectoryName(fileArgument);
                        string parentDirectory = null;

                        while( !( parentDirectory = Path.GetFullPath(Path.Combine(directory,"..")) ).Equals(directory) )
                        {
                            if( new DirectoryInfo(parentDirectory).GetFiles("*" + Constants.SettingsFileExtension).Length > 0 )
                            {
                                _helpComponents.projectPath = parentDirectory;
                                break;
                            }

                            directory = parentDirectory;
                        }

                        if( _helpComponents.projectPath == null )
                        {
                            MessageBox.Show("The help settings file could not be located.");
                            Close();
                            return;
                        }
                    }

                    else
                    {
                        MessageBox.Show("The file type cannot be loaded: " + fileArgument);
                        Close();
                        return;
                    }
                }
            }

            if( _helpComponents.projectPath == null )
            {
                MessageBox.Show("The program was loaded with invalid parameters.");
                Close();
                return;
            }

            if( !LoadProject() )
                return;

            if( generateAndClose )
            {
                GenerateHelps(true);
                Close();
            }

            else if( initialTopicFilename != null )
                ShowOrCreateForm(_helpComponents.preprocessor.GetTopic(initialTopicFilename));
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
            NewTopicForm dlg = new NewTopicForm(_helpComponents);

            if( dlg.ShowDialog() == DialogResult.OK )
            {
                UpdateItemLists();
                ShowOrCreateForm(dlg.PreprocessedTopic);
            }
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
                    ((TextEditForm)form).SaveFile();
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
            ShowOrCreateForm(new ItemListForm(_helpComponents.preprocessor,true));
        }

        private void viewImagesMenuItem_Click(object sender,EventArgs e)
        {
            ShowOrCreateForm(new ItemListForm(_helpComponents.preprocessor,false));
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
                UpdateItemLists();
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
                    if( !((TextEditForm)form).IsFileReady() )
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

        private void toolsRegisterFileAssociationsMenuItem_Click(object sender,EventArgs e)
        {
            try
            {
                const string registryPath = @"HKEY_CURRENT_USER\Software\Classes\";
                string executableFilename = Application.ExecutablePath;

                for( int i = 0; i < 2; i++ )
                {
                    string documentName = ( i == 0 ) ? Constants.RegistrySettingsDocumentName : Constants.RegistryTopicDocumentName;
                    string extension = ( i == 0 ) ? Constants.SettingsFileExtension : Constants.TopicExtension;

                    Registry.SetValue(registryPath + extension,"",documentName);
                    Registry.SetValue(registryPath + documentName + @"\DefaultIcon","",executableFilename + ",0");
                    Registry.SetValue(registryPath + documentName + @"\shell\Open\command","","\"" + executableFilename + "\" \"%1\"");
                }

                MessageBox.Show("Registry settings changed successfully.");
            }

            catch( Exception exception )
            {
                MessageBox.Show("There was an error modifying the registry: " + exception.Message);
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

        public void ShowOrCreateForm(Object formObject)
        {
            bool isSyntaxHelp = ( formObject is Type && ((Type)formObject) == typeof(SyntaxHelp) );
            bool isItemListForm = ( formObject is ItemListForm );
            bool isTextEditableInterface = ( formObject is TextEditableInterface );
            bool isTopic = ( formObject is Preprocessor.TopicPreprocessor );

            // multiple item lists are allowed but otherwise multiple windows of a given type are not
            foreach( Form form in MdiChildren )
            {
                if( ( isSyntaxHelp && form is SyntaxHelp ) ||
                    ( isTextEditableInterface && form is TextEditForm && ((TextEditForm)form).IsOfType(((TextEditableInterface)formObject)) ) ||
                    ( isTopic && form is TopicEditForm && ((TopicEditForm)form).PreprocessedTopic == (Preprocessor.TopicPreprocessor)formObject ) )
                {
                    form.Activate();
                    return;
                }
            }

            // open a new form
            Form newForm = null;

            if( isSyntaxHelp )
                newForm = new SyntaxHelp();

            else if( isItemListForm )
                newForm = (Form)formObject;
            
            else if( isTextEditableInterface )
                newForm = new TextEditForm((TextEditableInterface)formObject,_helpComponents.preprocessor);

            else if( isTopic )
                newForm = new TopicEditForm((Preprocessor.TopicPreprocessor)formObject,_helpComponents);

            if( newForm != null )
            {
                newForm.MdiParent = this;
                newForm.Show();
            }
        }

        private void GenerateHelps(bool generateAndClose)
        {
            GenerateHelpsForm dlg = new GenerateHelpsForm(_helpComponents,generateAndClose);
            dlg.ShowDialog();
        }

        public void UpdateItemLists()
        {
            foreach( Form form in MdiChildren )
            {
                if( form is ItemListForm )
                    ((ItemListForm)form).UpdateList(true);
            }
        }
    }
}
