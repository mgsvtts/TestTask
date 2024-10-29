using Grpc.Translation;

namespace WebServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddGrpcClient<GrpcTranslationService.GrpcTranslationServiceClient>(x =>
        {
            x.Address = new Uri(builder.Configuration.GetValue<string>("GrpcServerUrl")!);
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (!app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}
