using System.Text;

namespace Colorizer
{
    class LogicColorizerHtml : LogicColorizerInterface
    {
        protected virtual void StartBlockBeforeDiv(StringBuilder sb)
        {
            sb.Append(
                "<!doctype html>\n" +
                "<html>\n" +
                "<head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8' /></head>\n" +
                "<body>\n");
        }

        public void StartBlock(StringBuilder sb)
        {
            StartBlockBeforeDiv(sb);
            sb.Append("<div style='word-wrap:break-word;margin:0px;padding:0px;border:0px;background-color:#ffffff;color:#000000;font-family:Courier New;font-size:10pt;'>\n");
        }

        public void EndBlock(StringBuilder sb)
        {
            sb.Append("</div>\n");
            EndBlockAfterDiv(sb);
        }

        protected virtual void EndBlockAfterDiv(StringBuilder sb)
        {
            sb.Append("</body>\n</html>\n");
        }

        public void AddComment(StringBuilder sb,string comment)
        {
            sb.AppendFormat("<font color=\"green\">{0}</font>",HelperFunctions.Htmlize(comment));
        }

        public void AddQuotedString(StringBuilder sb,string quotedString)
        {
            sb.AppendFormat("<font color=\"fuchsia\">{0}</font>",HelperFunctions.Htmlize(quotedString));
        }

        public void AddNumber(StringBuilder sb,string number)
        {
            sb.AppendFormat("<font color=\"red\">{0}</font>",HelperFunctions.Htmlize(number));
        }

        public void AddKeyword(StringBuilder sb,string keyword)
        {
            sb.AppendFormat("<font color=\"blue\">{0}</font>",HelperFunctions.Htmlize(keyword));
        }

        public void AddNewLine(StringBuilder sb)
        {
            sb.Append("<br />\n");
        }

        public void AddText(StringBuilder sb,string text)
        {
            sb.Append(HelperFunctions.Htmlize(text));
        }
    }

    class LogicColorizerHtmlHelp : LogicColorizerHtml
    {
        protected override void StartBlockBeforeDiv(StringBuilder sb)
        {
        }

        protected override void EndBlockAfterDiv(StringBuilder sb)
        {
        }
    }
}
