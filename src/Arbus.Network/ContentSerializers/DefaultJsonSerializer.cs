using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arbus.Network.ContentSerializers;

public static class DefaultJsonSerializer
{
    private static TimeSpan _maxDeserializationTime = TimeSpan.FromSeconds(5);
    private static JsonSerializerOptions? _serializerOptions;

    public static JsonSerializerOptions SerializerOptions
    {
        get => _serializerOptions = GetDefaultSerializerOptions();
        set => _serializerOptions = value;
    }

    public static void Serialize<TValue>(Stream utf8Json, TValue value)
            => JsonSerializer.Serialize(utf8Json, value, _serializerOptions);

    public static string Serialize(object? value) => JsonSerializer.Serialize(value, _serializerOptions);

    public static TValue? Deserialize<TValue>(string json)
        => JsonSerializer.Deserialize<TValue>(json, _serializerOptions);

    public static TValue? Deserialize<TValue>(Stream utf8Json)
        => JsonSerializer.Deserialize<TValue>(utf8Json, _serializerOptions);

    public static ValueTask<TValue?> DeserializeAsync<TValue>(Stream utf8Json, CancellationToken? cancellationToken = default)
    {
        cancellationToken ??= new CancellationTokenSource(_maxDeserializationTime).Token;
        return JsonSerializer.DeserializeAsync<TValue>(utf8Json, _serializerOptions, cancellationToken.Value);
    }

    public static JsonSerializerOptions GetDefaultSerializerOptions() => new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}