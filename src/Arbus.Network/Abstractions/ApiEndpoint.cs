using Arbus.Network.Exceptions;
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

    public virtual HttpRequestMessage CreateRequest(Uri? baseUrl)
    {
        var requestUri = CreateRequestUri(baseUri);

        var request = new HttpRequestMessage(Method, requestUri)
        {
            Content = CreateContent()
        };
        AddRequestHeaders(request.Headers);
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

    public virtual HttpContent? CreateContent() => default;

    protected virtual void AddRequestHeaders(HttpRequestHeaders headers)
    {
    }

    protected virtual StringContent ToJson(object value)
    {
        return new(
            JsonSerializer.Serialize(
                value, GlobalJsonSerializerOptions.Options), Encoding.UTF8, HttpContentType.Application.Json);
    }

    public virtual Task ValidateResponse(HttpResponseMessage responseMessage)
    {
        return EnsureSuccessResponse(responseMessage);
    }

    private async Task EnsureSuccessResponse(HttpResponseMessage responseMessage)
    {
        if (responseMessage.IsSuccessStatusCode is false)
            throw await HandleNotSuccessStatusCode(responseMessage).ConfigureAwait(false);
    }

    public virtual Task<Exception> HandleNotSuccessStatusCode(HttpResponseMessage responseMessage)
    {
        if (responseMessage.Content.Headers.ContentType?.MediaType == HttpContentType.Application.ProblemJson)
            return HandleProblemDetailsResponse(responseMessage);
        return HandleAnyResponse(responseMessage);
    }

    public static async Task<Exception> HandleProblemDetailsResponse(HttpResponseMessage responseMessage)
    {
        var responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);

        var problemDetails = await JsonSerializer.DeserializeAsync<ProblemDetails>(
            responseStream, GlobalJsonSerializerOptions.Options).ConfigureAwait(false)
            ?? throw new Exception("Failed to deserialize ProblemDetails.");

        return NetworkExceptionFactory.Create(responseMessage.StatusCode, problemDetails);
    }

    public virtual async Task<Exception> HandleAnyResponse(HttpResponseMessage responseMessage)
    {
        var responseString = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        return NetworkExceptionFactory.Create(responseMessage.StatusCode, responseString);
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
            .ReadAsStreamAsync();
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