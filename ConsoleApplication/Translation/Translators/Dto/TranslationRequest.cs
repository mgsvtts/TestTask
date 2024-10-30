namespace ConsoleApplication.Translation.Translators.Dto;
public readonly record struct TranslationRequest
{
    public string Text { get; }
    public string From { get; }
    public string To { get; }

    public TranslationRequest(string text, string from, string to)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (string.IsNullOrEmpty(from))
        {
            throw new ArgumentNullException(nameof(from));
        }

        if (string.IsNullOrEmpty(to))
        {
            throw new ArgumentNullException(nameof(to));
        }

        Text = text;
        From = from;
        To = to;
    }
}