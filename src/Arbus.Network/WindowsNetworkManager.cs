using System.Net.NetworkInformation;

namespace Arbus.Network;

public class WindowsNetworkManager : INetworkManager
{
    public event EventHandler<bool>? NetworkAvailabilityChanged;

    public bool IsNetworkAvailable => GetIsNetworkAvailable();

    //NetworkInterface.GetIsNetworkAvailable() always returns 'true' for Xamarin.iOS and Xamarin.Android
    public static bool GetIsNetworkAvailable()
    {
        foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (networkInterface.OperationalStatus == OperationalStatus.Up
                && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Tunnel
                && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                return true;
        }
        return false;
    }
}