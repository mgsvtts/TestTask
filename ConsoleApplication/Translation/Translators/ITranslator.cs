using ConsoleApplication.Translation.Translators.Dto;

namespace ConsoleApplication.Translation.Translators;

public interface ITranslator
{
    Task<(string Size, string Memory)> GetInfoAsync();

    Task<string> TranslateAsync(TranslationRequest request);
}