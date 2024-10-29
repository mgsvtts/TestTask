using ConsoleApplication.Translation.Translators.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Translation.Translators;

public interface ITranslator
{
    Task<(string Size, string Memory)> GetInfoAsync();
    Task<string> TranslateAsync(TranslationRequest request);
}
