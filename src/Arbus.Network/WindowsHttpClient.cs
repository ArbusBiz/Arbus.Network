using System.Net;
using System.Net.Http.Headers;

namespace Arbus.Network;

public class WindowsHttpClient : NativeHttpClient
{
    static WindowsHttpClient()
    {
        SetTlsForWindows7();

        static void SetTlsForWindows7()
        {
            if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor < 2)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
    }

    public WindowsHttpClient(INetworkMonitor networkMonitor) : base(networkMonitor)
    {

    }

    public WindowsHttpClient(INetworkMonitor networkMonitor, ProductInfoHeaderValue userAgent) : base(networkMonitor, userAgent)
    {
    }
}