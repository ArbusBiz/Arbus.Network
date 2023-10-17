namespace Arbus.Network;

public interface INativeHttpClient
{
    Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, CancellationToken cancellationToken, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseHeadersRead);
}