using ConsoleApplication.HttpTranslator.Dto;
using ConsoleApplication.Translation.Translators.Dto;
using ConsoleApplication.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text.Json;

namespace ConsoleApplication.Translation.Translators;

public sealed class WebTranslator : ITranslator
{
    private readonly IServiceProvider _services;
    private readonly string _url;
    public WebTranslator(IServiceProvider services)
    {
        var config  = services.GetRequiredService<IConfiguration>();
        _url = config.GetValue<string>("HttpServerUrl")!;
        _services = services;
    }

    public async Task<(string Size, string Memory)> GetInfoAsync()
    {
        var token = CancellationTokenUtil.GetToken();

        using var httpClient = _services.GetRequiredService<HttpClient>();

        var content = await httpClient.GetAsync(_url + "/api/translate/info", token);

        var response = await content.Content.ReadFromJsonAsync<GetInfoResponse>(token);

        return (response!.Size, response.Memory);
    }

    public async Task<string> TranslateAsync(TranslationRequest request)
    {
        var token = CancellationTokenUtil.GetToken();

        using var httpClient = _services.GetRequiredService<HttpClient>();

        var message = new HttpRequestMessage
        {
            RequestUri = new Uri(_url + "/api/translate"),
            Method = HttpMethod.Post,
            Content = new StringContent(JsonSerializer.Serialize(new GetTranslationRequest
            {
                Text = request.Text,
                LanguageFrom = request.From,
                LanguageTo = request.To,
            }), System.Text.Encoding.UTF8, "application/json")
        };

        var response = await httpClient.SendAsync(message, token);

        response.EnsureSuccessStatusCode();

        var translation = await response.Content.ReadFromJsonAsync<GetTranslationResponse>(token);

        return translation.Translation;
    }
}
