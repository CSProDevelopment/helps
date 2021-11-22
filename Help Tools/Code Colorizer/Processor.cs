﻿using System;
using System.Windows.Forms;
using Colorizer;

namespace Code_Colorizer
{
	class Processor
	{
        public enum BufferType { Logic, Pff, Message };
        public enum UsersType { Forum, Blog };

        private CSPro.Logic.Colorizer _colorizer;
        private PffColorizer _pffColorizer;

        public Processor(Form form)
		{
            _colorizer = new CSPro.Logic.Colorizer(form.Handle.ToInt32());
            _pffColorizer = new PffColorizer();
		}

		public void HtmlProcessor(string text, BufferType buffer_type)
		{
			text = HelperFunctions.TrimTrailingSpace(text);

            string formattedText =
                ( buffer_type == BufferType.Logic )   ? _colorizer.LogicToHtml(text) :
                ( buffer_type == BufferType.Message ) ? _colorizer.MessageToHtml(text) :
                                                        _pffColorizer.Colorize(new PffColorizerHtml(), text);

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
            Clipboard.SetText(htmlCopyText, TextDataFormat.Html);
		}

        public void UsersProcessor(string text, UsersType users_type)
        {
            text = HelperFunctions.TrimTrailingSpace(text);

            string formattedText = 
                ( users_type == UsersType.Forum ) ? _colorizer.LogicToCSProUsersForum(text) :
                                                    _colorizer.LogicToCSProUsersBlog(text);

            Clipboard.Clear();
            Clipboard.SetText(formattedText, TextDataFormat.UnicodeText);
        }
	}
}
