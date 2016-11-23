namespace Help_Generator
{
    interface TextEditableInterface
    {
        string Filename { get; }
        string Title { get; }
        string HelpText { get; }
        string Compile(string[] lines);
    }
}
