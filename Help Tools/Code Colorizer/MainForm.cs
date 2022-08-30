using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Colorizer;

namespace Code_Colorizer
{
    partial class MainForm : Form
    {
		private Processor _processor;
        private string _initialWindowTitle;
        private Processor.BufferType _bufferType = Processor.BufferType.Logic;
        private string _loadedFilename = null;

        public MainForm()
        {
            InitializeComponent();

            _initialWindowTitle = this.Text;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
				_processor = new Processor(this);
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
                {
                    LoadFile(argument);
                }
            }
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void notepadMenuItem_Click(object sender, EventArgs e)
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

                // logic
                words.Add(CSPro.Logic.Colorizer.GetReservedWords());

                // PFF
                words.Add(new SortedSet<string>(CSPro.Util.PFF.GetAppTypeWords()));
                words.Add(new SortedSet<string>(CSPro.Util.PFF.GetHeadingWords()));
                words.Add(new SortedSet<string>(CSPro.Util.PFF.GetAttributeWords()));

                string[] templateWords = new string[]
                {
                    "%template-logic%", "%template-pff1%", "%template-pff2%", "%template-pff3%"
                };

                // write out the file
                using( TextWriter tw = new StreamWriter(outFilename, false, Encoding.ASCII) )
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
                                sb.AppendFormat("{0}{1}", sb.Length > 0 ? " " : "", word);

                            // replace the template
                            outLine = String.Concat(outLine.Substring(0, startReplacementPos),
                                sb, outLine.Substring(startReplacementPos + templateWords[templateIndex].Length));
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

        private void languageMenuItem_Click(object sender, EventArgs e)
        {
            _bufferType = (Processor.BufferType)((ToolStripMenuItem)sender).Tag;
            RefreshUiElements();
        }

        private void RefreshUiElements()
        {
            logicMenuItem.Checked = ( _bufferType == Processor.BufferType.Logic );
            pffMenuItem.Checked = ( _bufferType == Processor.BufferType.Pff );
            messageMenuItem.Checked = ( _bufferType == Processor.BufferType.Message );
            reportMenuItem.Checked = ( _bufferType == Processor.BufferType.Report );

            buttonCopyUsersForum.Enabled = ( _bufferType != Processor.BufferType.Pff );
            buttonCopyUsersBlog.Enabled = ( _bufferType != Processor.BufferType.Pff );

            this.Text = String.Format("{0}{1} [{2}]", _loadedFilename == null ? "" : ( _loadedFilename + " - " ), _initialWindowTitle,
                ( _bufferType == Processor.BufferType.Logic )   ? "Logic File" :
                ( _bufferType == Processor.BufferType.Pff )     ? "PFF File" :
                ( _bufferType == Processor.BufferType.Message ) ? "Message File" :
                                                                  "Report (HTML) File");

            // refresh the current coloring
            editControl_TextChanged(null, null);
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSPro Logic, PFF, Message, and Report Files (*.apc;*.app;*.pff;*.mgf;*.html)|*.apc;*.app;*.pff;*.mgf;*.html|All files (*.*)|*.*";
				 
            if( ofd.ShowDialog() == DialogResult.OK )
                LoadFile(ofd.FileName);
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
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

                string extension = Path.GetExtension(filename);
                _bufferType = extension.Equals(".pff", StringComparison.InvariantCultureIgnoreCase)  ? Processor.BufferType.Pff :
                              extension.Equals(".mgf", StringComparison.InvariantCultureIgnoreCase)  ? Processor.BufferType.Message :
                              extension.Equals(".html", StringComparison.InvariantCultureIgnoreCase) ? Processor.BufferType.Report :
                              extension.Equals(".htm", StringComparison.InvariantCultureIgnoreCase)  ? Processor.BufferType.Report :
                                                                                                       Processor.BufferType.Logic;
                RefreshUiElements();
            }

            catch( Exception exception )
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void editControl_TextChanged(object sender, EventArgs e)
        {
            webBrowser.DocumentText = _processor.GetHtml(editControl.Text, _bufferType);
        }

        private void buttonCopyHtml_Click(object sender, EventArgs e)
        {
			_processor.CopyHtml(editControl.Text, _bufferType);
        }

        private void buttonCopyUsersForum_Click(object sender, EventArgs e)
        {
			_processor.CopyTextForCSProUsersForum(editControl.Text, _bufferType);
        }

        private void buttonCopyUsersBlog_Click(object sender, EventArgs e)
        {
            _processor.CopyTextForCSProUsersBlog(editControl.Text, _bufferType);
        }

        private void logicVersionMenuItem_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = true;

            if( sender == logicVersion0MenuItem )
            {
                _processor.SetLogicVersion(CSPro.Logic.LogicVersion.V0);
                logicVersion8_0MenuItem.Checked = false;
            }
            
            else
            {
                _processor.SetLogicVersion(CSPro.Logic.LogicVersion.V8_0);
                logicVersion0MenuItem.Checked = false;
            }

            RefreshUiElements();
        }
    }
}
