using Grpc.Translation;
using GrpcServer.Application.Services;
using GrpcServer.Application.Services.Abstractions;
using GrpcServer.Infrastructure.HttpClients;
using GrpcServer.Infrastructure.HttpClients.Abstractions;
using GrpcServer.Presentation.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace GrpcServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddGrpc();
        builder.Services.AddScoped<ITranslationService, TranslationService>();
        builder.Services.AddHttpClient<IOpenAIHttpClient, OpenAIHttpClient>((context, services) =>
        {
            return new OpenAIHttpClient
            (
                context,
                services.GetRequiredService<ILogger<OpenAIHttpClient>>(),
                builder.Configuration.GetValue<string>("OpenAIUrl")!,
                builder.Configuration.GetValue<string>("OpenAIKey")!
            );
        });

        var redis = builder.Configuration.GetConnectionString("Redis")!;
        builder.Services.AddStackExchangeRedisCache(x =>
        {
            x.Configuration = redis;
        });

        builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
        {
            return ConnectionMultiplexer.Connect(redis);
        });

        builder.Services.AddCors();

        var app = builder.Build();

        app.MapGrpcService<TranslatorServiceImpl>();

        app.UseCors(x =>
        {
            x.AllowAnyHeader();
            x.AllowAnyMethod();
            x.AllowAnyOrigin();
        }); 

        app.Run();
    }
}
