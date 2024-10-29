using Grpc.Core;
using GrpcImpl.HttpClients.Dto;
using Grpc.Translation;
using GrpcServer.Application.Services.Abstractions;
using GrpcServer.Application.Services.Dto;
using GrpcServer.Infrastructure.HttpClients.Dto.ChatCompletion;

namespace GrpcServer.Presentation.Grpc;

public class TranslatorServiceImpl : GrpcTranslationService.GrpcTranslationServiceBase
{
    private readonly ITranslationService _service;

    public TranslatorServiceImpl(ITranslationService service)
    {
        _service = service;
    }

    public override async Task<GrpcTranslateResponse> Translate(GrpcTranslateRequest request, ServerCallContext context)
    {
        var translation = await _service.TranslateAsync(new TranslateRequest
        (
            request.Text,
            request.LanguageFrom,
            request.LanguageTo
        ), context.CancellationToken);

        return new GrpcTranslateResponse
        {
            Message = translation
        };
    }

    public override async Task<GrpcGetInfoResponse> GetInfo(Empty request, ServerCallContext context)
    {
        var (size, memory) = await _service.GetInformationAsync();

        return new GrpcGetInfoResponse
        {
            Size = $"{size} elements",
            Memory = $"{memory} megabytes",
            System = "Open AI"
        };
    }
}
