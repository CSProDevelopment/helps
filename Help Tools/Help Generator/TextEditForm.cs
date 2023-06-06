using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Colorizer;

namespace Help_Generator
{
    partial class TextEditForm : Form, TextModifiedHandlerInterface
    {
        private TextEditableInterface _textEditableInterface;
        private HelpComponents _helpComponents;
        public string Filename { get; private set; }
        private bool _modified;
        private string _baseWindowTitle;
        private DateTime _modifiedDate;

        private TextEditFormTopicCompilerSettings _topicCompilerSettings;
        private TopicViewerForm _topicViewerForm;

        public TextEditForm(TextEditableInterface textEditableInterface,HelpComponents helpComponents)
        {
            InitializeComponent();

            _textEditableInterface = textEditableInterface;
            _helpComponents = helpComponents;
            Filename = textEditableInterface.Filename;

            _modified = false;
            _baseWindowTitle = this.Text;
            UpdateWindowTitle();

            editControl.SetTextModifiedHandler(this);

            linkLabelFilename.Text = Filename;

            if( _textEditableInterface is Topic )
                _topicCompilerSettings = new TextEditFormTopicCompilerSettings();

            LoadFile();
        }

        private void TextEditForm_Shown(object sender, EventArgs e)
        {
            // resize the form to fill the 90% of the vertical space
            int newHeight = (int)( Parent.Height * 0.90 ) - Location.Y;

            if( newHeight > 0 )
                Size = new Size(Size.Width, newHeight);
        }

        private void TextEditForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            if( _modified && ( QueryAndSave() == DialogResult.Cancel ) )
                e.Cancel = true;

            if( _topicViewerForm != null && !_topicViewerForm.IsDisposed )
                _topicViewerForm.Close();
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
                if( _textEditableInterface is Topic )
                {
                    _topicCompilerSettings.HelpGeneratorMainForm = ((MainForm)this.ParentForm);

                    string html = ((Topic)_textEditableInterface).CompileForHtml(this, editControl.Text,_helpComponents,_topicCompilerSettings);
                    UpdateWindowTitle();

                    // update the HTML view
                    if( _topicViewerForm == null || _topicViewerForm.IsDisposed )
                    {
                        _topicViewerForm = new TopicViewerForm(_helpComponents.preprocessor);

                        // position the new window to the right of the text editor (with some spacing)
                        const int SpacingGap = 15;
                        const int MinWidth = 100;
                        int thisRightMostPosition = this.Location.X + this.Width;
                        _topicViewerForm.Location = new Point(thisRightMostPosition + SpacingGap,this.Location.Y);
                        _topicViewerForm.Size = new Size(Math.Max(MinWidth,this.ParentForm.Width - thisRightMostPosition - 4 * SpacingGap),this.Height);

                        _topicViewerForm.MdiParent = this.ParentForm;
                        _topicViewerForm.Show();
                    }

                    _topicViewerForm.UpdateContents(GetTitle(), FixImgPaths(html));
                }

                else
                    _textEditableInterface.Compile(GetLinesArray(),_helpComponents.preprocessor);

                textBoxResults.Text = Constants.CompilationSuccessfulMessage;
            }

            catch( Exception exception )
            {
                textBoxResults.Text = Constants.ComplationErrorMessage + exception.Message;
            }
        }

        // WebBrowser control requires absolute file:/// paths for images
        // Relative paths do not work. For the special help generator <image> tag
        // we generate absolute paths but for regular <img> tags inside <html> tags
        // we need to fix this or the images don't show up in preview. The relative
        // paths work fine in the generated html, chm and pdf.
        private string FixImgPaths(string html)
        {
            var imgRegex = new Regex(@"<img\s+src\s*=\s*""(?!file:///)[^\"">]*""[^>]*>");
            var srcRegex = new Regex(@"(src="")((?!file :///)[^\""]*"")");
            string imagesPath = Path.Combine(_helpComponents.projectPath, Constants.ImagesDirectoryName);
            var srcReplacement = @"$1file:///" + imagesPath + @"\$2";
            return imgRegex.Replace(html, m => srcRegex.Replace(m.ToString(), srcReplacement));
        }

        public void Format()
        {
            try
            {
                editControl.Text = _textEditableInterface.Format(GetLinesArray(),_helpComponents.preprocessor);
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

        public Preprocessor.TopicPreprocessor PreprocessedTopic { get { return ( _textEditableInterface is Topic ) ? ((Topic)_textEditableInterface).PreprocessedTopic : null; } }

        public void CheckExternalFileEdit()
        {
            try
            {
                FileInfo fi = new FileInfo(Filename);

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

        private string GetTitle()
        {
            return ( _topicCompilerSettings != null && _topicCompilerSettings.Title != null ) ? _topicCompilerSettings.Title : _textEditableInterface.Title;
        }

        private void UpdateWindowTitle()
        {
            string title = GetTitle();
            labelTitle.Text = title;
            this.Text = String.Format("{0}{1} - {2}",_modified ? "*" : "",title,_baseWindowTitle);
        }

        public void SetTextModified(bool modified)
        {
            _modified = modified;
            UpdateWindowTitle();
        }

        private DialogResult QueryAndSave()
        {
            string query = String.Format("Save file {0} ?",new FileInfo(Filename).Name);
            DialogResult result = MessageBox.Show(query,this.Text,MessageBoxButtons.YesNoCancel);

            if( result == DialogResult.Yes )
                SaveFile();

            return result;
        }

        private void LoadFile()
        {
            try
            {
                editControl.Text = File.ReadAllText(Filename);
                editControl.EmptyUndoBuffer();
                editControl.SetSavePoint();
                _modifiedDate = new FileInfo(Filename).LastWriteTimeUtc;
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
                File.WriteAllText(Filename,editControl.Text,Encoding.UTF8);
                editControl.SetSavePoint();
                _modifiedDate = new FileInfo(Filename).LastWriteTimeUtc;
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
            const string notepadPlusPlusPath = @"Notepad++\notepad++.exe";

            string[] possibleEditorFilenames = new string[]
            {
                Path.Combine(HelperFunctions.GetProgramFilesDirectory(HelperFunctions.ProgramFilesDirectory.x64), notepadPlusPlusPath),
                Path.Combine(HelperFunctions.GetProgramFilesDirectory(HelperFunctions.ProgramFilesDirectory.x86), notepadPlusPlusPath),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "notepad.exe")
            };

            foreach( string editorFilename in possibleEditorFilenames )
            {
                if( File.Exists(editorFilename) )
                {
                    Process.Start(editorFilename,"\"" + Filename + "\"");
                    break;
                }
            }
        }

        private void linkLabelOpenContainingFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", $"/select, \"{Filename}\"");
        }

        private void linkLabelCopyFullPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(Filename);
        }

        private void linkLabelCopyFilename_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(Path.GetFileName(Filename));
        }

        public void ProcessEditCommand(string command)
        {
            if( command == "bold" )
                InsertTags("<b>","</b>");

            else if( command == "italics" )
                InsertTags("<i>","</i>");

            else if( command == "font" )
                InsertTags("<font ...>","</font>");

            else if( command == "subheader" )
                InsertTags("<subheader>","</subheader>");

            else if( command == "center" )
                InsertTags("<center>","</center>");

            else if( command == "link" )
				InsertLink();

            else if( command == "list" )
                InsertTags("<list>\n\t<li>","</li>\n</list>");

            else if( command == "table" )
                InsertTags("<table 2>\n\t<cell>","</cell><cell> </cell>\n</table>");

            else if( command == "logiccolor" )
                InsertTags("<logiccolor>","</logiccolor>");

            else if( command == "syntaxargument" )
                InsertTags("<arg>","</arg>");

            else if( command == "pffcolor" )
                InsertTags("<pffcolor>","</pffcolor>");

			else if( command == "listbuilder" )
				ListBuilder();

            else
                throw new NotImplementedException();
        }

        private void InsertTags(string startTag,string endTag)
        {
            int selectionStart = editControl.SelectionStart;
            int selectionEnd = editControl.SelectionEnd;

            editControl.BeginUndoAction();
            editControl.InsertText(selectionEnd,endTag);
            editControl.InsertText(selectionStart,startTag);
            editControl.EndUndoAction();

            editControl.SelectionStart = selectionStart + startTag.Length;
            editControl.SelectionEnd = selectionEnd + startTag.Length;
        }

		private void InsertLink()
        {
            string link = "";

            // if a topic's name is on the clipboard, automatically add it
            if( Clipboard.ContainsText() )
            {
                string clipboard_text = Clipboard.GetText().Trim().ToLower();

                foreach( string extension in new string[] { "", Constants.TopicExtension } )
                {
                    try
                    {
                        string full_text = clipboard_text + extension;
                        _helpComponents.preprocessor.GetTopic(full_text);
                        link = " " + full_text;
                        break;
                    }

                    catch( Exception )
                    {
                    }
                }
            }               
                
            InsertTags("<link" + link + ">","</link>");
        }

		private void ListBuilder()
		{
			string text = editControl.SelectedText + '\r';

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<list>");

            int lastPos = 0;
            int newlinePos = 0;

            while( ( newlinePos = text.IndexOf('\r',lastPos) ) >= 0 )
            {
                string line = text.Substring(lastPos,newlinePos - lastPos).Trim();

                if( !String.IsNullOrWhiteSpace(line) )
                    sb.AppendLine("\t<li>" + line + "</li>");

                lastPos = newlinePos + 1;
            }

            sb.AppendLine("</list>");

            editControl.BeginUndoAction();
            editControl.ReplaceSelection(sb.ToString());
            editControl.EndUndoAction();
		}
    }
}
