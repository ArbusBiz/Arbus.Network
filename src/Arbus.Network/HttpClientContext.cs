using Arbus.Network.Abstractions;
using Arbus.Network.Extensions;
using System.Net.Http.Headers;

namespace Arbus.Network;

public class HttpClientContext : IHttpClientContext
{
    protected IDefaultHttpClient _httpClientHandler;

    public HttpClientContext(IDefaultHttpClient httpClientHandler)
    {
        _httpClientHandler = httpClientHandler;
    }

    public async Task RunEndpoint(ApiEndpoint endpoint)
    {
        using var response = await RunEndpointInternal(endpoint).ConfigureAwait(false);
    }

    public async Task<T> RunEndpoint<T>(ApiEndpoint<T> endpoint)
    {
        using var response = await RunEndpointInternal(endpoint).ConfigureAwait(false);
        return await endpoint.GetResponse(response.Content).ConfigureAwait(false);
    }

    public async Task<TStream> RunStreamEndpoint<TStream>(ApiEndpoint<TStream> endpoint) where TStream : Stream
    {
        var response = await RunEndpointInternal(endpoint).ConfigureAwait(false);
        return await endpoint.GetResponse(response.Content).ConfigureAwait(false);
    }

    public async Task<THttpContent> RunHttpContentEndpoint<THttpContent>(ApiEndpoint<THttpContent> endpoint) where THttpContent : HttpContent
    {
        var response = await RunEndpointInternal(endpoint).ConfigureAwait(false);
        return await endpoint.GetResponse(response.Content).ConfigureAwait(false);
    }

    public virtual Task<HttpResponseMessage> RunEndpointInternal(ApiEndpoint endpoint)
    {
        var request = new HttpRequestMessage(endpoint.Method, GetUri(endpoint.Path));
        request.SetTimeout(endpoint.Timeout);
        request.Content = endpoint.CreateContent();

        if (endpoint.AdditionalHeaders is not null)
        {
            foreach (var header in endpoint.AdditionalHeaders)
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        AddHeaders(request.Headers);

        return _httpClientHandler.SendRequest(request, default);
    }

    protected virtual Uri GetUri(string uri) => new(uri);

    protected virtual void AddHeaders(HttpRequestHeaders headers)
    {
    }
}