using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Colorizer;

namespace Code_Colorizer
{
    public partial class MainForm : Form
    {
        LogicColorizer _logicColorizer;
        PffColorizer _pffColorizer;
        string _initialWindowTitle;
        bool _formattingLogic = true;
        string _loadedFilename = null;

        public MainForm()
        {
            InitializeComponent();

            editControl.StyleResetDefault();
            editControl.Styles[ScintillaNET.Style.Default].Font = "Courier New";
            editControl.Styles[ScintillaNET.Style.Default].Size = 10;
            editControl.StyleClearAll();

            _initialWindowTitle = this.Text;
        }

        private void MainForm_Load(object sender,EventArgs e)
        {
            try
            {
                _logicColorizer = new LogicColorizer();
                _pffColorizer = new PffColorizer();
            }

            catch( Exception exception )
            {
                MessageBox.Show(exception.Message);
                Close();
                return;
            }

            RefreshUiElements();

            Array commandArgs = Environment.GetCommandLineArgs();

            if( commandArgs.Length >= 2 )
            {
                string argument = (string)commandArgs.GetValue(1);

                // if the argument Notepad++ is passed, generate the colorization file and then close
                if( argument.Equals("Notepad++",StringComparison.InvariantCultureIgnoreCase) )
                {
                    GenerateNotepadFile(true);
                    Close();
                }

                else
                    LoadFile(argument);
            }
        }

        private void exitMenuItem_Click(object sender,EventArgs e)
        {
            Close();
        }

        private void notepadMenuItem_Click(object sender,EventArgs e)
        {
            GenerateNotepadFile(false);
        }

        private void GenerateNotepadFile(bool silent)
        {
            try
            {
                string templateFilename = HelperFunctions.ReferenceFileLocate("userDefineLang-template.xml");
                string outFilename = Path.Combine(Path.GetDirectoryName(templateFilename),"userDefineLang.xml");

                // read in the template file
                string[] fileLines = File.ReadAllLines(templateFilename);

                // read in the words
                List<SortedSet<string>> words = new List<SortedSet<string>>();

                HighlightWordReader hwr = new HighlightWordReader(HighlightWordReader.LogicFilename);
                words.Add(hwr.ReadWordBlock(false));

                hwr = new HighlightWordReader(HighlightWordReader.PffFilename);
                words.Add(hwr.ReadWordBlock(true));
                words.Add(hwr.ReadWordBlock(true));
                words.Add(hwr.ReadWordBlock(true));

                string[] templateWords = new string[]
                {
                    "%template-logic%", "%template-pff1%", "%template-pff2%", "%template-pff3%"
                };

                // write out the file
                using( TextWriter tw = new StreamWriter(outFilename,false,Encoding.UTF8) )
                {
                    foreach( string fileLine in fileLines )
                    {
                        string outLine = fileLine;

                        // see if we need to replace a chunk
                        int templateIndex = 0;
                        int startReplacementPos = 0;

                        foreach( string template in templateWords )
                        {
                            startReplacementPos = fileLine.IndexOf(template);

                            if( startReplacementPos >= 0 )
                                break;

                            templateIndex++;
                        }

                        if( templateIndex < templateWords.Length )
                        {
                            // generate the word list
                            StringBuilder sb = new StringBuilder();

                            foreach( string word in words[templateIndex] )
                                sb.AppendFormat("{0}{1}",sb.Length > 0 ? " " : "",word);

                            // replace the template
                            outLine = String.Concat(outLine.Substring(0,startReplacementPos),
                                sb,outLine.Substring(startReplacementPos + templateWords[templateIndex].Length));
                        }

                        tw.WriteLine(outLine);
                    }
                }

                if( !silent )
                    MessageBox.Show("Successfully wrote the Notepad++ colorization file to:\n\n" + outFilename);
            }

            catch( Exception exception )
            {
                MessageBox.Show(exception.Message);
            }
        }


        private void languageMenuItem_Click(object sender,EventArgs e)
        {
            _formattingLogic = !_formattingLogic;
            RefreshUiElements();
        }

        private void RefreshUiElements()
        {
            logicMenuItem.Checked = _formattingLogic;
            pffMenuItem.Checked = !_formattingLogic;
            buttonCopyUsersForum.Enabled = _formattingLogic;

            this.Text = String.Format("{0}{1} [{2}]",_loadedFilename == null ? "" : ( _loadedFilename + " - " ),_initialWindowTitle,
                _formattingLogic ? "CSPro Logic File" : "PFF File");
        }

        private void openMenuItem_Click(object sender,EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSPro Logic and PFF Files (*.apc;*.app;*.pff)|*.apc;*.app;*.pff|All files (*.*)|*.*";
				 
            if( ofd.ShowDialog() == DialogResult.OK )
                LoadFile(ofd.FileName);
        }

        private void MainForm_DragEnter(object sender,DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void MainForm_DragDrop(object sender,DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            LoadFile(files[0]);
        }

        private void LoadFile(string filename)
        {
            try
            {
                editControl.Text = File.ReadAllText(filename);

                _loadedFilename = Path.GetFileName(filename);
                _formattingLogic = !Path.GetExtension(filename).Equals(".pff",StringComparison.InvariantCultureIgnoreCase);

                RefreshUiElements();
            }

            catch( Exception exception )
            {
                MessageBox.Show(exception.Message);
            }
        }


        private void buttonCopyHtml_Click(object sender,EventArgs e)
        {
            string text = HelperFunctions.TrimTrailingSpace(editControl.Text);
            string formattedText = _formattingLogic ?
                _logicColorizer.Colorize(new LogicColorizerHtml(),text) : _pffColorizer.Colorize(new PffColorizerHtml(),text);

            // html to clipboard code from: http://blogs.msdn.com/b/jmstall/archive/2007/01/21/sample-code-html-clipboard.aspx
            string htmlCopyText = "Format:HTML Format Version:1.0\nStartHTML:<<<<<<<1\nEndHTML:<<<<<<<2\nStartFragment:<<<<<<<3\nEndFragment:<<<<<<<4\n";
            int startHTML = htmlCopyText.Length;

            int beginChunkPos = formattedText.IndexOf("<div");
            int endChunkPos = formattedText.IndexOf("</div>") + 6; // 6 is length if </div>

            htmlCopyText += formattedText.Substring(0,beginChunkPos) + "<!--StartFragment-->";
            int startFragment = htmlCopyText.Length;
				 
            htmlCopyText += formattedText.Substring(beginChunkPos,endChunkPos - beginChunkPos);
            int endFragment = htmlCopyText.Length;

            htmlCopyText += "<!--EndFragment-->" + formattedText.Substring(endChunkPos);
            int endHTML = htmlCopyText.Length;

            htmlCopyText = htmlCopyText.Replace("<<<<<<<1",String.Format("{0,8}",startHTML));
            htmlCopyText = htmlCopyText.Replace("<<<<<<<2",String.Format("{0,8}",endHTML));
            htmlCopyText = htmlCopyText.Replace("<<<<<<<3",String.Format("{0,8}",startFragment));
            htmlCopyText = htmlCopyText.Replace("<<<<<<<4",String.Format("{0,8}",endFragment));

            Clipboard.Clear();
            Clipboard.SetText(htmlCopyText,TextDataFormat.Html);
        }

        private void buttonCopyUsersForum_Click(object sender,EventArgs e)
        {
            string text = HelperFunctions.TrimTrailingSpace(editControl.Text);
            string formattedText = _logicColorizer.Colorize(new LogicColorizerUsersForum(),text);

            Clipboard.Clear();
            Clipboard.SetText(formattedText,TextDataFormat.UnicodeText);
        }
    }
}
