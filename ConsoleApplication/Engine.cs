using ConsoleApplication.Translation;
using ConsoleApplication.Translation.Translators;
using ConsoleApplication.Translation.Translators.Dto;

namespace ConsoleApplication;

public class Engine
{
    private readonly IServiceProvider _services;

    public Engine(IServiceProvider services)
    {
        _services = services;
    }

    public async Task StartAsync()
    {
        while (true)
        {
            try
            {
                var action = ChooseAction();

                var task = action switch
                {
                    "1" => GetTranslationInfoAsync(),
                    "2" => TranslateTextAsync(),
                    _ => Task.FromResult(new NotImplementedException())
                };

                await task;

                Console.WriteLine(new string('=', 50));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private async Task GetTranslationInfoAsync()
    {
        var type = ChooseType();

        var translator = TranslatorFactory.Create(_services, type);

        var info = await translator.GetInfoAsync();

        Console.WriteLine($"Items in cache: {info.Size}\nMemory usage: {info.Memory}");
    }

    private async Task TranslateTextAsync()
    {
        var (request, type) = GetTranslationRequest();

        var translator = TranslatorFactory.Create(_services, type);

        var translation = await translator.TranslateAsync(request);

        Console.WriteLine($"Translation: {translation}");
    }

    private static string ChooseAction()
    {
        Console.WriteLine("Choose action:\n" +
            "1 - Show translation info\n" +
            "2 - Translate\n\n");

        return Console.ReadLine();
    }

    private static (TranslationRequest Request, TranslatorType Type) GetTranslationRequest()
    {
        Console.WriteLine("Enter language from (only russian letters): ");
        var from = Console.ReadLine();

        Console.WriteLine("Enter language to (only russian letters): ");
        var to = Console.ReadLine();

        Console.WriteLine("Enter text: ");
        var text = Console.ReadLine();

        var type = ChooseType();

        return (new TranslationRequest(text, from, to), type);
    }

    private static TranslatorType ChooseType()
    {
        Console.WriteLine
        (
            "Enter type: \n" +
            "1 - Web\n" +
            "2 - Grpc\n\n"
        );
        var type = Console.ReadLine();

        return type.ToTranslatorType();
    }
}