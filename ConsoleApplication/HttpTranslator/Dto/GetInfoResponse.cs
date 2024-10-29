using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApplication.HttpTranslator.Dto;
public sealed class GetInfoResponse
{
    [JsonPropertyName("size")]
    public string Size { get; set; }

    [JsonPropertyName("memory")]
    public string Memory { get; set; }
}
