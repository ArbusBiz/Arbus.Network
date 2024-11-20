using System.Net.Http.Headers;

namespace Arbus.Network;

public class HttpClientContext : IHttpClientContext
{
    protected INativeHttpClient _nativeHttpClient;

    public HttpClientContext(INativeHttpClient httpClientHandler)
    {
        _nativeHttpClient = httpClientHandler;
    }

    public async Task RunEndpoint(ApiEndpoint endpoint)
    {
        using var responseMessage = await RunEndpointInternal(endpoint).ConfigureAwait(false);
    }

    public async Task<TResponse> RunEndpoint<TResponse>(ApiEndpoint<TResponse> endpoint)
    {
        using var responseMessage = await RunEndpointInternal(endpoint).ConfigureAwait(false);
        return await endpoint.GetResponse(responseMessage).ConfigureAwait(false);
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
            GetBaseUri());
        AddEndpointHeaders(request.Headers, endpoint);
        return request;
    }

    public virtual Uri? GetBaseUri() => default;

    protected virtual void AddEndpointHeaders(HttpRequestHeaders headers, ApiEndpoint endpoint)
    {
    }
}