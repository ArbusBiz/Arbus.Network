using System.Net.NetworkInformation;

namespace Arbus.Network.Abstractions;

public interface INetworkManager
{
    bool IsNetworkAvailable { get; }
    public event EventHandler<bool>? NetworkStatusChangedEventHandler;
}