using System.Net.NetworkInformation;

namespace Arbus.Network.Abstractions;

public interface INetworkManager
{
    bool IsNetworkAvailable { get; }
    NetworkAvailabilityChangedEventHandler? NetworkAvailabilityChangedEventHandler { get; set; }
}