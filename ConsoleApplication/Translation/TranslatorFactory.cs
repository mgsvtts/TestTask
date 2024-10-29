using ConsoleApplication.Translation.Translators;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplication.Translation;

public static class TranslatorFactory
{
    public static ITranslator Create(IServiceProvider services, TranslatorType type)
    {
        return type switch
        {
            TranslatorType.Grpc => new GrpcTranslator(services),
            TranslatorType.Web => new WebTranslator(services),
            _=>throw new NotImplementedException(),
        };
    }
}
