using GrpcServer.Application.Services.Abstractions;
using GrpcServer.Application.Services.Dto;
using GrpcServer.Infrastructure.HttpClients.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace GrpcServer.Application.Services;

public sealed class TranslationService : ITranslationService
{
    private readonly IDistributedCache _cache;
    private readonly IConnectionMultiplexer _redis;
    private readonly IOpenAIHttpClient _httpClient;
    private readonly ILogger<TranslationService> _logger;

    public TranslationService(
        IOpenAIHttpClient httpClient,
        IDistributedCache cache, 
        IConnectionMultiplexer redis,
        ILogger<TranslationService> logger)
    {
        _httpClient = httpClient;
        _cache = cache;
        _redis = redis;
        _logger = logger;
    }

    public async Task<string> TranslateAsync(TranslateRequest request, CancellationToken token)
    {
        var key = $"{request.LanguageFrom}{request.LanguageTo}{request.Text}";

        var cached = await _cache.GetStringAsync(key, token);

        if (cached is not null)
        {
            return cached;
        }

        var translation = await _httpClient.TranslateAsync(request, token);

        await _cache.SetStringAsync(key, translation, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
        }, token);

        return translation;
    }

    public async Task<(long Size, double Memory)> GetInformationAsync()
    {
        try
        {
            var db = _redis.GetDatabase();

            var size = await db.ExecuteAsync("DBSIZE");
            var memory = await GetUsedMemoryInMegabytes();

            return ((long)size, memory);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get redis info");

            throw;
        }
    }

    private async Task<double> GetUsedMemoryInMegabytes()
    {
        var server = _redis.GetServer(_redis.GetEndPoints()[0]);

        var stats = (await server.MemoryStatsAsync()).ToDictionary();

        var total = (double)stats["total.allocated"];
        var start = (double)stats["startup.allocated"];

        var allocated = total - start;

        return allocated / 1024 / 1024;
    }
}
