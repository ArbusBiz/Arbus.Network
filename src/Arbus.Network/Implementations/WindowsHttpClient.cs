using System.Net;
using System.Net.Http.Headers;

namespace Arbus.Network.Implementations;

public class WindowsHttpClient : GeneralHttpClient
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

    public WindowsHttpClient() : base()
    {

    }

    public WindowsHttpClient(ProductInfoHeaderValue userAgent) : base(userAgent)
    {
    }
}