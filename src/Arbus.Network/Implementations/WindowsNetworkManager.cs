using Arbus.Network.Abstractions;
using System.Net.NetworkInformation;
using static Arbus.Network.Abstractions.INetworkManager;

namespace Arbus.Network.Implementations;

public class WindowsNetworkManager : INetworkManager
{
    public NetworkAvailabilityChangedEventHandler? NetworkAvailabilityChangedEventHandler { get; set; }

    public WindowsNetworkManager()
    {
        NetworkChange.NetworkAvailabilityChanged += NetworkAvailabilityChangedEventHandler;
    }

    public bool IsNetworkAvailable => GetIsNetworkAvailable();

    //NetworkInterface.GetIsNetworkAvailable() always returns 'true' for Xamarin.Ios and Xamarin.Android
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