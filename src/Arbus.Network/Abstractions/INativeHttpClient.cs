namespace Arbus.Network.Abstractions;

public interface INativeHttpClient
{
    Task<HttpResponseMessage> Send(HttpRequestMessage httpRequest, CancellationToken timeout, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseHeadersRead);
}