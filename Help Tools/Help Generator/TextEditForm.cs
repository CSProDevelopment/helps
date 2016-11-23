using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Help_Generator
{
    partial class TextEditForm : Form
    {
        private TextEditableInterface _textEditableInterface;
        Preprocessor _preprocessor;
        private string _filename;
        private DateTime _modifiedDate;

        public TextEditForm(TextEditableInterface textEditableInterface,Preprocessor preprocessor)
        {
            InitializeComponent();

            _textEditableInterface = textEditableInterface;
            _preprocessor = preprocessor;
            _filename = textEditableInterface.Filename;

            labelTitle.Text = _textEditableInterface.Title;
            linkLabelFilename.Text = _filename;

            LoadFile();
        }

        private void TextEditForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            if( editControl.Modified )
            {
                string query = String.Format("Save file {0} ?",new FileInfo(_filename).Name);
                DialogResult result = MessageBox.Show(query,this.Text,MessageBoxButtons.YesNoCancel);

                if( result == DialogResult.Yes )
                    SaveFile();

                else if( result == DialogResult.Cancel )
                    e.Cancel = true;
            }
        }

        public void ShowHelp()
        {
            textBoxResults.Text = _textEditableInterface.HelpText;
        }

        public void Compile()
        {
            string[] lines = new string[editControl.Lines.Count];

            for( int i = 0; i < lines.Length; i++ )
                lines[i] = editControl.Lines[i].Text;

            try
            {
                _textEditableInterface.Compile(lines,_preprocessor);
                textBoxResults.Text = Constants.CompilationSuccessfulMessage;
            }

            catch( Exception exception )
            {
                textBoxResults.Text = Constants.ComplationErrorMessage + exception.Message;
            }            
        }

        public bool IsOfType(TextEditableInterface otherTextEditableInterface)
        {
            return ( _textEditableInterface.GetType() == otherTextEditableInterface.GetType() );
        }

        public void CheckExternalFileEdit()
        {
            try
            {
                FileInfo fi = new FileInfo(_filename);

                if( fi.LastWriteTimeUtc > _modifiedDate )
                {
                    if( MessageBox.Show(String.Format("The file {0} has been modified in an external program. Reload it?",fi.Name),this.Text,MessageBoxButtons.YesNo) == DialogResult.Yes )
                        LoadFile();
                }
            }

            catch( Exception )
            {
            }
        }

        private void LoadFile()
        {
            try
            {
                editControl.Text = File.ReadAllText(_filename);
                editControl.SetSavePoint();
                _modifiedDate = new FileInfo(_filename).LastWriteTimeUtc;
            }

            catch( Exception exception )
            {
                MessageBox.Show("The file could not be read: " + exception.Message);
                _modifiedDate = DateTime.MinValue;
            }
        }

        public void SaveFile()
        {
            try
            {
                File.WriteAllText(_filename,editControl.Text,Encoding.UTF8);
                editControl.SetSavePoint();
                _modifiedDate = new FileInfo(_filename).LastWriteTimeUtc;
            }

            catch( Exception exception )
            {
                MessageBox.Show("The file could not be saved: " + exception.Message);
                _modifiedDate = DateTime.MinValue;
            }
        }

        private void linkLabelFilename_LinkClicked(object sender,LinkLabelLinkClickedEventArgs e)
        {
            // open the file in either Notepad++ or Notepad
            string notepadPlusPlusPath = @"Notepad++\notepad++.exe";

            string[] possibleEditorFilenames = new string[]
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),notepadPlusPlusPath),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),notepadPlusPlusPath),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows),"notepad.exe")
            };

            foreach( string editorFilename in possibleEditorFilenames )
            {
                if( File.Exists(editorFilename) )
                {
                    Process.Start(editorFilename,"\"" + _filename + "\"");
                    break;
                }
            }
        }
    }
}
