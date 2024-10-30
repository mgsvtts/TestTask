using ConsoleApplication.Translation.Translators;

namespace ConsoleApplication.Translation;

public static class TypeExtensions
{
    public static TranslatorType ToTranslatorType(this string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentNullException(nameof(input));
        }

        return input switch
        {
            "1" => TranslatorType.Web,
            "2" => TranslatorType.Grpc,
            _ => throw new NotImplementedException()
        };
    }
}