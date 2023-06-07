using Arbus.Network.Abstractions;
using System.Net.Http.Headers;

namespace Arbus.Network.Implementations;

public class HttpClientContext : IHttpClientContext
{
    protected INativeHttpClient _nativeHttpClient;

    public HttpClientContext(INativeHttpClient httpClientHandler)
    {
        _nativeHttpClient = httpClientHandler;
    }

    public async Task RunEndpoint(ApiEndpoint endpoint)
    {
        using var response = await RunEndpointInternal(endpoint).ConfigureAwait(false);
    }

    public async Task<T> RunEndpoint<T>(ApiEndpoint<T> endpoint)
    {
        using var response = await RunEndpointInternal(endpoint).ConfigureAwait(false);
        return await endpoint.GetResponse(response).ConfigureAwait(false);
    }

    [Obsolete("Create an issue on GitHub if in use")]
    public async Task<TStream> RunStreamEndpoint<TStream>(ApiEndpoint<TStream> endpoint) where TStream : Stream
    {
        var response = await RunEndpointInternal(endpoint).ConfigureAwait(false);
        return await endpoint.GetResponse(response).ConfigureAwait(false);
    }

    [Obsolete("Create an issue on GitHub if in use")]
    public async Task<THttpContent> RunHttpContentEndpoint<THttpContent>(ApiEndpoint<THttpContent> endpoint) where THttpContent : HttpContent
    {
        var response = await RunEndpointInternal(endpoint).ConfigureAwait(false);
        return await endpoint.GetResponse(response).ConfigureAwait(false);
    }

    public virtual async Task<HttpResponseMessage> RunEndpointInternal(ApiEndpoint endpoint)
    {
        var request = CreateRequest(endpoint);

        var response = await _nativeHttpClient.SendRequest(
            request, endpoint.CancellationToken ?? default).ConfigureAwait(false);

        await endpoint.ValidateResponse(response).ConfigureAwait(false);

        return response;
    }

    private HttpRequestMessage CreateRequest(ApiEndpoint endpoint)
    {
        var request = endpoint.CreateRequest(
            GetBaseUrl());
        AddHeaders(request.Headers);
        return request;
    }

    public virtual Uri? GetBaseUrl() => default;

    protected virtual void AddHeaders(HttpRequestHeaders headers)
    {
    }
}