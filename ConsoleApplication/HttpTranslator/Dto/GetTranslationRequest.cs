namespace ConsoleApplication.HttpTranslator.Dto;

public sealed class GetTranslationRequest
{
    public string Text { get; set; }
    public string LanguageFrom { get; set; }
    public string LanguageTo { get; set; }
}