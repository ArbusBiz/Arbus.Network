using Arbus.Network.Extensions;
using System.Net.Http.Headers;
using System.Text;

namespace Arbus.Network.Abstractions;

public abstract class ApiEndpoint
{
    private static readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(10);

    public abstract string Path { get; }

    public abstract HttpMethod Method { get; }

    public virtual TimeSpan Timeout => _defaultTimeout;

    public CancellationToken? CancellationToken { get; set; }

    protected internal virtual HttpRequestMessage CreateRequest(Uri? baseUrl)
    {
        var requestUri = CreateRequestUri(baseUrl);

        var request = new HttpRequestMessage(Method, requestUri)
        {
            Content = CreateContent()
        };
        request.SetTimeout(Timeout);

        return request;
    }

    private Uri CreateRequestUri(Uri? baseUrl)
    {
        Uri uri;
        if (baseUrl is null)
            uri = new(Path);
        else
            uri = new(baseUrl, Path);
        return uri;
    }

    protected internal virtual HttpContent? CreateContent() => default;

    protected virtual void AddRequestHeaders(HttpRequestHeaders headers)
    {
    }

    protected virtual StringContent ToJson(object value)
    {
        return new(
            JsonSerializer.Serialize(
                value, GlobalJsonSerializerOptions.Options), Encoding.UTF8, HttpContentType.Application.Json);
    }
}

public abstract class ApiEndpoint<TResponse> : ApiEndpoint
{
    public virtual ValueTask<TResponse> GetResponse(HttpResponseMessage responseMessage)
        => FromJson<TResponse>(responseMessage.Content);

    public static async ValueTask<T> FromJson<T>(HttpContent content, CancellationToken cancellationToken = default)
    {
        using var responseStream = await content
            .ReadAsStreamAsync()
            .ConfigureAwait(false);

        var deserializedObject = await JsonSerializer.DeserializeAsync<T>(
            responseStream, GlobalJsonSerializerOptions.Options, cancellationToken).ConfigureAwait(false);

        return deserializedObject ?? throw new Exception("Unable to deserialize stream.");
    }
}