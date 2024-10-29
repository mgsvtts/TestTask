using System.Net.Http.Json;
using System.Text.Json;
using GrpcServer.Application.Services.Dto;
using GrpcServer.Infrastructure.HttpClients.Abstractions;
using GrpcServer.Infrastructure.HttpClients.Dto.ChatCompletion;
using Microsoft.Extensions.Logging;

namespace GrpcServer.Infrastructure.HttpClients;

public sealed class OpenAIHttpClient : IOpenAIHttpClient
{
    private readonly string _url;
    private readonly string _token;
    private readonly HttpClient _httpClient;
    private readonly ILogger<OpenAIHttpClient> _logger;

    public OpenAIHttpClient(HttpClient httpClient, ILogger<OpenAIHttpClient> logger, string url, string token)
    {
        _httpClient = httpClient;
        _logger = logger;
        _url = url;
        _token = token;
    }

    public async Task<string> TranslateAsync(TranslateRequest request, CancellationToken token)
    {
        try
        {
            var openAiRequest = CompletionRequest.FromTranslateRequest(request);
            var content = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(JsonSerializer.Serialize(openAiRequest)),
                RequestUri = new Uri(_url + "/completions"),
                Headers =
                {
                    {"Authorization", $"Bearer {_token}" }
                }
            };

            var responseMessage = await _httpClient.SendAsync(content, token);

            responseMessage.EnsureSuccessStatusCode();

            var response = await responseMessage.Content.ReadFromJsonAsync<CompletionResponse>(cancellationToken: token);

            return response!.Choices[0].Message.Content;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending request to OpenAI. Request: {TranslateRequest}", request);

            return $"Mock translation {Guid.NewGuid()}";
        }
    }
}
