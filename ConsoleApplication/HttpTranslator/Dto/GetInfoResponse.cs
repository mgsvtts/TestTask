using System.Text.Json.Serialization;

namespace ConsoleApplication.HttpTranslator.Dto;

public sealed class GetInfoResponse
{
    [JsonPropertyName("size")]
    public string Size { get; set; }

    [JsonPropertyName("memory")]
    public string Memory { get; set; }
}