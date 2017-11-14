using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Help_Generator
{
    partial class GenerateHelpsForm : Form
    {
        public enum GenerationType { Generate, GenerateAndClose, GenerateChmAndClose, CompileOnly };

        private HelpComponents _helpComponents;
        private GenerationType _generationType;

        private string _projectName;
        private string _outputChmFilename;
        private string _temporaryFilesPath;

        private Dictionary<Preprocessor.TopicPreprocessor,string> _outputTopicFilenames;
        private GenerateChmTopicCompilerSettings _chmTopicCompilerSettings;

        private string _outputWebsitePath;
        private GenerateWebsiteTopicCompilerSettings _websiteTopicCompilerSettings;

        private string _outputPdfFilename;
        private string _outputPdfTopicsFilename;
        private GeneratePdfTopicCompilerSettings _pdfTopicCompilerSettings;

        private BackgroundWorker _backgroundThread;

		private bool DoGenerateChm { get { return ( _generationType != GenerationType.CompileOnly ); } }
		private bool DoGenerateWebsite { get { return ( _generationType != GenerationType.CompileOnly ) && ( _generationType != GenerationType.GenerateChmAndClose ); } }
		private bool DoGeneratePdf { get { return _helpComponents.settings.CreatePdf && ( _generationType != GenerationType.CompileOnly ) && ( _generationType != GenerationType.GenerateChmAndClose ); } }

        public GenerateHelpsForm(HelpComponents helpComponents,GenerationType generationType)
        {
            InitializeComponent();

            _helpComponents = helpComponents;
            _generationType = generationType;

            _projectName = new DirectoryInfo(_helpComponents.projectPath).Name;

            string outputPath = Path.GetFullPath(Path.Combine(_helpComponents.projectPath,"..",Constants.OutputsDirectoryName));

            _temporaryFilesPath = Path.Combine(_helpComponents.projectPath,Constants.TemporaryFileDirectoryName);
            Directory.CreateDirectory(_temporaryFilesPath);

            _backgroundThread = new BackgroundWorker();

            // for the CHM
            string outputChmPath = Path.Combine(outputPath,Constants.OutputsChmDirectoryName);
            Directory.CreateDirectory(outputChmPath);

            _outputChmFilename = Path.Combine(outputChmPath,_projectName + Constants.ChmFileExtension);
            File.Delete(_outputChmFilename);

            _outputTopicFilenames = new Dictionary<Preprocessor.TopicPreprocessor,string>();
            _chmTopicCompilerSettings = new GenerateChmTopicCompilerSettings();

            // for the website
            _outputWebsitePath = Path.Combine(outputPath,Constants.OutputsWebsiteDirectoryName,_projectName);

            if( Directory.Exists(_outputWebsitePath) )
                Directory.Delete(_outputWebsitePath,true);

            Directory.CreateDirectory(_outputWebsitePath);

            _websiteTopicCompilerSettings = new GenerateWebsiteTopicCompilerSettings(_helpComponents);

            // for the PDF
            string outputPdfPath = Path.Combine(outputPath,Constants.OutputsPdfDirectoryName);
            Directory.CreateDirectory(outputPdfPath);

            _outputPdfFilename = Path.Combine(outputPdfPath,_projectName + ".pdf");
            File.Delete(_outputPdfFilename);

            _outputPdfTopicsFilename = Path.Combine(_temporaryFilesPath,"_output_pdf_topics.html");

            _pdfTopicCompilerSettings = new GeneratePdfTopicCompilerSettings();
        }

        private enum ThreadUpdateMessage { Cancel, Complete, SettingsComplete, TableOfContentsComplete, IndexComplete, TopicsProgress, TopicsComplete, WebsiteComplete, PdfComplete, ChmComplete, HideProgressBar };

        private void GenerateHelpsForm_Load(object sender,EventArgs e)
        {
            _backgroundThread.WorkerReportsProgress = true;
            _backgroundThread.WorkerSupportsCancellation = true;

            _backgroundThread.DoWork += new DoWorkEventHandler(
                delegate(object o,DoWorkEventArgs args)
                {
                    GenerateHelps();
                });

            _backgroundThread.ProgressChanged += new ProgressChangedEventHandler(
                delegate(object o,ProgressChangedEventArgs args)
                {
                    if( args.UserState is ThreadUpdateMessage )
                    {
                        ThreadUpdateMessage message = (ThreadUpdateMessage)args.UserState;

                        switch( message )
                        {
                            case ThreadUpdateMessage.Cancel:
                                _backgroundThread = null;
                                Close();
                                break;

                            case ThreadUpdateMessage.SettingsComplete:
                                pictureBoxCheckmarkSettings.Visible = true;
                                break;

                            case ThreadUpdateMessage.TableOfContentsComplete:
                                pictureBoxCheckmarkTableOfContents.Visible = true;
                                break;

                            case ThreadUpdateMessage.IndexComplete:
                                pictureBoxCheckmarkIndex.Visible = true;
                                progressBarTopics.Visible = true;
                                break;

                            case ThreadUpdateMessage.TopicsProgress:
                                if( args.ProgressPercentage == 0 )
                                    progressBarTopics.PerformStep();

                                else
                                    progressBarTopics.Maximum = args.ProgressPercentage;

                                break;

                            case ThreadUpdateMessage.TopicsComplete:
                                progressBarTopics.Visible = false;
                                pictureBoxCheckmarkTopics.Visible = true;
                                break;

                            case ThreadUpdateMessage.WebsiteComplete:
                                pictureBoxCheckmarkWebsite.Visible = true;
                                buttonOpenWebsite.Enabled = true;
                                break;

                            case ThreadUpdateMessage.PdfComplete:
                                pictureBoxCheckmarkPdf.Visible = true;
                                buttonOpenPdf.Enabled = _helpComponents.settings.CreatePdf;
                                break;

                            case ThreadUpdateMessage.ChmComplete:
                                pictureBoxCheckmarkChm.Visible = true;
                                buttonOpenChm.Enabled = true;
                                break;

                            case ThreadUpdateMessage.HideProgressBar:
                                progressBarTopics.Visible = false;
                                break;

                            case ThreadUpdateMessage.Complete:
                                buttonCancelClose.Text = "Close";
                                _backgroundThread = null;

                                if( ( _generationType == GenerationType.GenerateAndClose ) || ( _generationType == GenerationType.GenerateChmAndClose ) )
                                    Close();

                                break;
                        }
                    }

                    else if( args.UserState is String )
                        textBoxLog.AppendText((string)args.UserState + "\r\n\r\n");

                    else if( args.UserState is DataReceivedEventArgs )
                        textBoxLog.AppendText(String.Format("HTML Help Compiler: {0}\r\n",(string)((DataReceivedEventArgs)args.UserState).Data));
                });

            _backgroundThread.RunWorkerAsync();
        }

        private void GenerateHelps()
        {
            int processingStep = 0;
            string[] stepStrings = new string[] { "Settings", "Table of Contents", "Index", "Topics", "Website", "PDF", "Microsoft HTML Help" };

            try
            {
                for( ; processingStep < stepStrings.Length && !_backgroundThread.CancellationPending; processingStep++ )
                {
                    if( ( ( processingStep == 4 ) && !DoGenerateWebsite ) ||
						( ( processingStep == 5 ) && !DoGeneratePdf ) ||
						( ( processingStep == 6 ) && !DoGenerateChm ) )
                        continue;

                    _backgroundThread.ReportProgress(0,String.Format("Processing {0}...",stepStrings[processingStep]));

                    if( processingStep == 0 )
                    {
                        _helpComponents.settings.Compile(_helpComponents.preprocessor);
                        _backgroundThread.ReportProgress(0,ThreadUpdateMessage.SettingsComplete);
                    }

                    else if( processingStep == 1 )
                    {
                        _helpComponents.tableOfContents.Compile(_helpComponents.preprocessor);
                        _backgroundThread.ReportProgress(0,ThreadUpdateMessage.TableOfContentsComplete);
                    }

                    else if( processingStep == 2 )
                    {
                        _helpComponents.index.Compile(_helpComponents.preprocessor);
                        _backgroundThread.ReportProgress(0,ThreadUpdateMessage.IndexComplete);
                    }

                    else if( processingStep == 3 )
                    {
                        ProcessTopics();

                        if( !_backgroundThread.CancellationPending )
                            _backgroundThread.ReportProgress(0,ThreadUpdateMessage.TopicsComplete);

                        ListUnusedImages();
                    }

                    else if( processingStep == 4 )
                    {
                        GenerateWebsite();
                        _backgroundThread.ReportProgress(0,ThreadUpdateMessage.WebsiteComplete);
                    }

                    else if( processingStep == 5 )
                    {
                        GeneratePdf();
                        _backgroundThread.ReportProgress(0,ThreadUpdateMessage.PdfComplete);
                    }

                    else if( processingStep == 6 )
                    {
                        GenerateChm();
                        _backgroundThread.ReportProgress(0,ThreadUpdateMessage.ChmComplete);
                    }

                    _backgroundThread.ReportProgress(0,"Successfully processed " + stepStrings[processingStep]);
                }

                if( _backgroundThread.CancellationPending )
                    _backgroundThread.ReportProgress(0,ThreadUpdateMessage.Cancel);

                else
                    _backgroundThread.ReportProgress(0,ThreadUpdateMessage.Complete);
            }

            catch( Exception exception )
            {
                _backgroundThread.ReportProgress(0,ThreadUpdateMessage.HideProgressBar);
                _backgroundThread.ReportProgress(0,String.Format("Help Generation Error ({0}):\r\n\r\n{1}",stepStrings[processingStep],exception.Message));
            }

            _backgroundThread = null;
        }

        private void buttonOpenWebsite_Click(object sender,EventArgs e)
        {
            string defaultTopicFilename = _websiteTopicCompilerSettings.GetHtmlFilename(_helpComponents.settings.DefaultTopic);
            Process.Start(Path.Combine(_outputWebsitePath,defaultTopicFilename));
        }

        private void buttonOpenPdf_Click(object sender,EventArgs e)
        {
            Process.Start(_outputPdfFilename);
        }

        private void buttonOpenChm_Click(object sender,EventArgs e)
        {
            Help.ShowHelp(null,_outputChmFilename);
        }

        private void GenerateHelpsForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            if( _backgroundThread != null && _backgroundThread.IsBusy )
            {
                if( MessageBox.Show("Are you sure that you want to cancel the operation?",this.Text,MessageBoxButtons.YesNo) == DialogResult.Yes )
                    _backgroundThread.CancelAsync();

                e.Cancel = true;
            }

            else if( checkBoxDeleteTemporaryFiles.Checked )
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(_temporaryFilesPath);

                    foreach( FileInfo fi in di.GetFiles() )
                        fi.Delete();

                    di.Delete();
                }

                catch( Exception exception )
                {
                    MessageBox.Show("There was an error deleting the temporary files: " + exception.Message);
                }
            }
        }

        private void ProcessTopics()
        {
            using( TextWriter twPdf = ( DoGeneratePdf ? new StreamWriter(_outputPdfTopicsFilename,false,Encoding.UTF8) : null ) )
            {
                if( twPdf != null )
                    twPdf.WriteLine(PdfHtmlHeader);

                HashSet<Preprocessor.TopicPreprocessor> allTopics = _helpComponents.preprocessor.GetAllTopics();
                _backgroundThread.ReportProgress(allTopics.Count,ThreadUpdateMessage.TopicsProgress);

                // remove any shared topics that aren't in the table of contents or index
                HashSet<Preprocessor.TopicPreprocessor> unusedSharedTopics = new HashSet<Preprocessor.TopicPreprocessor>(allTopics);
                _helpComponents.tableOfContents.RemoveUsedTopics(unusedSharedTopics);
                _helpComponents.index.RemoveUsedTopics(unusedSharedTopics);

                // process the topics in table of contents order (which is necessary if generating a PDF)
                List<TopicListParser.TopicListNode> tocTopics = _helpComponents.tableOfContents.GetOrderedTopicsForPdf();
                int tocTopicsIndex = 0;

                while( allTopics.Count > 0 || tocTopicsIndex < tocTopics.Count )
                {
                    // get to the next topic
                    bool processedChapter = false;

                    while( tocTopicsIndex < tocTopics.Count && tocTopics[tocTopicsIndex].Topic == null )
                    {
                        // write the chapter heading for the PDF
                        if( twPdf != null )
                        {
                            twPdf.WriteLine(String.Format("<h1{0}>{1}</h1>",processedChapter ?
                                " style=\"page-break-before: avoid;\"" : "",tocTopics[tocTopicsIndex].Title));
                            processedChapter = true;
                        }

                        tocTopicsIndex++;
                    }

                    Preprocessor.TopicPreprocessor preprocessedTopic;

                    if( tocTopicsIndex < tocTopics.Count )
                    {
                        preprocessedTopic = tocTopics[tocTopicsIndex].Topic;
                        tocTopicsIndex++;
                    }

                    else if( allTopics.Count > 0 )
                        preprocessedTopic = allTopics.First();

                    else
                        continue;

                    allTopics.Remove(preprocessedTopic);


                    if( unusedSharedTopics.Contains(preprocessedTopic) )
                        _backgroundThread.ReportProgress(0,String.Format("Skipping unused shared topic \"{0}\"...",preprocessedTopic.Title));

                    else
                    {
                        _backgroundThread.ReportProgress(0,String.Format("Processing topic \"{0}\"...",preprocessedTopic.Title));

                        Topic topic = new Topic(preprocessedTopic);
                        string topicText = File.ReadAllText(preprocessedTopic.Filename);

                        // generate the topic for the CHM
                        string htmlFilename = _chmTopicCompilerSettings.GetHtmlFilename(preprocessedTopic);
                        string html = topic.CompileForHtml(topicText,_helpComponents,_chmTopicCompilerSettings);

						if( DoGenerateChm )
							File.WriteAllText(Path.Combine(_temporaryFilesPath,htmlFilename),html,Encoding.UTF8);

                        _outputTopicFilenames.Add(preprocessedTopic,htmlFilename);

                        // generate the topic for the website
						if( DoGenerateWebsite )
						{
							htmlFilename = _websiteTopicCompilerSettings.GetHtmlFilename(preprocessedTopic);
							html = topic.CompileForHtml(topicText,_helpComponents,_websiteTopicCompilerSettings);
							File.WriteAllText(Path.Combine(_outputWebsitePath,htmlFilename),html,Encoding.UTF8);
						}

                        // generate the topic for the PDF
                        if( DoGeneratePdf && ( twPdf != null ) )
                        {
                            html = topic.CompileForHtml(topicText,_helpComponents,_pdfTopicCompilerSettings);
                            twPdf.WriteLine(html);
                        }
                    }

                    _backgroundThread.ReportProgress(0,ThreadUpdateMessage.TopicsProgress);

                    if( _backgroundThread.CancellationPending )
                        return;
                }

                if( twPdf != null )
                    twPdf.WriteLine(PdfHtmlFooter);
            }

			// ♻ once all topics have been converted, remove the following code ♻:
			// ♻ ( starting here ) ♻
			foreach( Preprocessor.ImagePreprocessor image in _helpComponents.preprocessor.GetAllImages() )
			{
				_chmTopicCompilerSettings.UsedImageFilenames.Add(image.Filename);
				_websiteTopicCompilerSettings.UsedImageFilenames.Add(image.Filename);

				if( DoGeneratePdf )
					File.Copy(image.Filename,Path.Combine(Path.GetDirectoryName(_outputPdfTopicsFilename),Path.GetFileName(image.Filename)));
			}
			// ♻ ( ending here ) ♻
        }

        private void GenerateChm()
        {
            string settingsFilename = Path.Combine(_temporaryFilesPath,_projectName + ".hhp");
            string tableOfContentsFilename = Path.Combine(_temporaryFilesPath,_projectName + ".hhc");
            string indexFilename = Path.Combine(_temporaryFilesPath,_projectName + ".hhk");

            File.Delete(_outputChmFilename);

            File.WriteAllText(Path.Combine(_temporaryFilesPath,Constants.TopicStylesheetFilename),Properties.Resources.TopicStylesheet,Encoding.ASCII);

            _helpComponents.tableOfContents.SaveForChm(tableOfContentsFilename,_outputTopicFilenames);

            _helpComponents.index.SaveForChm(indexFilename,_outputTopicFilenames);

            _helpComponents.settings.SaveForChm(settingsFilename,_outputChmFilename,
                Path.GetFileName(tableOfContentsFilename),
                Path.GetFileName(indexFilename),
                _outputTopicFilenames,
                _chmTopicCompilerSettings.UsedImageFilenames,
                _chmTopicCompilerSettings.ContextSensitiveHelps,
                _helpComponents.index.MergeFiles);

            // compile the file
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.FileName = _helpComponents.htmlHelpCompilerExecutable;
            processStartInfo.Arguments = "\"" + settingsFilename + "\"";

            int numErrors = 0;

            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.EnableRaisingEvents = true;
            process.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate(object sender,DataReceivedEventArgs e)
                {
                    string hhcOutput = e.Data;

                    if( !String.IsNullOrWhiteSpace(hhcOutput) )
                    {
                        if( hhcOutput.Contains("HHC") && hhcOutput.Contains("Error") )
                            numErrors++;

                        _backgroundThread.ReportProgress(0,e);
                    }
                }
            );

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.CancelOutputRead();

            if( numErrors > 2 ) // there will be two errors because of false reports about problems compiling the jump buttons
            {
                if( File.Exists(_outputChmFilename) )
                    File.Delete(_outputChmFilename);

                throw new Exception("The HTML Help Compiler reported errors.");
            }
        }

        private void GenerateWebsite()
        {
            // write out the .htaccess file listing the default topic
            string htAccessFilename = Path.Combine(_outputWebsitePath,".htaccess");
            string defaultTopicFilename = _websiteTopicCompilerSettings.GetHtmlFilename(_helpComponents.settings.DefaultTopic);
            File.WriteAllText(htAccessFilename,String.Format("DirectoryIndex {0}\n",defaultTopicFilename));

			// and write out the web.config file
			string webConfigFilename = Path.Combine(_outputWebsitePath,"web.config");
			File.WriteAllText(webConfigFilename,Properties.Resources.WebConfig.Replace("%template-topic%",defaultTopicFilename));


            // copy over any used images
            foreach( string imageFilename in _websiteTopicCompilerSettings.UsedImageFilenames )
                File.Copy(imageFilename,Path.Combine(_outputWebsitePath,Path.GetFileName(imageFilename)));

            // copy over the stylesheets and chapter/topic images
            File.WriteAllText(Path.Combine(_outputWebsitePath,Constants.TopicStylesheetFilename),Properties.Resources.TopicStylesheet,Encoding.ASCII);
            File.WriteAllText(Path.Combine(_outputWebsitePath,Constants.WebsiteStylesheetFilename),Properties.Resources.WebsiteStylesheet,Encoding.ASCII);
            Properties.Resources.WebsiteChapterClosed.Save(Path.Combine(_outputWebsitePath,"hgweb_chapter_closed.png"));
            Properties.Resources.WebsiteChapterOpen.Save(Path.Combine(_outputWebsitePath,"hgweb_web_chapter_open.png"));
            Properties.Resources.WebsiteTopic.Save(Path.Combine(_outputWebsitePath,"hgweb_web_topic.png"));
            Properties.Resources.WebsiteTopicCurrent.Save(Path.Combine(_outputWebsitePath,"hgweb_web_topic_current.png"));
        }

        private void GeneratePdf()
        {
            _backgroundThread.ReportProgress(0,"Processing PDF cover page...");
            string outputPdfCoverFilename = Path.Combine(_temporaryFilesPath,"_output_pdf_cover.html");

            Preprocessor.TopicPreprocessor preprocessedTopic = _helpComponents.preprocessor.GetTopic(Constants.PdfCoverFilename);
            string topicText = File.ReadAllText(preprocessedTopic.Filename);

            Topic topic = new Topic(preprocessedTopic);
            string html = topic.CompileForHtml(topicText,_helpComponents,_pdfTopicCompilerSettings);
            File.WriteAllText(outputPdfCoverFilename,PdfHtmlHeader + html + PdfHtmlFooter,Encoding.UTF8);

            // generate the PDF
            _backgroundThread.ReportProgress(0,"Generating the PDF...");

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.FileName = _helpComponents.wkhtmltopdfExecutable;
            processStartInfo.Arguments = String.Format("--print-media-type cover \"{0}\" toc \"{1}\" \"{2}\"",
                outputPdfCoverFilename,_outputPdfTopicsFilename,_outputPdfFilename);

            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.EnableRaisingEvents = true;
            process.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate(object sender,DataReceivedEventArgs e)
                {
                    if( !String.IsNullOrWhiteSpace(e.Data) )
                        _backgroundThread.ReportProgress(0,e);
                }
            );

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.CancelOutputRead();
        }

        private string PdfHtmlHeader
        {
            get
            {
                return
                    "<html>" +
                    "<head>" +
                    "<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">" +
                    _pdfTopicCompilerSettings.GetTopicStylesheet() +
                    "</head>" +
                    "<body>";
            }
        }

        private string PdfHtmlFooter { get { return "</body></html>"; } }

        private void ListUnusedImages()
        {
            bool displayedHeader = false;

            foreach( Preprocessor.ImagePreprocessor image in _helpComponents.preprocessor.GetAllImages() )
            {
                if( !image.Shared && !_websiteTopicCompilerSettings.UsedImageFilenames.Contains(image.Filename) )
                {
                    if( !displayedHeader )
                    {
                        _backgroundThread.ReportProgress(0,"There are unused images:");
                        displayedHeader = true;
                    }

                    _backgroundThread.ReportProgress(0,image.Filename);
                }
            }
        }
    }
}
