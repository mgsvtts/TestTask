using Grpc.Translation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplication;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var serviceProvider = new ServiceCollection()
            .AddHttpClient()
            .AddSingleton<IConfiguration>(config);

        serviceProvider.AddGrpcClient<GrpcTranslationService.GrpcTranslationServiceClient>(x =>
        {
            x.Address = new Uri(config.GetValue<string>("GrpcServerUrl")!);
        });

        var services = serviceProvider.BuildServiceProvider();

        await new Engine(services).StartAsync();
    }
}