using ConsoleApplication.Translation.Translators.Dto;
using ConsoleApplication.Utils;
using Grpc.Translation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Translation.Translators;

public sealed class GrpcTranslator : ITranslator
{
    private readonly GrpcTranslationService.GrpcTranslationServiceClient _grpcClient;
    public GrpcTranslator(IServiceProvider services)
    {
        _grpcClient = services.GetRequiredService<GrpcTranslationService.GrpcTranslationServiceClient>();
    }

    public async Task<string> TranslateAsync(TranslationRequest request)
    {
        var token = CancellationTokenUtil.GetToken();

        var translation = await _grpcClient.TranslateAsync(new GrpcTranslateRequest
        {
            LanguageFrom = request.From,
            LanguageTo = request.To,
            Text = request.Text,
        }, cancellationToken: token);


        return translation.Message;
    }

    public async Task<(string Size, string Memory)> GetInfoAsync()
    {
        var token = CancellationTokenUtil.GetToken();

        var info = await _grpcClient.GetInfoAsync(new Empty(), cancellationToken: token);

        return (info.Size, info.Memory);
    }
}
