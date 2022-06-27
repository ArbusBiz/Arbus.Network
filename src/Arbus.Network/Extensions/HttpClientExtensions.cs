using System.Net.Http.Headers;

namespace Arbus.Network.Extensions;

public static class HttpClientExtensions
{
    public static void SetUserAgentHeader(this HttpClient request, ProductInfoHeaderValue productInfoHeader)
        => request.DefaultRequestHeaders.UserAgent.Add(productInfoHeader);
}
