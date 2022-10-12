namespace Arbus.Network.Abstractions
{
    public interface INativeHttpClient
    {
        Task<HttpResponseMessage> Send(HttpRequestMessage httpRequest, CancellationToken timeout, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseHeadersRead);
        Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, CancellationToken cancellationToken);
        Task<string> GetString(string url, TimeSpan? timeout = default);
        Task<string> GetString(Uri uri, TimeSpan? timeout = default);
    }
}