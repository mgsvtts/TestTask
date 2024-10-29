using System.Text.Json.Serialization;
using GrpcServer.Application.Services.Dto;

namespace GrpcServer.Infrastructure.HttpClients.Dto.ChatCompletion;

public sealed class CompletionRequest
{
    [JsonPropertyName("messages")]
    public List<OpenAiMessage> Messages { get; private set; }

    [JsonPropertyName("model")]
    public string Model { get; } = "gpt-4o-mini";

    private CompletionRequest(string text)
    {
        Messages = [new OpenAiMessage
        {
            Content = text
        }];
    }

    public static CompletionRequest FromTranslateRequest(TranslateRequest request)
    {
        return new CompletionRequest
        (
            $"Переведи этот текст с языка: {request.LanguageFrom.Value} " +
            $"на {request.LanguageTo.Value} и не пиши ничего кроме перевода: {request.Text}"
        );
    }
}

public sealed class OpenAiMessage
{
    [JsonPropertyName("role")]
    public string Role { get; } = "user";

    [JsonPropertyName("content")]
    public string Content { get; set; }
}