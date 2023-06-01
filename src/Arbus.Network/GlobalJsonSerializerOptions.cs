using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

namespace Arbus.Network;

public static class GlobalJsonSerializerOptions
{
    public static JsonSerializerOptions Options { get; set; } = GetDefaultSerializerOptions();

    public static JsonSerializerOptions GetDefaultSerializerOptions() => new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}