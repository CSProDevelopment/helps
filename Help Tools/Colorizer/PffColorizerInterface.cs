using System.Text;

namespace Colorizer
{
    interface PffColorizerInterface
    {
        void StartBlock(StringBuilder sb);
        void EndBlock(StringBuilder sb);

        void AddHeading(StringBuilder sb,string heading);
        void AddType(StringBuilder sb,string type);
        void AddAttribute(StringBuilder sb,string attribute);

        void AddNewLine(StringBuilder sb);
        void AddText(StringBuilder sb,string text);
    }
}
