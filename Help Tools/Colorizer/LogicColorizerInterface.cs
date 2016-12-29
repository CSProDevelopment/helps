using System.Text;

namespace Colorizer
{
    interface LogicColorizerInterface
    {
        void StartBlock(StringBuilder sb,bool inlineColorization);
        void EndBlock(StringBuilder sb,bool inlineColorization);

        void AddComment(StringBuilder sb,string comment);
        void AddQuotedString(StringBuilder sb,string quotedString);
        void AddNumber(StringBuilder sb,string number);
        void AddKeyword(StringBuilder sb,string keyword);

        void AddNewLine(StringBuilder sb);
        void AddText(StringBuilder sb,string text);
    }
}
