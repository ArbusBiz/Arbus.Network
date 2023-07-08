using System.Net.NetworkInformation;

namespace Arbus.Network;

public class WindowsNetworkMonitor : INetworkMonitor
{
    public event EventHandler<bool>? NetworkAvailabilityChanged;

    public bool IsNetworkAvailable => NetworkInterface.GetAllNetworkInterfaces().Any(IsNetworkInterfaceAvailable);

    private static bool IsNetworkInterfaceAvailable(NetworkInterface networkInterface)
    {
        return networkInterface.OperationalStatus == OperationalStatus.Up &&
               networkInterface.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
               networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback;
    }
}