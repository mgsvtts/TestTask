using GrpcServer.Infrastructure.Exceptions;
using System.Text.RegularExpressions;

namespace GrpcImpl.HttpClients.Dto;

public readonly partial record struct Language
{
    public string Value { get; }

    public Language(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value.Length > 50)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Language length must be less than 50");
        }

        if (!OnlyRussianLetters().IsMatch(value))
        {
            throw new LanguageFormatException();
        }

        Value = value;
    }

    [GeneratedRegex(@"^[а-яА-ЯёЁ\s,.!?-]*$", RegexOptions.Compiled, 1_000)]
    private static partial Regex OnlyRussianLetters();

    public static implicit operator string(Language language)
    {
        return language.Value;
    }

    public static implicit operator Language(string language)
    {
        return new Language(language);
    }

    public override string ToString()
    {
        return Value;
    }
}
