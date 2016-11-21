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

        public MainForm()
        {
            InitializeComponent();
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

            Array commandArgs = Environment.GetCommandLineArgs();

            if( commandArgs.Length >= 2 )
            {
                string argument = (string)commandArgs.GetValue(1);

                // if the argument Notepad++ is passed, generate the colorization file and then close
                if( argument.Equals("Notepad++",StringComparison.InvariantCultureIgnoreCase) )
                {
                    GenerateNotepadFile(true);
                    Close();
                    return;
                }
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
                string templateFilename = ReferenceFile.Locate("userDefineLang-template.xml");
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

        private void openMenuItem_Click(object sender,EventArgs e)
        {
            MessageBox.Show("TODO: openMenuItem_Click");
        }

    }
}
