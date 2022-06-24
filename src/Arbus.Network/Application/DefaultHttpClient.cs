using Arbus.Network.Application.ContentSerializers;
using Arbus.Network.Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Arbus.Network.Application;

public class DefaultHttpClient : IDefaultHttpClient
{
    private readonly INativeHttpClient _httpClient;
    private readonly INetworkManager _networkManager;
    private readonly ILogger<DefaultHttpClient> _logger;

    public DefaultHttpClient(INativeHttpClient httpClient, INetworkManager networkManager, ILogger<DefaultHttpClient> logger)
    {
        _httpClient = httpClient;
        _networkManager = networkManager;
        _logger = logger;
    }

    public Task<string> GetString(string uri, TimeSpan? timeout = null) => GetString(new Uri(uri), timeout);

    public async Task<string> GetString(Uri uri, TimeSpan? timeout = null)
    {
        HttpRequestMessage request = new(HttpMethod.Get, uri);
        if (timeout.HasValue)
            request.SetTimeout(timeout.Value);
        using var response = await SendRequest(request, CancellationToken.None).ConfigureAwait(false);
        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    }

    public async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        EnsureNetworkAvailable();
        using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        try
        {
            linkedTokenSource.CancelAfter(request.GetTimeout());
            var response = await _httpClient.Send(request, linkedTokenSource.Token).ConfigureAwait(false);
            return await EnsureSuccessResponse(request, response).ConfigureAwait(false);
        }
        catch (Exception e) when (cancellationToken.IsCancellationRequested is false)
        {
            EnsureNoTimeout(linkedTokenSource);
            EnsureNetworkAvailable();
            _logger.LogError(e, "SendRequest");
            throw;
        }
        finally
        {
            request.Dispose();
        }
    }

    public void EnsureNetworkAvailable()
    {
        if (_networkManager.IsNetworkAvailable is false)
            throw new NoNetoworkConnectionException();
    }

    public static void EnsureNoTimeout(CancellationTokenSource linkedTokenSource)
    {
        if (linkedTokenSource.IsCancellationRequested)
            throw new HttpTimeoutException();
    }

    public Task<HttpResponseMessage> EnsureSuccessResponse(HttpRequestMessage request, HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return Task.FromResult(response);
        else
        {
            if (response.Content.Headers.ContentType.MediaType == HttpContentType.Application.ProblemJson)
                return HandleProblemDetailsResponse(request, response);
            else
                return HandleAnyResponse(request, response);
        }
    }

    public async Task<HttpResponseMessage> HandleProblemDetailsResponse(HttpRequestMessage request, HttpResponseMessage response)
    {
        var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        var problemDetails = await DefaultJsonSerializer.DeserializeAsync<ProblemDetails>(responseStream).ConfigureAwait(false)
            ?? throw new Exception("Failed to deserialize ProblemDetails.");
        _logger.LogWarning("{method} {url} {StatusCode} {IntStatusCode} {ProblemDetails}",
            request.Method, request.RequestUri, response.StatusCode, (int)response.StatusCode, problemDetails);
        throw NetworkExceptionFactory.Create(response.StatusCode, problemDetails);
    }

    public async Task<HttpResponseMessage> HandleAnyResponse(HttpRequestMessage request, HttpResponseMessage response)
    {
        var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        _logger.LogWarning("{method} {url} {StatusCode} {IntStatusCode} {Content}",
            request.Method, request.RequestUri, response.StatusCode, (int)response.StatusCode, responseString);
        throw NetworkExceptionFactory.Create(response.StatusCode, responseString);
    }
}