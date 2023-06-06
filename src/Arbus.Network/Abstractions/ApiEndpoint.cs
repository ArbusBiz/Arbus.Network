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
    private static readonly TimeSpan _maxDeserializationTime = TimeSpan.FromSeconds(5);

    public virtual ValueTask<TResponse> GetResponse(HttpResponseMessage responseMessage)
        => FromJson<TResponse>(responseMessage.Content);

    public static async ValueTask<T> FromJson<T>(HttpContent content, CancellationToken? cancellationToken = default)
    {
        using var responseStream = await GetStream(content, cancellationToken).ConfigureAwait(false);

        using var tokenSource = new CancellationTokenSource(_maxDeserializationTime);
        cancellationToken = tokenSource.Token;

        return await Deserialize<T>(responseStream, cancellationToken.Value).ConfigureAwait(false)
            ?? throw new Exception($"Object of type {typeof(T)} is null after deserialization.");
    }

    private static Task<Stream> GetStream(HttpContent content, CancellationToken? cancellationToken)
    {
        return content
#if NETSTANDARD
            .ReadAsStreamAsync()
#else
            .ReadAsStreamAsync(cancellationToken ?? default);
#endif
    }

    private static ValueTask<T?> Deserialize<T>(Stream stream, CancellationToken cancellationToken)
    {
        return JsonSerializer.DeserializeAsync<T>(
            stream, GlobalJsonSerializerOptions.Options, cancellationToken);
    }
}