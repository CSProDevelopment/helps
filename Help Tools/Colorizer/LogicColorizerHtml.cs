using System.Text;

namespace Colorizer
{
    class LogicColorizerHtml : LogicColorizerInterface
    {
        protected virtual string Htmlize(string text)
        {
            return HelperFunctions.Htmlize(text);
        }

        public virtual void StartBlock(StringBuilder sb)
        {
            sb.Append(
                "<!doctype html>\n" +
                "<html>\n" +
                "<head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8' /></head>\n" +
                "<body>\n" +
                "<div style='word-wrap:break-word;margin:0px;padding:0px;border:0px;background-color:#ffffff;color:#000000;font-family:Courier New;font-size:10pt;'>\n");
        }

        public virtual void EndBlock(StringBuilder sb)
        {
            sb.Append("</div>\n</body>\n</html>\n");
        }

        public void AddComment(StringBuilder sb,string comment)
        {
            sb.AppendFormat("<font color=\"green\">{0}</font>",Htmlize(comment));
        }

        public void AddQuotedString(StringBuilder sb,string quotedString)
        {
            sb.AppendFormat("<font color=\"fuchsia\">{0}</font>",Htmlize(quotedString));
        }

        public void AddNumber(StringBuilder sb,string number)
        {
            sb.AppendFormat("<font color=\"red\">{0}</font>",Htmlize(number));
        }

        public virtual void AddKeyword(StringBuilder sb,string keyword,string helpTopic)
        {
            sb.AppendFormat("<font color=\"blue\">{0}</font>",Htmlize(keyword));
        }

        public virtual void AddTextWithHelp(StringBuilder sb,string text,string helpTopic)
        {
            AddText(sb,text);
        }

        public void AddNewLine(StringBuilder sb)
        {
            sb.Append("<br />\n");
        }

        public void AddText(StringBuilder sb,string text)
        {
            sb.Append(Htmlize(text));
        }
    }

    class LogicColorizerHtmlHelp : LogicColorizerHtml
    {
        public interface GetHtmlFilenameForKeywordInterface
        {
            string GetHtmlFilenameForKeyword(string helpTopic);
        }

        private GetHtmlFilenameForKeywordInterface _getHtmlFilenameForKeywordInterface;

        public LogicColorizerHtmlHelp(GetHtmlFilenameForKeywordInterface getHtmlFilenameForKeywordInterface)
        {
            _getHtmlFilenameForKeywordInterface = getHtmlFilenameForKeywordInterface;
        }

        public override void StartBlock(StringBuilder sb)
        {
            sb.Append("<div class=\"code_colorization indent\">");
        }

        public override void EndBlock(StringBuilder sb)
        {
            sb.Append("</div>");
        }

        private void AddKeywordOrTextWithHelp(StringBuilder sb,string text,string helpTopic,string color)
        {
            string linkText = ( helpTopic == null ) ? null : _getHtmlFilenameForKeywordInterface.GetHtmlFilenameForKeyword(helpTopic);

            if( linkText == null )
                sb.AppendFormat("<font color=\"{0}\">{1}</font>",color,Htmlize(text));

            else
                sb.AppendFormat("<a href=\"{0}\" class=\"code_colorization_keyword_link\"><span style=\"color: {1};\">{2}</span></a>",linkText,color,Htmlize(text));
        }

        public override void AddKeyword(StringBuilder sb,string keyword,string helpTopic)
        {
            AddKeywordOrTextWithHelp(sb,keyword,helpTopic,"blue");
        }

        public override void AddTextWithHelp(StringBuilder sb,string text,string helpTopic)
        {
            AddKeywordOrTextWithHelp(sb,text,helpTopic,"black");
        }
    }

    class LogicColorizerHtmlHelpInline : LogicColorizerHtmlHelp
    {
        public LogicColorizerHtmlHelpInline(GetHtmlFilenameForKeywordInterface getHtmlFilenameForKeywordInterface)
            : base(getHtmlFilenameForKeywordInterface)
        {
        }

        protected override string Htmlize(string text)
        {
            return HelperFunctions.HtmlizeWhitespace(text);
        }

        public override void StartBlock(StringBuilder sb)
        {
            sb.Append("<font class=\"code_colorization\">");
        }

        public override void EndBlock(StringBuilder sb)
        {
            sb.Append("</font>");
        }
    }
}
