using System.Text;

namespace Colorizer
{
    class LogicColorizerUsersForum: LogicColorizerInterface
    {
        public void StartBlock(StringBuilder sb)
        {
            sb.Append("[cspro]");
        }

        public void EndBlock(StringBuilder sb)
        {
            sb.Append("[/cspro]");
        }

        public void AddComment(StringBuilder sb,string comment)
        {
            sb.AppendFormat("[color=green]{0}[/color]",PrepareText(comment));
        }

        public void AddQuotedString(StringBuilder sb,string quotedString)
        {
            sb.AppendFormat("[color=fuchsia]{0}[/color]",PrepareText(quotedString));
        }

        public void AddNumber(StringBuilder sb,string number)
        {
            sb.AppendFormat("[color=red]{0}[/color]",PrepareText(number));
        }

        public void AddKeyword(StringBuilder sb,string keyword)
        {
            sb.AppendFormat("[color=blue]{0}[/color]",PrepareText(keyword));
        }

        public void AddNewLine(StringBuilder sb)
        {
            sb.Append("\r\n");
        }

        public void AddText(StringBuilder sb,string text)
        {
            sb.Append(PrepareText(text));
        }

        private string PrepareText(string text)
        {
            return HelperFunctions.ReplaceWhitespace(text,"[sp][/sp]");
        }
    }
}
