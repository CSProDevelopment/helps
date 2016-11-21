using System.Text;

namespace Colorizer
{
    class PffColorizerHtml : PffColorizerInterface
    {
        public void StartBlock(StringBuilder sb)
        {
            sb.Append(
                "<!doctype html>\n" +
                "<html>\n" +
                "<head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8' /></head>\n" +
                "<body>\n" +
                "<div style='word-wrap:break-word;margin:0px;padding:0px;border:0px;background-color:#ffffff;color:#000000;font-family:Courier New;font-size:10pt;'>\n");
        }

        public void EndBlock(StringBuilder sb)
        {
            sb.Append("</div>\n</body>\n</html>\n");
        }

        public void AddHeading(StringBuilder sb,string heading)
        {
            sb.AppendFormat("<font color=\"#000080\"><strong>{0}</strong></font>",HelperFunctions.Htmlize(heading));
        }

        public void AddType(StringBuilder sb,string type)
        {
            sb.AppendFormat("<strong>{0}</strong>",HelperFunctions.Htmlize(type));
        }

        public void AddAttribute(StringBuilder sb,string attribute)
        {
            sb.AppendFormat("<font color=\"#000080\">{0}</font>",HelperFunctions.Htmlize(attribute));
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
}
