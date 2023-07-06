namespace Arbus.Network;

public interface INativeHttpClient
{
    Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, CancellationToken cancellationToken, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseHeadersRead);
    Task<string> GetString(string uri, TimeSpan? timeout = default);
    Task<string> GetString(Uri uri, TimeSpan? timeout = default);
}