using System.Text;

namespace Colorizer
{
    class LogicColorizerHtml : LogicColorizerInterface
    {
        public virtual void StartBlock(StringBuilder sb,bool inlineColorization)
        {
            sb.Append(
                "<!doctype html>\n" +
                "<html>\n" +
                "<head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8' /></head>\n" +
                "<body>\n" +
                "<div style='word-wrap:break-word;margin:0px;padding:0px;border:0px;background-color:#ffffff;color:#000000;font-family:Courier New;font-size:10pt;'>\n");
        }

        public virtual void EndBlock(StringBuilder sb,bool inlineColorization)
        {
            sb.Append("</div>\n</body>\n</html>\n");
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
        public override void StartBlock(StringBuilder sb,bool inlineColorization)
        {
            sb.Append(inlineColorization ? "<font class=\"code_colorization\">" :
                "<div class=\"code_colorization indent\">");
        }

        public override void EndBlock(StringBuilder sb,bool inlineColorization)
        {
            sb.Append(inlineColorization ? "</font>" : "</div>");
        }
    }
}
