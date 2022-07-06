using Arbus.Network.Abstractions;
using Arbus.Network.Exceptions;
using Arbus.Network.Implementations;

namespace Arbus.Network.Demo;

public static class Program
{
    private static readonly INetworkManager _networkManager;
    private static readonly INativeHttpClient _nativeHttpClient;
    private static readonly IDefaultHttpClient _defaulHttpClient;
    private static readonly IHttpClientContext _httpClientContext;

    static Program()
    {
        _networkManager = new WindowsNetworkManager();
        _nativeHttpClient = new WindowsHttpClient();
        _defaulHttpClient = new DefaultHttpClient(_nativeHttpClient, _networkManager);
        _httpClientContext = new HttpClientContext(_defaulHttpClient);
    }

    public static async Task Main()
    {
        try
        {
            GetAllOrdersApiEndpoint endpoint = new();
            var orders = await _httpClientContext.RunEndpoint(endpoint).ConfigureAwait(false);
        }
        catch (NetworkException e)
        {
            Console.WriteLine($"{e.StatusCode}\n{e.Message}\n{e.StackTrace}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}