using GrpcImpl.HttpClients.Dto;

namespace GrpcServer.Application.Services.Dto;

public readonly record struct TranslateRequest
{
    public string Text { get; }
    public Language LanguageFrom { get; }
    public Language LanguageTo { get; }

    public TranslateRequest(string text, Language languageFrom, Language languageTo)
    {
        Text = text;
        LanguageFrom = languageFrom;
        LanguageTo = languageTo;
    }
}
