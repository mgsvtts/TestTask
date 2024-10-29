using GrpcServer.Application.Services.Dto;

namespace GrpcServer.Application.Services.Abstractions;
public interface ITranslationService
{
    Task<(long Size, double Memory)> GetInformationAsync();
    Task<string> TranslateAsync(TranslateRequest request, CancellationToken token);
}