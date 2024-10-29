using ConsoleApplication.Translation.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Translation;
public static class TypeExtensions
{
    public static TranslatorType ToTranslatorType(this string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentNullException(nameof(input));
        }

        return input switch
        {
            "1" => TranslatorType.Web,
            "2" => TranslatorType.Grpc,
            _ => throw new NotImplementedException()
        };
    }
}
