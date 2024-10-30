using GrpcServer.Application.Services.Dto;

namespace GrpcServer.Infrastructure.HttpClients.Abstractions;

public interface IOpenAIHttpClient
{
    Task<string> TranslateAsync(TranslateRequest request, CancellationToken token);
}