using Arbus.Network.Exceptions;

namespace Arbus.Network.Demo;

public static class Program
{
    public static async Task Main()
    {
        var networkMonitor = new WindowsNetworkMonitor();
        var nativeHttpClient = new WindowsHttpClient(networkMonitor);
        var httpClientContext = new HttpClientContext(nativeHttpClient);
        
        try
        {
            GetAllOrdersApiEndpoint endpoint = new();
            await httpClientContext.RunEndpoint(endpoint).ConfigureAwait(false);
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