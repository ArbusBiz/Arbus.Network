using Arbus.Network.Abstractions;
using Arbus.Network.ContentSerializers;
using Arbus.Network.Exceptions;
using Arbus.Network.Extensions;
using System.Net.Http.Headers;

namespace Arbus.Network;

public class NativeHttpClient : INativeHttpClient
{
    private static readonly HttpClient _httpClient = new();
    private readonly INetworkManager _networkManager;

    public NativeHttpClient(INetworkManager networkManager)
    {
        _networkManager = networkManager;
    }
    
    public NativeHttpClient(INetworkManager networkManager, ProductInfoHeaderValue userAgent)
    {
        _networkManager = networkManager;
        _httpClient.DefaultRequestHeaders.UserAgent.Add(userAgent);

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

    public virtual Task<HttpResponseMessage> Send(HttpRequestMessage httpRequest, CancellationToken timeout, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseHeadersRead)
    {
        return _httpClient.SendAsync(httpRequest, httpCompletionOption, timeout);
    }

    public async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        try
        {
            linkedTokenSource.CancelAfter(request.GetTimeout());
            var response = await Send(request, linkedTokenSource.Token).ConfigureAwait(false);
            return await EnsureSuccessResponse(response).ConfigureAwait(false);
        }
        catch (Exception) when (cancellationToken.IsCancellationRequested is false)
        {
            EnsureNetworkAvailable();
            EnsureNoTimeout(linkedTokenSource);
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

    public async Task<HttpResponseMessage> HandleProblemDetailsResponse(HttpResponseMessage response)
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