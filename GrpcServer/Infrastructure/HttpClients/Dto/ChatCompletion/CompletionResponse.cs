using System.Text.Json.Serialization;

namespace GrpcServer.Infrastructure.HttpClients.Dto.ChatCompletion;

public sealed class CompletionResponse
{
    public List<Choice> Choices { get; set; }
}

public sealed class Choice
{
    [JsonPropertyName("message")]
    public Message Message { get; set; }
}

public sealed class Message
{
    [JsonPropertyName("content")]
    public string Content { get; set; }
}