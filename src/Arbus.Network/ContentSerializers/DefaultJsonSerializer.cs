using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arbus.Network.ContentSerializers;

public static class DefaultJsonSerializer
{
    private static TimeSpan _maxDeserializationTime = TimeSpan.FromSeconds(5);

    public static JsonSerializerOptions SerializerOptions = GetDefaultSerializerOptions();

    public static void Serialize<TValue>(Stream utf8Json, TValue value)
            => JsonSerializer.Serialize(utf8Json, value, SerializerOptions);

    public static string Serialize(object? value) => JsonSerializer.Serialize(value, SerializerOptions);

    public static TValue? Deserialize<TValue>(string json)
        => JsonSerializer.Deserialize<TValue>(json, SerializerOptions);

    public static TValue? Deserialize<TValue>(Stream utf8Json)
        => JsonSerializer.Deserialize<TValue>(utf8Json, SerializerOptions);

    public static ValueTask<TValue?> DeserializeAsync<TValue>(Stream utf8Json, CancellationToken? cancellationToken = default)
    {
        cancellationToken ??= new CancellationTokenSource(_maxDeserializationTime).Token;
        return JsonSerializer.DeserializeAsync<TValue>(utf8Json, SerializerOptions, cancellationToken.Value);
    }

    public static JsonSerializerOptions GetDefaultSerializerOptions() => new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}