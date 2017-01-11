using System.Text;

namespace Colorizer
{
    interface LogicColorizerInterface
    {
        void StartBlock(StringBuilder sb);
        void EndBlock(StringBuilder sb);

        void AddComment(StringBuilder sb,string comment);
        void AddQuotedString(StringBuilder sb,string quotedString);
        void AddNumber(StringBuilder sb,string number);
        void AddKeyword(StringBuilder sb,string keyword,string helpTopic);
        void AddTextWithHelp(StringBuilder sb,string text,string helpTopic);

        void AddNewLine(StringBuilder sb);
        void AddText(StringBuilder sb,string text);
    }
}
