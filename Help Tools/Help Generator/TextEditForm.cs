using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Colorizer;

namespace Help_Generator
{
    partial class TextEditForm : Form, TextModifiedHandlerInterface
    {
        private TextEditableInterface _textEditableInterface;
        private Preprocessor _preprocessor;
        private string _filename;
        private bool _modified;
        private string _baseWindowTitle;
        private DateTime _modifiedDate;

        public TextEditForm(TextEditableInterface textEditableInterface,Preprocessor preprocessor)
        {
            InitializeComponent();

            _textEditableInterface = textEditableInterface;
            _preprocessor = preprocessor;
            _filename = textEditableInterface.Filename;

            _modified = false;
            _baseWindowTitle = String.Format("{0} - {1}",_textEditableInterface.Title,this.Text);
            editControl.SetTextModifiedHandler(this);

            labelTitle.Text = _textEditableInterface.Title;
            linkLabelFilename.Text = _filename;

            LoadFile();
        }

        private void TextEditForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            if( _modified && ( QueryAndSave() == DialogResult.Cancel ) )
                e.Cancel = true;
        }

        public void ShowHelp()
        {
            textBoxResults.Text = _textEditableInterface.HelpText;
        }

        private string[] GetLinesArray()
        {
            string[] lines = new string[editControl.Lines.Count];

            for( int i = 0; i < lines.Length; i++ )
                lines[i] = editControl.Lines[i].Text;

            return lines;
        }

        public void Compile()
        {
            try
            {
                _textEditableInterface.Compile(GetLinesArray(),_preprocessor);
                textBoxResults.Text = Constants.CompilationSuccessfulMessage;
            }

            catch( Exception exception )
            {
                textBoxResults.Text = Constants.ComplationErrorMessage + exception.Message;
            }
        }

        public void Format()
        {
            try
            {
                editControl.Text = _textEditableInterface.Format(GetLinesArray(),_preprocessor);
                textBoxResults.Text = Constants.FormattedSuccessfulMessage;
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

        public bool IsFileReady()
        {
            return ( !_modified || ( QueryAndSave() == DialogResult.Yes ) );
        }

        public void SetTextModified(bool modified)
        {
            _modified = modified;
            this.Text = ( modified ? "*" : "" ) + _baseWindowTitle;
        }

        private DialogResult QueryAndSave()
        {
            string query = String.Format("Save file {0} ?",new FileInfo(_filename).Name);
            DialogResult result = MessageBox.Show(query,this.Text,MessageBoxButtons.YesNoCancel);

            if( result == DialogResult.Yes )
                SaveFile();

            return result;
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
