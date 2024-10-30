using Grpc.Translation;
using Microsoft.AspNetCore.Mvc;
using WebServer.Controllers.Dto.GetInfo;
using WebServer.Controllers.Dto.Translate;

namespace WebServer.Controllers;

[ApiController]
[Route("api/translate")]
public sealed class TranslatorController : ControllerBase
{
    private readonly ILogger<TranslatorController> _logger;
    private readonly GrpcTranslationService.GrpcTranslationServiceClient _client;

    public TranslatorController(GrpcTranslationService.GrpcTranslationServiceClient client, ILogger<TranslatorController> logger)
    {
        _client = client;
        _logger = logger;
    }

    [HttpPost]
    public async Task<TranslateResponse> Translate(TranslateRequest request, CancellationToken token)
    {
        try
        {
            var response = await _client.TranslateAsync(new GrpcTranslateRequest
            {
                LanguageFrom = request.LanguageFrom,
                LanguageTo = request.LanguageTo,
                Text = request.Text,
            }, cancellationToken: token);

            return new TranslateResponse(response.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send grpc request. {TranslateRequest}", request);

            throw;
        }
    }

    [HttpGet("info")]
    public async Task<GetInfoResponse> GetInfo(CancellationToken token)
    {
        try
        {
            var response = await _client.GetInfoAsync(new Empty(), cancellationToken: token);

            return new GetInfoResponse(response.Size, response.Memory);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send grpc request.");

            throw;
        }
    }
}