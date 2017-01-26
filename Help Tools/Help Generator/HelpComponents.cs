using Colorizer;

namespace Help_Generator
{
    class HelpComponents
    {
        public string htmlHelpCompilerExecutable;
        public string wkhtmltopdfExecutable;
        public string projectPath;

        public Settings settings;
        public TableOfContents tableOfContents;
        public Index index;
        public Preprocessor preprocessor;

        public LogicColorizer _logicColorizer;
        public LogicColorizer _inlineLogicColorizer;
        public PffColorizer _pffColorizer;
    }
}
