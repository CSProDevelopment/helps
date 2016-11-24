namespace Help_Generator
{
    interface TextEditableInterface
    {
        string Filename { get; }
        string Title { get; }
        string HelpText { get; }
        void Compile(string[] lines,Preprocessor preprocessor);
        string Format(string[] lines,Preprocessor preprocessor);
    }
}
