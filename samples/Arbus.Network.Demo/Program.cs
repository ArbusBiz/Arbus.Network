using Arbus.Network.Exceptions;

namespace Arbus.Network.Demo;

public static class Program
{
    private static readonly INetworkManager _networkManager;
    private static readonly INativeHttpClient _nativeHttpClient;
    private static readonly IHttpClientContext _httpClientContext;

    static Program()
    {
        _networkManager = new WindowsNetworkManager();
        _nativeHttpClient = new WindowsHttpClient(_networkManager);
        _httpClientContext = new HttpClientContext(_nativeHttpClient);
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