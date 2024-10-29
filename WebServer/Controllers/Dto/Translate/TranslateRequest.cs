namespace WebServer.Controllers.Dto.Translate;

public sealed record TranslateRequest(string Text, string LanguageFrom, string LanguageTo);
