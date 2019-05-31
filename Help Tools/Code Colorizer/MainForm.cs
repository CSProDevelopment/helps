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

        private void MainForm_Load(object sender,EventArgs e)
        {
            try
            {
				_processor = new Processor();
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

                // logic
                words.Add(CSPro.Logic.Colorizer.GetReservedWords());

                // PFF
                HighlightWordReader hwr = new HighlightWordReader(HighlightWordReader.PffFilename);
                words.Add(hwr.ReadWordBlock());
                words.Add(hwr.ReadWordBlock());
                words.Add(hwr.ReadWordBlock());

                string[] templateWords = new string[]
                {
                    "%template-logic%", "%template-pff1%", "%template-pff2%", "%template-pff3%"
                };

                // write out the file
                using( TextWriter tw = new StreamWriter(outFilename,false,Encoding.ASCII) )
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
            _bufferType = (Processor.BufferType)((ToolStripMenuItem)sender).Tag;
            RefreshUiElements();
        }

        private void RefreshUiElements()
        {
            logicMenuItem.Checked = ( _bufferType == Processor.BufferType.Logic );
            pffMenuItem.Checked = ( _bufferType == Processor.BufferType.Pff );
            messageMenuItem.Checked = ( _bufferType == Processor.BufferType.Message );
            buttonCopyUsersForum.Enabled = ( _bufferType == Processor.BufferType.Logic );
            buttonCopyUsersBlog.Enabled = ( _bufferType == Processor.BufferType.Logic );

            this.Text = String.Format("{0}{1} [{2}]", _loadedFilename == null ? "" : ( _loadedFilename + " - " ), _initialWindowTitle,
                ( _bufferType == Processor.BufferType.Logic ) ? "Logic File" :
                ( _bufferType == Processor.BufferType.Pff )   ? "PFF File" :
                                                                "Message File");
        }

        private void openMenuItem_Click(object sender,EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSPro Logic, PFF, and Message Files (*.apc;*.app;*.pff;*.mgf)|*.apc;*.app;*.pff;*.mgf|All files (*.*)|*.*";
				 
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
                _bufferType = Path.GetExtension(filename).Equals(".pff", StringComparison.InvariantCultureIgnoreCase) ? Processor.BufferType.Pff :
                              Path.GetExtension(filename).Equals(".mgf", StringComparison.InvariantCultureIgnoreCase) ? Processor.BufferType.Message :
                                                                                                                        Processor.BufferType.Logic;
                RefreshUiElements();
            }

            catch( Exception exception )
            {
                MessageBox.Show(exception.Message);
            }
        }


        private void buttonCopyHtml_Click(object sender,EventArgs e)
        {
			_processor.HtmlProcessor(editControl.Text, _bufferType);
        }

        private void buttonCopyUsersForum_Click(object sender,EventArgs e)
        {
			_processor.UsersProcessor(CSPro.Logic.Colorizer.Format.LogicToCSProUsersForum, editControl.Text);
        }

        private void buttonCopyUsersBlog_Click(object sender, EventArgs e)
        {
            _processor.UsersProcessor(CSPro.Logic.Colorizer.Format.LogicToCSProUsersBlog, editControl.Text);
        }
    }
}
