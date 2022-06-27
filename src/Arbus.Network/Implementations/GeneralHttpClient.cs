using Arbus.Network.Abstractions;
using System.Net.Http.Headers;

namespace Arbus.Network.Implementations;

public class GeneralHttpClient : INativeHttpClient
{
    private static readonly HttpClient _httpClient = new();

    public GeneralHttpClient()
    {
    }

    public GeneralHttpClient(ProductInfoHeaderValue userAgent)
    {
        _httpClient.DefaultRequestHeaders.UserAgent.Add(userAgent);
    }

    public Task<HttpResponseMessage> Send(HttpRequestMessage httpRequest, CancellationToken cancellationToken, HttpCompletionOption httpCompletionOption)
    {
        return _httpClient.SendAsync(httpRequest, httpCompletionOption, cancellationToken);
    }
}