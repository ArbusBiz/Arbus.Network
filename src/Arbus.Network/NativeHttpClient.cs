using Arbus.Network.Exceptions;
using Arbus.Network.Extensions;
using System.Net.Http.Headers;

namespace Arbus.Network;

public class NativeHttpClient : INativeHttpClient
{
    protected static readonly HttpClient _httpClient = new()
    {
        Timeout = Timeout.InfiniteTimeSpan
    };
    private readonly INetworkMonitor _networkMonitor;

    public NativeHttpClient(INetworkMonitor networkMonitor, ProductInfoHeaderValue userAgent) : this(networkMonitor)
    {
        _httpClient.DefaultRequestHeaders.UserAgent.Add(userAgent);
    }

    public NativeHttpClient(INetworkMonitor networkMonitor)
    {
        _networkMonitor = networkMonitor;
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
            //await here to catch exceptions
            return await _httpClient.SendAsync(
                request,
                httpCompletionOption,
                timeoutCts?.Token ?? cancellationToken).ConfigureAwait(false);
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
        if (_networkMonitor.IsNetworkAvailable is false)
            throw new NoNetworkConnectionException();
    }

    public static void EnsureNoTimeout(CancellationTokenSource? cts)
    {
        if (cts != null && cts.IsCancellationRequested)
            throw new HttpTimeoutException();
    }
}