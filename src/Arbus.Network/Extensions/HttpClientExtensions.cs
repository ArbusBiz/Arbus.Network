using System.Net.Http.Headers;

namespace Arbus.Network.Extensions;

public static class HttpClientExtensions
{
    public static void SetUserAgentHeader(this HttpClient httpClient, ProductInfoHeaderValue productInfoHeader)
        => httpClient.DefaultRequestHeaders.UserAgent.Add(productInfoHeader);
}
