using Arbus.Network.Abstractions;
using Arbus.Network.ContentSerializers;
using Arbus.Network.Exceptions;
using Arbus.Network.Extensions;
using System.Net.Http.Headers;

namespace Arbus.Network;

public class NativeHttpClient : INativeHttpClient
{
    private static readonly HttpClient _httpClient = new()
    {
        Timeout = Timeout.InfiniteTimeSpan
    };
    private readonly INetworkManager _networkManager;

    public NativeHttpClient(INetworkManager networkManager, ProductInfoHeaderValue userAgent) : this(networkManager)
    {
        _httpClient.DefaultRequestHeaders.UserAgent.Add(userAgent);
    }

    public NativeHttpClient(INetworkManager networkManager)
    {
        _networkManager = networkManager;
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

    public virtual async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, CancellationToken cancellationToken, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseHeadersRead)
    {
        using var timeoutCts = GetTimeoutCts(request, cancellationToken);
        try
        {
            var response = await _httpClient.SendAsync(request, httpCompletionOption, timeoutCts?.Token ?? cancellationToken).ConfigureAwait(false);
            return await EnsureSuccessResponse(response).ConfigureAwait(false);
        }
        catch (Exception) when (cancellationToken.IsCancellationRequested is false)
        {
            EnsureNetworkAvailable();
            EnsureNoTimeout(timeoutCts);
            throw;
        }
        finally
        {
            request.Dispose();
        }
    }

    public static CancellationTokenSource? GetTimeoutCts(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var timeout = request.GetTimeout();
        var hasTimeout = timeout != Timeout.InfiniteTimeSpan;
        if (hasTimeout)
        {
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(timeout);
            return cts;
        }
        return default;
    }

    public void EnsureNetworkAvailable()
    {
        if (_networkManager.IsNetworkAvailable is false)
            throw new NoNetoworkConnectionException();
    }

    public static void EnsureNoTimeout(CancellationTokenSource? cts)
    {
        if (cts != null && cts.IsCancellationRequested)
            throw new HttpTimeoutException();
    }

    public virtual Task<HttpResponseMessage> EnsureSuccessResponse(HttpResponseMessage response) => response.IsSuccessStatusCode
        ? Task.FromResult(response)
        : HandleNotSuccessStatusCode(response);

    public virtual Task<HttpResponseMessage> HandleNotSuccessStatusCode(HttpResponseMessage response)
    {
        if (response.Content.Headers.ContentType?.MediaType == HttpContentType.Application.ProblemJson)
            return HandleProblemDetailsResponse(response);
        else
            return HandleAnyResponse(response);
    }

    public static async Task<HttpResponseMessage> HandleProblemDetailsResponse(HttpResponseMessage response)
    {
        var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        var problemDetails = await DefaultJsonSerializer.DeserializeAsync<ProblemDetails>(responseStream).ConfigureAwait(false)
            ?? throw new Exception("Failed to deserialize ProblemDetails.");
        throw NetworkExceptionFactory.Create(response.StatusCode, problemDetails);
    }

    public virtual async Task<HttpResponseMessage> HandleAnyResponse(HttpResponseMessage response)
    {
        var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        throw NetworkExceptionFactory.Create(response.StatusCode, responseString);
    }
}