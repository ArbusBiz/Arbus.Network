using System.Text;

namespace Arbus.Network.Application.ContentSerializers;

public static class JsonContentSerializer
{
    public static HttpContent Serialize(object value)
    {
        return new StringContent(DefaultJsonSerializer.Serialize(value), Encoding.UTF8, HttpContentType.Application.Json);
    }

    public static async Task<T> Deserialize<T>(HttpContent content, CancellationToken cancellationToken = default)
    {
        using var responseStream = await content.ReadAsStreamAsync().ConfigureAwait(false);
        var deserializedObject = await DefaultJsonSerializer.DeserializeAsync<T>(responseStream, cancellationToken).ConfigureAwait(false);
        return deserializedObject ?? throw new Exception("Unable to deserialize stream.");
    }
}